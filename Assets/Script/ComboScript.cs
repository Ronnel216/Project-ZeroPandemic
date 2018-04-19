using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComboScript : MonoBehaviour
{
    private static int comboNum;

    [SerializeField]
    private float comboTime = 3.0f;

    // Use this for initialization
    void Start ()
    {
        comboNum = 0;
    }

    // Update is called once per frame
    void Update ()
    {
	}

    public int PlusCombo()
    {
        return comboNum++;
    }

    public int GetCombo()
    {
        return comboNum;
    }

    public IEnumerator ComboCoroutine()
    {
        yield return new WaitForSeconds(comboTime);
        comboNum = 0;
    }
}
