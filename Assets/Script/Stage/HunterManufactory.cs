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
    private int m_manufactureNumMax = 5;        // 製作に関われる人数上限
    private int m_manufactureNum = 0;           // 製作に関わっている人数

    private float m_manufactureRate = 0.0f;     // ハンターの製作進行度(0%~100%)
    public float ManufactureRate
    {
        get { return m_manufactureRate; }
        set { m_manufactureRate = ManufactureRate; }
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
        m_manufactureRate += m_manufactureNum * 0.01f;

        // ハンターの製作進行度が100%
        if (m_manufactureRate >= 100.0f)
            Completion();

            m_manufactureNum = 0;
	}



    //----------------------------------------------------------------------
    //! @brief ヒット時処理
    //!
    //! @param[in] 当たったオブジェクト
    //!
    //! @return なし
    //----------------------------------------------------------------------
    public void OnTriggerStay(Collider other)
    {
        // 製作人数が上限
        if (m_manufactureNum <= m_manufactureNumMax) return;
       
        GameObject hitObj = other.gameObject;

        // 触れたのがゾンビ
        if (hitObj.name != "Player" && hitObj.tag == "InfectedActor")
            m_manufactureNum++;
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
        Instantiate(m_hunter, this.transform);
    }



    //----------------------------------------------------------------------
    //! @brief ここに向かうべきか
    //!
    //! @param[in] なし
    //!
    //! @return false:向かわない　true:向かう
    //---------------r-------------------------------------------------------
    public bool ShouldGo()
    {
        // 製作人数が上限
        if (m_manufactureNum <= m_manufactureNumMax) return false;

        return true;
    }
}
