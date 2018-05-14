//__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/
//! @file   SaveStr
//!
//! @brief  SaveStrの管理スクリプト
//!
//! @date   2018/04/07 
//!
//! @author Y.okada
//__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveStr : MonoBehaviour
{
    // 名前
    [SerializeField]
    private string userName;
    // スコア
    [SerializeField]
    private float resultScore;

    // Use this for initialization
    //----------------------------------------------------------------------
    //! @brief Startメソッド
    //!
    //! @param[in]なし
    //!
    //! @return なし
    //----------------------------------------------------------------------
    void Start()
    {
        userName = "";
        resultScore = 0;

        DontDestroyOnLoad(this);
    }

    // Update is called once per frame
    //----------------------------------------------------------------------
    //! @brief Updateメソッド
    //!
    //! @param[in]なし
    //!
    //! @return なし
    //----------------------------------------------------------------------
    void Update()
    {

    }

    //----------------------------------------------------------------------
    //! @brief 名前をセットする処理
    //!
    //! @param[in] name
    //!
    //! @return なし
    //----------------------------------------------------------------------
    public void SetuserName(string name)
    {
        userName = name;
    }

    //----------------------------------------------------------------------
    //! @brief スコアをセットする処理
    //!
    //! @param[in] name
    //!
    //! @return なし
    //----------------------------------------------------------------------
    public void SetresultScore(float score)
    {
        resultScore = score;
    }

    //----------------------------------------------------------------------
    //! @brief 名前をゲットする処理
    //!
    //! @param[in] name
    //!
    //! @return なし
    //----------------------------------------------------------------------
    public string GetuserName()
    {
        return userName;
    }

    //----------------------------------------------------------------------
    //! @brief スコアをゲットする処理
    //!
    //! @param[in] name
    //!
    //! @return なし
    //----------------------------------------------------------------------
    public float GetresultScore()
    {
        return resultScore;
    }

}
