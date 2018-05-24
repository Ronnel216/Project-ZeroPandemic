//__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/
//! @file   FlowScript
//!
//! @brief  流れるスクリプト
//!
//! @date   2018/05/24 
//!
//! @author 澤田
//__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowScript : MonoBehaviour
{
    [SerializeField]
    private Vector3 m_flowVec;              // 流される方向



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
		
	}


    //----------------------------------------------------------------------
    //! @brief ヒット時処理
    //!
    //! @param[in] オブジェクト
    //!
    //! @return なし
    //----------------------------------------------------------------------
    private void OnTriggerStay(Collider other)
    {
        if (this.enabled == false) return;
        if (other.tag == "InfectedActor" || other.tag == "Actor")
        {
            other.transform.position += m_flowVec;
        }
    }
}
