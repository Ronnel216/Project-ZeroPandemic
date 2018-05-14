//__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/
//! @file   FlowScript
//!
//! @brief  流され管理スクリプト
//!
//! @date   2018/04/26 
//!
//! @author Y.okada
//__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowScript : MonoBehaviour
{
    private Rigidbody rb;
    [SerializeField]
    private Vector3 vec;

    // Use this for initialization
    void Start ()
    {
        rb = this.GetComponent<Rigidbody>();
    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}


    //----------------------------------------------------------------------
    //! @brief OnTriggerStay
    //!        触れている間Rigidbodyを持ったキャラクターはして方向に流される
    //!
    //! @param[in] Collider other(キャラクター)
    //!
    //! @return なし
    //----------------------------------------------------------------------
    void OnTriggerStay(Collider other)
    {
        if(other.gameObject.tag == "Flow")
        {
            rb.velocity = vec;
        }
    }
}
