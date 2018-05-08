using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VirusAmount : MonoBehaviour
{
    public GameManager GameManagerScript;

    public ComboScript combCount;

    [SerializeField]
    float virusAmount;                      //現在のウイルス量
    [SerializeField]
    float maxVirusAmount = 100.0f;          //最大のウイルス量
    [SerializeField]
    float recoveryAmount = 10.0f;           //回復するウイルス量
    float time;                             //経過時間

    bool isSetGame;
    bool isVirusControll;                   //ウイルスコントロール用フラグ
    bool comboSeparation;                   //コンボ用フラグ

    int comboNum;                           //回復用コンボ数

    PlayerController playerController;

    // Use this for initialization
    void Start()
    {
        comboNum = 5;
        time = 0;
        isVirusControll = false;
        virusAmount = maxVirusAmount;
        isSetGame = GameManagerScript.GetStartPandemic();
        playerController = GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isSetGame)
        {
            isVirusControll = playerController.IsAction();

            if (isVirusControll)
            {
                //ウイルス使用量
                float virusamout = 0.2f;
                //ウイルス量を減らす
                DecreaseVirusAmout(virusamout);
            }
            //時間でウイルスゲージを減らす
            time = Time.deltaTime;
            maxVirusAmount -= time;
            //ウイルスコントロールを使用しておらず最大値より少なければ回復
            if (virusAmount <= maxVirusAmount && !isVirusControll)
                virusAmount += time;

            //ウイルス量が最大値を超えないように
            if (virusAmount >= maxVirusAmount)
                virusAmount = maxVirusAmount;

            //コンボした時決まったコンボ値かの判断
            if (combCount.GetCombo() > 0)
            {
                comboSeparation = combCount.GetCombo() % comboNum == 0;
            }
            //決まったコンボ値で様々な値を回復
            if (comboSeparation)
            {
                maxVirusAmount += recoveryAmount;
                virusAmount += recoveryAmount;
                if (maxVirusAmount >= 100.0f)
                    maxVirusAmount = 100.0f;
                if (virusAmount >= maxVirusAmount)
                    virusAmount = maxVirusAmount;
            }

            //ゲームオーバーの判断
            if (maxVirusAmount <= 0.0f)
            {
                GameObject.Find("GameManager").GetComponent<GameManager>().GameOver();
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
        virusAmount -= virusamout;
    }

    //----------------------------------------------------------------------
    //! @brief ウイルス最大値量を減らす
    //!
    //! @param[in]　ウイルス量
    //!
    //! @return なし
    //----------------------------------------------------------------------
    public void DecreaseMaxVirusAmout(float virusamout)
    {
        maxVirusAmount -= virusamout;
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
        return virusAmount;
    }
    public float GetMaxVirusAmount()
    {
        return maxVirusAmount;
    }

}
