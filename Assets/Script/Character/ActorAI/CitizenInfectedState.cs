using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CitizenInfectedState : CitizenAI.State {
 
    //void CitizenNormalState(StateData data)
    //{
        

    //}
    [SerializeField]
    static private float angle;
    private bool wasInitialize = false;
    public override void Excute(StateData data)
    {
        // 
        if (wasInitialize)
        {
            NavMeshAgent agent = data.ai.GetComponent<NavMeshAgent>();
            ExpansionControl expansion = data.virus.GetOriginal().GetComponent<ExpansionControl>();
            GameObject[] infectedPerson = GameObject.FindGameObjectsWithTag("InfectionArea");
            Vector3 vec;
            Vector3 avevec;
            // 感染原(プレイヤ)
            Virus target = data.virus.GetOriginal();
            // 感染者のウィルス
            Virus selfVirus = data.virus;
            if (target.gameObject == null) return;

            vec = selfVirus.gameObject.transform.position - target.gameObject.transform.position;
            vec.Normalize();

            angle += 360.0f / infectedPerson.Length;

            avevec = Quaternion.Euler(0.0f, angle, 0.0f) * vec;

            agent.destination = avevec * expansion.ExpansionArea;
        }  
        
    }
}
