//__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/
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
    private Movement m_move;                                        // 移動コンポーネント

    [SerializeField]
    private GameManager m_gameManager;                              // ゲームマネージャーコンポーネント
    [SerializeField]
    private KeyCode m_actionButton = KeyCode.Z;                     // アクションボタン設定

    private VirusAmount m_virusAmount;                              // ウィルス量
    private ExpansionControl m_expansion;                           // 拡張範囲

    private GameObject m_carryObject;                               // 運ぶオブジェクト
    [SerializeField]
    private float m_throwPower = 300.0f;                            // 投げる力
    private float m_carryUpPos = 0.0f;                              // 持ち上げ量

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
        m_expansion = GetComponent<ExpansionControl>();
        m_virusAmount = GetComponent<VirusAmount>();
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

        // ボタンを押している間広がる
        if (IsAction())
        {
            m_expansion.Expand();
            m_move.Move(Vector3.zero);

            // 持っているオブジェクトを投げる
            if (m_carryObject)
                Throw();
        }
        else
        {
            m_expansion.Shrinking();
            m_move.Move(vec);
            Carrying();
        }
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
    //! @brief ウィルスコントロール状態か
    //!
    //! @param[in] なし
    //!
    //! @return true:Yes false:No
    //----------------------------------------------------------------------
    public bool IsAction()
    {
        bool result = false;

        bool key = Input.GetKey(m_actionButton);
        bool con = Input.GetButton("Button A");
        bool virus = m_virusAmount.GetVirusAmount() > 0;

        // アクションキーが押されていてウィルスがある場合true
        result = (key | con) & virus;
        return result;
    }



    //----------------------------------------------------------------------
    //! @brief ヒット時処理
    //!
    //! @param[in] なし
    //!
    //! @return なし
    //----------------------------------------------------------------------
    private void OnCollisionStay(Collision collision)
    {
        Carry(collision.gameObject);
       
    }



    //----------------------------------------------------------------------
    //! @brief 持ち上げる
    //!
    //! @param[in] なし
    //!
    //! @return なし
    //----------------------------------------------------------------------
    private void Carry(GameObject obj)
    {
        // 現在持ち運んでいるオブジェクトがないか
        if (m_carryObject) return;
        // 持ち運べるオブジェクトか
        if (obj.tag != "CarryObj") return;

        CarryObject carryObject = obj.GetComponent<CarryObject>();
        Debug.Log(carryObject.CarryZombieNum);
        // 持ち運ぶために必要なゾンビ数を超えている
        if (carryObject.CarryZombieNum + 1 >= carryObject.RequiredNum)
        {
            m_carryObject = obj;
            m_carryUpPos = 3.0f + m_carryObject.transform.localScale.y / 2.0f;
            m_carryObject.transform.position = transform.position + new Vector3(0, m_carryUpPos, 0);
        }
    }



    //----------------------------------------------------------------------
    //! @brief 運ぶ
    //!
    //! @param[in] なし
    //!
    //! @return なし
    //----------------------------------------------------------------------
    private void Carrying()
    {
        if (m_carryObject == null) return;

        m_carryObject.transform.position = transform.position + new Vector3(0, m_carryUpPos, 0);
    }



    //----------------------------------------------------------------------
    //! @brief 投げる
    //!
    //! @param[in] なし
    //!
    //! @return なし
    //----------------------------------------------------------------------
    private void Throw()
    {
        Rigidbody rigid = m_carryObject.GetComponent<Rigidbody>();
        Vector3 vec = transform.forward * m_throwPower;
        rigid.AddForce(vec, ForceMode.Impulse);
        m_carryObject = null;
    }
};
