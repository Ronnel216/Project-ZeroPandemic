using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CitizenInfectedState : CitizenAI.State {

    public override void Excute(StateData data)
    {
        NavMeshAgent agent = data.ai.GetComponent<NavMeshAgent>();

        data.viewer.Target("Player");
        
        //GameObject target = data.viewer.GetClose();
        Virus target = data.virus.GetOriginal(); 
        if (target.gameObject == null) return;

        Vector3 moveVec = (target.gameObject.transform.position - data.ai.gameObject.transform.position).normalized;
        moveVec *= 0.1f;
        moveVec = new Vector3(moveVec.x, 0.0f, moveVec.z);
        data.movement.Move(moveVec);

        agent.destination = target.gameObject.transform.position;
    }
}
