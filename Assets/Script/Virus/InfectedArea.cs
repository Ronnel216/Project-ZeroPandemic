using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class InfectedArea : MonoBehaviour {

    Virus virus;
    VirusAbility virusAbility;

    Virus m_candidate;       // 感染候補者

	// Use this for initialization
	void Start () {
        virus = GetComponentInParent<Virus>();
        virusAbility = GetComponentInParent<VirusAbility>();

        // アタッチしていない場合に落とす
        if (virusAbility == null) Debug.Break();
	}
	
	// Update is called once per frame
	void Update () {
        Infected();
	}


    private void OnTriggerEnter(Collider other)
    {
        // 感染対象のウィルスを確認
        Virus virus = other.gameObject.GetComponent<Virus>();
        if (virus == null) return;
        if (virus.IsInfected() == true) return;

        if (!virus.GetvirusFlag())
        {
            virus.SetVirusFlag(true);
        }

        // 感染候補者を登録
        if (m_candidate)
        {
            float canDist = (m_candidate.gameObject.transform.position - this.transform.position).magnitude;
            float dist = (other.gameObject.transform.position - this.transform.position).magnitude;

            if (canDist > dist)
                m_candidate = virus;
        }
        else
        {
            m_candidate = virus;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // 感染対象のウィルスを確認
        Virus virus = other.gameObject.GetComponent<Virus>();

        // ウィルス感染可能な対象を感染させる
        if (virus == null) return;

        //// 
        //if (virus.GetvirusFlag())
        //{
        //    virus.SetVirusFlag(false);
        //}
    }

    private void Infected()
    {
        // 感染候補者がいない
        if (m_candidate == null) return;

        // 感染能力を持っていない
        if (this.virus.NoneAbilityActor) return;

        // 感染者のウィルスのベースを確認
        Virus orginalVirus = this.virus.GetOriginal();

        // 感染源が存在しない
        if (orginalVirus == null) Debug.Break();

        // 感染させる
        m_candidate.Infected(orginalVirus.gameObject);
        m_candidate = null;

        // ビルに入れるようにする
        //NavMeshAgent navAgent = m_candidate.GetComponent<NavMeshAgent>();
        //navAgent.areaMask |= 1 << NavMesh.GetAreaFromName("Building");
    }
}
