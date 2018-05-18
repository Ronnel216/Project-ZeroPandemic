using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CitizenNormalState : CitizenAI.State {
    float stayTime = 0.0f;
    float moveTime = 0.0f;
    Vector3 moveDirection;
    float speed;

    public CitizenNormalState()
    {
        stayTime = Random.Range(2.0f, 30.0f);
        moveDirection = Quaternion.AngleAxis(Random.Range(0.0f, 360.0f), Vector3.up) * Vector3.forward;
    }

    public override void Excute(StateData data)
    {

        data.viewer.Target("InfectedActor");
        if (data.virus.IsInfected())
        {
            data.ai.ChangeState(new CitizenInfectedState());
        }
        else
        {
            GameObject actor = data.viewer.GetClose();
            if (actor != null)
            {
                data.ai.ChangeState(new CitizenEsacapeState());
            }
        }
        data.movement.Move(moveDirection * speed);
        moveTime -= Time.deltaTime;
        if (moveTime < 0.0f) speed = 0.0f;

        stayTime -= Time.deltaTime;
        if (stayTime > 0.0f) return;

        stayTime = Random.Range(2.0f, 30.0f);

        moveDirection = Quaternion.AngleAxis(Random.Range(0.0f, 360.0f), Vector3.up) * Vector3.forward;
        moveTime = Random.Range(0.0f, 3.0f);
        speed = Random.Range(0.025f, 0.1f);
    }

}
