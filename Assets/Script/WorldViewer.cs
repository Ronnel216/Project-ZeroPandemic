using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldViewer : MonoBehaviour {

    // 登録オブジェクト
    static Dictionary<string, HashSet<GameObject>> objList;

    // 値の初期化
    void Awake()
    {
        objList = new Dictionary<string, HashSet<GameObject>>();
        objList.Clear();
    }

    // Use this for initialization
    void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    // オブジェクトの登録
    static public void Register(GameObject obj)
    {
        // キーを決定する
        string key = CheckArea(obj);

        // キーが存在しない時に生成する
        if (objList.ContainsKey(key) == false)
            objList.Add(key, new HashSet<GameObject>());

        // 登録
        objList[key].Add(obj);
    }

    // オブジェクトの登録解除
    static public void RemoveObject(GameObject obj)
    {
        // キーを探す
        string key = CheckArea(obj);

        // 登録されていないオブジェクト
        if (objList.ContainsKey(key) == false)
        {
            Debug.Log("登録されていないオブジェクト");
            Debug.Break();
        }

        // まだ書いてないよ
        //objList[key].find
    }

    // オブジェクトを取得する
    static public HashSet<GameObject> GetAllObjects(string key)
    {
        // 存在しないキーが指定された
        if (objList.ContainsKey(key) == false)
        {
            Debug.Log("存在しないキーが指定された");
            Debug.Break();
            return null;
        }

        // エリアの配列を渡す
        var result = objList[key]; 
        return result;
    }

    // 一番近いオブジェクトを取得する
    static public GameObject GetCloseObjects(string key, Vector3 position)
    {
        // まだ書いてないよ
        return null;
    }

    static public int CountObjects(string key)
    {
        // 存在しないキーが指定された (returnを 0にするか微妙なライン)
        if (objList.ContainsKey(key) == false)
        {
            Debug.Log("存在しないキーが指定された");
            Debug.Break();
            return -1;
        }

        // 数を返す
        return objList[key].Count;
    }

    // エリアを決定する
    static string CheckArea(GameObject obj)
    {
        // まだ仮だよ
        return obj.tag;
    }
}
