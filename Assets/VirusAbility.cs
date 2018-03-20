using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VirusAbility : MonoBehaviour {

    // ボーナスステータス(％)
    public class BounsStatus
    {
        float moveSpeedRate;
        public void Plus(BounsStatus bouns)
        {
            moveSpeedRate += bouns.moveSpeedRate;
        }
    }

    BounsStatus bounsStatus;
    List<Skill> skills;

    Movement movement;

    // Use this for initialization
    void Start () {
        movement = GetComponent<Movement>();
        if (movement == null)
        {
            Debug.Log("Failen : GetComponent<Movement>()");
            Debug.Break();
        }
	}
	
	// Update is called once per frame
	void Update () {        
        //foreach(Skill skill in skills)
        //{
        //    skill.Update(gameObject);
        //}
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
