using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlipFloor : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	}

    private void OnTriggerStay(Collider other)
    {
        if (CanSlip(other.gameObject))
        {
            var movement = other.GetComponent<Movement>();
            var spd = movement.GetSpeed();
            movement.Flip(true);

        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (CanSlip(other.gameObject))
        {
            var movement = other.GetComponent<Movement>();
            var spd = movement.GetSpeed();
            movement.Flip(false);

        }
    }

    bool CanSlip(GameObject obj)
    {
        return obj.tag == "InfectedActor" ||
            obj.tag == "Actor";
    }


}
