using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CitizenInfectedState : CitizenAI.State {
  
    [SerializeField]
    static private float angle;
    bool wasInitialize = false;
    float moveSpeed = 20.0f;

    const string citizenTag = "Actor";

    Vector3 lastTargetPos;

    //Vector3 hogePlayer;

    public override void Excute(StateData data)
    {

        // ナビゲーション対象のエージェント
        NavMeshAgent agent = data.ai.GetComponent<NavMeshAgent>();
        agent.speed = moveSpeed;
        // 拡張範囲の取得
        ExpansionControl expansion = data.virus.GetOriginal().GetComponent<ExpansionControl>();

        if (wasInitialize == false)
        {

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
            Movement movement = target.GetComponent<Movement>();
            if (movement == null) Debug.Break();
            playerVec = vec.normalized * movement.GetSpeed();

            // 拡張範囲を考慮した差
            Vector3 offset = selfVirus.gameObject.transform.position - target.transform.position;
            offset = playerVec;
            offset.Normalize();
            offset *= expansion.ExpansionArea;

            Vector3 targetPos;

            // コントロールがなにもない時
            if (offset.magnitude < Mathf.Epsilon)
                agent.stoppingDistance = expansion.ExpansionArea;
            else
                agent.stoppingDistance = 0.0f;

            targetPos = target.gameObject.transform.position + offset;

            agent.SetDestination(targetPos);
            lastTargetPos = targetPos;
            wasInitialize = true;
        }
        else wasInitialize = false;
        
    }

    public override void OnTriggerEnter(Collider other, StateData data)
    {
        base.OnTriggerEnter(other, data);

        // 感染者
        if (other.tag == citizenTag)
        {
            var targetAi = other.GetComponent<CitizenAI>();
            if (targetAi == null) return;
                    
            // 拘束されていないなら拘束する
            if (targetAi.CheckState<CitizenFriezeState>() == false)
            {
                data.catchObj = other.gameObject;
                CitizenAI.State state = new CitizenCatchState();
                data.ai.ChangeState(state);

                state = new CitizenFriezeState();
                targetAi.ChangeState(state);
            }          
        }
    }
}
