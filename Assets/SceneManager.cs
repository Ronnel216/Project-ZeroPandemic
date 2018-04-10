using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
        var obj = GameObject.Find("SaveScore");
        Destroy(obj);
	}
	
	// Update is called once per frame
	void Update () {
        if (Application.loadedLevelName == "TitleScene")
        {            
            if (Input.anyKeyDown)
                Application.LoadLevel("Prototype");
        }
    }
    public void LoadPlayScene()
    {
    }
}
