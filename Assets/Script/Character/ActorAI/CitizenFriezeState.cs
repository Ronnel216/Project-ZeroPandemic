using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CitizenFriezeState : CitizenAI.State
{

    //// 捕獲中の市民
    //GameObject citizen;

    public CitizenFriezeState()
    {

    }

    public override void Excute(StateData data)
    {
        if (data.virus.IsInfected())
        {
            data.ai.ChangeState(new CitizenInfectedState());
        }

        //// 市民が感染したら開放する
        //if (citizen.GetComponent<Virus>().IsInfected())
        //{

        //}

        //// プレイヤをターゲットに
        //GameObject targetObj = data.virus.GetOriginal().gameObject;

        //// エージェント
        //var agent = data.ai.GetComponent<NavMeshAgent>();

        //// 速度変更
        //agent.speed = 0.1f;

        //// 目的地
        //Vector3 targetPos = targetObj.transform.position;

        //// 目的地を設定
        //agent.SetDestination(targetPos);

    }

}
