using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeyAction : MonoBehaviour {

    float scale = 1.0f;
    public void SetChar(string str)
    {
        gameObject.GetComponentInChildren<Text>().text = str;
    }

    public void Focus() {
        scale = 3.0f;
    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        scale = 1.0f + (scale - 1.0f) * 0.1f;
        gameObject.transform.localScale = Vector3.one * scale;
	}
}
