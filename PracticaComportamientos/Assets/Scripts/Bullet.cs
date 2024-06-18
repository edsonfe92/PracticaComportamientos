using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private void OnTriggerEnter(Collider other) 
    {
        if(other != null)
        {
            if(other.gameObject.GetComponent<EnemyManager>() != null)
            {
                other.gameObject.GetComponent<EnemyManager>().TakeHit();
            }
            Destroy(gameObject);
        }    
    }
}
