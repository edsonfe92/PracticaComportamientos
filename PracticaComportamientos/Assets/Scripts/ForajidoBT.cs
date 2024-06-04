using System.Collections;
using System.Collections.Generic;
using BehaviourAPI.BehaviourTrees;
using BehaviourAPI.Core;
using BehaviourAPI.Core.Actions;
using BehaviourAPI.UnityToolkit;
using BehaviourAPI.UnityToolkit.GUIDesigner.Runtime;
using UnityEngine;
using UnityEngine.AI;

public class ForajidoBT : BehaviourRunner
{
    //BehaviourTree forajidobt = new BehaviourTree();

    //public Transform[] pivotArray;

    public List<Transform> PivotTransforms = new List<Transform>();

    public GameObject forajido;
    private int destination;

    BSRuntimeDebugger _debugger;

    protected override void Init()
    {
        _debugger = GetComponent<BSRuntimeDebugger>();
        PivotTransforms = PivotList.Instance.Pivots;
        base.Init();
    }    

    protected override BehaviourGraph CreateGraph()
    {

        Location valueDestination = (Location)Random.Range(0, 3);
        destination = (int)valueDestination;

        Location valueEscape = (Location)Random.Range(3, 6);
        int Escape = (int)valueEscape;

        BehaviourTree forajidobt = new BehaviourTree();

        //var BankPos = new Vector3(PivotTransforms[(int)Location.BANCO].position.x, transform.position.y, PivotTransforms[(int)Location.BANCO].position.z);
        var Destino = new Vector3(PivotTransforms[destination].position.x, transform.position.y, PivotTransforms[destination].position.z);

        Debug.Log(PivotTransforms[destination].position);

        /*var Entrada1Pos = new Vector3(PivotTransforms[(int)Location.ENTRADA1].position.x, transform.position.y, PivotTransforms[(int)Location.ENTRADA1].position.z);
        var Entrada2Pos = new Vector3(PivotTransforms[(int)Location.ENTRADA2].position.x, transform.position.y, PivotTransforms[(int)Location.ENTRADA2].position.z);
        var Entrada3Pos = new Vector3(PivotTransforms[(int)Location.ENTRADA3].position.x, transform.position.y, PivotTransforms[(int)Location.ENTRADA3].position.z);*/

        var Salida = new Vector3(PivotTransforms[Escape].position.x, transform.position.y, PivotTransforms[Escape].position.z);

        var Fuera = new Vector3(PivotTransforms[(int)Location.FUERA].position.x, transform.position.y, PivotTransforms[(int)Location.FUERA].position.z);

        var walkToDestAction = new WalkAction(Destino); 
        var walkToEscapeAction = new WalkAction(Salida);
        var stealAction = new FunctionalAction(EnterBank, () => Status.Success);
        var salirAction = new FunctionalAction(OuterBank, () => Status.Success);
        var detainAction = new FunctionalAction(Detained, () => Status.Success);
        //var Duel = new FunctionalAction(EnterBank);
        //var walkToMineAction = new WalkAction(MinePos);
        //var walkToTavernAction = new WalkAction(TavernPos);

        var walkToDestination = forajidobt.CreateLeafNode("walkToDestination", walkToDestAction); // Camina al destino
        var steal = forajidobt.CreateLeafNode("steal", stealAction);
        var salir = forajidobt.CreateLeafNode("salir", salirAction);
        var walkToEscape = forajidobt.CreateLeafNode("walkToEscape", walkToEscapeAction); // Camina al banco
        var timer = forajidobt.CreateDecorator<UnityTimerDecorator>("Timer", salir).SetTotalTime(5f);

        //var walkToMine = forajidobt.CreateLeafNode("walkToMine", walkToMineAction); // Camina a la mina
        //var walkToTavern = forajidobt.CreateLeafNode("walkToTavern", walkToTavernAction); // Camina a la taberna

        var seq = forajidobt.CreateComposite<SequencerNode>("key seq", false, walkToDestination, steal, timer, walkToEscape);
        forajidobt.SetRootNode(seq);
        _debugger.RegisterGraph(forajidobt, "main");
        return forajidobt;
    }

    private void EnterBank()
    {
        Debug.Log("EnterBank");
        DesaparecerForajido();
    }

    private void OuterBank()
    {
        Debug.Log("OuterBank");
        AparecerForajido();
    }

    private void Detained()
    {
        
    }

    private void DesaparecerForajido()
    {
        // Tepear al forajido fuera de la escena
        Debug.Log("Desaparesco");
        forajido.transform.position = new Vector3(PivotTransforms[(int)Location.FUERA].position.x, PivotTransforms[(int)Location.FUERA].position.y, PivotTransforms[(int)Location.FUERA].position.z);
    }

    private void AparecerForajido()
    {
        Debug.Log("Aparesco");
        // El forajido reaparece
        forajido.transform.position = new Vector3(PivotTransforms[destination].position.x, transform.position.y, PivotTransforms[destination].position.z);
    }

}
