using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ChangeMaterial : MonoBehaviour {

    [SerializeField]
    Material zombieMat;

    bool changeFlag;
    GameObject parent;
    // Use this for initialization
    void Start () {
        parent = gameObject.transform.parent.parent.gameObject;
        changeFlag = false;
    }

    // Update is called once per frame
    void Update () {
		if(parent.tag== "InfectedActor")
        {
            if(changeFlag==false)
            {
                ChangeMat(zombieMat);
                changeFlag = true;
            }
        }
	}
    void ChangeMat(Material mat)
    {
        SkinnedMeshRenderer smr = GetComponent<SkinnedMeshRenderer>();
        Material[] mats = smr.materials;
        for (int i = 0; i < mats.Length; i++)
        {
            mats[i] = mat;
        }
        smr.materials = mats;
    }
}
