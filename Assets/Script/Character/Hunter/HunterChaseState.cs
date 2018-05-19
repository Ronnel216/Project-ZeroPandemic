//__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/
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
        // プレイヤーを追跡
        hunter.NavMeshAgent.SetDestination(hunter.Player.transform.position);
    }

    public override void Update(HunterController hunter)
    {
        m_time += Time.deltaTime;

        // 指定時間毎にルート更新
        if (m_time >= hunter.RouteChangeSpan)
        {
            m_time = 0.0f;
            hunter.NavMeshAgent.SetDestination(hunter.Player.transform.position);
        }
    }

    public override void OnTriggerStay(Collider other, HunterController hunter)
    {
        GameObject hitObj = other.gameObject;

        // 追いかけ状態だったら捕獲
        if (hitObj.name == "Player")
            hunter.ChangeState(new HunterCaptureState());
    }
}
