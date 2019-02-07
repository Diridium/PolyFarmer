using UnityEngine;
using UnityEngine.UI;

public class ScriptInfoChar : MonoBehaviour {
    static public int money = 1000;

	// Use this for initialization

    void OnGUI()
    {
        
        GUI.Box(new Rect (Screen.width / 2 - 900, Screen.height / 2 - 300, 200, 50), "\r\n Argent : " + money + " $");
        
    }
}
