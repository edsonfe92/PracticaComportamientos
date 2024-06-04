using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BasicStats : MonoBehaviour
{
    public float vida;

    public Slider slider;

    private void Awake() 
    {
        slider.value = vida/100;
    }
    public bool UpdateHP(int hit)
    {
        vida -= hit;
        slider.value = (vida)/100;

        if(vida <= 100)
        {
            return true;
        }
        else 
        {
            return false;
        }
    }
}
