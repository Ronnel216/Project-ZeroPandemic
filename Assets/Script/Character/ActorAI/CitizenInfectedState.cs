using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CitizenInfectedState : CitizenAI.State {

    float moveSpeed = 20.0f;

    const string citizenTag = "Actor";

    Vector3 lastTargetPos;

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

        Vector3 targetPos;

        // コントロールがなにもない時
        if (offset.magnitude < Mathf.Epsilon)
            movement.NavMeshAgent.stoppingDistance = expansion.ExpansionArea;
        else
            movement.NavMeshAgent.stoppingDistance = 0.0f;

        // ウィルスコントロールをしているなら...
        if (target.GetComponent<PlayerController>().IsAction())
        {
            targetPos = target.gameObject.transform.position + offset;
        }
        else
            targetPos = target.gameObject.transform.position + offset;


        movement.SetDestination(targetPos);
        lastTargetPos = targetPos;        
    }

    public override void OnTriggerEnter(Collider other, StateData data)
    {
        base.OnTriggerEnter(other, data);

        //// 感染者
        //if (other.tag == citizenTag)
        //{
        //    // 仮 他のコライダー判定を考慮する
        //    if ((other.gameObject.transform.position - data.ai.gameObject.transform.position).magnitude > 2.0f) return;


        //    var targetAi = other.GetComponent<CitizenAI>();
        //    if (targetAi == null) return;
                    
        //    // 拘束されていないなら拘束する
        //    if (targetAi.CheckState<CitizenFriezeState>() == false)
        //    {
        //        data.ai.GetComponent<NavMeshAgent>().ResetPath();
        //        data.catchObj = other.gameObject;
        //        CitizenAI.State state = new CitizenCatchState();
        //        data.ai.ChangeState(state);

        //        state = new CitizenFriezeState();
        //        targetAi.ChangeState(state);
        //    }          
        //}
    }
}
