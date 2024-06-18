using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class VecinoWalkState : VecinoBaseState
{
    public float proximityThreshold = 1.0f;
    public override void EnterState(VecinoStateManager vecino)
    {
        Debug.Log("EnterState de WalkState");
        Debug.Log(vecino.spawn);
        Debug.Log(vecino.transform.position);
        //Si estan escondidos spawnear, si no caminar de nodo a nodo

        var Huida = new Vector3(vecino.PivotTransforms[(int)Location.HUIDA].position.x, vecino.transform.position.y, vecino.PivotTransforms[(int)Location.HUIDA].position.z);
        var Calle1 = new Vector3(vecino.PivotTransforms[(int)Location.CALLE1].position.x, vecino.transform.position.y, vecino.PivotTransforms[(int)Location.CALLE1].position.z);
        var Calle2 = new Vector3(vecino.PivotTransforms[(int)Location.CALLE2].position.x, vecino.transform.position.y, vecino.PivotTransforms[(int)Location.CALLE2].position.z);
        var Calle3 = new Vector3(vecino.PivotTransforms[(int)Location.CALLE3].position.x, vecino.transform.position.y, vecino.PivotTransforms[(int)Location.CALLE3].position.z);
        var Calle4 = new Vector3(vecino.PivotTransforms[(int)Location.CALLE4].position.x, vecino.transform.position.y, vecino.PivotTransforms[(int)Location.CALLE4].position.z);
        var Fuera = new Vector3(vecino.PivotTransforms[(int)Location.FUERA].position.x, vecino.transform.position.y, vecino.PivotTransforms[(int)Location.FUERA].position.z);
        var Taverna = new Vector3(vecino.PivotTransforms[(int)Location.TAVERNA].position.x, vecino.transform.position.y, vecino.PivotTransforms[(int)Location.TAVERNA].position.z);
        var Mina = new Vector3(vecino.PivotTransforms[(int)Location.MINA].position.x, vecino.transform.position.y, vecino.PivotTransforms[(int)Location.MINA].position.z);
        var Banco = new Vector3(vecino.PivotTransforms[(int)Location.BANCO].position.x, vecino.transform.position.y, vecino.PivotTransforms[(int)Location.BANCO].position.z);

        vecino.GetComponent<MeshRenderer>().enabled = true;
        vecino.navMesh.speed = 15;
        vecino.navMesh.angularSpeed = 120;
        vecino.navMesh.acceleration = 8;

        Debug.Log("posicion vecino spawn: "+ vecino.transform.position);
        Debug.Log("posicion mina: "+ Mina);
        if(Vector3.Distance(vecino.transform.position, Mina) < 0.5f)
        {
            Debug.Log("me voy al banco");
            vecino.navMesh.SetDestination(Banco);
        }

        Debug.Log("posicion Banco: " + Banco);
        if (Vector3.Distance(vecino.transform.position, Banco) < 0.5f)
        {
            Debug.Log("me voy a la taverna");
            vecino.navMesh.SetDestination(Taverna);
        }

        Debug.Log("posicion Taverna: " + Taverna);
        if (Vector3.Distance(vecino.transform.position, Taverna) < 0.5f)
        {
            Debug.Log("me voy a la mina");
            vecino.navMesh.SetDestination(Mina);
        }

        if (Vector3.Distance(vecino.transform.position, Huida) < 0.5f)
        {
            Debug.Log("me voy a la calle1");
            vecino.navMesh.SetDestination(Calle1);
        }

        if (Vector3.Distance(vecino.transform.position, Calle1) < 0.5f)
        {
            Debug.Log("me voy a la calle1");
            vecino.navMesh.SetDestination(Calle2);
        }

        if (Vector3.Distance(vecino.transform.position, Calle2) < 0.5f)
        {
            Debug.Log("me voy a la calle1");
            vecino.navMesh.SetDestination(Calle3);
        }

        if (Vector3.Distance(vecino.transform.position, Calle3) < 0.5f)
        {
            Debug.Log("me voy a la calle1");
            vecino.navMesh.SetDestination(Calle4);
        }

        if (Vector3.Distance(vecino.transform.position, Calle4) < 0.5f)
        {
            Debug.Log("me voy a la calle1");
            vecino.navMesh.SetDestination(Huida);
        }


    }

    public override void UpdateState(VecinoStateManager vecino)
    {

        var Huida = new Vector3(vecino.PivotTransforms[(int)Location.HUIDA].position.x, vecino.transform.position.y, vecino.PivotTransforms[(int)Location.HUIDA].position.z);
        var Calle1 = new Vector3(vecino.PivotTransforms[(int)Location.CALLE1].position.x, vecino.transform.position.y, vecino.PivotTransforms[(int)Location.CALLE1].position.z);
        var Calle2 = new Vector3(vecino.PivotTransforms[(int)Location.CALLE2].position.x, vecino.transform.position.y, vecino.PivotTransforms[(int)Location.CALLE2].position.z);
        var Calle3 = new Vector3(vecino.PivotTransforms[(int)Location.CALLE3].position.x, vecino.transform.position.y, vecino.PivotTransforms[(int)Location.CALLE3].position.z);
        var Calle4 = new Vector3(vecino.PivotTransforms[(int)Location.CALLE4].position.x, vecino.transform.position.y, vecino.PivotTransforms[(int)Location.CALLE4].position.z);
        var Fuera = new Vector3(vecino.PivotTransforms[(int)Location.FUERA].position.x, vecino.transform.position.y, vecino.PivotTransforms[(int)Location.FUERA].position.z);
        var Taverna = new Vector3(vecino.PivotTransforms[(int)Location.TAVERNA].position.x, vecino.transform.position.y, vecino.PivotTransforms[(int)Location.TAVERNA].position.z);
        var Mina = new Vector3(vecino.PivotTransforms[(int)Location.MINA].position.x, vecino.transform.position.y, vecino.PivotTransforms[(int)Location.MINA].position.z);
        var Banco = new Vector3(vecino.PivotTransforms[(int)Location.BANCO].position.x, vecino.transform.position.y, vecino.PivotTransforms[(int)Location.BANCO].position.z);

        if (GameManager.instance.hayDuelo)
        {
            Debug.Log("Soy vesino y Me updateo");
            vecino.SwitchState(vecino.PanicState);
        }
        else
        {
            //Debug.Log("posicion vecino spawn: " + vecino.transform.position);
            //Debug.Log("posicion mina: " + Mina);
            if (Vector3.Distance(vecino.transform.position, Mina) < 0.5f)
            {
                Debug.Log("me voy al banco");
                vecino.navMesh.SetDestination(Banco);
            }

            //Debug.Log("posicion Banco: " + Banco);
            if (Vector3.Distance(vecino.transform.position, Banco) < 0.5f)
            {
                Debug.Log("me voy a la taverna");
                vecino.navMesh.SetDestination(Taverna);
            }

            //Debug.Log("posicion Taverna: " + Taverna);
            if (Vector3.Distance(vecino.transform.position, Taverna) < 0.5f)
            {
                Debug.Log("me voy a la mina");
                vecino.navMesh.SetDestination(Mina);
            }

            if (Vector3.Distance(vecino.transform.position, Huida) < 0.5f)
            {
                Debug.Log("me voy a la calle1");
                vecino.navMesh.SetDestination(Calle1);
            }

            if (Vector3.Distance(vecino.transform.position, Calle1) < 0.5f)
            {
                Debug.Log("me voy a la calle1");
                vecino.navMesh.SetDestination(Calle2);
            }

            if (Vector3.Distance(vecino.transform.position, Calle2) < 0.5f)
            {
                Debug.Log("me voy a la calle1");
                vecino.navMesh.SetDestination(Calle3);
            }

            if (Vector3.Distance(vecino.transform.position, Calle3) < 0.5f)
            {
                Debug.Log("me voy a la calle1");
                vecino.navMesh.SetDestination(Calle4);
            }

            if (Vector3.Distance(vecino.transform.position, Calle4) < 0.5f)
            {
                Debug.Log("me voy a la calle1");
                vecino.navMesh.SetDestination(Huida);
            }
        }
    }
}
