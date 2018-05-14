//__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/
//! @file   StartManager
//!
//! @brief  StartManagerの管理スクリプト
//!
//! @date   2018/04/09 
//!
//! @author Y.okada
//__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartManager : MonoBehaviour
{
    [SerializeField]
    private Text countdownText;

    [SerializeField]
    GameManager gameManager;

    // Use this for initialization
    //----------------------------------------------------------------------
    //! @brief Startメソッド
    //!
    //! @param[in]なし
    //!
    //! @return なし
    //----------------------------------------------------------------------
    void Start ()
    {
        countdownText.text = "";
        StartCoroutine(CountdownCoroutine());
    }

    // Update is called once per frame
    //----------------------------------------------------------------------
    //! @brief Updateメソッド
    //!
    //! @param[in]なし
    //!
    //! @return なし
    //----------------------------------------------------------------------
    void Update ()
    {
		
	}

    //----------------------------------------------------------------------
    //! @brief Countdownコルーチン
    //!        指定秒数後に処理を開始
    //!
    //! @param[in]なし
    //!
    //! @return なし
    //----------------------------------------------------------------------
    IEnumerator CountdownCoroutine()
    {
        countdownText.gameObject.SetActive(true);

        countdownText.text = "3";
        yield return new WaitForSeconds(1.25f);
        countdownText.text = "2";
        yield return new WaitForSeconds(1.25f);
        countdownText.text = "1";
        yield return new WaitForSeconds(1.25f);
        countdownText.text = "START";
        yield return new WaitForSeconds(1.25f);
        countdownText.text = "";
        gameManager.StartGame();
        yield return new WaitForSeconds(1.25f);

    }
}
