//__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/
//! @file   Fall.cs
//!
//! @brief  穴
//!
//! @date   日付　2018/05/22
//!
//! @author 制作者名 澤田
//__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Fall : MonoBehaviour {



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
    //! @param[in] なし
    //!
    //! @return なし
    //----------------------------------------------------------------------
    private void OnTriggerEnter(Collider other)
    {
        // ナビメッシュを無効化
        Movement move = other.GetComponent<Movement>();
        if (move) move.SetUseNavMesh(false);

        // 重力を有効化
        Rigidbody rigid = other.GetComponent<Rigidbody>();
        if (rigid) rigid.useGravity = true;
    }
}
