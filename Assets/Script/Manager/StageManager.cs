//__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/
//! @file   StageManager.cs
//!
//! @brief  ステージ管理
//!
//! @date   日付　2018/04/09
//!
//! @author 制作者名 澤田
//__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class StageManager : MonoBehaviour {

    [SerializeField]
    private GameManager m_gameManager;                              // ゲームマネージャーコンポーネント
    [SerializeField]
    private GameObject m_player;                                    // プレイヤーオブジェクト
    [SerializeField]
    private GameObject m_mainCamera;                                // カメラオブジェクト
    [SerializeField]
    private Vector3 m_cameraMove = new Vector3(7.0f, 0.0f, 0.0f);   // カメラ移動量
    [SerializeField]
    private float m_migrationTime = 1.0f;                           // ステージ移動時間
    [SerializeField]
    private GameObject[] m_stage;                                   // ステージオブジェクト

    private GameObject m_nowStage;                                  // 現在のステージ
    private GameObject m_nextStage;                                 // 次のステージ


    private int m_stageNum = 0;                                     // 現在のステージ番号
    public int StageNum
    {
        get { return m_stageNum + 1; }
    }

    private float m_time;                                           // ラープ用時間
    private Vector3 m_startPos;                                     // ラープ用初期座標
    private Vector3 m_endPos;                                       // ラープ用目的座標
    private bool m_migrationStage = true;                           // ステージ移動が完了したか
    public bool MigrationStage
    {
        get { return m_migrationStage; }
    }

    private bool m_allClear = false;                                // 全ステージクリア
    public bool AllClear
    {
        get { return m_allClear; }
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
        m_nowStage = m_stage[m_stageNum];
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
        if (m_migrationStage)
        {
            // 現在のステージをクリアしている
            if (m_gameManager.IsClear())
                NextStage();
        }
        else
        {
            CameraMove();
        }
	}



    //----------------------------------------------------------------------
    //! @brief 次ステージを呼び出す
    //!
    //! @param[in] なし
    //!
    //! @return なし
    //----------------------------------------------------------------------
    public void NextStage()
    {
        m_allClear = m_stage.Length <= m_stageNum + 1;
        // 全ステージクリア
        if (m_allClear) return;

        Vector3 pos = m_nowStage.transform.position;
        pos += m_cameraMove;
        m_stageNum++;

        // 次ステージの生成
        m_nextStage = Instantiate(m_stage[m_stageNum], pos, Quaternion.identity);
        m_nextStage.transform.Find("LandShape").GetComponent<NavMeshSurface>().BuildNavMesh();

        // 現ステージの市民を削除
        Destroy(m_nowStage.transform.Find("Actors").gameObject);

        // 移動開始
        m_startPos = m_mainCamera.transform.position;
        m_endPos = m_startPos + Quaternion.Euler(m_mainCamera.transform.eulerAngles) * m_cameraMove;
        m_time = Time.time;
        m_migrationStage = false;     
    }



    //----------------------------------------------------------------------
    //! @brief カメラの移動
    //!
    //! @param[in] なし
    //!
    //! @return なし
    //----------------------------------------------------------------------
    public void CameraMove()
    {
        float step = (Time.time - m_time) / m_migrationTime;

        if (step <= 1.0f)
        {
            if (m_nextStage == null)
            {
                m_migrationStage = true;
                return;
            }

            m_mainCamera.transform.position = Vector3.Lerp(m_startPos, m_endPos, step);
        }
        else
        {
            m_migrationStage = true;
            MigrationCompletion();
        }
    }



    //----------------------------------------------------------------------
    //! @brief 移動完了
    //!
    //! @param[in] なし
    //!
    //! @return なし
    //----------------------------------------------------------------------
    public void MigrationCompletion()
    {
        // ステージの入れ替え
        Destroy(m_nowStage);
        m_nowStage = m_nextStage;

        // プレイヤー移動
        m_player.transform.position = m_nowStage.transform.position;
    }
}
