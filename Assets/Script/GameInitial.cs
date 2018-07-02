using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInitial 
{
    [RuntimeInitializeOnLoadMethod]
    static void OnRuntimeMethodLoad()
    {
        Screen.SetResolution(1600, 900, false);
    }
	
}
