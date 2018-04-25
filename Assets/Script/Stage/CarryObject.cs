//__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/
//! @file   CarryObject.cs
//!
//! @brief  運べるオブジェクト
//!
//! @date   日付　2018/04/24
//!
//! @author 制作者名 澤田
//__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarryObject : MonoBehaviour {

    [SerializeField]
    private int m_requiredNum = 5;                                  // 持ち上げるのに必要な人数
    public int RequiredNum
    {
        get { return m_requiredNum; }
    }

    private int m_carryZombieNum;                                    // 持ち上げようとしているゾンビ
    public int CarryZombieNum
    {
        get { return m_carryZombieNum; }
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
        m_carryZombieNum = 0;

    }



    //----------------------------------------------------------------------
    //! @brief ヒット時処理
    //!
    //! @param[in] なし
    //!
    //! @return なし
    //----------------------------------------------------------------------

    public void OnTriggerStay(Collider other)
    {
        GameObject hitObj = other.gameObject;

        // 触れたのがゾンビだったらカウント
        if (hitObj.name != "Player" && hitObj.tag == "InfectedActor")
            m_carryZombieNum++;
    }
}
