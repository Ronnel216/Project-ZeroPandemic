using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartManager : MonoBehaviour
{
    [SerializeField]
    private Text countdownText;

	// Use this for initialization
	void Start ()
    {
        countdownText.text = "";
        StartCoroutine(CountdownCoroutine());
    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    IEnumerator CountdownCoroutine()
    {
        countdownText.gameObject.SetActive(true);

        countdownText.text = "3";
        yield return new WaitForSeconds(1.25f);
        countdownText.text = "2";
        yield return new WaitForSeconds(1.25f);
        countdownText.text = "1";
        yield return new WaitForSeconds(1.25f);
        countdownText.text = "START";
        yield return new WaitForSeconds(1.25f);
        countdownText.text = "";
        yield return new WaitForSeconds(1.25f);

    }
}
