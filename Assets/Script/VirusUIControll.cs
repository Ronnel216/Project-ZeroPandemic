using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VirusUIControll : MonoBehaviour
{
    public Image VirusHPImage;

    public Image VirusSPImage;

    public Image MaxVirusHPImage;

    public Image MaxVirusSPImage;

    public GameManager GameManagerScript;

    public ComboScript combCount;

    float MaxVirusHP;
    float MaxVirusSP;
    float VirusHP;
    float VirusSP;
    float time;
    int ComboNum;
    bool isSetGame;



    // Use this for initialization
    void Start()
    {
        float MaxVirusHP = 100.0f;
        float MaxVirusSP = 100.0f;
        float VirusHP = MaxVirusHP;
        float VirusSP = MaxVirusSP;
        float time = GameManagerScript.GetTimeLimit();
        int ComboNum = 0;
        isSetGame = GameManagerScript.GetStartPandemic();
    }

    // Update is called once per frame
    void Update()
    {
        if (isSetGame)
        {
            MaxVirusHP -= GameManagerScript.GetTimeLimitStep();
            MaxVirusSP += GameManagerScript.GetTimeLimitStep();
            MaxVirusHPImage.fillAmount = MaxVirusHP;
            MaxVirusSPImage.fillAmount = MaxVirusSP;
            VirusHPImage.fillAmount = MaxVirusHP;
            VirusSPImage.fillAmount = MaxVirusSP;
        }
        else
        {
            isSetGame = GameManagerScript.GetStartPandemic();
        }
    }
}
