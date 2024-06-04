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
using System.Buffers;

public class SheriffBH : BehaviourRunner
{      
    public List<Transform> pivots = new List<Transform>();
    NavMeshAgent meshAgent;
    BSRuntimeDebugger _debugger;

    public CapsuleCollider detectionArea;
    private List<Vector3> patrolPoints = new List<Vector3>();

    public delegate void OnThiefDetected(GameObject gameObject);
    public static OnThiefDetected onThiefDetected;

    bool thiefDetected;

    protected override void Init()
    {                

        onThiefDetected += CurrentThiefDetected;    
        meshAgent = GetComponent<NavMeshAgent>();
        _debugger = GetComponent<BSRuntimeDebugger>();

        PivotTransforms = PivotList.Instance.Pivots;

        base.Init();
    }

    protected override BehaviourGraph CreateGraph()
    {
        //Tree declaration
        var bt = new BehaviourTree();            

        //Actions
        var patrolAction = new PathingAction(patrolPoints,3f);
        var alertAction = new FunctionalAction(Alert);
        alertAction.onStopped += aaa;

        //Leafs    
        var patrol = bt.CreateLeafNode("patrol", patrolAction);     
        var alert = bt.CreateLeafNode("alert", alertAction);

        //var seq = bt.CreateComposite<SequencerNode>("seq",false, );
        var sel = bt.CreateComposite<SelectorNode>("sel",false, patrol, alert);
        var loop = bt.CreateDecorator<LoopNode>(sel);
            
        bt.SetRootNode(loop);
        _debugger.RegisterGraph(bt, "main");
        return bt;
    }    

    private void CurrentThiefDetected(GameObject gameObject)
    {
        thiefDetected = true;
        meshAgent.isStopped = true;

    }
    private Status Alert()
    {
        if(thiefDetected)
        {
            Debug.Log("FORAJIDO DETECTADO");
            
            return Status.Success;
        }        
        else
        {
            return Status.Failure;
        }
        
        
    }
    private void aaa()
    {
        Debug.Log("AAAAA");
    }
    

    

}
    