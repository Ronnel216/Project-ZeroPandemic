using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreeSway : MonoBehaviour {

    [SerializeField]
    float time;

    Vector3 basePos;

	// Use this for initialization
	void Start () {
        basePos = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
        float radian = (Mathf.PI * 2.0f) * Time.time / time;
        transform.position = basePos + Vector3.up * Mathf.Sin(radian);
	}
}
