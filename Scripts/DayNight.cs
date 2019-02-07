using System;
using UnityEngine;
using Random = UnityEngine.Random;

// Cycle jour/nuit avec calcul de la rotation du soleil selon le nombre de jours, l'heure.
public class DayCycle : MonoBehaviour
{

    public Light m_Moon;			                // La lune
    public Light m_SmallSun;		                // Le soleil secondaire
    public Light m_Sun;       	                    // Le soleil principal
    private float m_Time;                           // Heures convertie en degrées
    [HideInInspector] public float m_TimeSpeedDay;  // Multiplicateur Jour
    [HideInInspector] public float m_TimeSpeedNight;// Multiplicateur Nuit
    [HideInInspector] public float m_Seconds;       // Seconds
    [HideInInspector] public float m_Minutes;       // Minutes
    [HideInInspector] public float m_Hours;         // Hours
    [HideInInspector] public float m_Days;          // Days
    [HideInInspector] public float m_Years;         // Years
    private float m_DaysCount;             		    // Days Counter for the event
    [HideInInspector] public float m_BigDay;        // The days countdown before the event
    [HideInInspector] public float m_BigDayEvent;   // The day of the event
    private float m_PlanetImpact;                   // The rotation of the day due to the rotation of the planet



    // Use this for initialization
    void Start()
    {
        // Définile nombre jours à passer avant l'event
        m_BigDayEvent = Random.Range(7f, 50f);
        m_BigDayEvent = Mathf.Round(m_BigDayEvent);

        // Permet de définir un multiplicateur aléatoire de la durée du jour ou de la nuit
        m_TimeSpeedDay = Random.Range(0.5f, 12f);
        m_TimeSpeedNight = Random.Range(0.5f, 12f);

        // Définile l'année de départ
        m_Years = Random.Range(-3578f, 3578f);

        // Définile le jour de départ
        m_Days = Random.Range(0f, 365f);

        // Permet d'avoir une période de la journée aléatoire à chaque chargement de la scène
        m_Seconds = Random.Range(0f, 60f);
        m_Minutes = Random.Range(0f, 60f);

        // Permet d'avoir un % pour commencer de jour et non de nuit
        if (Random.Range(0.0f, 100.0f) <= 70.0f) { m_Hours = Random.Range(8.0f, 20.0f); }
        else
        {
            m_Hours = Random.Range(20.01f, 29.99f);

            // évite d'avoir une valeur au dessus de 24h pour avoir une logique du cycle 24h
            if (m_Hours >= 24.0f) { m_Hours -= 24.0f; }
        }

        // permet d'éviter d'avoir une seconde au centieme pret par exemple
        m_Seconds = Mathf.Round(m_Seconds);
        m_Minutes = Mathf.Round(m_Minutes);
        m_Hours = Mathf.Round(m_Hours);
        m_Days = Mathf.Round(m_Days);
        m_Years = Mathf.Round(m_Years);
    }


    // Update is called once per frame
    void Update()
    {

        Timer();

        // transmet l'information de la rotation du soleil secondaire
        m_SmallSun.transform.rotation =
            Quaternion.Euler((m_Time - 20), m_SmallSun.transform.rotation.y, (m_PlanetImpact * 2));

        //if the sun is not visible, disabled the sun or enable the moon
        // TODO : add a fade transition to the small sun
        if (m_Time <= 3.184969f || m_Time >= 174.185f)
        {
            m_Sun.gameObject.SetActive(false);
            m_SmallSun.gameObject.SetActive(false);
            m_Moon.gameObject.SetActive(true);
        }
        else
        {
            m_Sun.gameObject.SetActive(true);
            m_SmallSun.gameObject.SetActive(true);
            m_Moon.gameObject.SetActive(false);
        }

        // transmet l'information de la rotation de la lune
        m_Moon.transform.rotation = Quaternion.Euler((m_Time - 180), m_Moon.transform.rotation.y, m_Moon.transform.rotation.z);

        // transmet l'information de la rotation du soleil
        m_Sun.transform.rotation = Quaternion.Euler(m_Time, m_Sun.transform.rotation.y, m_PlanetImpact);
    }


    // calcul l'heure
    void Timer()
    {
        if (m_Hours <= 8.0f || m_Hours >= 22.0f)
        {
            if (m_BigDay == m_BigDayEvent)
            { // si il fait nuit et Gros Jour, alors calcul le temps X m_TimeSpeedNight X 2
                m_Seconds += Time.deltaTime * m_TimeSpeedNight * 2.0f;
            }
            else
            {                        // si il fait nuit, alors calcul le temps X m_TimeSpeedNight
                m_Seconds += Time.deltaTime * m_TimeSpeedNight;
            }
        }
        else
        {                            // si il fait jour, alors calcul le temps X m_TimeSpeedDay
            m_Seconds += Time.deltaTime * m_TimeSpeedDay;
        }

        if (m_Seconds >= 60.0f) { m_Minutes += 1.0f; m_Seconds = 0.0f; }            // Gestion des secondes à minutes
        if (m_Minutes >= 60.0f) { m_Hours += 1.0f; m_Minutes = 0.0f; }            // Gestion des minutes à heures
        if (m_Hours >= 24.0f) { m_Days += 1.0f; m_Hours = 0.0f; }              // Gestion des heures à jours
        if (m_Days >= 365.0f) { m_Years += 1.0f; m_Days = 0.0f; }               // Gestion des jours en années

        //Total = heures + minutes + secondes - décalage du soleil * transformation heure en coordonnées 360°
        m_Time = (((m_Hours) + (m_Minutes / 100) + (m_Seconds / 10000) - 5.8f) * 15);           // Calcul pour la rotation du soleil

        //Total        = L'année + Rotation de base + décalage à cause du nombres de jours
        m_PlanetImpact = m_Years + m_Sun.transform.rotation.z + m_Days;

        BigDayEvent();
    }


    // Gestion des jours spéciaux
    void BigDayEvent()
    {
        // S'occupe de faire le décompte
        if (m_DaysCount != m_Days)
        {
            m_DaysCount = m_Days;
            m_BigDay += 1.0f;
        }
        // vérif si le jour spécial doit être actif ou non
        if (m_BigDay != m_BigDayEvent)
        {
            // FIXME corriger le bug de couleur
            m_Sun.color = new Vector4(0.234f, 0.218f, 0.037f); // Jaune
            m_Sun.intensity = 1.0f;
        }
        else if (m_BigDay == m_BigDayEvent)
        {
            m_Sun.color = new Vector4(0.153f, 0.0f, 0.0f); // Rouge
            m_Sun.intensity = 0.75f;
        }
        else
        {
            m_BigDay = 0.0f; // remet le conteur à 0
        }
        // si Event alors le 2eme soleil disparait
        if (m_BigDay == m_BigDayEvent)
        {
            m_SmallSun.intensity -= Time.deltaTime / 60.0f;

            if (m_SmallSun.intensity <= 0.0f) { m_SmallSun.enabled = false; }
        }
        else if (m_SmallSun.intensity < 0.4f)
        {
            m_SmallSun.enabled = true;
            m_SmallSun.intensity += Time.deltaTime / 60.0f;
        }
    }


}
