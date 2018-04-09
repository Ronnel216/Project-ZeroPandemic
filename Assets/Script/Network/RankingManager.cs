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
    private InputField m_input;
    [SerializeField]
    private GameObject m_inputField;
    [SerializeField]
    private string name;                        // 名前
    [SerializeField]
    private float score;                        // スコア
    private QuickRanking m_ranking;             // ランキング
    private SaveStr sv;
    private List<GameObject> m_nameText;        // 名前テキスト
    private List<GameObject> m_scoreText;       // スコアテキスト
    private bool m_drawFlag;
    private bool m_rankingFlag;

    private int drawRanking = 0;

    // Use this for initialization
    void Start()
    {
        m_nameText = new List<GameObject>();
        m_scoreText = new List<GameObject>();
        m_ranking = GetComponent<QuickRanking>();
        m_ranking.FetchRanking();

        m_inputField.SetActive(false);
        m_input.enabled = false;
        sv = GameObject.FindGameObjectWithTag("Data").GetComponent<SaveStr>();
        m_drawFlag = true;
        m_rankingFlag = false;
        score = sv.GetresultScore();

    }

    // Update is called once per frame
    void Update()
    {
        List<RankingData> rankingData = new List<RankingData>(m_ranking.GetRanking());

        if (drawRanking > 0)
        {
            drawRanking++;
            if (drawRanking >= 30)
            {
                if (Input.anyKeyDown)
                    Application.LoadLevel("TitleScene");
            }
        }


        if (m_rankingFlag)
        {
            if(m_drawFlag)
            {
                name = sv.GetuserName();
                score = sv.GetresultScore();
                m_ranking.SaveRanking(name, score);
                DrawRanking();
                m_drawFlag = false;
            }
            if (rankingData.Count >= m_ranking.count)
            {
                for (int i = 0; i < m_ranking.count; i++)
                {
                    string text = (i + 1).ToString() + "        " + rankingData[i].name;
                    m_nameText[i].GetComponent<Text>().text = text;

                    text = rankingData[i].score.ToString("F");
                    m_scoreText[i].GetComponent<Text>().text = text;

                    if (drawRanking == 0)
                        drawRanking = 1;
                }
            }
        }
        else
        {
            if (rankingData.Count >= m_ranking.count)
            {
                for (int i = 0; i < m_ranking.count; i++)
                {
                    CheckRanking(rankingData[4].score);
                }
            }
        }
    }

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
            tmpObj.transform.localScale = new Vector3(2, 2, 2);
            tmpObj.transform.localPosition = new Vector3(250, i * -150 + 200, 0);

            m_nameText.Add(tmpObj);

            tmpObj = Instantiate(m_scoreTextPrehub);
            tmpObj.transform.parent = m_canvas.transform;
            tmpObj.transform.localScale = new Vector3(2, 2, 2);
            tmpObj.transform.localPosition = new Vector3(1000, i * -150 + 200, 0);

            m_scoreText.Add(tmpObj);
        }
    }

    private void CheckRanking(float rankingscore)
    {   
            if (rankingscore < score)
            {
                m_inputField.SetActive(true);
                m_input.enabled = true;
            }
            else 
            {
                m_rankingFlag = true;
            }
    }

    public void SetRankingFlag(bool flag)
    {
        m_rankingFlag = flag;
    }
}
