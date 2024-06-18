using System.Collections;
using System.Collections.Generic;
using BehaviourAPI.BehaviourTrees;
using BehaviourAPI.Core;
using BehaviourAPI.Core.Actions;
using BehaviourAPI.UnityToolkit;
using BehaviourAPI.UnityToolkit.GUIDesigner.Runtime;
using UnityEngine;
using UnityEngine.AI;

public class SpawnSystem : MonoBehaviour
{
    private List<Transform> PivotTransforms = new List<Transform>();

    public GameObject character;
    public Vector2 RangeSpawn;
    //private int spawn;
    private bool hasSpawned = false;

    public int numSpawn;


    // Start is called before the first frame update
    void Start()
    {
        PivotTransforms = PivotList.Instance.Pivots;

        if (!hasSpawned)
        {
            for (int i = 0; i < numSpawn; i++)
            {             
                Spawn();   
            }
            hasSpawned = true;            
            //enabled = false; //SI QUITO ESTE SPAWNEAN INFINITOS
        }
       
    }

    public void SetPivoteList(List<Transform> spawnList)
    {
        PivotTransforms = spawnList;
    }

    private void Spawn()
    {
        Location valueSpawn = (Location)Random.Range(RangeSpawn.x, RangeSpawn.y);
        //forajido = GameObject.Find("Character");

        var bicho = Instantiate(character, PivotTransforms[(int)valueSpawn].position, Quaternion.identity);
        if(bicho.GetComponent<VecinoStateManager>() != null)
        {
            bicho.GetComponent<VecinoStateManager>().spawn = valueSpawn;
        }        
        /*if(bicho.GetComponent<ForajidoBT>() != null)
        {
            bicho.GetComponent<ForajidoBT>().enabled = true;
        }*/
        Debug.Log("Spawned");
    }
}
