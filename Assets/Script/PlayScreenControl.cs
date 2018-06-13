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
    // ハンターの完成度テキスト
    public Text hunterProgressText;
    // 警告画像
    //public Image warningImage;
    //==================================


    public GameManager gameManagerScript;
    public ComboScript combCount;
    static public HunterManufactory hunterManufactory;

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
    // 赤
    float red = 0.0f;
    // 緑
    float green = 0.0f;
    // 青
    float blue = 0.0f;
    // カラーのアルファ値
    float alfaColor = 0.0f;
    // パンデミック可能数値
    float pandemicPossible = 80.0f;
    // ハンター制作度
    float hunterProgress = 0.0f;

    //
    string confirmationText = "";
    // 表示フラグ
    bool isIndicate;
    //ゲームが始まったかどうか
    bool isSetGame;
    //
    bool[] useDisplayText;

    // ハンター制作通知の移動を行っている
    bool isMoveHunterProgressText;

    //　ハンター完成通知の移動を行っている
    bool isMoveWarningImage;

    //一度しか通らない
    bool isOnece = true;

    [SerializeField]
    float hunterUISpeed = 1.0f;

    private void Awake()
    {
        hunterManufactory = null;
    }

    // Use this for initialization
    void Start()
    {
        time = gameManagerScript.GetTimeLimit();
        remainsPerson = gameManagerScript.GetActorNum();
        hunterProgressText.transform.localPosition = new Vector3(800.0f, -100.0f, 0.0f);
        useDisplayText = new bool[4];
    }
    // Update is called once per frame
    void Update () {
        //ゲームの状態を取得
        isSetGame = gameManagerScript.GetStartPandemic();

        //時間の取得
        time = gameManagerScript.GetTime();
        timeLimitText.text = "Time : " + time.ToString("F");

        // ゾンビ数を取得
        int infectedNum = WorldViewer.CountObjects("InfectedActor");

        infectedPerson = (float)infectedNum;
        allPerson = (float)(remainsPerson + infectedNum);

        //感染率を割りだす
        rateinfected = infectedPerson / allPerson * 100.0f;

        PandemicTextReduction(rateinfected);

        SendNotice();

        HunterProgressInformation();

        ResetHunterText();

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
            pandemicText.color = new Color(red, green, blue, alfaColor);
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

    void Send(string _message)
    {
        if(hunterProgressText.text != _message)
        confirmationText = _message;

        if (isOnece)
        {
            if (_message == "ハンターが30%完成")
                useDisplayText[0] = true;
            else if (_message == "ハンターが50%完成")
                useDisplayText[1] = true;
            else if (_message == "ハンターが80%完成")
                useDisplayText[2] = true;
            else if (_message == "ハンターが完成")
            {
                useDisplayText[3] = true;
                isMoveWarningImage = true;
            }
            // テキストの移動を開始する
            isMoveHunterProgressText = true;
        }


    }

    void SendNotice()
    {
       
            if (hunterProgress >= 100.0f)
            {
                Send("ハンターが完成");
            }
            else if (hunterProgress >= 80.0f)
            {
                Send("ハンターが80%完成");
            }
            else if (hunterProgress >= 50.0f)
            {
                Send("ハンターが50%完成");
            }
            else if (hunterProgress >= 30.0f)
            {
                Send("ハンターが30%完成");
            }
        
    }

    //
    void HunterProgressInformation()
    {
        if (hunterManufactory == null) return;
        hunterProgress = hunterManufactory.ManuFactureRate;
        string text = "";

        // 通知を確認
        int result = -1;
        for (int i = useDisplayText.Length - 1; i >= 0; i--)
        {
            if (useDisplayText[i]) {
                result = i;
                break;
            }; 
        }

        // データの更新
        switch (result)
        {
            case 0:
                text = "ハンターが30%完成";
                break;
            case 1:
                text = "ハンターが50%完成";
                break;
            case 2:
                text = "ハンターが80%完成";
                break;
            case 3:
                text = "ハンターが完成";
                break;
        }
        hunterProgressText.text = text;

        // 移動力
        Vector3 moveVec = Vector3.left * hunterUISpeed * Time.deltaTime;
        // 座標設定の更新
        if (isMoveHunterProgressText)
            hunterProgressText.transform.Translate(moveVec);

        if(useDisplayText[3] && isMoveWarningImage)
        {
            //warningImage.transform.Translate(moveVec);
        }
    }

    void ResetHunterText()
    {
        if (hunterProgressText.transform.localPosition.x < -600.0f)
        {
            hunterProgressText.transform.localPosition = new Vector3(800.0f, -100.0f, 0.0f);
            isMoveHunterProgressText = false;
        }

        //if (warningImage.transform.localPosition.x < -800.0f)
        //{
        //    warningImage.transform.localPosition = new Vector3(800.0f, 0.0f, 0.0f);
        //    isMoveWarningImage = false;
        //}

        if (hunterProgressText.text == confirmationText)
            isOnece = false;
        else
            isOnece = true;
        
    }

    public void ChengePandemicTextColor(float r, float g, float b)
    {
        red = r;
        green = g;
        blue = b;
        pandemicText.color = new Color(red, green, blue, alfaColor);
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
