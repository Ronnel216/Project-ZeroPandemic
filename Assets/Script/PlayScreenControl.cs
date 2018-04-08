using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayScreenControl : MonoBehaviour {

    public Text timeLimitText;
    public Text survivorText;
    public Text infectedText;

    public Image infectedImage;

    public GameManager GameManagerScript;

    int targetPerson = 16;
    int remainsPerson = 15;
    float time;
    float gaugeNum;
    bool isSetGame;
    // Use this for initialization
    void Start () {
        time = 60.0f;
        isSetGame = GameManagerScript.GetStartPandemic();
        
        gaugeNum = 1 / (60.0f * 60.0f);

    }

    // Update is called once per frame
    void Update () {
        timeLimitText.text = "Time : " + time.ToString("F");
        if (!isSetGame)
        {
            isSetGame = GameManagerScript.GetStartPandemic();
        }

        if (isSetGame)
        {
            survivorText.text = remainsPerson.ToString();
            infectedText.text = GameManager.infectedNum.ToString();
            remainsPerson = targetPerson - GameManager.infectedNum - GameManager.killedNum;
            time -= Time.deltaTime;
            infectedImage.fillAmount -= gaugeNum;
            if(infectedImage.fillAmount<=0)
            {
                infectedImage.fillAmount = 0;
                time = 0;
            }
        }

    }
}
