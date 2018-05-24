using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwampFloor : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if (this.enabled == false) return;
        if (CanTripped(other.gameObject))
        {
            var movement = other.GetComponent<Movement>();
            movement.Tripped(true);

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (this.enabled == false) return;
        if (CanTripped(other.gameObject))
        {
            var movement = other.GetComponent<Movement>();
            movement.Tripped(false);

        }
    }

    bool CanTripped(GameObject obj)
    {
        return obj.tag == "InfectedActor" ||
            obj.tag == "Actor";
    }

}
