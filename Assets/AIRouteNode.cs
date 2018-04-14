using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIRouteNode : MonoBehaviour {

    // ルートの中継点ノード
    public struct Node
    {
        public Vector3 postion;
        public float stayTime;
    }

    [SerializeField]
    float stayTime;

	// Use this for initialization
	void Start () {

    }
	
	// Update is called once per frame
	void Update () {
        // 登録 //
        Destroy(gameObject);
    }

    // ノード値を取り出す
    public static void GetNodeData(GameObject[] route, out Node[] node)
    {
        node = new Node[route.Length];
        for (int i = 0; i < route.Length; i++) {
            node[i].postion = route[i].transform.position;
            node[i].stayTime = route[i].GetComponent<AIRouteNode>().stayTime;
        }
    }
}
