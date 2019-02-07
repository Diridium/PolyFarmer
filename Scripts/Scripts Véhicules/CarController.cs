using UnityEngine.UI;
using UnityEngine;

public class CarController : MonoBehaviour {

    public GameObject tractor;
    public GameObject EnterBox;
    public Text TxtSpeed;
    public WheelCollider front_left;
    public WheelCollider front_right;
    public WheelCollider back_left;
    public WheelCollider back_right; 
   
    public Transform FL;
    public Transform FR;
    public Transform BL;
    public Transform BR;

    public float Torque = 20000;
    public float Speed;
    public float MaxSpeed = 40f;
    public int Brake = 10000;
    public float CoefAcceleration = 5f;
    public float WheelAngleMax = 10f;
	public bool Freinage = false;

       void Start()
       {
        //Réglage du centerOfMass
        GetComponent<Rigidbody>().centerOfMass = new Vector3(0f, 0f , 0f);
       }

    void Update()
    {

        //Frein auto


        //Son du moteur
        tractor.GetComponent<AudioSource>().pitch = Speed / MaxSpeed + 1f;

        //Affichage de la vitesse
        Speed = GetComponent<Rigidbody>().velocity.magnitude * 3.6f;

        //Acceleration
        if (Input.GetKey(KeyCode.Z) && Speed < MaxSpeed)
        {
            if (!Freinage)
            {
                front_left.brakeTorque = 0;
                front_right.brakeTorque = 0;
                back_left.brakeTorque = 0;
                back_right.brakeTorque = 0;
                back_left.motorTorque = Input.GetAxis("Vertical") * Torque * CoefAcceleration * Time.deltaTime;
                back_right.motorTorque = Input.GetAxis("Vertical") * Torque * CoefAcceleration * Time.deltaTime;
            }
        }

        //Décélération
        if (!Input.GetKey(KeyCode.Z) && !Freinage || Speed > MaxSpeed)
        {
            back_left.motorTorque = 0;
            back_right.motorTorque = 0;
            back_left.brakeTorque = Brake * CoefAcceleration * Time.deltaTime;
            back_right.brakeTorque = Brake * CoefAcceleration * Time.deltaTime;
        }

        //Direction du véhicule
        front_left.steerAngle = Input.GetAxis("Horizontal") * WheelAngleMax;
        front_right.steerAngle = Input.GetAxis("Horizontal") * WheelAngleMax;

        //Freinage
        if (Input.GetKey(KeyCode.S))
        {
            Freinage = true;
        }
        else
        {
            Freinage = false;
        }

        //Marche arrière
        if (Input.GetKey(KeyCode.V) && !Freinage && Speed < MaxSpeed)
        {
            MaxSpeed = 40f;
            Torque = -10000;
            front_left.brakeTorque = 0;
            front_right.brakeTorque = 0;
            back_left.brakeTorque = 0;
            back_right.brakeTorque = 0;
            back_left.motorTorque = Input.GetAxis("Vertical") * Torque * CoefAcceleration * Time.deltaTime;
            back_right.motorTorque = Input.GetAxis("Vertical") * Torque * CoefAcceleration * Time.deltaTime;
        }
        else
        {
            Torque = 20000;
        }

        //Rotation des roues
        FL.Rotate(front_left.rpm / 60 * 360 * Time.deltaTime, 0, 0);
        FR.Rotate(front_right.rpm / 60 * 360 * Time.deltaTime, 0, 0);
        BL.Rotate(back_left.rpm / 60 * 360 * Time.deltaTime, 0, 0);
        BR.Rotate(back_right.rpm / 60 * 360 * Time.deltaTime, 0, 0);

        //SteerAngle (Rotation des mesh des roues)
        FL.localEulerAngles = new Vector3(FL.localEulerAngles.x, front_left.steerAngle - FL.localEulerAngles.z, FL.localEulerAngles.z);
        FR.localEulerAngles = new Vector3(FR.localEulerAngles.x, front_right.steerAngle - FR.localEulerAngles.z, FR.localEulerAngles.z);

        //Freinage
        if (Freinage == true)
        {
            back_left.brakeTorque = Mathf.Infinity;
            back_right.brakeTorque = Mathf.Infinity;
            front_left.brakeTorque = Mathf.Infinity;
            front_right.brakeTorque = Mathf.Infinity;
            back_left.motorTorque = 0;
            back_right.motorTorque = 0;
            // EnterBox.GetComponent<CarEnter>(false);

        }
    }

        void OnGUI()
        {
            GUI.Box(new Rect(Screen.width / 2 - 900, Screen.height / 2 - 300, 200, 50), "\r\n Vitesse : " + (int)Speed + " km/h");
        }
}