using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayScreenControl : MonoBehaviour {

    public Text timeLimitText;
    public Text survivorText;
    public Text infectedText;
    public Text CombText;
    public Text RateText;

    //public Image infectedImage;

    public GameManager GameManagerScript;
    public ComboScript combCount;
    GameObject[] tagObjects;

    int targetPerson = 16;
    int remainsPerson = 15;
    int combNum;
    float rateinfected = 0.0f;
    float time;
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
        rateinfected = (float)GameManager.infectedNum / (float)(remainsPerson + GameManager.infectedNum)* 100.0f;

        if (isSetGame)
        {
            ReceiveValue();
            ScreenText();
            remainsPerson = Check("Actor");
            //infectedImage.color = Color.magenta;
            //infectedImage.fillAmount = GameManagerScript.GetTimeLimitStep();

            //if (accelNum > 1)
            //    infectedImage.color = Color.red;

            //if (infectedImage.fillAmount <= 0 || time <= 0)
            //    infectedImage.fillAmount = 0;
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
    //ScrenUIのテキストに代入
    public void ScreenText()
    {
        survivorText.text = remainsPerson.ToString();
        infectedText.text = GameManager.infectedNum.ToString();
        CombText.text = combNum.ToString() + "コンボ";
        RateText.text = rateinfected.ToString("N0") + "%";
    }
    //他スクリプトから値を受け取る
    public void ReceiveValue()
    {
        accelNum = GameManagerScript.GetAccelRate();
        combNum = combCount.GetCombo();
    }
}
