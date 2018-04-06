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
    private QuickRanking m_ranking;             // ランキング
    private List<GameObject> m_nameText;        // 名前テキスト
    private List<GameObject> m_scoreText;       // スコアテキスト

    // Use this for initialization
    void Start()
    {
        m_nameText = new List<GameObject>();
        m_scoreText = new List<GameObject>();

        DrawRanking();

        SaveStr sv = GameObject.FindGameObjectWithTag("Data").GetComponent<SaveStr>();
        string name = sv.GetuserName();
        float score = sv.GetresultScore();

        m_ranking.SaveRanking(name, score);
    }

    // Update is called once per frame
    void Update()
    {
        List<RankingData> rankingData = new List<RankingData>(m_ranking.GetRanking());

        if (rankingData.Count >= m_ranking.count)
        {
            for (int i = 0; i < m_ranking.count; i++)
            {
                string text = (i + 1).ToString() + "." + rankingData[i].name;
                m_nameText[i].GetComponent<Text>().text = text;

                text = rankingData[i].score.ToString();
                m_scoreText[i].GetComponent<Text>().text = text;
            }
        }
    }

    private void DrawRanking()
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
            tmpObj.transform.localPosition = new Vector3(175, i * -150 + 600, 0);

            m_nameText.Add(tmpObj);

            tmpObj = Instantiate(m_scoreTextPrehub);
            tmpObj.transform.parent = m_canvas.transform;
            tmpObj.transform.localScale = new Vector3(2, 2, 2);
            tmpObj.transform.localPosition = new Vector3(900, i * -150 + 600, 0);

            m_scoreText.Add(tmpObj);
        }
    }
}
