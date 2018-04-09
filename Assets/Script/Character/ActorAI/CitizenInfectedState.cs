﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CitizenInfectedState : CitizenAI.State {
  
    [SerializeField]
    static private float angle;
    bool wasInitialize = false;
    float moveSpeed = 20.0f;

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
            //// 感染が完全に弱まった
            //if (data.virus.IsInfected() == false)
            //{
            //    agent.ResetPath();            
            //    data.ai.ChangeState(new CitizenEsacapeState());
            //    return;
            //}

            //GameObject[] infectedPerson = GameObject.FindGameObjectsWithTag("InfectionArea");

            //Vector3 vec;
            //Vector3 avevec;

            // 感染原(プレイヤ)
            Virus target = data.virus.GetOriginal();
            Virus selfVirus = data.virus;

            //!　ゲームの仕様上　ターゲットが見つからないことはない
            if (target.gameObject == null) Debug.Break();

            //vec = selfVirus.gameObject.transform.position - target.gameObject.transform.position;
            //vec.Normalize();

            //angle += 360.0f / infectedPerson.Length;

            //avevec = Quaternion.Euler(0.0f, angle, 0.0f) * vec;

            //agent.destination = avevec * expansion.ExpansionArea;

            Vector3 playerVec/* = target.transform.position - hogePlayer*/;
            // 感染源の移動量
            float x = Input.GetAxisRaw("Horizontal");
            float z = Input.GetAxisRaw("Vertical");
            Vector3 vec = new Vector3(x, 0, z);
            Movement movement = target.GetComponent<Movement>();
            if (movement == null) Debug.Break();
            playerVec = vec.normalized * movement.GetSpeed();


            //hogePlayer = target.transform.position;

            // 拡張範囲を考慮した差
            Vector3 offset = selfVirus.gameObject.transform.position - target.transform.position;
            offset = playerVec;
            offset.Normalize();
            //offset += playerVec;
            //offset.Normalize();
            offset *= expansion.ExpansionArea;
            //offset = new Vector3();

            Vector3 targetPos;

            // コントロールがなにもない時
            if (offset.magnitude < Mathf.Epsilon)
            {
                //Vector3 targetToLast = (lastTargetPos - target.gameObject.transform.position);
                //if (targetToLast.magnitude > expansion.ExpansionArea)
                //    targetToLast = targetToLast.normalized * expansion.ExpansionArea;
                //offset = targetToLast;
                agent.stoppingDistance = expansion.ExpansionArea;
            }
            else
                agent.stoppingDistance = 0.0f;

            targetPos = target.gameObject.transform.position + offset;

            agent.SetDestination(targetPos);
            lastTargetPos = targetPos;
            wasInitialize = true;
        }
        else wasInitialize = false;
        
    }
}
