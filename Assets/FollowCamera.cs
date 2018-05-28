using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour {

    // ターゲット
    [SerializeField]
    GameObject target;

    // ターゲットからのオフセット
    [SerializeField]
    Vector3 offset;

    // 追尾角
    [SerializeField]
    float followRadian;

    // 現在の回転角
    Quaternion currentRotate;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 targetPos = target.transform.position;

        // 向きを考慮する
        currentRotate = Quaternion.Lerp(currentRotate, target.transform.rotation, 0.05f);
        Vector3 offsetTemp = currentRotate * offset;
        
        Vector3 cameraPos = targetPos + offsetTemp;
        transform.position = cameraPos;

    }
}
