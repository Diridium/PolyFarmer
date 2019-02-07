using UnityEngine.UI;
using UnityEngine;

public class CarControllerBS : MonoBehaviour
{

    public GameObject combine;
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
        GetComponent<Rigidbody>().centerOfMass = new Vector3(0f, -3f, 0f);
    }

    void Update()
    {

        //Son du moteur
        combine.GetComponent<AudioSource>().pitch = Speed / MaxSpeed + 1f;

        //Affichage de la vitesse
        Speed = GetComponent<Rigidbody>().velocity.magnitude * 3.6f;
        TxtSpeed.text = "Vitesse (km/h): " + (int)Speed;

        //Acceleration
        if (Input.GetKey(KeyCode.UpArrow) && Speed < MaxSpeed)
        {
            if (!Freinage)
            {
                front_left.brakeTorque = 0;
                front_right.brakeTorque = 0;
                back_left.brakeTorque = 0;
                back_right.brakeTorque = 0;
                front_left.motorTorque = Input.GetAxis("Vertical") * Torque * CoefAcceleration * Time.deltaTime;
                front_right.motorTorque = Input.GetAxis("Vertical") * Torque * CoefAcceleration * Time.deltaTime;
            }
        }

        //Décélération
        if (!Input.GetKey(KeyCode.UpArrow) && !Freinage || Speed > MaxSpeed)
        {
            front_left.motorTorque = 0;
            front_right.motorTorque = 0;
            front_left.brakeTorque = Brake * CoefAcceleration * Time.deltaTime;
            front_right.brakeTorque = Brake * CoefAcceleration * Time.deltaTime;
        }

        //Direction du véhicule
        back_left.steerAngle = Input.GetAxis("Horizontal") * WheelAngleMax;
        back_right.steerAngle = Input.GetAxis("Horizontal") * WheelAngleMax;

        //Freinage
        if (Input.GetKey(KeyCode.DownArrow))
        {
            Freinage = true;
            back_left.brakeTorque = Mathf.Infinity;
            back_right.brakeTorque = Mathf.Infinity;
            front_left.brakeTorque = Mathf.Infinity;
            front_right.brakeTorque = Mathf.Infinity;
            front_left.motorTorque = 0;
            front_right.motorTorque = 0;
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
            front_left.motorTorque = Input.GetAxis("Vertical") * Torque * CoefAcceleration * Time.deltaTime;
            front_right.motorTorque = Input.GetAxis("Vertical") * Torque * CoefAcceleration * Time.deltaTime;
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
        BL.localEulerAngles = new Vector3(BL.localEulerAngles.x, back_left.steerAngle - BL.localEulerAngles.z, BL.localEulerAngles.z);
        BR.localEulerAngles = new Vector3(BR.localEulerAngles.x, back_right.steerAngle - BR.localEulerAngles.z, BR.localEulerAngles.z);

    }
}