using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CitizenNormalState : CitizenAI.State {

    public CitizenNormalState()
    {

    }

    public override void Excute(StateData data)
    {
        // ターゲットの設定    
        data.viewer.Target("InfectedActor");

        // 感染したなら感染ステイトへ
        if (data.virus.IsInfected())
        {
            data.ai.ChangeState(new CitizenInfectedState());
            return;
        }

        // 感染者が視界内にいるなら逃げる状態へ
        GameObject actor = data.viewer.GetClose();
        if (actor != null)
        {
            data.ai.ChangeState(new CitizenEsacapeState());
        }

    }

}
