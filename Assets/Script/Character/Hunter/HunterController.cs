﻿//__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/
//! @file   HunterController.cs
//!
//! @brief  ハンター
//!
//! @date   日付　2018/04/28
//!
//! @author 制作者名 澤田
//__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class HunterController : MonoBehaviour {

    // ハンター行動状態クラス
    public abstract class HunterState
    {
        // 初期化処理
        public abstract void Enter(HunterController hunter);
        // 更新処理
        public abstract void Update(HunterController hunter);
        // ヒット判定
        public virtual void OnTriggerStay(Collider other, HunterController hunter) { }
        public virtual void OnDestroy() { }

    }

    HunterState m_state;                            // 現在の行動状態
    HunterState m_nextState;                        // 次の行動状態

    private GameObject m_player;                    // プレイヤー
    public GameObject Player
    {
        get { return m_player; }
    }

    [SerializeField]
    private float m_routeChangeSpan;                // ルート変更時間
    public float RouteChangeSpan
    {
        get { return m_routeChangeSpan; }
    }

    private NavMeshAgent m_navMeshAgent;            // ナビメッシュ
    public NavMeshAgent NavMeshAgent
    {
        get { return m_navMeshAgent; }
    }
    private float m_defaultSpeed;                   // 通常状態のスピード

    [SerializeField]
    private int m_restraintNum;                     // 拘束に必要なゾンビ数
    private int m_zombieNum;                        // 近くにいるゾンビ数

    [SerializeField]
    private float m_captureTime = 3.0f;             // 捕獲時間
    public float CaptureTime
    {
        get { return m_captureTime; }
    }
    [SerializeField]
    private float m_decreaseMaxAmount = 10;         // 捕獲時のウィルス最大値減少量          
    public float DecreaseMaxAmount
    {
        get { return m_decreaseMaxAmount; }
    }

    [SerializeField]
    private float m_coolTime;                       // クールタイム
    public float CoolTime
    {
        get { return m_coolTime; }
    }

    [SerializeField]
    private float m_downSpeed = 5.0f;               // 速度減少


    //----------------------------------------------------------------------
    //! @brief 初期化処理
    //!
    //! @param[in] なし
    //!
    //! @return なし
    //----------------------------------------------------------------------
    void Start ()
    {
        m_state = new HunterChaseState();
        m_nextState = m_state;

        m_player = GameObject.Find("Player");

        m_navMeshAgent = GetComponent<NavMeshAgent>();
        m_defaultSpeed = m_navMeshAgent.speed;
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
        // 状態変更
        if (m_nextState != m_state)
        {
            m_state = m_nextState;
            m_state.Enter(this);
        }

        float speed = m_defaultSpeed;

        // 拘束されていたら速度を落とす
        if (m_zombieNum >= m_restraintNum)
            speed /= m_downSpeed;
        // 速度代入
        m_navMeshAgent.speed = speed;

        m_zombieNum = 0;


        m_state.Update(this);
    }



    //----------------------------------------------------------------------
    //! @brief 状態変更
    //!
    //! @param[in] 次の状態クラス
    //!
    //! @return なし
    //----------------------------------------------------------------------
    public void ChangeState(HunterState state)
    {
        this.m_nextState = state;
    }



    //----------------------------------------------------------------------
    //! @brief ヒット時処理
    //!
    //! @param[in] 当たったオブジェクト
    //!
    //! @return なし
    //----------------------------------------------------------------------
    private void OnTriggerStay(Collider other)
    {
        GameObject hitObj = other.gameObject;

        m_state.OnTriggerStay(other, this);

        // 触れたのがゾンビだったらカウント
        if (hitObj.name != "Player" && hitObj.tag == "InfectedActor")
            m_zombieNum++;
    }

    private void OnDestroy()
    {
        m_state.OnDestroy();
    }
}
