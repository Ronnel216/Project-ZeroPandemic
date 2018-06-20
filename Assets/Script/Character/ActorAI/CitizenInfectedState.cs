using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CitizenInfectedState : CitizenAI.State {

    float moveSpeed = 20.0f;

    const string citizenTag = "Actor";

    bool isUpdateDestination = true;

    static Vector3 followPoint = new Vector3();

    public override void Init(StateData data)
    {

    }

    float waitTime = 1.0f;          // 感染後の待機時間
    float time = 0.0f;
    public override void Excute(StateData data)
    {
        time += Time.deltaTime;
        // 感染後の待機時間
        if (time <= waitTime) return;

        // 移動コンポーネント
        Movement movement = data.movement;
        movement.SetSpeed(moveSpeed);
        // 拡張範囲の取得
        ExpansionControl expansion = data.virus.GetOriginal().GetComponent<ExpansionControl>();
        
        // 感染原(プレイヤ)
        Virus target = data.virus.GetOriginal();
        Virus selfVirus = data.virus;

        //!　ゲームの仕様上　ターゲットが見つからないことはない
        if (target.gameObject == null) Debug.Break();

        Vector3 playerVec/* = target.transform.position - hogePlayer*/;
        // 感染源の移動量
        float x = Input.GetAxisRaw("Horizontal2");
        float z = Input.GetAxisRaw("Vertical2");
        Vector3 vec = new Vector3(x, 0, z);
        Movement move = target.GetComponent<Movement>();
        if (move == null) Debug.Break();
        //playerVec = vec.normalized * move.GetSpeed();

        // 拡張範囲を考慮した差
        Vector3 offset = vec;
        //offset.Normalize();
        float factor = 1.2f;
        offset *= expansion.ExpansionArea * factor;
        followPoint = target.transform.position +  offset * 1;

        Vector3 targetPos = new Vector3();

        // ウィルスコントロールをしているなら...
        if (target.GetComponent<PlayerController>().IsAction())
        {
            isUpdateDestination = true;

            //pathToFollowPoint = null;

            // 探索が終わっていない時　        
            targetPos = followPoint;
            movement.SetPriority(50);
            movement.SetSpeed(moveSpeed);                    
            if (AIManager.infectedPathTemp == null)
                movement.CalculatePath(targetPos, out AIManager.infectedPathTemp);
        }
        else
        {
            followPoint = new Vector3();
            // 集合範囲内にいるか
            if ((target.transform.position - selfVirus.gameObject.transform.position).sqrMagnitude < expansion.ExpansionArea * expansion.ExpansionArea)
            {
                isUpdateDestination = false;
            }
            else
            {
                isUpdateDestination = true;
            }

            targetPos = target.gameObject.transform.position;
        }

        // 目的地の更新
        if (isUpdateDestination)
        {
            if (AIManager.infectedPathTemp != null)
                movement.SetPath(AIManager.infectedPathTemp);                
            else
                movement.SetDestination(targetPos);
            //movement.SetIsStopped(false);
        }
        else
        {
            //movement.SetIsStopped(true);
            movement.ResetPath();
        }
    }

    public override void OnTriggerEnter(Collider other, StateData data)
    {
        base.OnTriggerEnter(other, data);

    }
}
