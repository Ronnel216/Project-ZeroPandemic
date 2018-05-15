//__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/
//! @file   InputFieldManager
//!
//! @brief  InputFieldの管理スクリプト
//!
//! @date   2018/04/09 
//!
//! @author Y.okada
//__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/


using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class InputFieldManager : MonoBehaviour
{

    InputField m_inputField;

    //値保存用
    GameObject m_saveString;
    [SerializeField]
    private RankingManager m_rankingManager;

    // CSVファイル
    private TextAsset csvFile;
    // CSVの中身を入れるリスト
    private List<string[]> csvDatas = new List<string[]>();
    // CSVの行数
    private int height = 0;



    //----------------------------------------------------------------------
    //! @brief Startメソッド
    //!        InputFieldコンポーネントの取得および初期化メソッドの実行
    //! 
    //! @param[in] なし
    //!
    //! @return なし
    //----------------------------------------------------------------------
    void Start()
    {

        m_rankingManager = GameObject.FindGameObjectWithTag("RankingManager").GetComponent<RankingManager>();

        /* Resouces/CSV下のCSV読み込み */
        csvFile = Resources.Load("CSV/kinshi") as TextAsset;
        StringReader reader = new StringReader(csvFile.text);

        while (reader.Peek() > -1)
        {
            string line = reader.ReadLine();
            // リストに入れる
            csvDatas.Add(line.Split(','));
            // 行数加算
            height++;
        }

        m_inputField = GetComponent<InputField>();

        m_saveString = GameObject.FindWithTag("Data");

        InitInputField();
    }

    //----------------------------------------------------------------------
    //! @brief Log出力用メソッド
    //!        入力値を取得して確認後Savestrにセット、初期化する
    //! 
    //! @param[in] なし
    //!
    //! @return なし
    //----------------------------------------------------------------------
    public void InputLogger()
    {

        string inputValue = m_inputField.text;

        if (!CheckString(inputValue))
        {
            InputEndField(m_inputField.text);
        }
        else
        {
            // 値をリセット
            m_inputField.placeholder.GetComponent<Text>().text = "不適切です";

            InitInputField();
        }
    }

    //----------------------------------------------------------------------
    //! @brief InputFieldの初期化用メソッド
    //!        入力値をリセットして、フィールドにフォーカスする
    //! 
    //! @param[in] なし
    //!
    //! @return なし
    //----------------------------------------------------------------------
    private void InitInputField()
    {
        // 値をリセット
        m_inputField.text = "";

        // フォーカス
        m_inputField.ActivateInputField();
    }

    //----------------------------------------------------------------------
    //! @brief InputFieldの入力終了用メソッド
    //!        入力値をセットして、各フラグをセットする
    //! 
    //! @param[in] name
    //!
    //! @return なし
    //----------------------------------------------------------------------

    public void InputEndField(string name)
    {
        // 値をセット
        m_inputField.text = name;

        m_saveString.GetComponent<SaveStr>().SetuserName(name);

        m_rankingManager.SetRankingFlag(true);

        gameObject.SetActive(false);
    }


    //----------------------------------------------------------------------
    //! @brief InputFieldの不適切用語確認メソッド
    //!        入力値を確認して、フィールドにフォーカスする
    //! 
    //! @param[in] text
    //!
    //! @return true(不適切用語が入力された時)　false(不適切用語が入力されなかった時)
    //----------------------------------------------------------------------

    private bool CheckString(string text)
    {
        for (int i = 0; i < height; i++)
        {
            for (int j = 0; j < 2; j++)
            {
                if (text == csvDatas[i][j])
                {
                    return true;
                }
            }
        }
        return false;
    }
}
