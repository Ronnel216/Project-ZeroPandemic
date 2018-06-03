using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Linq;

public class CitizenSpawner : MonoBehaviour {

    [SerializeField]
    int citizenNum;

    private void Awake()
    {
        GameObject prefab = (GameObject)Resources.Load("Prefabs/Citizen");

        const float baseSize = 10.0f;
        float scaleX = transform.lossyScale.x;
        float scaleZ = transform.lossyScale.z;
   
        // 配置の位置を示した配列
        int sizeX = Random.Range(citizenNum, citizenNum + 1);
        int sizeZ = Random.Range(citizenNum, citizenNum + 1);

        // 座標を入れる仮の入れ物
        var tempList = new Vector2[sizeX * sizeZ];
        
        for (int i = 0; i < tempList.Length; i++)
        {
            float x = i % sizeX;
            float y = i / sizeZ;
            x = x * (baseSize * scaleX / sizeX);
            //tempList[i] = new Vector2(, );
        }

        // シャッフル
        var posList = tempList.OrderBy(i => System.Guid.NewGuid()).ToArray();

        Destroy(gameObject);
    }
    
}
