using UnityEngine;

public class ChooseSeed : MonoBehaviour { // ça tu devrais le mec en statique pour faire un call dans le seeder

    public GameObject Seeder;
    private bool showGUI = false;

	// Use this for initialization
	void Start () {

        Seeder.tag = "Untagged";

	}

    void Update() {

            if (showGUI == true)
            {
                if (Input.GetKeyDown("1"))
                {
                    Seeder.tag = "Seed Corn";
                    Debug.Log("Maïs Choisi");
                }
                    if (Input.GetKeyDown("2"))
                    {
                        Seeder.tag = "Seed Wheat";
                        Debug.Log("Blé Choisi");
                    }
        }
        }

        void OnTriggerEnter(Collider hit)
        {
            if (hit.gameObject.tag == "Player")
            {
                showGUI = true;
            }
        }

        void OnTriggerExit(Collider hit)
        {
            if (hit.gameObject.tag == "Player")
            {
                showGUI = false;
            }
        }

        void OnGUI()
        {
            if (showGUI == true)
            {
                GUI.Box(new Rect(Screen.width / 2 - 100, Screen.height / 2 - 12.5f, 200, 50), "\r\n Press 1 to Seed Corn");
            }
        }
}
