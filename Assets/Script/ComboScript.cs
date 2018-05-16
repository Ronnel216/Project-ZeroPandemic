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

public class ComboScript : MonoBehaviour
{
    //コンボ数
    private static int comboNum;

    [SerializeField]
    private float comboTime = 180.0f;

    private float time;

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
    }

    void Update()
    {
        ResetCombo();
    }

    //----------------------------------------------------------------------
    //! @brief Updateメソッド
    //!
    //! @param[in]なし
    //!
    //! @return なし
    //----------------------------------------------------------------------
    // Update is called once per frame
    void FixedUpdate ()
    {
        time++;
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
        comboNum = 0;
        time = 0;
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
        time = 0;
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
        if (time >= comboTime)
        {
            comboNum = 0;
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
        yield return new WaitForSeconds(comboTime);
        comboNum = 0;
    }
}
