//__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/
//! @file   VirusUIControll
//!
//! @brief  ウイルスUIの管理スクリプト
//!
//! @date   2018/05/03 
//!
//! @author Y.okada
//__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VirusUIControll : MonoBehaviour
{
    //各ウイルスゲージの画像
    public Image VirusLeftImage;

    public Image VirusRightImage;

    public Image MaxVirusLeftImage;

    public Image MaxVirusRightImage;

    [SerializeField]
    VirusAmount virusAmount;
    //----------------------------------------------------------------------
    //! @brief 初期化処理
    //!
    //! @param[in] なし
    //!
    //! @return なし
    //----------------------------------------------------------------------
    void Start()
    {
    }

    //----------------------------------------------------------------------
    //! @brief 更新処理
    //!        画像を徐々に消していく
    //!
    //! @param[in] なし
    //!
    //! @return なし
    //----------------------------------------------------------------------
    void Update()
    {
            MaxVirusLeftImage.fillAmount = virusAmount.GetMaxVirusAmount() * 0.01f;
            MaxVirusRightImage.fillAmount = virusAmount.GetMaxVirusAmount() * 0.01f;
            VirusLeftImage.fillAmount = virusAmount.GetVirusAmount() * 0.01f;
            VirusRightImage.fillAmount = virusAmount.GetVirusAmount() * 0.01f;

    }
}
