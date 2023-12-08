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
    public List<Transform> PivotTransforms = new List<Transform>();

    public GameObject forajido;
    //private int spawn;
    private static bool hasSpawned = false;

    // Start is called before the first frame update

    void Start()
    {
        if (!hasSpawned)
        {
            Spawn();
            hasSpawned = true;
            Debug.Log("AAA");
            enabled = false; //SI QUITO ESTE SPAWNEAN INFINITOS
        }
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void Spawn()
    {
        Location valueSpawn = (Location)Random.Range(0, 3);
        int spawn = (int)valueSpawn;
        //forajido = GameObject.Find("Character");
        
        Instantiate(forajido, PivotTransforms[spawn].position, Quaternion.identity);
        
        Debug.Log("Spawned");
    }
}
