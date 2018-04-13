//__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/
//! @file   QuickRanking
//!
//! @brief  QuickRankingの管理スクリプト
//!
//! @date   2018/04/07 
//!
//! @author Y.okada
//__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/


using NCMB;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class QuickRanking : MonoBehaviour
{
    public string rankingClassName = "Ranking";//NCMB側のランキングクラス名//
    public int count = 5;//いくつまでランキングデータを取得するか//
    private List<RankingData> rankingDataList = new List<RankingData>();//取得したランキングデータ//
    public bool IsRankingDataValid { get; private set; }//ランキングデータの取得に成功しているか//

    public int PlayerCount { get; private set; }//いままで何人がスコアを登録したか//

    private string currentObjectid;//自分のスコアのidを一時保存する//

    public static QuickRanking Instance;//シングルトン//

    public Text text;

    public void Awake()
    {
        if (Instance != null)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;

            CheckNCMBValid();
        }
    }

    //----------------------------------------------------------------------
    //! @brief ランキングのソート処理
    //!
    //! @param[in] callback
    //!
    //! @return なし
    //----------------------------------------------------------------------
    public void FetchRanking(UnityAction callback = null)
    {
        if (CheckNCMBValid() == false)
        {
            text.enabled = true;
            StartCoroutine(RankingCoroutine());
            if (callback != null) callback();
            return;
        }

        NCMBQuery<NCMBObject> query = new NCMBQuery<NCMBObject>(rankingClassName);

        //Scoreの値で降順にソート//
        query.OrderByDescending("Score");

        //取得数の設定//
        query.Limit = count;

        query.FindAsync((List<NCMBObject> objList, NCMBException e) =>
        {
            if (e != null)
            {
                //検索失敗時の処理
                IsRankingDataValid = false;
            }
            else
            {
                int num = 1;

                rankingDataList.Clear();

                foreach (NCMBObject obj in objList)
                {
                    rankingDataList.Add(new RankingData(
                         num++,
                         name: obj["Name"] as string,
                         score: (float)Convert.ToDouble(obj["Score"]),
                         objectid: obj.ObjectId

                        ));
                }
                IsRankingDataValid = true;
            }

            if (callback != null) callback();
        });
    }

    //----------------------------------------------------------------------
    //! @brief ランキングの保存処理
    //!
    //! @param[in] name, score, callback
    //!
    //! @return なし
    //----------------------------------------------------------------------
    public void SaveRanking(string name, float score, UnityAction callback = null)
    {
        //スコアがゼロなら登録しない//
        if (CheckNCMBValid() == false || score <= 0.0f)
        {
            if (callback != null) callback();
            return;
        }

        //rankingClassNameに設定したオブジェクトを作る//
        NCMBObject ncmbObject = new NCMBObject(rankingClassName);

        //nameが空だったらNoNameと入れる//
        if (string.IsNullOrEmpty(name)) name = "No Name";

        // オブジェクトに値を設定
        ncmbObject["Name"] = name;
        ncmbObject["Score"] = score;

        // データストアへの登録
        ncmbObject.SaveAsync((NCMBException e) =>
        {
            if (e != null)
            {
                //接続失敗//
                GetRanking();
            }
            else
            {
                //接続成功//
                //保存したオブジェクトidを記録//
                currentObjectid = ncmbObject.ObjectId;
            }

            //ランキングの更新//
            if(callback != null)
            {
                FetchRanking(callback);
            }else
            {
                FetchRanking();
            }

        });
    }

    //----------------------------------------------------------------------
    //! @brief ランキングの取得処理
    //!
    //! @param[in] name, score, callback
    //!
    //! @return なし
    //----------------------------------------------------------------------
    public List<RankingData> GetRanking()
    {
        //すでにStart()でフェッチ済みのデータを渡すだけ//
        return rankingDataList;
    }

    //----------------------------------------------------------------------
    //! @brief ランキングのText設定処理
    //!
    //! @param[in] name, score, callback
    //!
    //! @return なし
    //----------------------------------------------------------------------
    public string GetRankingByText()
    {
        if (IsRankingDataValid)
        {
            string text = string.Empty;

            foreach (RankingData rankingData in rankingDataList)
            {
                string rankNum = string.Format("{0, 2}", rankingData.rankNum);
                string name = string.Format("{0, -10}", rankingData.name);
                string score = string.Format("{0:f}", rankingData.score.ToString());

                Debug.Log(score);
                //さっき保存したスコアがあった場合は赤に着色する//
                if (rankingData.objectid == currentObjectid)
                {
                    text += "<color=white>" + rankNum + ": \t" + name + ": \t" + score + "</color> \n";
                }
                else
                {
                    text += rankNum + ": \t" + name + ": \t" + score + "\n";
                }
            }

            return text;
        }
        else
        {
            return "No Ranking Data";
        }
    }

    //----------------------------------------------------------------------
    //! @brief ランキングのプレイヤカウント処理
    //!
    //! @param[in] name, score, callback
    //!
    //! @return なし
    //----------------------------------------------------------------------
    public void FetchPlayerCount(UnityAction callback = null)
    {
        if (CheckNCMBValid() == false)
        {
            if (callback != null) callback();
            return;
        }

        NCMBQuery<NCMBObject> query = new NCMBQuery<NCMBObject>(rankingClassName);
        query.CountAsync((int count, NCMBException e) => {
            if (e != null)
            {
                //接続失敗//
            }
            else
            {
                //接続成功//
                PlayerCount = count;
            }
            if (callback != null) callback();
        });
    }


    //----------------------------------------------------------------------
    //! @brief Rankingコルーチン
    //!
    //! @param[in]なし
    //!
    //! @return なし
    //----------------------------------------------------------------------
    IEnumerator RankingCoroutine()
    {
        text.text = "ネットワーク接続されていないためランキング表示ができません";
        yield return new WaitForSeconds(4.0f);
        text.enabled = false;
        UnityEngine.SceneManagement.SceneManager.LoadScene("TitleScene");
    }


    //----------------------------------------------------------------------
    //! @brief ランキングの有効か判断する処理
    //!
    //! @param[in] name, score, callback
    //!
    //! @return なし
    //----------------------------------------------------------------------
    private bool CheckNCMBValid()
    {
#if UNITY_WEBGL
            Debug.LogWarning("NCMB SDK はWebGLに対応していません。");
            return false;
#else
        if (Application.internetReachability == NetworkReachability.NotReachable)
        {
            Debug.LogWarning("ネットワーク接続がありません。");
            return false;
        }
        else
        {
            return true;
        }
#endif
    }
}


//__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/
//! @file   RankingData
//!
//! @brief  RankingDataのクラス
//!
//! @date   2018/04/07 
//!
//! @author Y.okada
//__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/
public class RankingData
{
    public readonly int rankNum;//順位（本クラス内でつける）//
    public readonly string name;//プレイヤー名//
    public readonly float score;//点数//
    public readonly string objectid;//NCMBのオブジェクトID//

    //----------------------------------------------------------------------
    //! @brief ランキングデータの登録処理
    //!
    //! @param[in] rankNum, name, score, objectid
    //!
    //! @return なし
    //----------------------------------------------------------------------
    public RankingData(int rankNum, string name, float score, string objectid)
    {
        this.rankNum = rankNum;
        this.name = name;
        this.score = score;
        this.objectid = objectid;
    }
}