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
    public Text combText;
    // 感染率
    public Text rateText;
    // パンデミック可能テキスト
    public Text pandemicText;
    //==================================


    public GameManager GameManagerScript;
    public ComboScript combCount;

    //現在のステージ番号
    int nowStageNum = 1;
    //市民の数
    int remainsPerson = 0;
    //感染者の数
    float infectedPerson = 0.0f;
    //全体の人数
    float allPerson = 0.0f;
    //コンボ数
    int combNum;
    //感染率
    float rateinfected = 0.0f;
    //制限時間
    float time;
    // カラーのアルファ値
    float alfaColor = 0.0f;
    // パンデミック可能数値
    float pandemicPossible = 80.0f;
    // 表示フラグ
    bool isIndicate;
    //ゲームが始まったかどうか
    bool isSetGame;
    // Use this for initialization
    void Start () {
        time = GameManagerScript.GetTimeLimit();
        remainsPerson = GameManagerScript.GetActorNum();
    }

    // Update is called once per frame
    void Update () {
        //ゲームの状態を取得
        isSetGame = GameManagerScript.GetStartPandemic();

        //時間の取得
        time = GameManagerScript.GetTime();
        timeLimitText.text = "Time : " + time.ToString("F");

        // ゾンビ数を取得
        int infectedNum = WorldViewer.CountObjects("InfectedActor");

        infectedPerson = (float)infectedNum;
        allPerson = (float)(remainsPerson + infectedNum);

        //感染率を割りだす
        rateinfected = infectedPerson / allPerson * 100.0f;

        PandemicTextReduction(rateinfected);

        if (float.IsNaN(rateinfected))
            rateinfected = 100.0f;

        //ゲームが始まった時だけ処理
        if (isSetGame)
        {
            //市民の数を取得
            remainsPerson = CheckObject("Actor");

            ReceiveValue();
            ScreenText();
        }
        Debug.Log(infectedNum);
    }

    // パンデミックテキストを点減させる
    void PandemicTextReduction(float infectedamount)
    {
        if (float.IsNaN(infectedamount))
            infectedamount = 0.0f;
        if(infectedamount >= pandemicPossible)
        {
            pandemicText.enabled = true;
            // テキストの透明度を変更する
            pandemicText.color = new Color(0, 0, 0, alfaColor);
            if (isIndicate) alfaColor -= Time.deltaTime;
            else alfaColor += Time.deltaTime;
            if(alfaColor < 0)
            {
                alfaColor = 0.0f;
                isIndicate = false;
            }
            else if(alfaColor > 1)
            {
                alfaColor = 1.0f;
                isIndicate = true;
            }
        }
        else pandemicText.enabled = false;
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
        // ゾンビ数を取得
        int infectedNum = WorldViewer.CountObjects("InfectedActor");

        stageText.text = "ステージ" + nowStageNum.ToString();
        actorText.text = remainsPerson.ToString();
        infectedText.text = infectedNum.ToString();
        combText.text = combNum.ToString() + "コンボ";
        rateText.text = rateinfected.ToString("N0") + "%";
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
