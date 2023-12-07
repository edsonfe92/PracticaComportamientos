using System.Collections;
using System.Collections.Generic;
using BehaviourAPI.BehaviourTrees;
using BehaviourAPI.Core;
using BehaviourAPI.Core.Actions;
using BehaviourAPI.UnityToolkit.GUIDesigner.Runtime;
using BehaviourAPI.UnityToolkit;
using UnityEngine;
using UnityEngine.AI;

public class SheriffBH : BehaviourRunner
{      
    public List<Transform> pivots = new List<Transform>();
    NavMeshAgent meshAgent;
    BSRuntimeDebugger _debugger;


    protected override void Init()
    {                

        meshAgent = GetComponent<NavMeshAgent>();
        _debugger = GetComponent<BSRuntimeDebugger>();
        base.Init();
    }

    protected override BehaviourGraph CreateGraph()
    {
        var bt = new BehaviourTree();
        var bankPos = pivots[(int)Location.BANCO].position;

        var toBank = new WalkAction(bankPos);
        /*var toTavern = new WalkAction(pivots[(int)Location.TAVERNA].position);
        var toMines = new WalkAction(pivots[(int)Location.MINA].position);*/

        var patrolToBank = bt.CreateLeafNode("patrolToBank", toBank);
        /*var patrolToTavern = bt.CreateLeafNode("patrolToTavern", toTavern);
        var patrolToMines = bt.CreateLeafNode("patrolToMines", toMines);*/

        //var seq = bt.CreateComposite<SequencerNode>("seq",false, patrolToBank,patrolToTavern,patrolToMines);
        var loop = bt.CreateDecorator<LoopNode>(patrolToBank);
            
        bt.SetRootNode(patrolToBank);
        _debugger.RegisterGraph(bt, "main");
        return bt;
    }

}
    