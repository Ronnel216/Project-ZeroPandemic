using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PandemicState : CitizenAI.State
{
    bool Init = true;
    float moveSpeed = 20.0f;
    GameObject targetObj;
    public override void Excute(StateData data)
    {

        Debug.Log("dd");
        NavMeshAgent agent = data.ai.GetComponent<NavMeshAgent>();

        agent.speed = moveSpeed * 2.0f;
        GameObject ai;
        if (Init)
        {
            ai = GameObject.Find("AIManger");
            Debug.Log(ai);
            targetObj = ai.GetComponent<AIManager>().GetTarget();
            Init = false;
        }
            agent.SetDestination(targetObj.transform.position);
    }

}
