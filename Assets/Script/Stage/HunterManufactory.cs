//__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/
//! @file   HunterManufactory
//!
//! @brief  ハンター製作所
//!
//! @date   2018/05/02 
//!
//! @author 澤田
//__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HunterManufactory : MonoBehaviour {

    [SerializeField]
    private GameObject m_hunter;                // ハンターオブジェクト
    [SerializeField]
    private float m_addRate = 0.01f;            // 一回の製作加算量
    private float m_manufactureRate = 0.0f;     // ハンターの製作進行度(0%~100%)
    public float ManuFactureRate
    {
        get { return m_manufactureRate; }
    }
    [SerializeField]
    private int m_manufactureNumMax = 5;        // 製作に関われる人数上限
    private int m_manufactureNum = 0;           // 製作に関わっている人数
    public int ManufactureNum
    {
        get { return m_manufactureNum; }
        set { m_manufactureNum = value; }
    }
    [SerializeField]
    private float m_factoryRange = 0;           // 製作所の範囲
    public float FactoryRange
    {
        get { return m_factoryRange; }
    }

    private bool m_completion = false;          // 完成済みか
    private Transform m_createrPos =null;       // 作成者座標
    public Transform CreaterPos
    {
        set { m_createrPos = value; }
    }

    //----------------------------------------------------------------------
    //! @brief 初期化処理
    //!
    //! @param[in] なし
    //!
    //! @return なし
    //----------------------------------------------------------------------
    void Start ()
    {
        PlayScreenControl.hunterManufactory = this;
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
        if (m_completion) return;

        // ハンターの製作進行度が100%
        if (m_manufactureRate >= 100.0f)
        {
            m_manufactureRate = 100.0f;
            // 完成
            Completion();
        }
	}



    //----------------------------------------------------------------------
    //! @brief ハンター完成
    //!
    //! @param[in] なし
    //!
    //! @return なし
    //----------------------------------------------------------------------
    private void Completion()
    {
        // ハンター放出
        Vector3 thisPos = this.transform.position;

        // 出現位置
        Vector3 pos = m_createrPos.position;
        GameObject hunter = Instantiate(m_hunter, pos,Quaternion.identity);
        hunter.transform.parent = GameObject.Find("Actors").transform;
        m_completion = true;
    }



    //----------------------------------------------------------------------
    //! @brief 製作に向かう必要があるか
    //!
    //! @param[in] なし
    //!
    //! @return false:ない　true:ある
    //----------------------------------------------------------------------
    public bool IsSupport()
    {
        // 製作人数が上限
        if (m_manufactureNum >= m_manufactureNumMax) return false;
        if (m_completion) return false;

        return true;
    }



    //----------------------------------------------------------------------
    //! @brief 製作を進める
    //!
    //! @param[in] なし
    //!
    //! @return true:製作終了
    //----------------------------------------------------------------------
    public bool ManufactureHunter()
    {
        if (m_manufactureRate >= 100.0f) return true;

        m_manufactureRate += m_addRate;
        return false;
    }
}
