using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIControl : MonoBehaviour {

    public float size;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        ChangeSize(size);

    }

    public void ChangeSize(float num)
    {
        transform.localScale = new Vector3(num, 0.1f, num);
    }
}
