using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrazySkill : Skill {

    float radian;

	// Update is called once per frame
	public override void Update (GameObject obj) {
        var movement = obj.GetComponent<Movement>();
        radian += 10.0f;
        Vector3 move = Quaternion.AngleAxis(radian, Vector3.up) * Vector3.forward;
        movement.Move(move * 0.1f);
	}
}
