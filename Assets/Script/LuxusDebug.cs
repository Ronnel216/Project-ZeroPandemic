using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LuxusDebug : MonoBehaviour
{
    private GameObject m_player;
    [SerializeField]
    private bool m_view = true;
    [SerializeField]
    private GameObject m_luxusDebugObj;

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        // 感染者を探す
        m_player = GameObject.FindGameObjectWithTag("Player");

        if (m_view) ViewDebug(m_player);

    }

    private void ViewDebug(GameObject obj)
    {
        // 新しい感染者にデバッグ表示
        GameObject debugObj = Instantiate(m_luxusDebugObj);
        debugObj.transform.parent = obj.transform;
        debugObj.transform.localPosition = (obj.transform.localRotation) * -debugObj.transform.position;
        m_view = false;
    }

}
