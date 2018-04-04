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
    // Use this for initialization
    void Start () {
        time = 0.0f;
        isStartPandemic = false;

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
        time += Time.deltaTime;
        testText.text = "Time : " + time.ToString("F") + "s 感染" + infectedNum.ToString();
        //Debug.Log("TimeLimit : " + time.ToString("F") + "s / " + timeLimit.ToString("F") + "s");
        if (timeLimit < time) FinishGame();

       // エリア内の市民を全員感染させた時の処理 //
       // if(全員感染) ステージを拡張する

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

    // 制限時間の進行度
    public float GetTimeLimitStep()
    {
        return time / GetTimeLimit();
    }


    // privateメソッド //
    void FinishGame()
    {
        Debug.Log("FinishedGame");
        Debug.Break();
    }
}
