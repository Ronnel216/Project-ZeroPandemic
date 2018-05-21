using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakingWall : MonoBehaviour {
    //タグ名
    public string tagName;
    //衝突しているか
    bool hit;
    //壁のY座標
    float wallPosY;
    //終点
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
    //地面に潜る(仮)
    public void WallAction()
    {
        if (hit)
        {
            this.gameObject.transform.Translate(
                    this.gameObject.transform.position.x,
                    this.gameObject.transform.position.y + wallPosY,
                    this.gameObject.transform.position.z);
        }
    }
}
