using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// スキル
public abstract class Skill
{
    public class VirusStatus
    {
        public VirusAbility.BounsStatus bouns;
        public VirusStatus()
        {
            bouns = new VirusAbility.BounsStatus();
        }
    }
    
    public VirusStatus status;

    public Skill()
    {
        status = new VirusStatus();
    }

    public void SetBouns(VirusStatus status)
    {
        this.status = status;
    }

    // 更新
    public abstract void Update(GameObject obj); 
}

// ステータス増加のみのスキル
//memo それ以外のユニークスキルは他のスクリプト 
public class StatusBaffler : Skill
{
    public override void Update(GameObject obj)
    {
        
    }
}


