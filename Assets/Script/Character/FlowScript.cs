using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowScript : MonoBehaviour
{
    private Rigidbody rb;
    [SerializeField]
    private Vector3 vec;

    // Use this for initialization
    void Start ()
    {
        rb = this.GetComponent<Rigidbody>();
        //vec = new Vector3(0.0f, 0.0f, 0.0f);
    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    void OnTriggerStay(Collider other)
    {
        if(other.gameObject.tag == "Flow")
        {
            rb.velocity = vec;
        }
    }
}
