using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputFieldManager : MonoBehaviour
{

    InputField m_inputField;

    GameObject m_saveString;
    [SerializeField]
    private RankingManager m_rankingManager;


    /// <summary>
    /// Startメソッド
    /// InputFieldコンポーネントの取得および初期化メソッドの実行
    /// </summary>
    void Start()
    {

        m_inputField = GetComponent<InputField>();

        m_saveString = GameObject.FindWithTag("Data");

        InitInputField();

        //inputField.interactable = false;
    }



    /// <summary>
    /// Log出力用メソッド
    /// 入力値を取得してLogに出力し、初期化
    /// </summary>
    public void InputLogger()
    {
        m_rankingManager = GameObject.FindGameObjectWithTag("RankingManager").GetComponent<RankingManager>();

        string inputValue = m_inputField.text;

        // 値をセット
        m_inputField.text = inputValue;

        m_saveString.GetComponent<SaveStr>().SetuserName(inputValue);

        m_rankingManager.SetRankingFlag(true);

        gameObject.SetActive(false);

        Debug.Log(inputValue);

        //InitInputField();
    }



    /// <summary>
    /// InputFieldの初期化用メソッド
    /// 入力値をリセットして、フィールドにフォーカスする
    /// </summary>


    void InitInputField()
    {

        // 値をリセット
        m_inputField.text = "名前を入力してね";

        // フォーカス
        m_inputField.ActivateInputField();
    }

}
