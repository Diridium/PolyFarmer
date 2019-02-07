using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldBehaviour : MonoBehaviour {

    public int tailleX, tailleY;
    public float space;

    public GameObject chunkObject;


	// Use this for initialization
	void Start () {
        GenerateField();
	}
	
    void GenerateField(){
        for (int x = 0; x < tailleX; x++){
            for (int y = 0; y < tailleY; y++){
                GameObject newChunk = Instantiate(chunkObject, transform) as GameObject;
                newChunk.transform.localPosition = new Vector3(0.5f - space * x, 37, 0.45f -space * y);
            }
        }
    }
}
