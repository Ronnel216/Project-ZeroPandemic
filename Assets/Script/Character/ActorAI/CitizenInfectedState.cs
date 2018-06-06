using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CitizenInfectedState : CitizenAI.State {

    float moveSpeed = 20.0f;

    const string citizenTag = "Actor";

    bool isUpdateDestination = true;

    static GameObject leader = null;
    static NavMeshPath pathToLeader = null;

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
        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");
        Vector3 vec = new Vector3(x, 0, z);
        Movement move = target.GetComponent<Movement>();
        if (move == null) Debug.Break();
        playerVec = vec.normalized * move.GetSpeed();

        // 拡張範囲を考慮した差
        Vector3 offset = selfVirus.gameObject.transform.position - target.transform.position;
        offset = playerVec;
        offset.Normalize();
        offset *= expansion.ExpansionArea;

        Vector3 targetPos = new Vector3();

        // ウィルスコントロールをしているなら...
        if (target.GetComponent<PlayerController>().IsAction())
        {
            if (leader == null)
                leader = selfVirus.gameObject;
            isUpdateDestination = true;

            pathToLeader = null;

            // リーダが存在する時　
            if (leader)
            {
                if (leader != selfVirus.gameObject)
                {
                    targetPos = leader.transform.position;
                    movement.SetPriority(50);
                    movement.SetSpeed(moveSpeed);                    
                    if (pathToLeader == null)
                        movement.CalculatePath(targetPos, out pathToLeader);
                    
                }
                else
                {
                    targetPos = selfVirus.gameObject.transform.position + playerVec;
                    movement.SetPriority(10);
                    movement.SetSpeed(moveSpeed / 2);

                }
            }

        }
        else
        {
            leader = null;
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

        if (isUpdateDestination)
        {
            if (leader)
            {
                if (leader != selfVirus.gameObject)
                    movement.SetPath(pathToLeader);
                else
                    movement.SetDestination(targetPos);

            }
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
