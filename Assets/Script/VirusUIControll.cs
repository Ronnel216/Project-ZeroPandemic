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
    float VirusAmount;                      //現在のウイルス量
    [SerializeField]
    float MaxVirusAmount;                   //最大のウイルス量
    [SerializeField]
    float RecoveryAmount;                   //回復するウイルス量
    float time;                             //経過時間

    bool isSetGame;
    bool isVirusControll;                   //ウイルスコントロール用フラグ
    bool comboSeparation;                   //コンボ用フラグ

    int comboNum;                           //回復用コンボ数


    //----------------------------------------------------------------------
    //! @brief 初期化処理
    //!
    //! @param[in] なし
    //!
    //! @return なし
    //----------------------------------------------------------------------
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

    //----------------------------------------------------------------------
    //! @brief 更新処理
    //!
    //! @param[in] なし
    //!
    //! @return なし
    //----------------------------------------------------------------------
    void Update()
    {
        if (isSetGame)
        {
            //時間でウイルスゲージを減らす
            time = Time.deltaTime;
            MaxVirusAmount -= time;
            //ウイルスコントロールを使用しておらず最大値より少なければ回復
            if (VirusAmount <= MaxVirusAmount && !isVirusControll)
                VirusAmount += time;
            MaxVirusLeftImage.fillAmount = MaxVirusAmount * 0.01f;
            MaxVirusRightImage.fillAmount = MaxVirusAmount * 0.01f;
            VirusLeftImage.fillAmount = VirusAmount * 0.01f;
            VirusRightImage.fillAmount = VirusAmount * 0.01f;

            //ウイルス量が最大値を超えないように
            if (VirusAmount >= MaxVirusAmount)
                VirusAmount = MaxVirusAmount;

            //コンボした時決まったコンボ値かの判断
            if (combCount.GetCombo() > 0)
            {
                comboSeparation = combCount.GetCombo() % comboNum == 0;
            }
            //決まったコンボ値で様々な値を回復
            if (comboSeparation)
            {
                MaxVirusAmount += RecoveryAmount;
                VirusAmount += RecoveryAmount;
                if (MaxVirusAmount >= 100.0f)
                    MaxVirusAmount = 100.0f;
                if (VirusAmount >= MaxVirusAmount)
                    VirusAmount = MaxVirusAmount;
            }

            //ゲームオーバーの判断
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

    //----------------------------------------------------------------------
    //! @brief ウイルス量を減らす
    //!
    //! @param[in]　ウイルス量
    //!
    //! @return なし
    //----------------------------------------------------------------------
    public void DecreaseVirusAmout(float virusamout)
    {
        VirusAmount -= virusamout;
    }

    //----------------------------------------------------------------------
    //! @brief ウイルスコントロールを使用しているかの判断
    //!
    //! @param[in] ウイルスコントロールをしていれば真、それ以外は偽
    //!
    //! @return なし
    //----------------------------------------------------------------------
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
