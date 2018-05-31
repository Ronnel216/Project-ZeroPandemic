using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIManager : MonoBehaviour {

    // AIの感染者達
    struct InfectedActor
    {
        public GameObject actor;
        public Vector3 target;
    }

    List<InfectedActor> infectedActors;

    public int degugnum;
    public static GameObject[] m_zombies;
    public static GameObject[] m_hunterManufactorys;
    //市民
    GameObject[] m_actors;
    //感染者
    GameObject[] m_infecters;
    int m_cnt = 0;

    //// 経路パスの管理
    //NavMeshPath[,] infectedPath = new NavMeshPath[30, 30];

    //public void SetPath(NavMeshPath path, Vector3 pos)
    //{
    //    float separationWide = 1.0f;

    //    Vector2Int index;
         
    //}

    //public void ResetPath()
    //{
    //    for (int i = 0; i < infectedPath.Length; i++)
    //    {
    //        for (int j = 0; j < infectedPath.Length; j++)
    //        {
    //            infectedPath[i, j].ClearCorners();
    //            infectedPath[i, j] = null;
    //        }
    //    }
    //}

    // Use this for initialization
    void Start()
    {
        infectedActors = new List<InfectedActor>();
        m_hunterManufactorys = GameObject.FindGameObjectsWithTag("HunterManufactory");
    }

    // Update is called once per frame
    void Update()
    {
        degugnum = infectedActors.Count;
    }

    public static GameObject GetCloseManufactory(Vector3 pos, float radius = -0.1f)
    {
        m_hunterManufactorys = GameObject.FindGameObjectsWithTag("HunterManufactory");

        GameObject result = null;
        float distance = radius;

        // 指定範囲内かつ人手が足りていない製作所を探す
        foreach (var manufactory in m_hunterManufactorys)
        {
            HunterManufactory factory = manufactory.GetComponent<HunterManufactory>();
            // 製作に向かう必要がない
            if (factory.IsSupport() == false) continue;

            // 近い製作所を優先
            Vector3 factoryPos = manufactory.transform.position;
            float dis = (factoryPos - pos).magnitude;
            if (dis < distance || distance < 0.0f)
            {
                distance = dis;
                result = manufactory;
            }
        }
        return result;
    }

    public static GameObject GetCloseZombie(Vector3 pos, float radius = -0.1f)
    {
        m_zombies = GameObject.FindGameObjectsWithTag("InfectedActor");

        GameObject result = null;
        float distance = radius;
        foreach (var zombie in m_zombies)
        {
            Vector3 factoryPos = zombie.transform.position;
            float dis = (factoryPos - pos).magnitude;
            if (dis < distance || distance < 0.0f)
            {
                distance = dis;
                result = zombie;
            }
        }
        return result;
    }

    //感染者をパンデミック状態へ移行
    public void StartPandemic()
    {
        int num = WorldViewer.CountObjects("Actor");
        m_actors = new GameObject[num];
        //残っている市民
        WorldViewer.GetAllObjects("Actor").CopyTo(m_actors, 0);

        num = WorldViewer.CountObjects("InfectedActor");
        m_infecters = new GameObject[num];
        //今感染している市民
        WorldViewer.GetAllObjects("InfectedActor").CopyTo(m_infecters);

        //感染者のステートを変更
        foreach (var infecter in m_infecters)
        {
            CitizenAI ai = infecter.GetComponent<CitizenAI>();
            if (ai)
                ai.ChangeState(new PandemicState());
        }
    }
    //感染者の目標にするオブジェクトを取得
    public GameObject GetTarget()
    {
        GameObject obj = null;

        if (m_cnt > m_actors.Length - 1)
            m_cnt = 0;

        obj = m_actors[m_cnt];
        m_cnt++;
        
        return obj; 
    }
}
