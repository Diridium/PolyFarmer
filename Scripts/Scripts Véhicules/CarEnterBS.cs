using UnityEngine;

public class CarEnterBS : MonoBehaviour
{

    public GameObject player;
    public GameObject combine;
    public GameObject carCamera;
    public GameObject insideCamera;
    public GameObject canvas;

    private bool canEnter;
    private bool isInside;

    private float timeLeft;
    private bool canLeave = false;

    private void Start()
    {
        combine = transform.parent.gameObject;
        player = GameObject.FindGameObjectWithTag("Player");
        timeLeft = 1f;
    }

    void Update()
    {

        // Si on entre dans la voiture
        if (canEnter && Input.GetKeyDown("e"))
        {
            isInside = true;

            player.transform.parent = combine.transform;
            player.SetActive(false);

            carCamera.SetActive(true);
            insideCamera.SetActive(false);
            canvas.SetActive(true);

            combine.GetComponent<CarControllerBS>().enabled = true;

            timeLeft = 1f;
        }

        //Pour changer de caméra
        if (isInside && Input.GetKey("c") && carCamera)
        {
            insideCamera.SetActive(true);
        }
        else
        {
            insideCamera.SetActive(false);
        }


        // Si on sort de la voiture
        if (isInside && canLeave && Input.GetKeyDown("e"))
        {
            player.transform.parent = null;
            player.SetActive(true);

            carCamera.SetActive(false);
            insideCamera.SetActive(false);
            canvas.SetActive(false);

            combine.GetComponent<CarControllerBS>().enabled = false;

            isInside = false;
            canLeave = false;

            timeLeft = 1f;
        }

        // Délais d'attente entre l'entrée et la sortie de la voiture
        // (Permet aussi d'utiliser la même touche pour entrer et sortir)
        if (timeLeft > 0 && isInside)
        {
            timeLeft -= Time.deltaTime;
            canLeave = false;
        }
        else if (timeLeft <= 0 && isInside)
        {
            canLeave = true;
        }
    }

    // Detection du joueur dans le cube d'entrée
    void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.tag == "Player")
        {
            canEnter = true;
        }
    }

    void OnTriggerExit(Collider other)
    {

        if (other.gameObject.tag == "Player")
        {
            canEnter = false;
        }

    }
}