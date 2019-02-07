
using UnityEngine;
using UnityEngine.UI;

public class Seeder : MonoBehaviour{

    static public float SeedAmount = 10000f;
    static public bool showGUI = false;
    void OnGUI(){

        if (showGUI == true){
            GUI.Box(new Rect(Screen.width / 2 - 900, Screen.height / 2 - 200, 200, 50), "\r\n Graines : " + (float)(SeedAmount));
        }
        
    }

    void Update(){

    }
}