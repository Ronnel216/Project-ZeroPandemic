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
        "TitleScene",
        "TownStageScene",
        "IceStageScene",
        "DesertStageScene",
        "DarknessStageScene"

    };
    private int m_stageNum = 0;
    private int m_selectNum = 0;                        // 選んでいるステージの要素番号

    // 長押し判定
    float inputTime = 0;
    // 入力直後待機時間
    float changeWaitTime = 0;

    // ステージイメージ
    GameObject stageImage;
    SpriteRenderer stageImageRenderer;

    [SerializeField]
    Sprite[] images;

    [SerializeField]
    float rotateSpdBase = 2.0f;
    float rotateSpd;

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
        m_selectNum = 1;    // オフセット

        stageImage = GameObject.Find("StageImage");
        stageImageRenderer = stageImage.GetComponent<SpriteRenderer>();
        stageImageRenderer.sprite = images[m_selectNum];


        rotateSpd = 100.0f;
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

        // 表示の更新

        // 回転
        stageImage.transform.Rotate(0.0f, 0.0f, rotateSpd);

        // 入力
        float axis = Input.GetAxis("Horizontal");

        bool canChangeSelect = true;
        changeWaitTime -= Time.deltaTime;
        if (changeWaitTime > 0) canChangeSelect = false;
        if (canChangeSelect == false) return;
        
        // スティック入力
        float freeAxis = 0.5f;
        if (Mathf.Abs(axis) < freeAxis) axis = 0.0f;
        else changeWaitTime = 0.25f;

        bool wasChangeNum = true;
        if (axis > 0)
            m_selectNum++;
        else if (axis < 0)
            m_selectNum--;
        else
            wasChangeNum = false;

        // ステージ数内に収める
        if (m_selectNum < 0) m_selectNum = m_stageNum - 1;
        if (m_selectNum >= m_stageNum) m_selectNum = 0;

        m_sceneNameText.text = m_sceneName[m_selectNum].Remove(m_sceneName[m_selectNum].Length -1 - 5, 5);

        // 回転速度の更新
        float lerpLevel = 0.1f;
        if (rotateSpd > rotateSpdBase) rotateSpd = rotateSpd + (rotateSpdBase - rotateSpd) * lerpLevel;

        if (wasChangeNum)
        {
            rotateSpd = 100.0f;
            stageImageRenderer.sprite = images[m_selectNum];
        }

        // ステージ決定
        if (Input.GetButtonDown("Button A"))
            SceneManager.LoadScene(m_sceneName[m_selectNum]);
    }
}
