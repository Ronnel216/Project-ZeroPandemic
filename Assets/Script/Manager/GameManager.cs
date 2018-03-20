using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    // 制限時間
    [SerializeField]
    float timeLimit;
    float time;
    bool isStartPandemic;

    [SerializeField]
    Virus playervirus; // 仮

    // Use this for initialization
    void Start () {
        time = 0.0f;
        isStartPandemic = false;

        //StartGame();
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKeyDown(KeyCode.Space))
        {
            playervirus.Infected(null);     // 仮  
            playervirus.gameObject.GetComponent<VirusAbility>().AddSkill(new CrazySkill());
            Skill buffer = new StatusBaffler();
            var bonus = new Skill.VirusStatus();
            bonus.bouns.moveSpeedRate = 0.5f;
            buffer.SetBouns(bonus);
            playervirus.gameObject.GetComponent<VirusAbility>().AddSkill(buffer);
        }

        // 感染開始後の処理 //
        if (isStartPandemic == false) return;      
        time += Time.deltaTime;
        Debug.Log("TimeLimit : " + time.ToString("F") + "s / " + timeLimit.ToString("F") + "s");
        if (timeLimit < time) FinishGame();

       // エリア内の市民を全員感染させた時の処理 //
       // if(全員感染) ステージを拡張する

	}

    // publicメソッド //
    public void StartGame()
    {
        isStartPandemic = true;
    }

    // privateメソッド //
    void FinishGame()
    {
        Debug.Log("FinishedGame");
        Debug.Break();
    }
}
