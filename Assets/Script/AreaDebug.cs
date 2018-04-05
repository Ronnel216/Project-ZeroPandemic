using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaDebug : MonoBehaviour
{

    private GameObject[] m_infectedPerson;
    [SerializeField]
    private bool m_view = true;
    [SerializeField]
    private GameObject m_areaDebugObj;
    [SerializeField]
    private Color m_areaColor = Color.green;

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        // 感染者を探す
        m_infectedPerson = GameObject.FindGameObjectsWithTag("InfectionArea");
        if (m_infectedPerson.Length == 0) return;

        if (m_view) ViewDebug(m_infectedPerson);
        else DeleteDebug(m_infectedPerson);

    }

    private void ViewDebug(GameObject[] infectedPerson)
    {
        // 新しい感染者を探す
        foreach (GameObject obj in infectedPerson)
        {
            Transform thisObj = obj.transform.Find(m_areaDebugObj.name + "(Clone)");
            if (thisObj == null)
            {
                // 新しい感染者にデバッグ表示
                GameObject debugObj = Instantiate(m_areaDebugObj);
                debugObj.transform.parent = obj.transform;
                debugObj.transform.localPosition = obj.transform.localPosition;//(obj.transform.localRotation) * -debugObj.transform.position;
                debugObj.GetComponent<MeshRenderer>().material.color = m_areaColor;
            }
        }
    }

    private void DeleteDebug(GameObject[] infectedPerson)
    {
        foreach (GameObject obj in infectedPerson)
        {
            Transform thisObj = obj.transform.Find(m_areaDebugObj.name + "(Clone)");
            if (thisObj) Destroy(thisObj.gameObject);
        }
    }
}