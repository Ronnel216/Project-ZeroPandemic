﻿using System.Collections;
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
    [SerializeField]
    Virus playerVirus;

    // 感染者数
    public static int infectedNum = 0;

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

    public ComboScript comboScript;                                   // コンボスクリプト

    static public Virus[] citizen; // 仮

    // スコア
    float score;

    // クリアしたか
    bool isClear;

    //感染率
    [SerializeField]
    float perdemic = 80.0f;
    AIManager pandemic;
    //パンデミックしているかどうか
    bool pandemicFlag = false;
    void Start () {
        // 制限時間を設定
        time = timeLimit;

        // staticメンバの初期化
        infectedNum = 0;
        killedNum = 0;
        
        // 加速率の初期値設定        
        acceleratorRate = minAccelRate;
        score = 0.0f;
        isStartPandemic = false;
        actionState = playerControllerScript.IsAction();

        actorNum = GameObject.FindGameObjectsWithTag("Actor").Length;
        pandemic = GameObject.Find("AIManger").GetComponent<AIManager>();

        // デバッグ処理
        if (saveStr == null)
        {
            Debug.Log("saveStr is null");
            Debug.Break();
        } 
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
            score = GetClearTime();
            saveStr.SetResultScore(score);
            UnityEngine.SceneManagement.SceneManager.LoadScene("RankingScene");
        }
        if (isClear)
        {
            infectedNum = 0;
            pandemicFlag = false;
            comboScript.Initialize();
        }

        // クリア前の処理
        if (IsClear()) return;
        time -= Time.deltaTime;// * acceleratorRate;
        if (time < 0.0f) time = 0.0f;


        //感染率が80%以上ならパンデミック開始
        if(!pandemicFlag)
        {
            if ((float)infectedNum / actorNum * 100.0f >= perdemic)
            {
                pandemic.StartPandemic();
                pandemicFlag = true;
            }
        }
        if (time == 0.0f)
        {
            score = 0;
            saveStr.SetResultScore(score);
            UnityEngine.SceneManagement.SceneManager.LoadScene("RankingScene");
        }
        //if (time == 0.0f)
        //{
        //    score = 0;
        //    saveStr.SetresultScore(score);
        //    UnityEngine.SceneManagement.SceneManager.LoadScene("RankingScene");
        //}
        //Debug.Log("TimeLimit : " + time.ToString("F") + "s / " + timeLimit.ToString("F") + "s");
        //if (timeLimit < time) FinishGame(false);

        //// エリア内の市民を全員感染させた時の処理 //
        //if (IsClear()) FinishGame(true);

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
        score = 0;
        saveStr.SetResultScore(score);
        UnityEngine.SceneManagement.SceneManager.LoadScene("RankingScene");

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
