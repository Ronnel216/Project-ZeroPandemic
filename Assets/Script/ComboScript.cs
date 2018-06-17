//__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/
//! @file   ComboScript
//!
//! @brief  コンボ管理スクリプト
//!
//! @date   2018/04/19 
//!
//! @author Y.okada
//__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ComboScript : MonoBehaviour
{
    //コンボ数
    private static int comboNum;

    [SerializeField]
    private float comboTime;

    [SerializeField]
    private Image comboCounter;

    [SerializeField]
    private Text comboText;

    private float time = 0.0f;

    private float resetTime = 0.0f;

    private bool timeFlag = false;

    //----------------------------------------------------------------------
    //! @brief Startメソッド
    //! 
    //! @param[in] なし
    //!
    //! @return なし
    //----------------------------------------------------------------------
    // Use this for initialization
    void Start ()
    {
        Initialize();
        StartCoroutine(ComboCoroutine());
    }

    void Update()
    {
        if(timeFlag)
            time += Time.deltaTime;

        Debug.Log(resetTime);

        if(time >= comboTime && comboNum > 0)
            ResetCombo();
    }

    //----------------------------------------------------------------------
    //! @brief PlusCombo
    //!        コンボをプラスする
    //!
    //! @param[in] なし
    //!
    //! @return なし
    //----------------------------------------------------------------------
    public void Initialize()
    {
        comboCounter.enabled = false;
        comboText.enabled = false;
        comboNum = 0;
        resetTime = 0;
    }


    //----------------------------------------------------------------------
    //! @brief PlusCombo
    //!        コンボをプラスする
    //!
    //! @param[in] なし
    //!
    //! @return なし
    //----------------------------------------------------------------------
    public int PlusCombo()
    {
        resetTime = 0;
        comboCounter.enabled = false;
        comboText.enabled = false;
        return comboNum++;
    }

    //----------------------------------------------------------------------
    //! @brief PlusCombo
    //!        コンボをプラスする
    //!
    //! @param[in] なし
    //!
    //! @return なし
    //----------------------------------------------------------------------
    public int ResetCombo()
    {
        resetTime += Time.deltaTime;
        if(resetTime >= 2)
        {
            comboCounter.enabled = true;
            comboText.enabled = true;
        }
        if (resetTime >= comboTime)
        {
            comboCounter.enabled = false;
            comboText.enabled = false;
            comboNum = 0;
            resetTime = 0;
            time = 0;
        }

        return comboNum;
    }

    public int GetCombo()
    {
        return comboNum;
    }


    //----------------------------------------------------------------------
    //! @brief ComboCoroutine
    //!        指定秒後にコンボをリセットする
    //!
    //! @param[in] なし
    //!
    //! @return なし
    //----------------------------------------------------------------------
    public IEnumerator ComboCoroutine()
    {
        yield return new WaitForSeconds(3.75f);
        timeFlag = true;
    }
}
