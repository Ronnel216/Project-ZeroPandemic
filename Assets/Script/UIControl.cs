using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIControl : MonoBehaviour {

    //座標が0だと地面に埋まるのでそれを防ぐためY座標のみ指定
    float UIPosY = 0.1f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
    }

    public void ChangeSize(float num)
    {
        transform.localScale = new Vector3(num, UIPosY, num);
    }
}
