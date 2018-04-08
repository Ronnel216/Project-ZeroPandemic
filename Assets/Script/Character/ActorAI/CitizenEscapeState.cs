using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CitizenEsacapeState : CitizenAI.State
{
    float moveSpeed = 2.0f;

    public override void Excute(StateData data)
    {
        // ナビゲーション対象のエージェント
        NavMeshAgent agent = data.ai.GetComponent<NavMeshAgent>();
        agent.speed = moveSpeed;
        data.viewer.Target("InfectedActor");
        if (data.virus.IsInfected())
        {
            agent.ResetPath();
            data.ai.ChangeState(new CitizenInfectedState());
            return;
        }

        GameObject target = data.viewer.GetClose();
        if (target == null) return;

        Vector3 moveVec = (data.ai.gameObject.transform.position - target.transform.position).normalized;
        agent.SetDestination(data.ai.gameObject.transform.position + moveVec);
        //moveVec *= 0.05f;
        //moveVec = new Vector3(moveVec.x, 0.0f, moveVec.z);
        //data.movement.Move(moveVec);

    }
}
