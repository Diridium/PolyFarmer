using UnityEngine;

public class SeedWheat : MonoBehaviour
{

    //Graine de base
    public GameObject seed;

    //Blé
    public GameObject wheat1;
    public GameObject wheat2;
    public GameObject wheat3;

    //Semoir et Parcelle
    public GameObject Seeder;
    public GameObject Chunk;

    //Croissance du blé
    public float growtime1 = 1728000f;
    public float growtime2 = 1728000f;
    public float growtime3 = 1728000f;

    public bool Seed;
    public bool Wheat1;
    public bool Wheat2;
    public bool Wheat3;
    public bool isHarvestable;

    //Semer du blé
    private void Start()
    {
        isHarvestable = false;
        Seed = false;
        Wheat1 = false;
        Wheat2 = false;
        Wheat3 = false;

        Seeder.tag = "Untagged";
    }

    void OnTriggerExit(Collider other)
    {
        if (Seeder.tag == "Seed Wheat" && Chunk.tag == "Empty")
        {
            Seed = true;
        }
        else
        {
            Seed = false;
        }
    }

    private void Update()
    {

        if (growtime1 > 0 && Seed)
        {
            growtime1 -= Time.deltaTime;

            //Croissance 1

            if (growtime1 < 0)
            {
                Seed = false;
                Wheat1 = true;
                Wheat2 = false;
                Wheat3 = false;
                Debug.Log("Croissance Etape 1");
            }
        }
        else
        {

            if (growtime2 > 0 && Wheat1)
            {
                growtime2 -= Time.deltaTime;

                //Croissance 2

                if (growtime2 < 0)
                {
                    Seed = false;
                    Wheat1 = false;
                    Wheat2 = true;
                    Wheat3 = false;
                    Debug.Log("Croissance Etape 2");
                }
            }
            else
            {

                if (growtime3 > 0 && Wheat2)
                {
                    growtime3 -= Time.deltaTime;

                    //Croissance 3

                    if (growtime3 < 0)
                    {
                        Seed = false;
                        Wheat1 = false;
                        Wheat2 = false;
                        Wheat3 = true;
                        Debug.Log("Maïs prêt à être récolté");
                    }
                }

            }
            if (growtime3 < 0)
            {
                isHarvestable = true;
            }

            //Booleans
            if (Seed)
            {
                seed.SetActive(true);
            }
            else
            {
                seed.SetActive(false);
            }

            if (Wheat1)
            {
                wheat1.SetActive(true);
            }
            else
            {
                wheat1.SetActive(false);
            }

            if (Wheat2)
            {
                wheat2.SetActive(true);
            }
            else
            {
                wheat2.SetActive(false);
            }

            if (Wheat3)
            {
                wheat3.SetActive(true);
            }
            else
            {
                wheat3.SetActive(false);
            }
        }
    }
}