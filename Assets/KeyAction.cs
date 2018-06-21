using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeyAction : MonoBehaviour {

    public void SetChar(string str)
    {
        gameObject.GetComponentInChildren<Text>().text = str;
    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
