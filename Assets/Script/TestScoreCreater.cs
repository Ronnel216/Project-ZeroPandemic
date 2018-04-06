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
    

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        UnityEngine.SceneManagement.SceneManager.LoadScene(nextScene);
    }
}
