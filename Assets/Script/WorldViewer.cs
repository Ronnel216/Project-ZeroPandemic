using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldViewer : MonoBehaviour {

    // 登録オブジェクト
    static Dictionary<string, HashSet<GameObject>> objList = null;

        // 登録キー
    static List<string> keys = new List<string>
    {
        "Player",
        "Actor",
        "InfectedActor"
    };

    // 値の初期化
    void Awake()
    {
        // 存在しないなら生成する
        if (objList == null)
        {
            // 登録オブジェクトの格納先の作成
            objList = new Dictionary<string, HashSet<GameObject>>();
            CreateAllKey();
            return;
        }

        // 登録オブジェクトの初期化
        foreach (var objs in objList)
        {
            objs.Value.Clear();
        }
        
    }

    // Use this for initialization
    void Start () {
	}
	
	// Update is called once per frame
	void Update () {        
        // 更新が必要なオブジェクト探す
        // 更新リスト
        var updateObjs = new List<GameObject>();

        // キーの異なるオブジェクトを探す
        string key = "Actor";
        SearchDifferentKey(key, ref updateObjs);
        DebugKey(key);      // デバッグ用コード
        // 正しいキーに格納する
        UpdateKey(key, updateObjs);

    }

    // オブジェクトの登録
    static public void Register(GameObject obj)
    {
        // キーを決定する
        string key = CheckKey(obj);

        // キーが存在しない時に生成する
        DebugKey(key);

        // 登録
        objList[key].Add(obj);
    }

    // オブジェクトの登録解除
    static public void RemoveObject(GameObject obj)
    {
        // キーを探す
        string key = CheckKey(obj);

        //// 登録されていないオブジェクト
        //if (objList.ContainsKey(key) == false)
        //{
        //    Debug.Log("登録されていないオブジェクト");
        //    Debug.Break();
        //}

        // 指定オブジェクトの登録解除
        objList[key].Remove(obj);

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
        // 存在しないキーが指定された
        if (objList.ContainsKey(key) == false)
        {
            Debug.Log("存在しないキーが指定された");
            Debug.Break();
            return null;
        }

        // 一番近い距離
        float temp = float.MaxValue;

        // 一番近いオブジェクト
        GameObject result = null;

        // 一番近いオブジェクトの検索
        foreach (var obj in objList[key])
        {
            // オブジェクトまでの距離
            float distance = (obj.transform.position - position).sqrMagnitude;

            // 一番近いオブジェクトを更新する        
            if (temp > distance)
            {
                temp = distance;
                result = obj;
            }
        }

        return result;
    }

    // オブジェクトをカウントする
    static public int CountObjects(string key)
    {
        // 存在しないキーが指定された (returnを 0にするか微妙なライン)
        if (objList.ContainsKey(key) == false)
        {
            Debug.Log("存在しないキーが指定された");
            Debug.Break();
            return 0;
        }

        // 数を返す
        return objList[key].Count;
    }

    // 新しいキーを作成する
    static void CreateAllKey()
    {
        foreach(var key in keys)
        {
            objList.Add(key, new HashSet<GameObject>());
        }
    }

    // エリアを決定する
    static public string CheckKey(GameObject obj)
    {
        return obj.tag;
    }

    // キーの異なるオブジェクトを探す
    void SearchDifferentKey(string key, ref List<GameObject> updateObjs)
    {
        string oldKey;
        oldKey = key;
        foreach (var obj in objList[oldKey])
        {
            if (CheckKey(obj) != oldKey)
            {
                // 更新リストに追加
                updateObjs.Add(obj);
            }
        }
    }

    // キーの更新
    void UpdateKey(string key, List<GameObject> updateObjs)
    {
        // 最適化のため2文に分けている
        foreach (var obj in updateObjs)
            objList[key].Remove(obj);
        foreach (var obj in updateObjs)
            objList[CheckKey(obj)].Add(obj);
    }

    // デバッグ用　存在しないキーか調べる
    static void DebugKey(string key)
    {
        if (objList.ContainsKey(key) == false)
        {
            Debug.Log("存在しないキー : " + key);
            Debug.Break();
        }
    }

}
