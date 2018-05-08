using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CitizenCatchState : CitizenAI.State {

    public CitizenCatchState()
    {

    }

    public override void Excute(StateData data)
    {
        // 市民が感染したら開放する
        if (data.catchObj.GetComponent<Virus>().IsInfected())
        {
            var state = new CitizenInfectedState();
            data.ai.ChangeState(state);
            data.catchObj = null;
            return;
        }

        // プレイヤをターゲットに
        GameObject targetObj = data.virus.GetOriginal().gameObject;

        // エージェント
        var agent = data.ai.GetComponent<NavMeshAgent>();

        // いどうそくど
        const float speed = 1.0f;
        
        // 速度変更
        agent.speed = speed;

        // 拘束した市民を移動させる
        data.catchObj.GetComponent<NavMeshAgent>().speed = speed;
        data.catchObj.GetComponent<NavMeshAgent>().SetDestination(data.ai.gameObject.transform.position);

        // 目的地
        Vector3 targetPos = targetObj.transform.position;

        // 目的地を設定
        agent.SetDestination(targetPos);

    }

}
