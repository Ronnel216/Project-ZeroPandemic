using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour {

    // ターゲット
    GameObject target;

    // 追尾の強さ
    float followLevel = 0.5f;

    // ターゲットからのオフセット
    Vector3 offset = new Vector3(0, 25, 0);

    // 中心点
    Vector3 centerBase;      

    public void SetCenter(Vector3 pos)
    {
        centerBase = pos;
    }

	// Use this for initialization
	void Start () {
        target = GameObject.Find("Player");
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 targetPos = Vector3.Lerp(centerBase, target.transform.position, followLevel);

        transform.position = targetPos + offset;
    }
    private void CameraLimit(Vector3 pos)
    {
        //

        ////カメラのX座標が制限座標より大きかったら
        //if (limitPosX < transform.position.x)
        //    transform.position = new Vector3(limitPosX, pos.y, pos.z);
        ////カメラのX座標が制限座標より小さかったら
        //if (-limitPosX > transform.position.x)
        //    transform.position = new Vector3(-limitPosX, pos.y, pos.z);

        ////カメラのZ座標が制限座標より大きかったら
        //if (limitPosZ < transform.position.z)
        //    transform.position = new Vector3(pos.x, pos.y, limitPosZ);
        ////カメラのZ座標が制限座標より小さかったら
        //if (-limitPosZ > transform.position.z)
        //    transform.position = new Vector3(pos.x, pos.y, -limitPosZ);

        ////カメラのX座標が制限座標より大きくてZ座標も制限座標より大きかったら
        //if ((limitPosX < transform.position.x)&&(limitPosZ < transform.position.z))
        //    transform.position = new Vector3(limitPosX, pos.y, limitPosZ);
        ////カメラのX座標が制限座標より大きくてZ座標も制限座標より小さかったら
        //if ((limitPosX < transform.position.x) && (-limitPosZ > transform.position.z))
        //    transform.position = new Vector3(limitPosX, pos.y, -limitPosZ);
        ////カメラのX座標が制限座標より小さくてZ座標も制限座標より小さかったら
        //if ((-limitPosX > transform.position.x) && (-limitPosZ > transform.position.z))
        //    transform.position = new Vector3(-limitPosX, pos.y, -limitPosZ);
        ////カメラのX座標が制限座標より小さくてZ座標も制限座標より大きかったら
        //if ((-limitPosX > transform.position.x) && (limitPosZ < transform.position.z))
        //    transform.position = new Vector3(-limitPosX, pos.y, limitPosZ);

    }
}
