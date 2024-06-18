using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EnemyManager : MonoBehaviour
{
    private float _healthpoints;
    public Slider healthDisplay;
    public TMP_Text bulletsUI;

    private int numBalas;    

    bool gotHit;
    bool noBullets;

    private void Awake()
    {
        _healthpoints = 100;    
        SetBalas(6);
        gotHit = false;
        noBullets = false;
    }

    public void SetBalas(int balas)
    {   
        numBalas = balas;
        UpdateBulletsUI();
    }
    public void UpdateBulletsUI()
    {
        bulletsUI.text = numBalas.ToString();
    }

    public bool TakeHit()
    {
        _healthpoints -= 10;
        healthDisplay.value = _healthpoints/100;
        gotHit = true;

        bool isDead = _healthpoints <= 0;
        if (isDead) _Die();
        return isDead;
    }

    public bool GotHit()
    {    
        bool a = gotHit;
        if(gotHit) gotHit = false;
        return a;
    }

    public bool NoBullets()
    {        
        return noBullets;
    }

    public bool Shoot()
    {
        numBalas -= 1;
        UpdateBulletsUI();

        noBullets = numBalas <= 0;
        
        return noBullets;
    }

    public bool Reload()
    {
        if(noBullets)
        {
            numBalas = 6;
            noBullets = false;
            return true;
        }        
        else return false;
        
    }

    private void _Die()
    {
        Destroy(gameObject);
    }
}