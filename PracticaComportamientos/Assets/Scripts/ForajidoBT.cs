using System.Collections;
using System.Collections.Generic;
using BehaviourAPI.BehaviourTrees;
using BehaviourAPI.Core;
using BehaviourAPI.Core.Actions;
using BehaviourAPI.UnityToolkit.GUIDesigner.Runtime;
using UnityEngine;
using UnityEngine.AI;

public class ForajidoBT : MonoBehaviour
{
    //BehaviourTree forajidobt = new BehaviourTree();

    public Transform[] pivotArray;

    BehaviourTree forajidobt = new BehaviourTree();
    public List<Transform> PivotTransforms = new List<Transform>();

    protected override BehaviourGraph CreateGraph()
    {
        var BankPos = PivotTransforms[(int)Location.BANCO].position;
        var MinePos = PivotTransforms[(int)Location.BANCO].position;
        var TavernPos = PivotTransforms[(int)Location.BANCO].position;

        var walkToBankAction = new WalkAction(BankPos);
        var walkToMineAction = new WalkAction(MinePos);       
        var walkToTavernAction = new WalkAction(TavernPos);

        var walkToMine = forajidobt.CreateLeafNode("walkToMine", walkToMineAction); // Camina a la mina
        var walkToBank = forajidobt.CreateLeafNode("walkToBank", walkToBankAction); // Camina al banco
        var walkToTavern = forajidobt.CreateLeafNode("walkToTavern", walkToTavernAction); // Camina a la taberna        
    }

    private void Start()
    {
        forajidobt.Start();
    }

    private void Update()
    {
        forajidobt.Update();
    }

}
