//__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/
//! @file   HunterCaptureState.cs
//!
//! @brief  捕獲状態
//!
//! @date   日付　2018/04/28
//!
//! @author 制作者名 澤田
//__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HunterCaptureState : HunterController.HunterState {

    private Movement m_playerMove;              // プレイヤーの移動コンポーネント
    private VirusAmount m_virusAmount;          // ウィルス
    private float m_decreaseAmount = 0.0f;      // 1秒のウィルス減少量
    private float m_decreaseMaxAmount = 0.0f;   // 1秒のウィルス最大値減少量

    public override void Enter(HunterController hunter)
    {
        // コンポーネントの取得
        m_playerMove = hunter.Player.GetComponent<Movement>();
        m_virusAmount = hunter.Player.GetComponent<VirusAmount>();

        // 追跡を止める
        hunter.NavMeshAgent.SetDestination(hunter.transform.position);

        // 1秒の減少量を計算
        m_decreaseAmount = m_virusAmount.GetVirusAmount() / hunter.CaptureTime;
        m_decreaseMaxAmount = hunter.DecreaseMaxAmount / hunter.CaptureTime;
    }

    public override void Update(HunterController hunter)
    {
        // プレイヤーを拘束
        m_playerMove.LockMove = true;

        // 1フレーム分の減少
        m_virusAmount.DecreaseVirusAmout(m_decreaseAmount * Time.deltaTime);
        m_virusAmount.DecreaseMaxVirusAmout(m_decreaseMaxAmount * Time.deltaTime);

        // ウィルスを減少しきったら解放
        if (m_virusAmount.GetVirusAmount() <= 0)
        {
            m_playerMove.LockMove = false;
            hunter.ChangeState(new HunterCooldownState());
        }   
    }
}
