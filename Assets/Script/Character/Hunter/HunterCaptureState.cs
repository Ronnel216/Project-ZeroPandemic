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

    int a = 60;             // 仮
    private Movement m_playerMove;            // プレイヤーの移動コンポーネント
    private float m_playerSpeed;              // プレイヤーの移動速度
    public override void Enter(HunterController hunter)
    {
        hunter.NavMeshAgent.SetDestination(hunter.transform.position);
        m_playerMove = hunter.Player.GetComponent<Movement>();
        m_playerSpeed = m_playerMove.GetSpeed();
    }

    public override void Update(HunterController hunter)
    {
        // 仮 ================================================
        a--;
        m_playerMove.LockMove = true;
        if (a <= 0)
        {
            m_playerMove.LockMove = false;
            hunter.ChangeState(new HunterCooldownState());
        }   
        // ====================================================
    }
}
