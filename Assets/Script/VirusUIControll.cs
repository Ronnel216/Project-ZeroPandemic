using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VirusUIControll : MonoBehaviour
{
    public Image VirusLeftImage;

    public Image VirusRightImage;

    public Image MaxVirusLeftImage;

    public Image MaxVirusRightImage;

    public GameManager GameManagerScript;

    public ComboScript combCount;

    [SerializeField]
    float VirusAmount;
    [SerializeField]
    float MaxVirusAmount;
    [SerializeField]
    float RecoveryAmount;
    bool isSetGame;
    bool isVirusControll;
    bool comboSeparation;

    int comboNum;

    float time;

    // Use this for initialization
    void Start()
    {
        MaxVirusAmount = 100.0f;
        comboNum = 5;
        RecoveryAmount = 10.0f;
        time = 0;
        isVirusControll = false;
        VirusAmount = MaxVirusAmount;
        isSetGame = GameManagerScript.GetStartPandemic();
    }

    // Update is called once per frame
    void Update()
    {
        if (isSetGame)
        {
            time = Time.deltaTime;
            MaxVirusAmount -= time;
            if (VirusAmount <= MaxVirusAmount && !isVirusControll)
                VirusAmount += time;
            MaxVirusLeftImage.fillAmount = MaxVirusAmount * 0.01f;
            MaxVirusRightImage.fillAmount = MaxVirusAmount * 0.01f;
            VirusLeftImage.fillAmount = VirusAmount * 0.01f;
            VirusRightImage.fillAmount = VirusAmount * 0.01f;

            if (VirusAmount >= MaxVirusAmount)
                VirusAmount = MaxVirusAmount;

            if (combCount.GetCombo() > 0)
            {
                comboSeparation = combCount.GetCombo() % comboNum == 0;
            }
            if (comboSeparation)
            {
                MaxVirusAmount += RecoveryAmount;
                VirusAmount += RecoveryAmount;
                if (MaxVirusAmount >= 100.0f)
                    MaxVirusAmount = 100.0f;
                if (VirusAmount >= MaxVirusAmount)
                    VirusAmount = MaxVirusAmount;
            }

            if (MaxVirusAmount <= 0.0f || VirusAmount <= 0.0f)
            {
                Debug.Log("げーむおーばー");
                Debug.Break();
            }
        }
        else
        {
            isSetGame = GameManagerScript.GetStartPandemic();
        }
    }

    public void SetVirusAmout(float virusamout)
    {
        VirusAmount -= virusamout;
    }

    public void VirusControll(bool viruscontroll)
    {
        isVirusControll = viruscontroll;
    }

    public float GetVirusAmount()
    {
        return VirusAmount;
    }
    public float GetMaxVirusAmount()
    {
        return MaxVirusAmount;
    }
}
