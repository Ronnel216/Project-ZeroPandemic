using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CitizenEsacapeState : CitizenAI.State
{

    public override void Excute(StateData data)
    {
        data.viewer.Target("InfectedActor");
        GameObject target = data.viewer.GetClose();
        if (target == null) return;

        Vector3 moveVec = (data.ai.gameObject.transform.position - target.transform.position).normalized;
        moveVec *= 0.05f;
        moveVec = new Vector3(moveVec.x, 0.0f, moveVec.z);
        data.movement.Move(moveVec);
    }
}
