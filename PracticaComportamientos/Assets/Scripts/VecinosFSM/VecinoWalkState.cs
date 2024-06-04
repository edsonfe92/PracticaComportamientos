using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class VecinoWalkState : VecinoBaseState
{
    private bool duelo = true;
    public float proximityThreshold = 1.0f;
    public override void EnterState(VecinoStateManager vecino)
    {
        Debug.Log("EnterState de WalkState");
        Debug.Log(vecino.spawn);
        Debug.Log(vecino.transform.position);
        //Si estan escondidos spawnear, si no caminar de nodo a nodo

        var Huida = new Vector3(vecino.PivotTransforms[(int)Location.HUIDA].position.x, vecino.transform.position.y, vecino.PivotTransforms[(int)Location.HUIDA].position.z);
        var Fuera = new Vector3(vecino.PivotTransforms[(int)Location.FUERA].position.x, vecino.transform.position.y, vecino.PivotTransforms[(int)Location.FUERA].position.z);
        var Taverna = new Vector3(vecino.PivotTransforms[(int)Location.TAVERNA].position.x, vecino.transform.position.y, vecino.PivotTransforms[(int)Location.TAVERNA].position.z);
        var Mina = new Vector3(vecino.PivotTransforms[(int)Location.MINA].position.x, vecino.transform.position.y, vecino.PivotTransforms[(int)Location.MINA].position.z);
        var Banco = new Vector3(vecino.PivotTransforms[(int)Location.BANCO].position.x, vecino.transform.position.y, vecino.PivotTransforms[(int)Location.BANCO].position.z);

        if (vecino.transform.position == Fuera)
        {
            Debug.Log("Estoy fuera");
            vecino.transform.position = Huida;
        }

        Debug.Log("posicion vecino spawn: "+ vecino.transform.position);
        Debug.Log("posicion mina: "+ Mina);
        if(Vector3.Distance(vecino.transform.position, Mina) < 0.5f)
        {
            Debug.Log("me voy al banco");
            vecino.GetComponent<NavMeshAgent>().SetDestination(Banco);
        }

        Debug.Log("posicion Banco: " + Banco);
        if (Vector3.Distance(vecino.transform.position, Banco) < 0.5f)
        {
            Debug.Log("me voy a la taverna");
            vecino.GetComponent<NavMeshAgent>().SetDestination(Taverna);
        }

        Debug.Log("posicion Taverna: " + Taverna);
        if (Vector3.Distance(vecino.transform.position, Taverna) < 0.5f)
        {
            Debug.Log("me voy a la mina");
            vecino.GetComponent<NavMeshAgent>().SetDestination(Mina);
        }
    }

    public override void UpdateState(VecinoStateManager vecino)
    {
        if (duelo)
        {
            Debug.Log("Soy vesino y Me updateo");
        }

        var Huida = new Vector3(vecino.PivotTransforms[(int)Location.HUIDA].position.x, vecino.transform.position.y, vecino.PivotTransforms[(int)Location.HUIDA].position.z);
        var Fuera = new Vector3(vecino.PivotTransforms[(int)Location.FUERA].position.x, vecino.transform.position.y, vecino.PivotTransforms[(int)Location.FUERA].position.z);
        var Taverna = new Vector3(vecino.PivotTransforms[(int)Location.TAVERNA].position.x, vecino.transform.position.y, vecino.PivotTransforms[(int)Location.TAVERNA].position.z);
        var Mina = new Vector3(vecino.PivotTransforms[(int)Location.MINA].position.x, vecino.transform.position.y, vecino.PivotTransforms[(int)Location.MINA].position.z);
        var Banco = new Vector3(vecino.PivotTransforms[(int)Location.BANCO].position.x, vecino.transform.position.y, vecino.PivotTransforms[(int)Location.BANCO].position.z);

        Debug.Log("posicion vecino spawn: " + vecino.transform.position);
        Debug.Log("posicion mina: " + Mina);
        if (Vector3.Distance(vecino.transform.position, Mina) < 0.5f)
        {
            Debug.Log("me voy al banco");
            vecino.GetComponent<NavMeshAgent>().SetDestination(Banco);
        }

        Debug.Log("posicion Banco: " + Banco);
        if (Vector3.Distance(vecino.transform.position, Banco) < 0.5f)
        {
            Debug.Log("me voy a la taverna");
            vecino.GetComponent<NavMeshAgent>().SetDestination(Taverna);
        }

        Debug.Log("posicion Taverna: " + Taverna);
        if (Vector3.Distance(vecino.transform.position, Taverna) < 0.5f)
        {
            Debug.Log("me voy a la mina");
            vecino.GetComponent<NavMeshAgent>().SetDestination(Mina);
        }
    }

}
