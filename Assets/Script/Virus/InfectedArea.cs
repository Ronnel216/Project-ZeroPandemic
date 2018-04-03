using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfectedArea : MonoBehaviour {

    Virus virus;
    VirusAbility virusAbility;

	// Use this for initialization
	void Start () {
        virus = GetComponentInParent<Virus>();
        virusAbility = GetComponentInParent<VirusAbility>();
        if (virusAbility == null) Debug.Break();
	}
	
	// Update is called once per frame
	void Update () {
		
	}


    private void OnTriggerEnter(Collider other)
    {
        // 感染対象のウィルスを確認
        Virus virus = other.gameObject.GetComponent<Virus>();
        if (virus == null) return;
        if (virus.IsInfected() == true) return;

        // 感染者のウィルスのベースを確認
        Virus orginalVirus = this.virus.GetOriginal();

        // 感染源が存在しない
        if (orginalVirus == null) Debug.Break();

        virus.Infected(orginalVirus.gameObject);

    }
}
