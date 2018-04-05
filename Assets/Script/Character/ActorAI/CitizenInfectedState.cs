﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CitizenInfectedState : CitizenAI.State {
  
    [SerializeField]
    static private float angle;
    private bool wasInitialize = false;

    Vector3 hogePlayer;

    public override void Excute(StateData data)
    {
        wasInitialize = true;
        if (Input.GetKey(KeyCode.R)) wasInitialize = false;
        if (wasInitialize == false)
        {
            // ナビゲーション対象のエージェント
            NavMeshAgent agent = data.ai.GetComponent<NavMeshAgent>();
            
            // 拡張範囲の取得
            ExpansionControl expansion = data.virus.GetOriginal().GetComponent<ExpansionControl>();

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

            Vector3 playerVec = target.transform.position - hogePlayer;
            float x = Input.GetAxisRaw("Horizontal");
            float z = Input.GetAxisRaw("Vertical");
            Vector3 vec = new Vector3(x, 0, z);
            playerVec = vec.normalized * 5.0f;
            hogePlayer = target.transform.position;

            // 拡張範囲を考慮した差
            Vector3 offset = selfVirus.gameObject.transform.position - target.transform.position;
            //offset = playerVec;
            offset.Normalize();
            offset *= expansion.ExpansionArea;
            offset += playerVec;

            // 目標地点
            Vector3 targetPos = target.gameObject.transform.position + offset;
            agent.SetDestination(targetPos);
        }  
        
    }
}
