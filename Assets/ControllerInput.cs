﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControllerInput : MonoBehaviour {

    string[] katakana = new string[]
       {
           "ア","イ","ウ","エ","オ",
           "カ","キ","ク","ケ","コ",
           "サ","シ","ス","セ","ソ",
           "タ","チ","ツ","テ","ト",
           "ナ","ニ","ヌ","ネ","ノ",
           "ハ","ヒ","フ","ヘ","ホ",
           "マ","ミ","ム","メ","モ",
           "ヤ","ユ","ヨ",
           "ラ","リ","ル","レ","ロ",
           "ワ","ヲ","ン"
       };

    string[] katakana_tenten = new string[]
    {
           "ヴ",
           "ガ","ギ","グ","ゲ","ゴ",
           "ザ","ジ","ズ","ゼ","ゾ",
           "ダ","ヂ","ヅ","デ","ド",
           "バ","ビ","ブ","ベ","ボ",
    };

    string[] katakana_maru = new string[]
    {
           "パ","ピ","プ","ペ","ポ",
    };

    string[] katakana_komoji = new string[]
    {
           "ァ","ィ","ゥ","ェ","ォ",
           "ッ",
           "ャ","ュ","ョ",
    };

    // 全てのカタカナ 
    List<string> all_katakana = new List<string>();

    // 表示用テキストオブジェクト
    GameObject text_obj;

    // 区切り位置 (ショートカットに使用する)
    List<int> separate_index = new List<int>();

    // フォーカスインデックス
    int focus_index = 0;
    int current_separate = 0;

    // キーのオブクト
    [SerializeField]
    GameObject keyPrefab;

    // 入力された文字列
    [SerializeField]
    string str;

    public void PassStr(out string result, string no_str = "NoName")
    {
        result = no_str;
    }

    // Use this for initialization
    void Start () {      
        text_obj = GameObject.Find("TestText").gameObject;
        Debug.Log(text_obj);

        separate_index.Add(all_katakana.Count);
        separate_index.Add(5);
        separate_index.Add(10);    
        separate_index.Add(15);
        separate_index.Add(20);
        separate_index.Add(25);
        separate_index.Add(30);
        separate_index.Add(35);
        separate_index.Add(38);
        separate_index.Add(43);
        all_katakana.AddRange(katakana);

        separate_index.Add(all_katakana.Count);
        separate_index.Add(all_katakana.Count + 1);
        separate_index.Add(all_katakana.Count + 6);
        separate_index.Add(all_katakana.Count + 11);
        separate_index.Add(all_katakana.Count + 16);
        all_katakana.AddRange(katakana_tenten);

        separate_index.Add(all_katakana.Count);
        all_katakana.AddRange(katakana_maru);

        separate_index.Add(all_katakana.Count);
        separate_index.Add(all_katakana.Count + 5);
        separate_index.Add(all_katakana.Count + 6);
        all_katakana.AddRange(katakana_komoji);

        Vector3 baseOffset = new Vector3(-300.0f, 100.0f);
        Vector3 offset = baseOffset;
        float distance = 30;
        for (int i = 0; i < all_katakana.Count; i++)
        {
            // ベースを基点に横に平行移動
            int index = separate_index.IndexOf(i);
            if (i != 0 && index > 0 )
                offset = baseOffset + Vector3.right * distance * index;
             
            var obj = Instantiate(keyPrefab, transform);
            obj.transform.localPosition = offset;
            obj.GetComponent<KeyAction>().SetChar(all_katakana[i]);
            offset += Vector3.down * distance;
        }

        //for (int i = 0; i < separate_index.Count ; i++)
        //    Debug.Log(all_katakana[separate_index[i]]);
    }

    // Update is called once per frame
    void Update () {
        Vector2 input_direc = new Vector2();
        input_direc = Input.GetKeyDown(KeyCode.RightArrow) ? Vector2.right : input_direc;
        input_direc = Input.GetKeyDown(KeyCode.LeftArrow) ? Vector2.left : input_direc;
        input_direc = Input.GetKeyDown(KeyCode.UpArrow) ? Vector2.up : input_direc;
        input_direc = Input.GetKeyDown(KeyCode.DownArrow) ? Vector2.down : input_direc;
        bool is_push = Input.GetKeyDown(KeyCode.Space);
        bool is_delete = Input.GetKeyDown(KeyCode.C);
        bool is_send = Input.GetKeyDown(KeyCode.S);   
        bool was_inputed = Input.anyKeyDown;

        // キーを押したら進む
        if (was_inputed == false) return;

        // 入力
        int new_focus_index = focus_index;
        
        if (input_direc == Vector2.right)
            new_focus_index = StepSeparateIndex(1);
        else if (input_direc == Vector2.left)
            new_focus_index = StepSeparateIndex(-1);
        else if (input_direc == Vector2.up)
            new_focus_index += -1;
        else if (input_direc == Vector2.down)
            new_focus_index += 1;

        UpdateFocusIndex(new_focus_index);

        // 文字の代入
        InputText(ref text_obj, focus_index);

        // データの格納
        if (is_push)
           InputText(ref str, focus_index);

        if (is_delete)
            DeleteStr(ref str);

        //if (is_send)
        //    keyPrefab.GetComponent<KeyAction>().SetChar(all_katakana[focus_index]);
    }

    bool InputText(ref string str, int index)
    {
        if (str.Length >= 5) return false;
        str += all_katakana[focus_index];
        return true;
    }

    bool DeleteStr(ref string str)
    {
        if (str.Length == 0) return false;
        str = str.Remove(str.Length - 1);
        return true;
    }

    void InputText(ref GameObject text_obj, int focus_index)
    {
        text_obj.GetComponent<Text>().text = all_katakana[focus_index];
    }

    int WarpValue(int value, int max, int min)
    {
        if (value < min) value = max;
        if (value > max) value = min;
        return value;
    }

    int StepSeparateIndex(int step_index)
    {
        step_index = Mathf.Clamp(step_index, -1, 1);
        int temp = WarpValue(current_separate + step_index, separate_index.Count - 1, 0);
        int result = separate_index[temp];
        return result;
    }

    // foucus_indexの更新
    void UpdateFocusIndex(int index)
    {
        // ループ処理
        index = WarpValue(index, all_katakana.Count - 1, 0);       

        focus_index = index;

        UpdateCurrentSeparate(focus_index);
    }

    // focus_indexの変化に対応させる
    void UpdateCurrentSeparate(int focus_index)
    {
        int temp = -1;
        for (int i = 0; i < separate_index.Count; i++)
        {
            if (focus_index >= separate_index[i])
                temp = i;
            else break;
        }

        if (temp < 0) {
            Debug.Log("temp" + temp);
            Debug.Break();
        }

        current_separate = temp;
    }


}
