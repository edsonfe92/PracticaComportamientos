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
    public void UpdateLife()
    {
        slider.value = vida/100;
    }
}
