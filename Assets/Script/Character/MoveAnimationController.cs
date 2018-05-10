//__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/
//! @file   MoveAnimationController.cs
//!
//! @brief  移動アニメーション管理
//!
//! @date   日付　2018/04/06
//!
//! @author 制作者名 澤田
//__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveAnimationController : MonoBehaviour {

    [SerializeField]
    private float m_walkMovement;           // 歩き移動量
    [SerializeField]
    private float m_runMovement;            // 走り移動量
    [SerializeField]
    private int m_updateFrame = 5;          // 更新フレーム

    private Animator m_animator;            // Animatorコンポーネント
    private Vector3 m_oldPosition;          // 指定フレーム前の座標
    private int m_frameCount = 0;           // フレームカウント

    //----------------------------------------------------------------------
    //! @brief 初期化処理
    //!
    //! @param[in] なし
    //!
    //! @return なし
    //----------------------------------------------------------------------
    void Start ()
    {
        // コンポーネントの取得
        m_animator = GetComponent<Animator>();
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
        m_frameCount++;

        // 指定フレーム後
        if (m_frameCount > m_updateFrame)
        {
            // 前回からの移動量を計算
            float move = (m_oldPosition - transform.position).magnitude;

            // 移動量からアニメーションを変更
            int step = 0;
            // 歩く
            if (move > m_walkMovement) step = 1;
            // 走る
            if (move > m_runMovement) step = 2;

            // アニメーション設定
            m_animator.SetInteger("Move", step);

            // 座標保存
            m_oldPosition = transform.position;
            m_frameCount = 0;
        }
	}
}
