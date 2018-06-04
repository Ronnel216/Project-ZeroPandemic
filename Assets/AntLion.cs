using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AntLion : MonoBehaviour {

    [SerializeField]
    float speed = .1f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "InfectedActor" || other.tag == "Actor")
        {
            Vector3 vec = transform.position - other.transform.position;
            vec = new Vector3(vec.x, 0.0f, vec.z);
            if (vec.magnitude > speed) vec = vec.normalized * speed;
            other.transform.Translate(vec, Space.World);
        }

    }
}
