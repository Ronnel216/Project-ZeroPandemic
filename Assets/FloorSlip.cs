//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class FloorSlip : MonoBehaviour {

//	// Use this for initialization
//	void Start () {
		
//	}
	
//	// Update is called once per frame
//	void Update () {
		
//	}
//}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorFlip : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }


    private void OnTriggerStay(Collider other)
    {
        //int a;
        //a = 10;
        //if (CanSlip(other.gameObject))
        //{
        //    var movement = other.GetComponent<Movement>();
        //    var spd = movement.GetSpeed();
        //    movement.Move(movement.GetMoveDirection());
        //}
    }

    bool CanSlip(GameObject obj)
    {
        return obj.tag == "InfectedActor" ||
            obj.tag == "Actor";
    }
}
