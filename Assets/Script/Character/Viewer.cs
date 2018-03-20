using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Viewer : MonoBehaviour {

    [SerializeField]
    float range = 1.0f;

    string targetTag;
    GameObject target;
    float minLength = float.MaxValue;

	// Use this for initialization
	void Start () {
        GetComponent<SphereCollider>().radius = range;	
	}
	
	// Update is called once per frame
	void Update () {
        if (target != null)
            minLength = (transform.position - target.transform.position).magnitude;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag != tag) return;
        float length = (transform.position - other.gameObject.transform.position).magnitude;
        if (minLength > length)
        {
            target = other.gameObject;
            minLength = length;
        } 
    }

    public void Target(string tag)
    {
        targetTag = tag;
    }

    public GameObject GetClose()
    {
        return target;
    }

    //public GameObject GetClose(ViewType type)
    //{
    //    Vector3 position = transform.position; 

    //    // オブジェクトが存在しない
    //    if (actors.Count == 0) return null;

    //    // 最短のオブジェクトを求める
    //    GameObject closeActor = actors[0];
    //    float closeLength = (closeActor.transform.position - position).magnitude;
    //    int count = actors.Count;
    //    for (int i = 0; i < count; i++)
    //    {
    //        float length = (actors[i].transform.position - position).magnitude;
    //        if (closeLength > length)
    //        {
    //            closeActor = actors[i];
    //            closeLength = length;
    //        }                    
    //    }
        
    //    return closeActor;
    //}
}
