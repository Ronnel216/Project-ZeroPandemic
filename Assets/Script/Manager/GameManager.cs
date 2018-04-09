using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEditor.SceneManagement;

public class GameManager : MonoBehaviour {

    // 制限時間
    [SerializeField]
    float timeLimit;
    float time;
    bool isStartPandemic;

    [SerializeField]
    Virus playervirus; // 仮
    [SerializeField]
    bool crazy;
    [SerializeField]
    bool speedUp;
    public static int infectedNum = 0;
    public static int killedNum = 0;
    // Use this for initialization

    //[SerializeField]
    //int targetInfectedNum = 47;

    [SerializeField]
    PlayerController PlayerControllerScript;

    //拡張範囲を広げた時のデメリット
    [SerializeField]
    float maxAccelRate;
    [SerializeField]
    float minAccelRate;
    private float accelRate = 1.0f;
    bool actionState;

    [SerializeField]
    StageManager stageMnager;

    [SerializeField]
    SaveStr saveStr;

    static public Virus[] citizen; // 仮

    // スコア
    float score;

    // クリアしたか
    bool isClear;

    void Start () {
        time = timeLimit;
        infectedNum = 0;
        killedNum = 0;
        //for (var i = 0; i < citizen.Length; i++)
        //{
        //    if (citizen)
        //}
        score = 0.0f;
        isStartPandemic = false;
        actionState = PlayerControllerScript.IsAction();
        //saveStr = GameObject.FindGameObjectWithTag("Data").GetComponent<SaveStr>();
        if (saveStr == null)
        {
            Debug.Log("saveStr is null");
            Debug.Break();
        } 
    }

    // Update is called once per frame
    void Update () {

        //if (Input.GetKeyDown(KeyCode.LeftShift))
        //{

        //    playervirus.Infected(null);     // 仮  
        //    if (crazy)
        //        playervirus.gameObject.GetComponent<VirusAbility>().AddSkill(new CrazySkill());
        //    if (speedUp)
        //    {
        //        Skill buffer = new StatusBaffler();
        //        var bonus = new Skill.VirusStatus();
        //        bonus.bouns.moveSpeedRate = 0.5f;
        //        buffer.SetBouns(bonus);
        //        playervirus.gameObject.GetComponent<VirusAbility>().AddSkill(buffer);
        //    }
        //    StartGame(); // 仮
        //}

        // 感染開始後の処理 //
        if (isStartPandemic == false) return;

        // クリアしたかの判定
        isClear = GameObject.FindGameObjectWithTag("Actor") == null;

        // 全ステージクリアした  
        // スコアの代入はここで
        if (stageMnager.AllClear)
        {
            score = GetClearTime();
            saveStr.SetresultScore(score);
            UnityEngine.SceneManagement.SceneManager.LoadScene("RankingScene");
        }

        // クリア前の処理
        if (IsClear()) return;
        time -= Time.deltaTime * accelRate;
        if (time < 0.0f) time = 0.0f;

        if (time == 0.0f)
        {
            score = 0;
            saveStr.SetresultScore(score);
            UnityEngine.SceneManagement.SceneManager.LoadScene("RankingScene");
        }
        //Debug.Log("TimeLimit : " + time.ToString("F") + "s / " + timeLimit.ToString("F") + "s");
        //if (timeLimit < time) FinishGame(false);

        //// エリア内の市民を全員感染させた時の処理 //
        //if (IsClear()) FinishGame(true);

    }

    // publicメソッド //
    public void StartGame()
    {
        playervirus.Infected(null);     // 仮  
        isStartPandemic = true;
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

    public float GetAccelRate()
    {
        actionState = PlayerControllerScript.IsAction();

        if (actionState) accelRate = maxAccelRate;
        else accelRate = minAccelRate;

        return accelRate;
    }

    //ゲーム開始状態の取得
    public bool GetStartPandemic()
    {
        return isStartPandemic;
    }

    public bool IsClear()
    {
        /*return GameManager.infectedNum == targetInfectedNum*/;
        return isClear;
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

}
