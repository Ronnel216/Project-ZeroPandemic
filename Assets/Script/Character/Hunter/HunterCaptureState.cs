﻿//__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/
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
    private float m_decreaseMaxAmount = 0.0f;   // 1秒のウィルス減少量
    private float m_time = 0.0f;

    public override void Enter(HunterController hunter)
    {
        // コンポーネントの取得
        m_playerMove = hunter.Player.GetComponent<Movement>();
        m_virusAmount = hunter.Player.GetComponent<VirusAmount>();

        // 追跡を止める
        hunter.NavMeshAgent.SetDestination(hunter.transform.position);

        Vector3 playerPos = m_playerMove.transform.position;
        Vector3 hunterPos = hunter.transform.position;
        Vector3 relativePos = playerPos - hunterPos;
        relativePos.y = 0; //上下方向の回転はしないように制御
        hunter.transform.rotation = Quaternion.LookRotation(relativePos);
    }

    public override void Update(HunterController hunter)
    {
        m_time += Time.deltaTime;

        // プレイヤーを拘束
        m_playerMove.LockMove = true;
        // ウィルスの自動回復を止める
        m_virusAmount.Stop = true;

        // 1フレーム分の減少
        m_virusAmount.DecreaseMaxVirusAmount(hunter.DecreaseMaxAmount * Time.deltaTime);

        // ウィルスを減少しきったら解放
        if (m_time >= hunter.CaptureTime)
        {
            m_playerMove.LockMove = false;
            m_virusAmount.Stop = false;
            hunter.ChangeState(new HunterCooldownState());
        }   
    }



    public override void OnDestroy()
    {
        // ハンターが消滅したら強制解放
        m_playerMove.LockMove = false;
        m_virusAmount.Stop = false;
    }

}
