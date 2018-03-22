using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfectedArea : MonoBehaviour {

    VirusAbility virusAbility;

	// Use this for initialization
	void Start () {
        virusAbility = GetComponentInParent<VirusAbility>();
        if (virusAbility == null) Debug.Break();
	}
	
	// Update is called once per frame
	void Update () {
		
	}


    private void OnTriggerEnter(Collider other)
    {
        Virus virus = other.gameObject.GetComponent<Virus>();
        if (virus == null) return;
        if (virus.IsInfected() == true) return;
        
        virus.Infected(virusAbility);

    }
}
