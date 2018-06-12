using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class TestChengeRanking : MonoBehaviour
{
    [SerializeField]
    private GameObject saveStr;

    [SerializeField]
    private SaveStr sv;
	// Use this for initialization
	void Start ()
    {
        saveStr = GameObject.FindGameObjectWithTag("Data");
        sv = GameObject.FindGameObjectWithTag("Data").GetComponent<SaveStr>();
    }

    // Update is called once per frame
    void Update ()
    {
        sv.SetStageNum(1);
        if (Input.anyKeyDown)
            SceneManager.LoadScene("RankingScene");
    }
}
