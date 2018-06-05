using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Viewer : MonoBehaviour {

    [SerializeField]
    string targetName;

    [SerializeField]
    float range = 1.0f;

    public string targetTag;
    GameObject target;
    float minLength = float.MaxValue;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
        // 視界から外れた時
        if (target)
        {
            if ((target.transform.position - transform.position).sqrMagnitude >= range * range){
                target = null;
            }
        }       
        
        // ターゲットが存在しない時
        if (target == null)
        {
            var temp = WorldViewer.GetCloseObject(targetTag, transform.position);
            if (temp)
            {
                if ((temp.transform.position - transform.position).sqrMagnitude < range * range){
                    target = temp;
                }
            }
        }
    }
    
    // ターゲットを決める
    public void Target(string tag)
    {
        targetTag = tag;
    }

    // 一番近いオブジェクト
    public GameObject GetClose()
    {
        return target;
    }

    public void Reset()
    {
        target = null;
    }

}
