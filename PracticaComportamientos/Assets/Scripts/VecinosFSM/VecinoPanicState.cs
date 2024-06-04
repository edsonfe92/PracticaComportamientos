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
        vecino.GetComponent<NavMeshAgent>().SetDestination(Huida);

        if(vecino.transform.position == Huida)
        {
            vecino.transform.position = Fuera;
        }
    }

    public override void UpdateState(VecinoStateManager vecino)
    {
        /*if(hay duelo){
            seguir desaparecido
        }else
        {
            vecino.SwitchState(vecino.WalkingState);
        }*/
    }

}
