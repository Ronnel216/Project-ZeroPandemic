using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CitizenNormalState : CitizenAI.State {

    bool upNodeIndex = true;
    int nodeIndex = 0;
    float stayTime = 0.0f;
    //float moveTime = 0.0f;
    //Vector3 moveDirection;
    //float speed;

    public CitizenNormalState()
    {
        //stayTime = Random.Range(2.0f, 30.0f);
        //moveDirection = Quaternion.AngleAxis(Random.Range(0.0f, 360.0f), Vector3.up) *  Vector3.forward;
    }

    public override void Excute(StateData data)
    {
        // 参照
        AIRouteNode.Node[] nodes = data.nodes;
        
        // ルートが設定されていない
        if (nodes.Length == 0) return;        

        if (nodes.Length < 2)
        {
            Debug.Log("ノード一つでどう動けと？");
            Debug.Break();
        }

        // 目的地にノードを設定する       
        NavMeshAgent agent = data.ai.GetComponent<NavMeshAgent>();
        agent.SetDestination(nodes[nodeIndex].postion);

        // 目標のノードにたどり着いたか
        float sameLavel = Mathf.Epsilon + 0.1f;
        if ((nodes[nodeIndex].postion - data.ai.gameObject.transform.position).sqrMagnitude > sameLavel)
        {
            // 次のノードへの移動準備が出来ていない
            if (stayTime > 0.0f)
            {
                stayTime -= Time.deltaTime;
                stayTime = Mathf.Max(0.0f, stayTime);
                return;
            }
            return;
        }

        // 次のノードを設定する       

        // ノードインデックスの進行方向を決める
        if (upNodeIndex)
        {
            if (nodes.Length <= nodeIndex + 1)
                upNodeIndex = false;
        }
        else
        {
            if (0 > nodeIndex - 1)
                upNodeIndex = true;
        }

        // ノードを進める
        if (upNodeIndex) nodeIndex++;
        else nodeIndex--;

        // 待機時間を設定する
        stayTime = nodes[nodeIndex].stayTime;

        //data.viewer.Target("InfectedActor");
        //if (data.virus.IsInfected())
        //{
        //    data.ai.ChangeState(new CitizenInfectedState());
        //}
        //else
        //{
        //    GameObject actor = data.viewer.GetClose();
        //    if (actor != null)
        //    {
        //        data.ai.ChangeState(new CitizenEsacapeState());
        //    }
        //}
        //    data.movement.Move(moveDirection * speed);
        //moveTime -= Time.deltaTime;
        //if (moveTime < 0.0f) speed = 0.0f;

        //stayTime -= Time.deltaTime;
        //if (stayTime > 0.0f) return;

        //stayTime = Random.Range(2.0f, 30.0f);

        //moveDirection = Quaternion.AngleAxis(Random.Range(0.0f, 360.0f), Vector3.up) * Vector3.forward;
        //moveTime = Random.Range(0.0f, 3.0f);
        //speed = Random.Range(0.025f, 0.1f);        
    }

}
