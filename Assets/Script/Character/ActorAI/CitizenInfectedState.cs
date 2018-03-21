using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CitizenInfectedState : CitizenAI.State {

    public override void Excute(StateData data)
    {
        data.viewer.Target("Actor");
        GameObject target = data.viewer.GetClose();
        if (target == null) return;

        Vector3 moveVec = (target.transform.position - data.ai.gameObject.transform.position).normalized;
        moveVec *= 0.1f;
        moveVec = new Vector3(moveVec.x, 0.0f, moveVec.z);
        data.movement.Move(moveVec);
    }
}
