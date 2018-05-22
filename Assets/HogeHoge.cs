using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HogeHoge : MonoBehaviour {

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
            //movement.Move(movement.GetMoveDirection());
            Debug.Log("滑ってるよ");
        }
    }

    bool CanSlip(GameObject obj)
    {
        return obj.tag == "InfectedActor" ||
            obj.tag == "Actor";
    }


}
