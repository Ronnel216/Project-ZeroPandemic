using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaControl : MonoBehaviour {

    [SerializeField]
    float radius = 2.0f;

    ParticleSystem particle;
    SphereCollider area;

	// Use this for initialization
	void Start () {
        particle = GetComponent<ParticleSystem>();
        area     = GetComponent<SphereCollider>();

        // speed * time * 2 = radius
        particle.startSpeed = radius / 4.0f;    // 仮の式
        area.radius = radius;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
