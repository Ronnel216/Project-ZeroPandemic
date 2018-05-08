using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VirusUIControll : MonoBehaviour
{
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
