using UnityEngine;
using System.Collections;

public class CheckTag : MonoBehaviour
{

    GameObject[] tagObjects;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
    }

    //シーン上の指定したタグが付いたオブジェクトを数える
    public int Check(string tagname)
    {
        tagObjects = GameObject.FindGameObjectsWithTag(tagname);
        return tagObjects.Length;
    }
}
