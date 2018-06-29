using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour {

    // 制限時間
    [SerializeField]
    float timeLimit;
    float time;
    bool isStartPandemic;

    // プレイヤのウィルス
    Virus playerVirus;

    // 感染者数
    //public static int infectedNum = 0;

    // 死者数
    public static int killedNum = 0;
    //市民の数
    int actorNum;

    // プレイヤのコントローラ
    [SerializeField]
    PlayerController playerControllerScript;

    // 拡張範囲を広げた時の　時間の加速率
    [SerializeField]
    float maxAccelRate;
    [SerializeField]
    float minAccelRate;
    float acceleratorRate;
    bool actionState;

    [SerializeField]
    StageManager stageMnager;

    [SerializeField]
    SaveStr saveStr;

    [SerializeField]
    PlayScreenControl screenCntrol;

    public ComboScript comboScript;                                   // コンボスクリプト

    static public Virus[] citizen; // 仮

    // スコア
    float score;

    // クリアしたか
    bool isClear;

    // ステージ番号
    int stageNum = 1;

    //感染率
    [SerializeField]
    float perdemic = 80.0f;
    AIManager pandemic;
    //パンデミックしているかどうか
    bool pandemicFlag = false;

    [SerializeField]
    KeyCode m_pandemicKey = KeyCode.Space;      // パンデミック発動キー

    AudioSource audio;

    AudioClip clearSE;
    AudioClip gameOverSE;

    float stayTime = 0;
    GameEndUI endUI;
    VirusAmount overFlag;
    void Start () {
        audio = GetComponent<AudioSource>();
        clearSE = Resources.Load("se_maoudamashii_onepoint11") as AudioClip;
        gameOverSE = Resources.Load("se_maoudamashii_se_syber01") as AudioClip;

        // 制限時間を設定
        time = timeLimit;

        // staticメンバの初期化
        //infectedNum = 0;
        killedNum = 0;
        
        // 加速率の初期値設定        
        acceleratorRate = minAccelRate;
        score = 0.0f;
        isStartPandemic = false;

        actorNum = GameObject.FindGameObjectsWithTag("Actor").Length;
        pandemic = GameObject.Find("AIManger").GetComponent<AIManager>();
        endUI = GameObject.Find("ScreenUI").GetComponent<GameEndUI>();
        overFlag = GameObject.Find("Player").GetComponent<VirusAmount>();
        // デバッグ処理
        if (saveStr == null)
        {
            Debug.Log("saveStr is null");
            Debug.Break();
        }
        GameObject player = GameObject.Find("Player");
        playerVirus = player.GetComponent<Virus>();
    }

    // Update is called once per frame
    void Update () {

        // 感染開始後の処理 //
        if (isStartPandemic == false) return;

        // クリアしたかの判定
        isClear = GameObject.FindGameObjectWithTag("Actor") == null;

        // 全ステージクリアした  
        // スコアの代入はここで
        if (stageMnager.AllClear)
        {
            if (stayTime <= 0.0f)
            {
                if (stayTime == 0.0f)
                    if (isClear)
                    {
                        audio.PlayOneShot(clearSE);
                    }
                    else
                    {
                        audio.PlayOneShot(gameOverSE);
                    }
                screenCntrol.SetClearFlag(isClear);
                endUI.CreateGameEndUI(isClear);
            }
            stayTime += Time.deltaTime;
            if (stayTime >= 3.0f)
            {
                score = GetClearTime();
                saveStr.SetResultScore(score);
                UnityEngine.SceneManagement.SceneManager.LoadScene("RankingScene");
                stayTime = 0;
            }
        }
        else if (isClear)
        {
            stageNum++;
            if (stageNum > 3) stageNum = 3; // 拡張不可
            //infectedNum = 0;
        }

        if (isClear)
        {
            screenCntrol.SetStageNum(stageNum);
            pandemicFlag = false;
            comboScript.Initialize();
            screenCntrol.ChengePandemicTextColor(0.0f, 0.0f, 0.0f, isClear);

        }

        // クリア前の処理
        if (IsClear()) return;

        // パンデミック時とステージ移動中はタイムを止める
        if (!pandemicFlag && stageMnager.MigrationStage)
        {
            time -= Time.deltaTime;// * acceleratorRate;
            if (time < 0.0f) time = 0.0f;
        }

        actorNum = WorldViewer.CountObjects("Actor");
        //感染率が80%以上ならパンデミック開始
        if (!pandemicFlag)
        {
            // ゾンビ数を取得
            int infectedNum = WorldViewer.CountObjects("InfectedActor");
            if ((float)infectedNum / (actorNum + infectedNum) * 100.0f >= perdemic)
            {
                // パンデミック発動キーが押された
                if (Input.GetKeyDown(m_pandemicKey) || Input.GetButtonDown("Button A"))
                {
                    pandemic.StartPandemic();
                    screenCntrol.ChengePandemicTextColor(255.0f,255.0f,0.0f, isClear);
                    pandemicFlag = true;
                }
            }
        }
        // 制限時間を迎えたらゲームオーバー
        if (time == 0.0f)
        {
            GameOver();
        }
        // ウイルスゲージが0になったらゲームオーバー
        if (overFlag.GetOverFlag()==false)
        {
            GameOver();
        }

    }

    // publicメソッド //
    public void StartGame()
    {
        playerVirus.Infected(null);
        isStartPandemic = true;
        comboScript.Initialize();
    }

    public void GameOver()
    {
        if (stayTime <= 0.0f)
        {
            endUI.CreateGameEndUI(isClear);
        }
        stayTime += Time.deltaTime;
        if (stayTime >= 3.0f)
        {
            score = 0;
            saveStr.SetResultScore(score);
            UnityEngine.SceneManagement.SceneManager.LoadScene("RankingScene");
            stayTime = 0;
        }
    }

    // 制限時間の取得
    public float GetTimeLimit()
    {
        return timeLimit;
    }

    // 経過時間の取得
    public float GetTime()
    {
        return time;
    }

    // 制限時間の取得
    public float GetClearTime()
    {               
        return time;
    }

    // 制限時間の進行度
    public float GetTimeLimitStep()
    {
        return time / timeLimit;
    }

    // 加速率の取得
    public float GetAccelRate()
    {
        actionState = playerControllerScript.IsAction();

        if (actionState) acceleratorRate = maxAccelRate;
        else acceleratorRate = minAccelRate;

        return acceleratorRate;
    }

    // ゲーム開始状態の取得
    public bool GetStartPandemic()
    {
        return isStartPandemic;
    }

    // クリア確認
    public bool IsClear()
    {
        return isClear;
    }
    //市民の数を取得
    public int GetActorNum()
    {
        return actorNum;
    }

    // privateメソッド //

    // ゲームが終了した時
    void FinishGame(bool isClear)
    {
        if (isClear)
        {
            Debug.Log("FinishedGame");
            Debug.Break();
        }
    }
    //パンデミック状態かどうか
    bool GetPandemic()
    {
        //tureならパンデミック中
        return pandemicFlag;
    }
}
