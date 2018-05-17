using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayScreenControl : MonoBehaviour {

    //========画面内のUIテキスト========
    //制限時間
    public Text timeLimitText;
    //市民の数
    public Text actorText;
    //感染者の数
    public Text infectedText;
    //コンボ数
    public Text CombText;
    //感染率
    public Text RateText;
    //==================================


    public GameManager GameManagerScript;
    public ComboScript combCount;

    //市民の数
    int remainsPerson;
    //コンボ数
    int combNum;
    //感染率
    float rateinfected = 0.0f;
    //制限時間
    float time;
    //ゲームが終了しているか？
    bool isSetGame;
    // Use this for initialization
    void Start () {
        time = GameManagerScript.GetTimeLimit();
        isSetGame = GameManagerScript.GetStartPandemic();
        remainsPerson = GameManagerScript.GetActorNum();
    }

    // Update is called once per frame
    void Update () {
        time = GameManagerScript.GetTime();
        timeLimitText.text = "Time : " + time.ToString("F");
        rateinfected = (float)GameManager.infectedNum / (float)(remainsPerson + GameManager.infectedNum)* 100.0f;
        if (isSetGame)
        {
            remainsPerson = CheckObject("Actor");
            ReceiveValue();
            ScreenText();
        }
        else
        {
            isSetGame = GameManagerScript.GetStartPandemic();
        }

    }
    //シーン上の指定したタグが付いたオブジェクトを数える
    public int CheckObject(string tagname)
    {
        GameObject[] tagObjects;

        tagObjects = GameObject.FindGameObjectsWithTag(tagname);
        return tagObjects.Length;
    }
    //ScrenUIのテキストに代入
    public void ScreenText()
    {
        actorText.text = remainsPerson.ToString();
        infectedText.text = GameManager.infectedNum.ToString();
        CombText.text = combNum.ToString() + "コンボ";
        RateText.text = rateinfected.ToString("N0") + "%";
    }
    //他スクリプトから値を受け取る
    public void ReceiveValue()
    {
        combNum = combCount.GetCombo();
    }
}
