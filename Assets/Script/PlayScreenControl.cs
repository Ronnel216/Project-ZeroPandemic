using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayScreenControl : MonoBehaviour {

    public Text timeLimitText;
    float time;
    // Use this for initialization
    void Start () {
        time = GetComponent<GameManager>().GetTimeLimit();

    }

    // Update is called once per frame
    void Update () {
        time = GetComponent<GameManager>().GetTimeLimit();

        timeLimitText.text = "Time : " + time.ToString("F");

    }
}
