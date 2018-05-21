using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayScreenControl : MonoBehaviour {

    //========画面内のUIテキスト========
    // 制限時間
    public Text timeLimitText;
    // ステージ名
    public Text stageText;
    // 市民の数
    public Text actorText;
    // 感染者の数
    public Text infectedText;
    // コンボ数
    public Text CombText;
    // 感染率
    public Text RateText;
    //==================================


    public GameManager GameManagerScript;
    public ComboScript combCount;

    //現在のステージ番号
    int nowStageNum = 1;
    //市民の数
    int remainsPerson;
    //感染者の数
    float infectedPerson;
    //全体の人数
    float allPerson;
    //コンボ数
    int combNum;
    //感染率
    float rateinfected = 0.0f;
    //制限時間
    float time;
    //ゲームが始まったかどうか
    bool isSetGame;
    // Use this for initialization
    void Start () {
        time = GameManagerScript.GetTimeLimit();
        remainsPerson = GameManagerScript.GetActorNum();
    }

    // Update is called once per frame
    void Update () {

        //時間の取得
        time = GameManagerScript.GetTime();
        timeLimitText.text = "Time : " + time.ToString("F");
  
        infectedPerson = (float)GameManager.infectedNum;
        allPerson = (float)(remainsPerson + GameManager.infectedNum);
        //感染率を割りだす
        rateinfected = infectedPerson / allPerson * 100.0f;

        //ゲームが始まった時だけ処理
        if (isSetGame)
        {
            //ゲームの状態を取得
            isSetGame = GameManagerScript.GetStartPandemic();
            //市民の数を取得
            remainsPerson = CheckObject("Actor");

            ReceiveValue();
            ScreenText();
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
        stageText.text = "ステージ" + nowStageNum.ToString();
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

    public void SetStageNum(int stagenum)
    {
        nowStageNum = stagenum;
    }
}
