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
    GameObject[] tagObjects;

    int targetPerson = 16;
    int remainsPerson = 15;
    float time;
    //float gaugeNum;
    float accelNum;
    bool isSetGame;

    // Use this for initialization
    void Start () {
        time = GameManagerScript.GetTimeLimit();
        isSetGame = GameManagerScript.GetStartPandemic();
        //gaugeNum = 1 / (60.0f * 60.0f);

    }

    // Update is called once per frame
    void Update () {
        time = GameManagerScript.GetTime();
        timeLimitText.text = "Time : " + time.ToString("F");

        if (isSetGame)
        {
            accelNum = GameManagerScript.GetAccelRate();

            //gaugeNum = 1 / (60.0f * 60.0f) * accelNum;

            survivorText.text = remainsPerson.ToString();
            infectedText.text = GameManager.infectedNum.ToString();
            remainsPerson = Check("Actor");
            //time -= Time.deltaTime * accelNum;
            infectedImage.color = Color.magenta;
            infectedImage.fillAmount = GameManagerScript.GetTimeLimitStep();
            if (accelNum > 1)
            {
                infectedImage.color = Color.red;
            }
            if (infectedImage.fillAmount <= 0 || time <= 0)
            {
                infectedImage.fillAmount = 0;
                //time = 0;
            }
        }
        else
        {
            isSetGame = GameManagerScript.GetStartPandemic();
        }

    }
    //シーン上の指定したタグが付いたオブジェクトを数える
    public int Check(string tagname)
    {
        tagObjects = GameObject.FindGameObjectsWithTag(tagname);
        return tagObjects.Length;
    }

}
