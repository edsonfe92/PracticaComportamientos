using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class VecinoPanicState : VecinoBaseState
{
    public override void EnterState(VecinoStateManager vecino)
    {
        //Correr a un nodo y desaparecer
        var Huida = new Vector3(vecino.PivotTransforms[(int)Location.HUIDA].position.x, vecino.transform.position.y, vecino.PivotTransforms[(int)Location.HUIDA].position.z);
        var Fuera = new Vector3(vecino.PivotTransforms[(int)Location.FUERA].position.x, vecino.transform.position.y, vecino.PivotTransforms[(int)Location.FUERA].position.z);

        Debug.Log("me voy a esconder YA");
        vecino.navMesh.speed = 25;
        vecino.navMesh.angularSpeed = 150;
        vecino.navMesh.acceleration = 12;
        //vecino.navMesh.angularSpeed = 10;
        vecino.navMesh.SetDestination(Huida);
        
        if (Vector3.Distance(vecino.transform.position, Huida) < 0.5f)
        {
            Debug.Log("Ya estoy escondido");
            vecino.transform.position = Fuera;
        }
    }

    public override void UpdateState(VecinoStateManager vecino)
    {
        var Huida = new Vector3(vecino.PivotTransforms[(int)Location.HUIDA].position.x, vecino.transform.position.y, vecino.PivotTransforms[(int)Location.HUIDA].position.z);
        var Fuera = new Vector3(vecino.PivotTransforms[(int)Location.FUERA].position.x, vecino.transform.position.y, vecino.PivotTransforms[(int)Location.FUERA].position.z);

        if (Vector3.Distance(vecino.transform.position, Huida) < 0.5f)
        {
            Debug.Log("Ya estoy escondido");
            vecino.GetComponent<MeshRenderer>().enabled = false;
        }

        if(GameManager.instance.hayDuelo == false)
        {
            Debug.Log("Ya salgo");
            vecino.SwitchState(vecino.WalkingState);
        }

    }

}
