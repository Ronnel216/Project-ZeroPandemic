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

        agent.speed = moveSpeed * 5.0f;
        GameObject ai;
        if (targetObj == null || targetObj.tag == "InfectedActor")
        {
            ai = GameObject.Find("AIManger");
            targetObj = ai.GetComponent<AIManager>().GetTarget();
        }
            agent.SetDestination(targetObj.transform.position);
    }

}
