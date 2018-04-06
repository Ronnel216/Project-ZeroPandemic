using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.SceneManagement;

public class TestScoreCreater : MonoBehaviour {

    [SerializeField]
    string nextScene;

    [SerializeField]
    float score;

    [SerializeField]
    HOGE saveScore;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        saveScore.SaveStr(score);
        UnityEngine.SceneManagement.SceneManager.LoadScene(nextScene);
    }
}
