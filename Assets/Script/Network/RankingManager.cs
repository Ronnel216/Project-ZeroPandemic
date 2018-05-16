//__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/
//! @file   RankingManager
//!
//! @brief  RankingManagerの管理スクリプト
//!
//! @date   2018/04/07 
//!
//! @author Y.okada
//__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RankingManager : MonoBehaviour
{
    [SerializeField]
    private GameObject m_canvas;                // 描画キャンバス 
    [SerializeField]
    private GameObject m_scoreTextPrehub;       // スコアテキストPrefab
    [SerializeField]
    private InputField m_input;                 // インプットフィールド
    [SerializeField]
    private GameObject m_inputField;            // 
    [SerializeField]
    private GameObject m_rankImagePrehub;       // ランキングイメージPrefab

    [SerializeField]
    private string m_name;                      // 名前
    [SerializeField]
    private float m_score;                      // スコア

    [SerializeField]
    private bool m_saveFlag;                    // デバック用セーブフラグ

    //各ランキング用スプライト
    

    [SerializeField]
    private Sprite[] rank;

    private QuickRanking m_ranking;             // ランキング
    private SaveStr m_sv;                       // テキスト保存スクリプト
    private List<GameObject> m_rankText;        // 順位テキスト
    private List<GameObject> m_nameText;        // 名前テキスト
    private List<GameObject> m_scoreText;       // スコアテキスト
    private List<GameObject> m_rankImage;       // ランキングイメージ
    private bool m_drawFlag;                    // 描画用フラグ
    private bool m_rankingFlag;                 //ランキング用フラグ

    private int drawRanking = 0;

    

    // Use this for initialization
    //----------------------------------------------------------------------
    //! @brief Startメソッド
    //!      　各値の初期化
    //!
    //! @param[in]なし
    //!
    //! @return なし
    //----------------------------------------------------------------------
    void Start()
    {
        m_rankText = new List<GameObject>();
        m_nameText = new List<GameObject>();
        m_scoreText = new List<GameObject>();
        m_rankImage = new List<GameObject>();
        m_ranking = GetComponent<QuickRanking>();
        m_ranking.FetchRanking();

        m_inputField.SetActive(false);
        m_input.enabled = false;
        m_sv = GameObject.FindGameObjectWithTag("Data").GetComponent<SaveStr>();
        m_drawFlag = true;
        m_rankingFlag = false;
        m_score = m_sv.GetResultScore();

    }

    // Update is called once per frame
    //----------------------------------------------------------------------
    //! @brief Updateメソッド
    //!
    //! @param[in]なし
    //!
    //! @return なし
    //----------------------------------------------------------------------
    void Update()
    {

        if (drawRanking > 0)
        {
            drawRanking++;
            if (drawRanking >= 30)
            {
                if (Input.anyKeyDown)
                    Application.LoadLevel("TitleScene");
            }
        }

        UpdateRanking();
        
    }

    //----------------------------------------------------------------------
    //! @brief ランキングの更新処理
    //!
    //! @param[in] なし
    //!
    //! @return なし
    //----------------------------------------------------------------------
    private void UpdateRanking()
    {
        //動的にランキングデータの取得
        List<RankingData> rankingData = new List<RankingData>(m_ranking.GetRanking());


        if (m_rankingFlag == false)
        {
            if (m_drawFlag)
            {
                m_name = m_sv.GetUserName();
                m_score = m_sv.GetResultScore();
                CheckSaveRank(m_name, m_score);
                DrawRanking();
                m_drawFlag = false; 
            }


            if (rankingData.Count >= m_ranking.count)
            {
                RankObjSetting(rankingData);
            }

        }
        else
        {
            CheckTopRank(rankingData);
        }
    }

    void CheckSaveRank(string name, float score)
    {
        if(m_saveFlag)
        {
            m_ranking.SaveRanking(name, score);
        }
    }

    void RankObjSetting(List<RankingData> rankingdata)
    {
        for (int i = 0; i < m_ranking.count; i++)
        {
            string text = (i + 1).ToString();
            m_rankText[i].GetComponent<Text>().text = text;

            text = rankingdata[i].name;
            m_nameText[i].GetComponent<Text>().text = text;

            text = rankingdata[i].score.ToString("F");
            m_scoreText[i].GetComponent<Text>().text = text;

            if (rankingdata[i].score >= 40)
            {
                m_rankImage[i].GetComponent<Image>().sprite = rank[0];
            }
            else if (rankingdata[i].score >= 30)
            {
                m_rankImage[i].GetComponent<Image>().sprite = rank[1];
            }
            else if (rankingdata[i].score >= 20)
            {
                m_rankImage[i].GetComponent<Image>().sprite = rank[2];
            }
            else if (rankingdata[i].score >= 10)
            {
                m_rankImage[i].GetComponent<Image>().sprite = rank[3];
            }
            else if (rankingdata[i].score >= 0)
            {
                m_rankImage[i].GetComponent<Image>().sprite = rank[4];
            }
            if (drawRanking == 0)
                drawRanking = 1;
        }
    }

    void CheckTopRank(List<RankingData> rankingdata)
    {
        if (rankingdata.Count >= m_ranking.count)
        {
            for (int i = 0; i < m_ranking.count; i++)
            {
                CheckRanking(rankingdata[4].score);
            }
        }
    }

　　//----------------------------------------------------------------------
　　//! @brief ランキングの描画処理
　　//!
　　//! @param[in] なし
　　//!
　　//! @return なし
　　//----------------------------------------------------------------------
    public void DrawRanking()
    {
        // 今までのランキング情報の取得
        m_ranking = GetComponent<QuickRanking>();
        m_ranking.FetchRanking();

        // スコアテキストの生成 =========================================================-
        for (int i = 0; i < m_ranking.count; i++)
        {
            GameObject tmpObj = Instantiate(m_scoreTextPrehub);
            tmpObj.transform.parent = m_canvas.transform;
            tmpObj.transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
            tmpObj.transform.localPosition = new Vector3(100, i * -150 + 200, 0);

            m_rankText.Add(tmpObj);

            tmpObj = Instantiate(m_scoreTextPrehub);
            tmpObj.transform.parent = m_canvas.transform;
            tmpObj.transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
            tmpObj.transform.localPosition = new Vector3(500, i * -150 + 200, 0);

            m_nameText.Add(tmpObj);

            tmpObj = Instantiate(m_scoreTextPrehub);
            tmpObj.transform.parent = m_canvas.transform;
            tmpObj.transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
            tmpObj.transform.localPosition = new Vector3(1000, i * -150 + 200, 0);

            m_scoreText.Add(tmpObj);

            tmpObj = Instantiate(m_rankImagePrehub);
            tmpObj.transform.parent = m_canvas.transform;
            tmpObj.transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
            tmpObj.transform.localPosition = new Vector3(-200, i * -155 + 250, 0);

            m_rankImage.Add(tmpObj);
        }
    }

    //----------------------------------------------------------------------
    //! @brief ランキングのチェック処理
    //!
    //! @param[in] なし
    //!
    //! @return なし
    //----------------------------------------------------------------------
    private void CheckRanking(float rankingscore)
    {   
            if (rankingscore < m_score)
            {
                m_inputField.SetActive(true);
                m_input.enabled = true;
            }
            else 
            {
                m_rankingFlag = true;
            }
    }

    //----------------------------------------------------------------------
    //! @brief ランキングのフラグをセットする処理
    //!
    //! @param[in] flag
    //!
    //! @return なし
    //----------------------------------------------------------------------
    public void SetRankingFlag(bool flag)
    {
        m_rankingFlag = flag;
    }
}
