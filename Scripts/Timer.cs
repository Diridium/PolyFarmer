// Ce script fonctionne comme une minuterie

using UnityEngine;

public class Timer : MonoBehaviour
{
    public float time1 = 5;
    public float time2 = 5;
    public float time3 = 5;

    public bool Maboolean1;
    public bool Maboolean2;
    public bool Maboolean3;  

    void Update()
    {
        if (time1 > 0)
        {
            time1 -= Time.deltaTime;
            Maboolean1 = true;
            Maboolean2 = false;
            Maboolean3 = false;
        }
        else
        {
            if (time2 > 0)
            {
                time2 -= Time.deltaTime;
                Maboolean1 = false;
                Maboolean2 = true;
                Maboolean3 = false;
            }
            else
            {

                if (time3 > 0)
                {
                    time3 -= Time.deltaTime;

                    if (time3 < 0)
                    {
                        Maboolean1 = false;
                        Maboolean2 = false;
                        Maboolean3 = true;
                    }
                }    
            }
        }
    }
}