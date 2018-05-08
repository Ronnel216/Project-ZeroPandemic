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

        Debug.Log("dd");
        NavMeshAgent agent = data.ai.GetComponent<NavMeshAgent>();

        agent.speed = moveSpeed * 2.0f;
        GameObject ai;
        if (targetObj == null || targetObj.tag == "InfectedActor")
        {
            ai = GameObject.Find("AIManger");
            Debug.Log(ai);
            targetObj = ai.GetComponent<AIManager>().GetTarget();
        }
            agent.SetDestination(targetObj.transform.position);
    }

}
