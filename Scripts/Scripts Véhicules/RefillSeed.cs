using UnityEngine;
using UnityEngine.UI;

public class RefillSeed : MonoBehaviour
{

    bool showGUI = false;


    void OnTriggerEnter(Collider other)
    {
        showGUI = false;

        //if Input.GetKeyDown("f")
        //{

            ScriptInfoChar.money -= 100;
            Seeder.SeedAmount = 10000;

        //}
     
    }

    void OnGUI()
    {
        if (showGUI == true)
        {
            GUI.Box(new Rect(Screen.width / 2 - 100, Screen.height / 2 - 12.5f, 200, 50), "10000 graines : 100$");
        }
    }

}