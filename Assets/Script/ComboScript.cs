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
        comboText = GameObject.Find("CombText").GetComponent<Text>();
        if (comboText == null) return;
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
        if (comboText == null) return 0;
        resetTime = 0;

        if (!comboText) return 0;

        comboText.enabled = true;
        comboText.color = new Color(0.0f, 0.0f, 0.0f, 1f);
        if (comboNum % 3 == 0)
        {
            if (comboText.transform.localScale.x <= 1.7)
                comboText.transform.localScale += new Vector3(0.1f, 0.1f, 0.0f);
        }
        return comboNum++;
    }

    //----------------------------------------------------------------------
    //! @brief ResetCombo
    //!        コンボをプラスする
    //!
    //! @param[in] なし
    //!
    //! @return なし
    //----------------------------------------------------------------------
    public int ResetCombo()
    {
        resetTime += Time.deltaTime;
        if (resetTime >= 1.5)
        {
            comboText.color += new Color(0.1f, 0.0f, 0.0f, 0.0f);
        }
        if (resetTime >= 2.2)
        {
            comboText.color += new Color(0.0f,0.0f,0.0f,-0.1f);
        }
        if (resetTime >= comboTime)
        {
            comboText.enabled = false;
            comboText.transform.localScale = new Vector3(1.0f, 1.0f, 0);
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
