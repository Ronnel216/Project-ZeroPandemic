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
    private GameObject m_inputField;            // インプットフィールドオブジェクト
    [SerializeField]
    private GameObject m_rankImagePrehub;       // ランキングイメージPrefab
    [SerializeField]
    private GameObject m_saveStr;


    [SerializeField]
    private string m_name;                      // 名前
    [SerializeField]
    private float m_score;                      // スコア

    [SerializeField]
    private bool m_saveFlag;                    // デバック用セーブフラグ

    //各ランキング用スプライト   
    [SerializeField]
    private Sprite[] rank;

    // スコア基準表
    [SerializeField]
    List<float> ScoreLevels = new List<float>
        {
            40,
            30,
            20,
            10,
        };


    private QuickRanking m_ranking;             // ランキング
    private SaveStr m_sv;                       // テキスト保存スクリプト
    private List<GameObject> m_rankText;        // 順位テキスト
    private List<GameObject> m_nameText;        // 名前テキスト
    private List<GameObject> m_scoreText;       // スコアテキスト
    private List<GameObject> m_rankImage;       // ランキングイメージ
    private bool m_drawFlag;                    // 描画用フラグ
    private bool m_rankingFlag;                 //ランキング用フラグ

    private int drawRanking = 0;

    private string nextScene = "";

    

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
        m_saveStr = GameObject.FindGameObjectWithTag("Data");
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
                Destroy(m_saveStr);
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

        // ランキングにデータが載るか確認する
        if (m_rankingFlag == false)
        {
            CheckTopRank(rankingData);
            return;
        }

        // ランキングにデータを登録する
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

    //----------------------------------------------------------------------
　　//! @brief でばっく用　ランキングを保存するかの判断
　　//!
　　//! @param[in] なし
　　//!
　　//! @return なし
　　//----------------------------------------------------------------------
    void CheckSaveRank(string name, float score)
    {
        if(m_saveFlag)
        {
            m_ranking.SaveRanking(name, score);
        }
    }

    //----------------------------------------------------------------------
　　//! @brief ランキングオブジェクトを設定する処理
　　//!
　　//! @param[in] ランキングデータ配列
　　//!
　　//! @return なし
　　//----------------------------------------------------------------------
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

            m_rankImage[i].GetComponent<Image>().sprite =
                GetRankingSprite(rankingdata[i].score);

            if (drawRanking == 0)
                drawRanking = 1;
        }
    }

    //----------------------------------------------------------------------
　　//! @brief ランキングの画像取得処理
　　//!
　　//! @param[in] スコア
　　//!
　　//! @return なし
　　//----------------------------------------------------------------------
    Sprite GetRankingSprite(float score)
    {
        // スコアの評価を判定する
        int i = 0;
        while (i < ScoreLevels.Count)
        {
            if (score >= ScoreLevels[i])
                return rank[i];
            i++;
        }                
        return rank[i];

    }

    //----------------------------------------------------------------------
　　//! @brief ランキング上位に入っているかの判断
　　//!
　　//! @param[in] ランキングデータ配列
　　//!
　　//! @return なし
　　//----------------------------------------------------------------------
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
            AddScoreText(i);
        }
    }

    //----------------------------------------------------------------------
    //! @brief テキスト、イメ―ジ生成
    //!
    //! @param[in] ランキングの添え字
    //!
    //! @return なし
    //----------------------------------------------------------------------
    void AddScoreText(int index)
    {
        GameObject tmpObj = Instantiate(m_scoreTextPrehub);
        tmpObj.transform.parent = m_canvas.transform;
        tmpObj.transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
        tmpObj.transform.localPosition = new Vector3(30, index * -130 + 120, 0);

        m_rankText.Add(tmpObj);

        tmpObj = Instantiate(m_scoreTextPrehub);
        tmpObj.transform.parent = m_canvas.transform;
        tmpObj.transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
        tmpObj.transform.localPosition = new Vector3(530, index * -130 + 120, 0);

        m_nameText.Add(tmpObj);

        tmpObj = Instantiate(m_scoreTextPrehub);
        tmpObj.transform.parent = m_canvas.transform;
        tmpObj.transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
        tmpObj.transform.localPosition = new Vector3(880, index * -130 + 120, 0);

        m_scoreText.Add(tmpObj);

        tmpObj = Instantiate(m_rankImagePrehub);
        tmpObj.transform.parent = m_canvas.transform;
        tmpObj.transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
        tmpObj.transform.localPosition = new Vector3(-200, index * -135 + 170, 0);

        m_rankImage.Add(tmpObj);

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
                m_rankingFlag = true;
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
