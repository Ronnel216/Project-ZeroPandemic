using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameEndUI : MonoBehaviour {
    [SerializeField]
    GameObject canvas;
    [SerializeField]
    GameObject clearUI;
    [SerializeField]
    GameObject overUI;

    // Use this for initialization
    void Start () {
    }
    public void CreateGameEndUI(bool flag)
    {
        if(flag)
        {
            GameObject prefab = (GameObject)Instantiate(clearUI);
            prefab.transform.SetParent(canvas.transform, false);
        }
        else
        {
            GameObject prefab = (GameObject)Instantiate(overUI);
            prefab.transform.SetParent(canvas.transform, false);
        }
    } 
}
