using System.Collections;
using System.Collections.Generic;
using BehaviourAPI.BehaviourTrees;
using BehaviourAPI.Core;
using BehaviourAPI.Core.Actions;
using BehaviourAPI.UnityToolkit.GUIDesigner.Runtime;
using BehaviourAPI.UnityToolkit;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class SheriffBH : BehaviourRunner
{      
    public List<Transform> pivots = new List<Transform>();
    NavMeshAgent meshAgent;
    BSRuntimeDebugger _debugger;

    public CapsuleCollider detectionArea;
    private List<Vector3> patrolPoints = new List<Vector3>();

    //delegate void On


    protected override void Init()
    {                

        meshAgent = GetComponent<NavMeshAgent>();
        _debugger = GetComponent<BSRuntimeDebugger>();

        var bankPos = new Vector3(pivots[(int)Location.BANCO].position.x,transform.position.y ,pivots[(int)Location.BANCO].position.z);
        var minesPos = new Vector3(pivots[(int)Location.MINA].position.x,transform.position.y ,pivots[(int)Location.MINA].position.z);
        var tavernPos = new Vector3(pivots[(int)Location.TAVERNA].position.x,transform.position.y ,pivots[(int)Location.TAVERNA].position.z);

        patrolPoints.Add(bankPos);
        patrolPoints.Add(minesPos);
        patrolPoints.Add(tavernPos);
        base.Init();
    }

    protected override BehaviourGraph CreateGraph()
    {
        //Tree declaration
        var bt = new BehaviourTree();            

        //Actions
        var patrolAction = new PathingAction(patrolPoints,3f);
        var alertAction = new FunctionalAction();

        //Leafs    
        var patrol = bt.CreateLeafNode("patrol", patrolAction);        

        var seq = bt.CreateComposite<SequencerNode>("seq",false, patrol);
        var loop = bt.CreateDecorator<LoopNode>(seq);
            
        bt.SetRootNode(loop);
        _debugger.RegisterGraph(bt, "main");
        return bt;
    }    

    //private void 

    

}
    