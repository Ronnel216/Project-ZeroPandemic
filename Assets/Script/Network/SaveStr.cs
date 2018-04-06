using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveStr : MonoBehaviour
{
    [SerializeField]
    private string userName;
    [SerializeField]
    private float resultScore;

    // Use this for initialization
    void Start()
    {
        userName = "";
        resultScore = 0;

        DontDestroyOnLoad(this);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetuserName(string name)
    {
        userName = name;
    }
    public void SetresultScore(float score)
    {
        resultScore = score;
    }

    public string GetuserName()
    {
        return userName;
    }

    public float GetresultScore()
    {
        return resultScore;
    }

}
