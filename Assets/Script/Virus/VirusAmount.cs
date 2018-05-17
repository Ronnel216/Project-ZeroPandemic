//__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/
//! @file   VirusAmount
//!
//! @brief  ウイルス量管理スクリプト
//!
//! @date   2018/05/05 
//!
//! @author Y.okada
//__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VirusAmount : MonoBehaviour
{
    public GameManager GameManagerScript;

    public ComboScript comboScript;

    [SerializeField]
    float virusAmount;                      // 現在のウイルス量
    [SerializeField]
    float maxVirusAmount = 100.0f;          // 最大のウイルス量
    [SerializeField]
    float recoveryAmount = 10.0f;           // 回復するウイルス量
    float decreseVirusAmount;               // 減らすウイルス量
    float increaseVirusAmount;              // 増やすウイルス量

    float time;                             // 経過時間

    [SerializeField]
    float maximumValue;                     // 最大値

    [SerializeField]
    int comboCount;                         // 回復用コンボ数

    bool isSetGame;
    bool isVirusControll;                   // ウイルスコントロール用フラグ
    bool comboSeparation;                   // コンボ用フラグ


    PlayerController playerController;

    //----------------------------------------------------------------------
    //! @brief Startメソッド
    //! 
    //! @param[in] なし
    //!
    //! @return なし
    //----------------------------------------------------------------------
    // Use this for initialization
    void Start()
    {
        time = 0;
        isVirusControll = false;
        virusAmount = maxVirusAmount;
        isSetGame = GameManagerScript.GetStartPandemic();
        playerController = GetComponent<PlayerController>();
    }

    //----------------------------------------------------------------------
    //! @brief Updateメソッド
    //!
    //! @param[in]なし
    //!
    //! @return なし
    //----------------------------------------------------------------------
    // Update is called once per frame
    void Update()
    {
        VirusAmountManagement();
    }

    //----------------------------------------------------------------------
    //! @brief VirusAmountManagement
    //!        ウイルス量の管理
    //!
    //! @param[in]なし
    //!
    //! @return なし
    //----------------------------------------------------------------------
    void VirusAmountManagement()
    {
        // ゲームが始まっていない
        if (isSetGame == false)
        {
            isSetGame = GameManagerScript.GetStartPandemic();
            return;
        }
        // ウィルスコントロール状態か
        isVirusControll = playerController.IsAction();
        // ウイルスコントロールを使用しているか
        UseVirusControll(isVirusControll);
        
        // 時間でウイルスゲージを減らす
        time = Time.deltaTime;
        decreseVirusAmount = time;
        increaseVirusAmount = time;
        maxVirusAmount -= decreseVirusAmount;

        // ウイルスコントロールを使用しておらず最大値より少なければ回復
        CheckIncreaseVirusAmount();
        // ウイルス量が最大値を超えていたら設定
        CheckVirusAmountExceed();
        // 決まったコンボ値で様々な値を回復
        ComboRecovery(isComboseparation());
        // ゲームオーバーの判断
        CheckGameOver();
    }

    //----------------------------------------------------------------------
    //! @brief UseVirusControll
    //!        ウイルスコントロールを使用していたらウイルス量を減らす
    //!
    //! @param[in] viruscontroll 
    //!
    //! @return なし
    //----------------------------------------------------------------------
    void UseVirusControll(bool viruscontroll)
    {
        if (viruscontroll)
        {
            //ウイルス使用量
            float virusamout = 0.2f;
            //ウイルス量を減らす
            DecreaseVirusAmount(virusamout);
        }
    }

    //----------------------------------------------------------------------
    //! @brief CheckIncreaseVirusAmount
    //!        ウイルスコントロールを使用しておらず最大値より少なければ回復
    //!
    //! @param[in] なし
    //!
    //! @return なし
    //----------------------------------------------------------------------
    void CheckIncreaseVirusAmount()
    {
        if (virusAmount < maxVirusAmount && !isVirusControll)
            virusAmount += increaseVirusAmount;
    }

    //----------------------------------------------------------------------
    //! @brief CheckVirusAmountExceed
    //!        ウイルス量が最大値を超えていたら設定
    //!
    //! @param[in] なし
    //!
    //! @return なし
    //----------------------------------------------------------------------
    void CheckVirusAmountExceed()
    {
        if (maxVirusAmount > maximumValue)
            maxVirusAmount = maximumValue;

        if (virusAmount > maxVirusAmount)
            virusAmount = maxVirusAmount;
    }

    //----------------------------------------------------------------------
    //! @brief isComboseparation
    //!        コンボした時決まったコンボ値かの判断
    //!
    //! @param[in] なし
    //!
    //! @return comboSeparation　決まったコンボ値(真)それ以外(偽)
    //----------------------------------------------------------------------
    bool isComboseparation()
    {
        if (comboScript.GetCombo() > 0)
        {
            comboSeparation = comboScript.GetCombo() % comboCount == 0;
        }

        return comboSeparation;
    }
    //----------------------------------------------------------------------
    //! @brief ComboRecovery
    //!        //決まったコンボ値で様々な値を回復
    //!
    //! @param[in] comboseparation
    //!
    //! @return なし
    //----------------------------------------------------------------------
    void ComboRecovery(bool comboseparation)
    {
        if (comboseparation)
        {
            maxVirusAmount += recoveryAmount;
            virusAmount += recoveryAmount;
            CheckVirusAmountExceed();
        }
    }

    //----------------------------------------------------------------------
    //! @brief CheckGameOver
    //!        ゲームオーバーの判断
    //!
    //! @param[in]
    //!
    //! @return 
    //----------------------------------------------------------------------
    void CheckGameOver()
    {
        if (maxVirusAmount <= 0.0f)
        {
            GameManagerScript.GameOver();
        }
    }

    //----------------------------------------------------------------------
    //! @brief ウイルス量を減らす
    //!
    //! @param[in]　ウイルス量
    //!
    //! @return なし
    //----------------------------------------------------------------------
    public void DecreaseVirusAmount(float virusamout)
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
    public void DecreaseMaxVirusAmount(float virusamout)
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
