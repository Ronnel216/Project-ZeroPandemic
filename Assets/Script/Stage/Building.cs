//__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/
//! @file   Building.cs
//!
//! @brief  建物
//!
//! @date   日付　2018/04/23
//!
//! @author 制作者名 澤田
//__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour {

    [SerializeField]
    private int m_requiredNum = 5;                                  // 乗り込みに必要な人数
    [SerializeField]                                            　   
    private int m_acquisitionNum = 5;                           　  // 建物感染時に獲得できる人数
    [SerializeField]
    private float m_infectionTime = 5.0f;                           // 建物感染に掛かる時間
    [SerializeField]
    private Color m_maxInfectionColor = Color.magenta;              // 感染具合最大時のモデル色

    // 感染状況
    enum INFECTION_STATE
    {
        E_NONE,
        E_INFECTION,
        E_INFECTED
    }
    private INFECTION_STATE m_state = INFECTION_STATE.E_NONE;       // 感染が開始しているか
    private List<GameObject> m_enterZombie;                         // 乗り込んだゾンビ               
    private float m_time = 0.0f;                                    // 経過時間
    private MeshRenderer m_modelMesh;                               // モデルのSkinnedMeshRendererrコンポーネント
    private Color[] m_defaultColor;                             　  // 初期マテリアル色



    //----------------------------------------------------------------------
    //! @brief 初期化処理
    //!
    //! @param[in] なし
    //!
    //! @return なし
    //----------------------------------------------------------------------
    void Start ()
    {
        m_enterZombie = new List<GameObject>();

        // モデルからMeshRendererコンポーネントを探す
        m_modelMesh = gameObject.GetComponentInChildren<MeshRenderer>();
        // 色を取得
        int size = m_modelMesh.materials.Length;
        m_defaultColor = new Color[size];
        for (int i = 0; i < size; i++)
            m_defaultColor[i] = m_modelMesh.materials[i].color;
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
        switch (m_state)
        {
            case INFECTION_STATE.E_NONE:
                // 乗り込んだゾンビ数が必要数に達した
                if (m_enterZombie.Count == m_requiredNum)
                    BuildingEnter();
                else
                    ResetEnter();
                break;
            case INFECTION_STATE.E_INFECTION:
                m_time += Time.deltaTime;
                UpdateModelCondition();
                Debug.Log("感染中" + m_time.ToString());
                // ゾンビを放つ
                if (m_time >= m_infectionTime)
                {
                    ReleaseZombies();
                }
                break;
            case INFECTION_STATE.E_INFECTED:
                break;
            default:
                break;
        }
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
        // 感染していない
        if (m_state != INFECTION_STATE.E_NONE) return;
        // 既に必要人数に達している
        if (m_enterZombie.Count == m_requiredNum) return;

        GameObject hitObj = other.gameObject;

        // 触れたのがゾンビだったら登録
        if (hitObj.name != "Player" && hitObj.tag == "InfectedActor") 
            m_enterZombie.Add(hitObj);
    }



    //----------------------------------------------------------------------
    //! @brief 建物に乗り込む
    //!
    //! @param[in] なし
    //!
    //! @return なし
    //----------------------------------------------------------------------
    private void BuildingEnter()
    {
        m_state = INFECTION_STATE.E_INFECTION;

        // 乗り込んだゾンビを見えなくする
        for (int i = 0; i < m_requiredNum; i++)
            m_enterZombie[i].SetActive(false);
    }



    //----------------------------------------------------------------------
    //! @brief 登録のリセット
    //!
    //! @param[in] なし
    //!
    //! @return なし
    //----------------------------------------------------------------------
    private void ResetEnter()
    {
        int num = m_enterZombie.Count;

        // 登録を削除しもとに戻す
        for (int i = 0; i < num; i++)
            m_enterZombie[i].SetActive(true);

        m_enterZombie.Clear();
    }



    //----------------------------------------------------------------------
    //! @brief モデルの色変更
    //!
    //! @param[in] なし
    //!
    //! @return なし
    //----------------------------------------------------------------------
    private void UpdateModelCondition()
    {
        // マテリアルの色を変える
        int size = m_modelMesh.materials.Length;
        float step = m_time / m_infectionTime;

        for (int i = 0; i < size; i++)
        {
            Color color = Color.Lerp(m_defaultColor[i], m_maxInfectionColor, step);
            m_modelMesh.materials[i].color = color;
        }

    }



    //----------------------------------------------------------------------
    //! @brief ゾンビ放出
    //!
    //! @param[in] なし
    //!
    //! @return なし
    //----------------------------------------------------------------------
    private void ReleaseZombies()
    {
        m_state = INFECTION_STATE.E_INFECTED;

        GameObject zombie = m_enterZombie[0];

        // 登録情報のリセット
        ResetEnter();

        GameObject original = zombie.GetComponent<Virus>().GetOriginal().gameObject;
        for (int i = 0; i < m_acquisitionNum; i++)
            Instantiate(zombie).GetComponent<Virus>().Infected(original);
    }
}
