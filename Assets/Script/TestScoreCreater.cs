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
    SaveStr saveScore;

	// Use this for initialization
	void Start () {
        saveScore.SetresultScore(score);

    }

    // Update is called once per frame
    void Update () {
        UnityEngine.SceneManagement.SceneManager.LoadScene(nextScene);
    }
}
