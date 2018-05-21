using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// スキル
public abstract class Skill
{
    // ウィルス所有者に影響を与えるステータス
    public class VirusStatus
    {
        public VirusAbility.BounsStatus bouns;
        public VirusStatus()
        {
            bouns = new VirusAbility.BounsStatus();
        }
    }
    
    public VirusStatus status;

    //// スキル
    //public Skill()
    //{
    //    status = new VirusStatus();
    //}

    // ボーナス値の設定
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
    // 処理を行わない
    public override void Update(GameObject obj)
    {
        
    }
}


