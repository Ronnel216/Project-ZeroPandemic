using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GameManager : MonoBehaviour {

    // 制限時間
    [SerializeField]
    float timeLimit;
    float time;
    bool isStartPandemic;

    [SerializeField]
    Virus playervirus; // 仮
    [SerializeField]
    Text testText;
    [SerializeField]
    bool crazy;
    [SerializeField]
    bool speedUp;
    public static int infectedNum = 0;
    public static int killedNum = 0;
    // Use this for initialization

    [SerializeField]
    int targetInfectedNum = 15;

    [SerializeField]
    PlayerController PlayerControllerScript;

    //拡張範囲を広げた時のデメリット
    [SerializeField]
    float maxAccelRate;
    [SerializeField]
    float minAccelRate;
    private float accelRate = 1.0f;
    bool actionState;

    void Start () {
        time = 60.0f;
        isStartPandemic = false;
        actionState = PlayerControllerScript.IsAction();

    }

    // Update is called once per frame
    void Update () {

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {

            playervirus.Infected(null);     // 仮  
            if (crazy)
                playervirus.gameObject.GetComponent<VirusAbility>().AddSkill(new CrazySkill());
            if (speedUp)
            {
                Skill buffer = new StatusBaffler();
                var bonus = new Skill.VirusStatus();
                bonus.bouns.moveSpeedRate = 0.5f;
                buffer.SetBouns(bonus);
                playervirus.gameObject.GetComponent<VirusAbility>().AddSkill(buffer);
            }
            StartGame(); // 仮
        }
        // 感染開始後の処理 //
        if (isStartPandemic == false) return;      
        time -= Time.deltaTime;
        testText.text = "Time : " + time.ToString("F") + "s 感染" + infectedNum.ToString();
        //Debug.Log("TimeLimit : " + time.ToString("F") + "s / " + timeLimit.ToString("F") + "s");
        if (timeLimit < time) FinishGame(false);

        // エリア内の市民を全員感染させた時の処理 //
        if (IsClear()) FinishGame(true);  

	}

    // publicメソッド //
    public void StartGame()
    {
        isStartPandemic = true;
    }

    // 制限時間の取得
    public float GetTimeLimit()
    {
        return time;
    }

    // 制限時間の取得
    public float GetClearTime()
    {               
        return GetTimeLimit();
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
    // privateメソッド //

    bool IsClear()
    {
        return GameManager.infectedNum == targetInfectedNum;
    }

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
