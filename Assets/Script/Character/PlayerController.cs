﻿//__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/
//! @file   PlayerController.cs
//!
//! @brief  プレイヤー移動
//!
//! @date   日付　2018/03/17
//!
//! @author 制作者名 澤田
//__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Movement m_move;                        // 移動コンポーネント

    [SerializeField]
    private KeyCode m_actionButton = KeyCode.Z;     // アクションボタン設定

    //----------------------------------------------------------------------
    //! @brief 初期化処理
    //!
    //! @param[in] なし
    //!
    //! @return なし
    //----------------------------------------------------------------------
    void Start()
    {
        // コンポーネントの取得
        m_move = GetComponent<Movement>();
    }



    //----------------------------------------------------------------------
    //! @brief 更新処理
    //!
    //! @param[in] なし
    //!
    //! @return なし
    //----------------------------------------------------------------------
    void Update()
    {
        // キー操作 ========================================================
        // 移動
        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");
        Vector3 vec = new Vector3(x, 0, z);
        vec.Normalize();

        m_move.Move(vec);
    }



    //----------------------------------------------------------------------
    //! @brief 移動速度の変更
    //!
    //! @param[in] スピード
    //!
    //! @return なし
    //----------------------------------------------------------------------
    public void SetMoveSpeed(float speed)
    {
        m_move.SetSpeed(speed);
    }



    //----------------------------------------------------------------------
    //! @brief アクションボタンが押されているか
    //!
    //! @param[in] なし
    //!
    //! @return true:押されている false:押されていない
    //----------------------------------------------------------------------
    public bool IsAction()
    {
        return Input.GetKey(m_actionButton);
    }


    // Get ========================================================================
    // Set ========================================================================
};