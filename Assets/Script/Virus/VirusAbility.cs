using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VirusAbility : MonoBehaviour {

    // ボーナスステータス(％)
    public class BounsStatus
    {
        public float moveSpeedRate;
        public void Plus(BounsStatus bouns)
        {
            moveSpeedRate += bouns.moveSpeedRate;
        }
    }

    BounsStatus bounsStatus = new BounsStatus();
    List<Skill> skills = new List<Skill>();

    Movement movement;

    float baseSpeed;    // 仮

    // Use this for initialization
    void Start () {

        movement = GetComponent<Movement>();
        if (movement == null)
        {
            Debug.Log("Failen : GetComponent<Movement>()");
            Debug.Break();
        }


        baseSpeed = movement.GetSpeed(); // 仮
    }

    // Update is called once per frame
    void Update () {
        // 仮 速度にウィルスの影響を反映する
        movement.SetSpeed(baseSpeed + baseSpeed * bounsStatus.moveSpeedRate);
        foreach (Skill skill in skills)
        {
            skill.Update(gameObject);
        }
    }

    public void Copy(VirusAbility virus)
    {
        virus.bounsStatus = bounsStatus;
        virus.skills = skills;
    }
    
    public void AddSkill(Skill skill)
    {
        bounsStatus.Plus(skill.status.bouns);
        skills.Add(skill);
    }
    
    // プロパティ
    public BounsStatus bouns
    {
        get { return bounsStatus; }
    }
    
}
