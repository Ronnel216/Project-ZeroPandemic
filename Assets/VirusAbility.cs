using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VirusAbility : MonoBehaviour {

    // ボーナスステータス(％)
    public class BounsStatus
    {
        float moveSpeedRate;
    }

    BounsStatus bounsStatus;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    
    // プロパティ
    public BounsStatus bouns
    {
        get { return bounsStatus; }
    }

}
