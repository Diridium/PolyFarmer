using System.Collections.Generic;
using UnityEngine;

public class SeedCorn : MonoBehaviour
{

    //Blé
    public List<float> growTimes = new List<float>() { 1728000f, 1728000f, 1728000f, 1728000f, 1728000f };// étape de ton temps de poussage
    public float grow;// Temps actuel
    public int currentCornStep = 1;// Etape en cours
    public GameObject cornObject;// Met l'object (blé) actif


    //Semoir et Parcelle
    public GameObject Chunk;

    public bool Planted = false;

    //Semer du blé
    private void Start()
    { 
        cornObject.transform.localScale = Vector3.zero;
    }

    void OnTriggerExit(Collider other){// s'active une seul fois à la sortie du collider
        if (other.tag == "Seed Corn" && Chunk.tag == "Empty" && Seeder.SeedAmount > 0)
        {
            Planted = true;
            Seeder.SeedAmount -= 1f;
            Debug.Log(Seeder.SeedAmount.ToString());// résultat 10000 - 1 = 9999 on est d'accord ?
        }
    }

    private void Update(){
        if (currentCornStep < growTimes.Count && Planted == true)
        {// si les étapes sont en dessous du nombre d'étape
            grow += Time.deltaTime;// on augmente le temp
            if (grow >= growTimes[currentCornStep - 1])
            {// si il est sup au temps de l'étape
                grow = 0;//on remet le temp à zero
                currentCornStep++;// on augment de 1 l'étape
                
                cornObject.transform.localScale = Vector3.one * (currentCornStep);// on change la taille
                cornObject.transform.localPosition += new Vector3(0, 0.30f, 0);
                
                Debug.Log("Etape : " + currentCornStep.ToString());// on donne le numéro de l'étape
                
            }
        }
    }
}