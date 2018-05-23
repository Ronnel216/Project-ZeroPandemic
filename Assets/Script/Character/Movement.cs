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
using UnityEngine.AI;

public class Movement : MonoBehaviour {

    [SerializeField]
    private float m_maxSpeed = 5.0f;

    [SerializeField]
    private float m_speed = 0.1f;               // 移動速度    
    private Vector3 m_velocity;                 // 現在の移動成分 
    private bool m_isFlip;                      // 滑っているか     

    private bool m_lockMove = false;            // 移動できるか
    public bool LockMove
    {
        get { return m_lockMove; }
        set { m_lockMove = value; }
    }

    [SerializeField]
    private bool m_lockDirection = false;       // 進行方向を向くか

    private Rigidbody m_rigidBody;              // 物理

    private NavMeshAgent m_navMeshAgent = null; // ナビメッシュ
    public NavMeshAgent NavMeshAgent
    {
        get { return m_navMeshAgent; }
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
        // コンポーネントの取得
        m_rigidBody = GetComponent<Rigidbody>();

        // ナビメッシュ取得(持ってない場合あり)
        m_navMeshAgent = GetComponent<NavMeshAgent>();
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
        // ロック時移動できない
        if (m_lockMove)
        {
            //// 移動速度を消去
            //veloctiy = Vector3.zero;
        }
        else
        {
            if (m_velocity.magnitude > m_maxSpeed)
                m_velocity = m_velocity.normalized * m_maxSpeed; 
            m_rigidBody.velocity = m_velocity;            
        }
        if (m_isFlip == false) m_velocity = Vector3.zero;
        else m_velocity *= 0.99f;

        // NavMeshを動かす
        SetIsStopped(m_lockMove);
        
        // 速度の反映
        if (GetUseNavMesh())
            m_navMeshAgent.speed = m_speed;
	}

    public void Flip(bool isFlip)
    {

        m_isFlip = isFlip;

        // ナビメッシュエージェントを滑らせる
        if (m_navMeshAgent)
        {
            if (isFlip)
                m_navMeshAgent.autoBraking = false;
            else
                m_navMeshAgent.autoBraking = true;
        }

    }

    private void LateUpdate()
    {

    }

    private void OnCollisionEnter(Collision collision)
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
        float speed = m_speed;

        if (m_isFlip) speed *= 0.1f;

        if (m_lockMove == false)
            m_velocity += vec * speed;

        // 進行方向を向かせる
        if (m_lockDirection == false)
            Direction(-vec);
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



    //----------------------------------------------------------------------
    //! @brief 目的地の設定
    //!
    //! @param[in] 目的地座標 
    //!
    //! @return なし
    //----------------------------------------------------------------------
    public void SetDestination(Vector3 pos)
    {
        if (GetUseNavMesh())
            m_navMeshAgent.SetDestination(pos);
    }



    //----------------------------------------------------------------------
    //! @brief ナビメッシュ移動を止めるか
    //!
    //! @param[in] true:動かす false:止める 
    //!
    //! @return なし
    //----------------------------------------------------------------------
    public void SetIsStopped(bool isStop)
    {
        if (GetUseNavMesh())
            m_navMeshAgent.updatePosition = !isStop;
    }



    //----------------------------------------------------------------------
    //! @brief 目的地の削除
    //!
    //! @param[in] なし 
    //!
    //! @return なし
    //----------------------------------------------------------------------
    public void ResetPath()
    {
        if (GetUseNavMesh())
            m_navMeshAgent.ResetPath();
    }



    //----------------------------------------------------------------------
    //! @brief ナビメッシュの使用切り替え
    //!
    //! @param[in] true:使用 false:不使用 
    //!
    //! @return なし
    //----------------------------------------------------------------------
    public void SetUseNavMesh(bool useNavMesh)
    {
        if (m_navMeshAgent)
            m_navMeshAgent.enabled = useNavMesh;
    }


    //----------------------------------------------------------------------
    //! @brief ナビメッシュが使用可能か
    //!
    //! @param[in] true:可能 false:不可能 
    //!
    //! @return なし
    //----------------------------------------------------------------------
    public bool GetUseNavMesh()
    {
        bool result = false;

        if (m_navMeshAgent)
            result = m_navMeshAgent.enabled;

        return result;
    }


    // Get ========================================================================
    public float GetSpeed() { return m_speed; }
    public Vector3 GetMoveDirection() { return m_velocity.normalized; }
    public bool GetLockDirection() { return m_lockDirection; }
    // Set ========================================================================
    public void SetSpeed(float speed) { m_speed = speed; }
    public void SetLockDirection(bool lockDirection) { m_lockDirection = lockDirection; }
}
