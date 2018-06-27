//__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/
//! @file   MiniMap.cs
//!
//! @brief  ミニマップ
//!
//! @date   日付　2018/06/01
//!
//! @author 制作者名 澤田
//__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MiniMap : MonoBehaviour
{
    [SerializeField]
    private StageManager m_stageManager = null;
    [SerializeField]
    private Image m_actorImage = null;

    [SerializeField]
    private Color m_playerColor = Color.yellow;
    [SerializeField]
    private Color m_citizenColor = Color.red;
    [SerializeField]
    private Color m_zombieColor = Color.green;

    private List<GameObject> m_actors;
    private List<Image> m_actorsImage;

    //----------------------------------------------------------------------
    //! @brief 初期化処理
    //!
    //! @param[in] なし
    //!
    //! @return なし
    //----------------------------------------------------------------------
    void Start()
    {
        m_actors = new List<GameObject>();
        m_actorsImage = new List<Image>();

        ChangeColor();
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
        // ステージ移動中は更新しない
        if (m_stageManager.MigrationStage == false) return;

        // キャラクターの取得
        GetActor();

        // 画像更新
        CheckActor();
        // 座標更新
        SyncPosition();
        // 色更新
        ChangeColor();
    }



    //----------------------------------------------------------------------
    //! @brief 色の変更
    //!
    //! @param[in] なし
    //!
    //! @return なし
    //----------------------------------------------------------------------
    private void ChangeColor()
    {
        for (int i = 0; i < m_actors.Count; i++)
        {
            if (m_actors[i].name == "Player")
                m_actorsImage[i].color = m_playerColor;
            else
            {
                if (m_actors[i].tag == "InfectedActor")
                    m_actorsImage[i].color = m_zombieColor;
                else
                    m_actorsImage[i].color = m_citizenColor;
            }
        }
    }



    //----------------------------------------------------------------------
    //! @brief キャラクター更新
    //!
    //! @param[in] なし
    //!
    //! @return なし
    //----------------------------------------------------------------------
    private void CheckActor()
    {
        int num = m_actorsImage.Count - m_actors.Count;
        if (num == 0) return;

        // 余っている画像を消す
        if (num > 0) DeleteActorImage(num);
        // 足りない分画像追加
        else AddActorImage(num * -1);
    }



    //----------------------------------------------------------------------
    //! @brief 座標更新
    //!
    //! @param[in] なし
    //!
    //! @return なし
    //----------------------------------------------------------------------
    private void SyncPosition()
    {
        int i = 0;
        foreach (var actor in m_actors)
        {
            Vector3 pos = actor.transform.position;

            float offsetX = m_stageManager.StagePos.x;
            m_actorsImage[i].transform.localPosition = new Vector3(pos.x - offsetX, pos.z, 0);
            i++;
        }
    }



    //----------------------------------------------------------------------
    //! @brief キャラクター取得
    //!
    //! @param[in] なし
    //!
    //! @return なし
    //----------------------------------------------------------------------
    private void GetActor()
    {
        m_actors.Clear();

        // 市民の取得
        HashSet<GameObject> actors = WorldViewer.GetAllObjects("Player");
        foreach (var actor in actors)
            m_actors.Add(actor);

        // 市民の取得
        actors = WorldViewer.GetAllObjects("Actor");
        foreach (var actor in actors)
            m_actors.Add(actor);

        // ゾンビの取得
        actors = WorldViewer.GetAllObjects("InfectedActor");
        foreach (var actor in actors)
            m_actors.Add(actor);
    }



    //----------------------------------------------------------------------
    //! @brief 画像追加
    //!
    //! @param[in] なし
    //!
    //! @return なし
    //----------------------------------------------------------------------
    private void AddActorImage(int num)
    {
        for (int i = 0; i < num; i++)
        {
            Image image = Instantiate(m_actorImage);
            image.transform.parent = transform;
            m_actorsImage.Add(image);
        }
    }



    //----------------------------------------------------------------------
    //! @brief 画像削除
    //!
    //! @param[in] なし
    //!
    //! @return なし
    //----------------------------------------------------------------------
    private void DeleteActorImage(int num)
    {
        for (int i = 0; i < num; i++)
        {
            Destroy(m_actorsImage[0]);
            m_actorsImage.RemoveAt(0);
        }
    }
}