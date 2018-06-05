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
        GetComponent<SphereCollider>().radius = range;	
	}
	
	// Update is called once per frame
	void Update () {
        if (target == null) {
            minLength = float.MaxValue;
            targetName = "NotFound";
            return;
                }
        targetName = target.name;
        minLength = (transform.position - target.transform.position).magnitude;
        if (target.tag != targetTag)
            target = null;
    }

    void OnTriggerStay(Collider other)
    {
        if (other.tag != targetTag) return;
        float length = (transform.position - other.gameObject.transform.position).magnitude;
        if (minLength > length)
        {
            target = other.gameObject;
            minLength = length;
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
