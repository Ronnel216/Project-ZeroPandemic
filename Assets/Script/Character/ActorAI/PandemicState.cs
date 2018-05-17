using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PandemicState : CitizenAI.State
{
    float moveSpeed = 20.0f;
    GameObject targetObj;
    public override void Excute(StateData data)
    {

        NavMeshAgent agent = data.ai.GetComponent<NavMeshAgent>();
        //移動速度＊5
        agent.speed = moveSpeed * 5.0f;
        GameObject ai;
        //感染者が市民をターゲットする
        if (targetObj == null || targetObj.tag == "InfectedActor")
        {
            ai = GameObject.Find("AIManger");
            targetObj = ai.GetComponent<AIManager>().GetTarget();
        }
            agent.SetDestination(targetObj.transform.position);
    }

}
