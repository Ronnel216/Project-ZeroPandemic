//__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/
//! @file   ExpansionControl.cs
//!
//! @brief  拡張範囲管理
//!
//! @date   日付　2018/03/30
//!
//! @author 制作者名 澤田
//__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpansionControl : MonoBehaviour
{
    [SerializeField]
    private float m_defaultArea = 3.0f;       // 初期拡張範囲
    [SerializeField]
    private float m_expandMaxArea = 6.0f;     // 拡張時拡張範囲
    private float m_expansionArea;            // 拡張範囲
    public float ExpansionArea
    {
        get { return m_expansionArea; }
        set { m_expansionArea = value; }
    }

    [SerializeField]
    private float m_increase = 0.1f;          // 増加量

    private UIControl m_uiController = null;  // UI操作


    //----------------------------------------------------------------------
    //! @brief 初期化処理
    //!
    //! @param[in] なし
    //!
    //! @return なし
    //----------------------------------------------------------------------
    void Start()
    {

    }



    //----------------------------------------------------------------------
    //! @brief 初期化処理
    //!
    //! @param[in] なし
    //!
    //! @return なし
    //----------------------------------------------------------------------
    void Update()
    {
        // 拡張範囲の更新 (初期拡張範囲 + 感染者１人に対する増加量 * 感染者数)
        //m_expansionArea = m_defaultArea + (m_increase * GameManager.infectedNum);

        if (m_uiController == null)
        {
            GameObject obj = GameObject.Find("LuxusArea(Clone)");
            if (obj)
                m_uiController = obj.GetComponent<UIControl>();
        }
        else                               // 直径に直す 
            m_uiController.ChangeSize(m_expansionArea * 2);
    }



    //----------------------------------------------------------------------
    //! @brief 拡張
    //!
    //! @param[in] なし
    //!
    //! @return なし
    //----------------------------------------------------------------------
    public void Expand()
    {
        m_expansionArea = m_expandMaxArea;
    }



    //----------------------------------------------------------------------
    //! @brief 縮小
    //!
    //! @param[in] なし
    //!
    //! @return なし
    //----------------------------------------------------------------------
    public void Shrinking()
    {
        m_expansionArea = m_defaultArea;
    }
}
