using System.Collections;
using System.Collections.Generic;
using BehaviourAPI.BehaviourTrees;
using BehaviourAPI.Core;
using BehaviourAPI.Core.Actions;
using BehaviourAPI.UnityToolkit;
using UnityEngine;

public class SheriffBH : MonoBehaviour
{      
    public List<Transform> pivots = new List<Transform>();
    BehaviourTree bt = new BehaviourTree();                 
    private void Start() 
    {                    
        WalkAction toBank = new WalkAction(pivots[(int)Location.BANCO].position);
        WalkAction toTavern = new WalkAction(pivots[(int)Location.TAVERNA].position);
        WalkAction toMines = new WalkAction(pivots[(int)Location.MINA].position);

        LeafNode patrolToBank = bt.CreateLeafNode("patrolToBank", toBank);
        LeafNode patrolToTavern = bt.CreateLeafNode("patrolToTavern", toTavern);
        LeafNode patrolToMines = bt.CreateLeafNode("patrolToMines", toMines);
        
        bt.SetRootNode(patrolToBank);

        bt.Start();
    }
    private void Update() 
    {
        bt.Update();
    }
}
    