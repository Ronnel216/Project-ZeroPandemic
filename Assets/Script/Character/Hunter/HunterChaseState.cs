﻿//__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/
//! @file   HunterChaseState.cs
//!
//! @brief  追いかける状態
//!
//! @date   日付　2018/04/28
//!
//! @author 制作者名 澤田
//__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HunterChaseState : HunterController.HunterState
{
    private float m_time = 0.0f;               // 時間
     
    public override void Enter(HunterController hunter)
    {
        hunter.NavMeshAgent.SetDestination(hunter.Player.transform.position);
    }

    public override void Update(HunterController hunter)
    {
        m_time += Time.deltaTime;

        // ルート更新
        if (m_time >= hunter.RouteChangeSpan)
        {
            m_time = 0.0f;
            hunter.NavMeshAgent.SetDestination(hunter.Player.transform.position);
        }
    }
}
