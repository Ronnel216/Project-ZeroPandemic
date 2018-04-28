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
    public override void Enter(HunterController hunter)
    {

    }

    public override void Update(HunterController hunter)
    {
        // 仮 ================================================
        a--;
        Debug.Log("捕獲中：あと "+a.ToString());
        if (a <= 0)
        {
            hunter.ChangeState(new HunterCooldownState());
        }   
        // ====================================================
    }
}
