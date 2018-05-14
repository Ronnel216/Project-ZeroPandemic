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
    private float comboTime = 3.0f;

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
        comboNum = 0;
    }

    //----------------------------------------------------------------------
    //! @brief Updateメソッド
    //!
    //! @param[in]なし
    //!
    //! @return なし
    //----------------------------------------------------------------------
    // Update is called once per frame
    void Update ()
    {
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
        return comboNum++;
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
