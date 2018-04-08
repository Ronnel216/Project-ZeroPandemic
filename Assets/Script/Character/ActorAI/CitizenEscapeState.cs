using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CitizenEsacapeState : CitizenAI.State
{
    float moveSpeed = 2.0f;

    //// 仮
    //Vector3[,] field;
    //const int cellNum = 4;
    //const float cellSize = 30 / cellNum;
    public CitizenEsacapeState()
    {
        //// 仮のフィールド生成
        //field = new Vector3[cellNum, cellNum];

    }

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
        Vector3 destination = data.ai.gameObject.transform.position + moveVec;
        float stageSizeHoge = 15.0f * 1.2f/*ステージを円形とした時の半径*/;
        bool isOnStage = destination.magnitude < stageSizeHoge;
        if (isOnStage == false)
        {
            destination = destination.normalized * stageSizeHoge;
        }
        agent.SetDestination(destination);
        //moveVec *= 0.05f;
        //moveVec = new Vector3(moveVec.x, 0.0f, moveVec.z);
        //data.movement.Move(moveVec);

    }
}
