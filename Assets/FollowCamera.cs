using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour {

    // ターゲット
    [SerializeField]
    GameObject target;

    // ターゲットからのオフセット
    [SerializeField]
    Vector3 offset;

    // 追尾角
    [SerializeField]
    float followRadian;

    //カメラの移動できる範囲(X座標)
    [SerializeField]
    float limitPosX;
    //カメラの移動できる範囲(Y座標)
    [SerializeField]
    float limitPosZ;
    // 現在の回転角
    Quaternion currentRotate;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 targetPos = target.transform.position;

        // 向きを考慮する
        currentRotate = Quaternion.Lerp(currentRotate, target.transform.rotation, 0.05f);
        Vector3 offsetTemp = currentRotate * offset;

        Vector3 cameraPos = targetPos + offsetTemp;
        transform.position = cameraPos;
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
