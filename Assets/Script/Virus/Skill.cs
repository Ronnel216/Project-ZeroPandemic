using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// スキル
public abstract class Skill
{
    VirusAbility.BounsStatus bouns;

    // スキルの種類の取得
    public abstract string GetType();

    // 更新
    public abstract void Update();

    public VirusAbility.BounsStatus bounsStatus
    {
        get { return bouns; }
    }
}

// ステータス増加のみのスキル
//memo それ以外のユニークスキルは他のスクリプト 
public class StatusBaffler : Skill
{
    public override string GetType()
    {
        return "Bouns";
    }

    public override void Update()
    {
        
    }
}


