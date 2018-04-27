using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakingWall : MonoBehaviour {

    public string tagName;
    bool hit;
    float wallPosY;
    float endPos;
	// Use this for initialization
	void Start () {
        tagName = "Player";
        hit = false;
        wallPosY = -0.05f;
        endPos = -this.gameObject.transform.position.y;        
    }
	
	// Update is called once per frame
	void Update () {
        WallAction();
        //オブジェクトが画面外に行ったときに破壊
        if (this.gameObject.transform.position.y <= endPos)
        {
            WallDestroy();
        }

    }
    //指定したタグと衝突したとき
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag==tagName)
        {
            hit = true;
        }
    }
    //オブジェクトを破壊する
    public void WallDestroy()
    {
        GameObject.Destroy(this.gameObject);
    }
    //とりあえず地面に潜る
    public void WallAction()
    {
        if (hit)
        {
            this.gameObject.transform.position = 
                new Vector3(
                    this.gameObject.transform.position.x,
                    this.gameObject.transform.position.y + wallPosY, 
                    this.gameObject.transform.position.z);
        }
    }
}
