using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleUIController : MonoBehaviour {

    Text pushButtonText;

	// Use this for initialization
	void Start () {
        pushButtonText = GameObject.Find("PushStartButton").GetComponent<Text>();
	}
    float time;
	// Update is called once per frame
	void Update () {
        time += Time.deltaTime;
        float alpha = (Mathf.Sin(time) + 1.0f) / 2.0f;
        Color color = new Color(pushButtonText.color.r, pushButtonText.color.g, pushButtonText.color.b, alpha);
        pushButtonText.color =  color;
	}
}
