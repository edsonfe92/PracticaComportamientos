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
    BehaviourTree forajidobt = new BehaviourTree();

    public Transform[] pivotArray;

    protected override BehaviourGraph CreateGraph()
    {
        var walkToMineAction = new WalkAction(MinePos);
        var walkToBankAction = new WalkAction(BankPos);
        var walkToTavernAction = new WalkAction(TavernPos);

        var walkToMine = forajidobt.CreateLeafNode("walkToMine", walkToMineAction); // Camina a la mina
        var walkToBank = forajidobt.CreateLeafNode("walkToBank", walkToBankAction); // Camina al banco
        var walkToTavern = forajidobt.CreateLeafNode("walkToTavern", walkToTavernAction); // Camina a la taberna
    }


}
