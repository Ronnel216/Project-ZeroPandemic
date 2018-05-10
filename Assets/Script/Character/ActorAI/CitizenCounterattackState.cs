using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class CitizenCounterattackState : CitizenAI.State
{
    float moveSpeed = 10.0f;            // 移動速度
    float viewRange = 10.0f;            // 警戒範囲
    GameObject targetObj = null;
    HunterManufactory targetFactory = null;

    public override void Excute(StateData data)
    {
        // 感染
        if (data.virus.IsInfected())
        {
            data.ai.ChangeState(new CitizenInfectedState());
            return;
        }

        // 近くにいるゾンビを探す
        GameObject actor = AIManager.GetCloseZombie(data.ai.transform.position, viewRange);

        // 逃げる
        if (actor != null)
        {
            data.ai.ChangeState(new CitizenEsacapeState());
            return;
        }

        // 製作所へ向かう
        if (targetFactory) BuildHunter(data);
        else GoToManufactory(data);
    }


    private void GoToManufactory(StateData data)
    {
        // ナビゲーション対象のエージェント
        NavMeshAgent agent = data.ai.GetComponent<NavMeshAgent>();
        agent.speed = moveSpeed;

        // 近くの製作所を探す
        GameObject target = AIManager.GetCloseManufactory(data.ai.transform.position);

        // 製作所がある
        if (target)
        {
            targetFactory = target.GetComponent<HunterManufactory>();
            targetObj = target;
            // 製作所に向かう
            agent.SetDestination(targetObj.transform.position);
            targetFactory.ManufactureNum++;
        }
        else
        {
            // 警戒状態解除
            data.ai.ChangeState(new CitizenNormalState());
            return;
        }
    }

    private void BuildHunter(StateData data)
    {
        float distance = (targetObj.transform.position - data.ai.transform.position).magnitude;
        // 製作所に着いている
        if (distance <= targetFactory.FactoryRange)
        {
            if (targetFactory.ManufactureHunter())
                data.ai.ChangeState(new CitizenNormalState());
        }
    }

    ~CitizenCounterattackState()
    {
        // 製作所から人数を減らす
        if (targetFactory)
            targetFactory.ManufactureNum--;
    }
}
