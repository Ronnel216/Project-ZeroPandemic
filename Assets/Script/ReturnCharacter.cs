using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ReturnCharacter : MonoBehaviour
{
    private GameObject parentObj;
    public Transform target;
    NavMeshAgent agent;


	// Use this for initialization
	void Start ()
    {
        agent = GetComponent<NavMeshAgent>();
        parentObj = transform.root.gameObject;
    }
	
	// Update is called once per frame
	void Update ()
    {
        agent.SetDestination(target.position);
	}
}
