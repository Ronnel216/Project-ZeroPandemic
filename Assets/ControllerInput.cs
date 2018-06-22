using System.Collections;
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

    KeyAction[] keys;

    bool isStickTrigger;

    public void PassStr(out string result, string no_str = "NoName")
    {
        if (str.Length > 0)
            result = str;
        else result = no_str;
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

        keys = new KeyAction[all_katakana.Count];

        Vector3 baseOffset = new Vector3(-300.0f, 100.0f);
        float distance = 35;
        for (int i = 0; i < all_katakana.Count; i++)
        {            
            // ベースを基点に横に平行移動
            int index = 0;
            for (int j = 0; j < separate_index.Count; j++)
                if (separate_index[j] <= i ) index = j;
                else break;

            float offsetX = baseOffset.x + distance * index;
             
            float offsetY = baseOffset.y;
            int indexY = (i - separate_index[index]);
            offsetY = baseOffset.y - indexY * distance;

            
            Vector3 offset = new Vector3(offsetX, offsetY, baseOffset.z);

            var obj = Instantiate(keyPrefab, transform);
            obj.transform.localPosition = offset;
            keys[i] = obj.GetComponent<KeyAction>();
            keys[i].SetChar(all_katakana[i]);


        }

        isStickTrigger = true;
    }

    // Update is called once per frame
    void Update () {
        Vector2 input_direc = new Vector2();
        input_direc.x = Input.GetAxis("Horizontal");
        input_direc.y = Input.GetAxis("Vertical");
        if (Mathf.Abs(input_direc.x) < 0.8f) input_direc.x = 0.0f;
        else input_direc.x = input_direc.x > 0 ? 1.0f : -1.0f;
        if (Mathf.Abs(input_direc.y) < 0.8f) input_direc.y = 0.0f;
        else input_direc.y = input_direc.y > 0 ? 1.0f : -1.0f;
        if (Mathf.Abs(input_direc.x) + Mathf.Abs(input_direc.y) < 0.3f) isStickTrigger = true;
        if (isStickTrigger == false) input_direc = Vector2.zero; 
        bool is_push = Input.GetButtonDown("Button A");
        bool is_delete = Input.GetButtonDown("Cancel");
        bool is_send = Input.GetButtonDown("Button Start");   


        // 文字の代入
        InputText(ref text_obj, str);

        keys[focus_index].Focus();
        
        if (input_direc.sqrMagnitude > 0)
            isStickTrigger = false;

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

        // データの格納
        if (is_push)
           InputText(ref str, focus_index);

        if (is_delete)
            DeleteStr(ref str);

        if (is_send)
            GetComponent<InputFieldManager>().InputLogger();
            //keyPrefab.GetComponent<KeyAction>().SetChar(all_katakana[focus_index]);
    }

    public void SetStr(string str)
    {
        this.str = str;
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

    void InputText(ref GameObject text_obj, string str)
    {
        text_obj.GetComponent<Text>().text = str;
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
