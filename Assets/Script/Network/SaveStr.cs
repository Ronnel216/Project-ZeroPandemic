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

    [SerializeField]
    private int stageNum = -1;

    private string[] rankingName 
        = { "Stage1",
            "Stage2",
            "Stage3",
            "Stage4",
          };

    //private string[] rankingName
    //    = { "Town",
    //        "Ice",
    //        "Desert",
    //        "Darkness",
    //      };

    [SerializeField]
    private QuickRanking rankingManager;

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
        if (stageNum < 0) Debug.Log("stageNumが設定されていない");
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
    public void SetUserName(string name)
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
    public void SetResultScore(float score)
    {
        resultScore = score;
    }

    //----------------------------------------------------------------------
    //! @brief スコアをセットする処理
    //!
    //! @param[in] name
    //!
    //! @return なし
    //----------------------------------------------------------------------
    public void SetStageNum(int num)
    {
        stageNum = num;
    }

    //----------------------------------------------------------------------
    //! @brief 名前をゲットする処理
    //!
    //! @param[in] name
    //!
    //! @return なし
    //----------------------------------------------------------------------
    public string GetUserName()
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
    public float GetResultScore()
    {
        return resultScore;
    }

    //----------------------------------------------------------------------
    //! @brief スコアをゲットする処理
    //!
    //! @param[in] name
    //!
    //! @return なし
    //----------------------------------------------------------------------
    public string GetRankingName()
    {
        return rankingName[stageNum];
    }
}
