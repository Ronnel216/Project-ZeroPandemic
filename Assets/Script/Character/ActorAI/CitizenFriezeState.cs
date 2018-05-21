using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CitizenFriezeState : CitizenAI.State
{
    public CitizenFriezeState()
    {

    }

    public override void Excute(StateData data)
    {
        // 感染状態に変更
        if (data.virus.IsInfected())
        {
            data.ai.ChangeState(new CitizenInfectedState());
        }
        
    }

}
