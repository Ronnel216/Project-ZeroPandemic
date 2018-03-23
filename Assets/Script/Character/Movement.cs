//__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/
//! @file   Movement.cs
//!
//! @brief  移動コンポーネント
//!
//! @date   日付　2018/03/17
//!
//! @author 制作者名 澤田
//__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour {

    [SerializeField]
    private float m_speed = 0.1f;               // 移動速度          
    
    [SerializeField]
    private bool m_lockDirection = false;       // 進行方向を向くか

    private Rigidbody m_rigidBody;              // 物理

    //----------------------------------------------------------------------
    //! @brief 初期化処理
    //!
    //! @param[in] なし
    //!
    //! @return なし
    //----------------------------------------------------------------------
    void Start ()
    {
        m_rigidBody = GetComponent<Rigidbody>();
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
    //! @brief オブジェクトの移動
    //!
    //! @param[in] 移動量
    //!
    //! @return なし
    //----------------------------------------------------------------------
    public void Move(Vector3 vec)
    {
        m_rigidBody.velocity = vec * m_speed;

        // 進行方向を向かせる
        if (m_lockDirection == false)
            Direction(vec);
    }



    //----------------------------------------------------------------------
    //! @brief 進行方向を向く
    //!
    //! @param[in] 進行方向ベクトル
    //!
    //! @return なし
    //----------------------------------------------------------------------
    public void Direction(Vector3 vec)
    {
        if (vec.magnitude > 0.01f)
            transform.rotation = Quaternion.LookRotation(-vec);
    }



    //----------------------------------------------------------------------
    //! @brief オブジェクトの回転
    //!
    //! @param[in] 回転量(度) 
    //!
    //! @return なし
    //----------------------------------------------------------------------
    public void Rotation(Vector3 rot)
    {
        transform.Rotate(rot);
    }



    // Get ========================================================================
    public float GetSpeed() { return m_speed; }
    public bool GetLockDirection() { return m_lockDirection; }
    // Set ========================================================================
    public void SetSpeed(float speed) { m_speed = speed; }
    public void SetLockDirection(bool lockDirection) { m_lockDirection = lockDirection; }
}
