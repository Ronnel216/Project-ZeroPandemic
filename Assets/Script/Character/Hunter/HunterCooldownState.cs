//__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/
//! @file   HunterCooldownState.cs
//!
//! @brief  クールダウン状態
//!
//! @date   日付　2018/04/28
//!
//! @author 制作者名 澤田
//__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HunterCooldownState : HunterController.HunterState
{
    private float m_time = 0.0f;               // 時間

    public override void Enter(HunterController hunter)
    {
        
    }

    public override void Update(HunterController hunter)
    {
        m_time += Time.deltaTime;
        // クールダウン終了
        if (m_time >= hunter.CoolTime)
            hunter.ChangeState(new HunterChaseState());
    }
}
