//__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/
//! @file   SelectController.cs
//!
//! @brief  ステージセレクト管理
//!
//! @date   日付　2018/05/31
//!
//! @author 制作者名 澤田
//__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SelectController : MonoBehaviour {

    [SerializeField]
    private Text m_sceneNameText = null;                // シーン名テキスト
    private static readonly string[] m_sceneName =      // ステージ名
    {                                                   // 新しくステージ追加したらシーン名をここに追加してね
        "Prototype",
        "TitleScene",
    };
    private int m_stageNum = 0;
    private int m_selectNum = 0;                        // 選んでいるステージの要素番号

    //----------------------------------------------------------------------
    //! @brief 初期化処理
    //!
    //! @param[in] なし
    //!
    //! @return なし
    //----------------------------------------------------------------------
    void Start ()
    {
        // ステージ数の取得
        m_stageNum = m_sceneName.Length;

    }



    //----------------------------------------------------------------------
    //! @brief 更新処理
    //!
    //! @param[in] なし
    //!
    //! @return なし
    //----------------------------------------------------------------------
    void Update ()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
            m_selectNum++;
        if (Input.GetKeyDown(KeyCode.LeftArrow))
            m_selectNum--;

        // ステージ数内に収める
        if (m_selectNum < 0) m_selectNum = m_stageNum - 1;
        if (m_selectNum >= m_stageNum) m_selectNum = 0;

        m_sceneNameText.text = m_sceneName[m_selectNum];

        // ステージ決定
        if (Input.GetKeyDown(KeyCode.Space))
            SceneManager.LoadScene(m_sceneName[m_selectNum]);
    }
}
