using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaDebug : MonoBehaviour
{
    [SerializeField]
    private bool m_view = true;
    [SerializeField]
    private GameObject m_areaDebugObj;
    [SerializeField]
    private Color m_areaColor = Color.green;

    // Use this for initialization
    void Start()
    {
        if (m_view == false) return;

        GameObject obj = GameObject.Find("Player");
        Transform areaDebugObj = obj.transform.Find(m_areaDebugObj.name + "(Clone)");
        Virus virus = obj.GetComponent<Virus>();

        if (areaDebugObj == null && virus.NoneAbilityActor == false)
        {
            // 感染者にデバッグ表示
            GameObject debugObj = Instantiate(m_areaDebugObj);
            debugObj.transform.parent = obj.transform;
            debugObj.transform.localPosition = (obj.transform.localRotation) * -debugObj.transform.position;
            debugObj.transform.localPosition += Vector3.up * 0.15f;
            debugObj.GetComponent<MeshRenderer>().material.color = m_areaColor;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (m_view) ViewDebug();
        else DeleteDebug();
    }

    private void ViewDebug()
    {
        HashSet<GameObject> infectedPerson = WorldViewer.GetAllObjects("InfectedActor");

        // 感染者
        foreach (GameObject obj in infectedPerson)
        {
            Transform areaDebugObj = obj.transform.Find(m_areaDebugObj.name + "(Clone)");
            Virus virus = obj.GetComponent<Virus>();

            if (areaDebugObj == null && virus.NoneAbilityActor == false)
            {
                // 感染者にデバッグ表示
                GameObject debugObj = Instantiate(m_areaDebugObj);
                debugObj.transform.parent = obj.transform;
                debugObj.transform.localPosition = (obj.transform.localRotation) * -debugObj.transform.position;
                debugObj.GetComponent<MeshRenderer>().material.color = m_areaColor;
            }
            if (areaDebugObj && virus.NoneAbilityActor)
            {
                Destroy(areaDebugObj.gameObject);
            }
        }
    }

    private void DeleteDebug()
    {
        GameObject player = GameObject.Find("Player");
        Transform playerObj = player.transform.Find(m_areaDebugObj.name + "(Clone)");
        if (playerObj) Destroy(playerObj.gameObject);

        HashSet<GameObject> infectedPerson = WorldViewer.GetAllObjects("InfectedActor");

        foreach (GameObject obj in infectedPerson)
        {
            Transform thisObj = obj.transform.Find(m_areaDebugObj.name + "(Clone)");
            if (thisObj) Destroy(thisObj.gameObject);
        }
    }
}