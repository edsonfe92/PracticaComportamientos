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

    public float tiempoDeEspera = 5.0f; // Tiempo en segundos antes de que el forajido aparezca nuevamente

    private float tiempoPasado = 0.0f;
    
    BSRuntimeDebugger _debugger;

    protected override void Init()
    {       
        _debugger = GetComponent<BSRuntimeDebugger>();
        base.Init();
    }

    

    protected override BehaviourGraph CreateGraph()
    {
        BehaviourTree forajidobt = new BehaviourTree();

        var BankPos = PivotTransforms[(int)Location.BANCO].position;

        Debug.Log(PivotTransforms[(int)Location.BANCO].position);

        var Entrada1Pos = PivotTransforms[(int)Location.ENTRADA1].position;
        var Entrada2Pos = PivotTransforms[(int)Location.ENTRADA2].position;
        var Entrada3Pos = PivotTransforms[(int)Location.ENTRADA3].position;
        var Fuera = PivotTransforms[(int)Location.FUERA].position;
        //var MinePos = PivotTransforms[(int)Location.BANCO].position;
        //var TavernPos = PivotTransforms[(int)Location.BANCO].position;

        var walkToBankAction = new WalkAction(BankPos); 
        var walkToEscapeAction = new WalkAction(Entrada1Pos);
        var stealAction = new FunctionalAction(EnterBank);
        var detainAction = new FunctionalAction(Detained);
        //var Duel = new FunctionalAction(EnterBank);
        //var walkToMineAction = new WalkAction(MinePos);
        //var walkToTavernAction = new WalkAction(TavernPos);

        var walkToBank = forajidobt.CreateLeafNode("walkToBank", walkToBankAction); // Camina al banco
        var steal = forajidobt.CreateLeafNode("steal", stealAction);
        var walkToEscape = forajidobt.CreateLeafNode("walkToEscape", walkToEscapeAction); // Camina al banco
        
        //var walkToMine = forajidobt.CreateLeafNode("walkToMine", walkToMineAction); // Camina a la mina
        //var walkToTavern = forajidobt.CreateLeafNode("walkToTavern", walkToTavernAction); // Camina a la taberna

        var seq = forajidobt.CreateComposite<SequencerNode>("key seq", false, walkToBank, steal, walkToEscape);
        forajidobt.SetRootNode(seq);
        _debugger.RegisterGraph(forajidobt, "main");
        return forajidobt;
    }

    private void EnterBank()
    {
        Debug.Log("EnterBank");
        GameObject forajido = GameObject.Find("Character");
        DesaparecerForajido(forajido);
        //Tiene que pasar tiempo o algo aquí
        if (!gameObject.activeSelf)
        {
            tiempoPasado += Time.deltaTime;

            // Verificar si ha pasado el tiempo de espera
            if (tiempoPasado >= tiempoDeEspera)
            {
                // Ha pasado el tiempo de espera, hacer que la persona aparezca nuevamente
                AparecerForajido(forajido);
            }
        }
    }

    private void Detained()
    {
        
    }

    private void DesaparecerForajido(GameObject forajido)
    {
        // Desactivar el GameObject del forajido
        Debug.Log("Desaparesco");
        forajido.transform.position = PivotTransforms[(int)Location.FUERA].position;
    }

    private void AparecerForajido(GameObject forajido)
    {
        // Activar el GameObject de la persona
        forajido.transform.position = PivotTransforms[(int)Location.BANCO].position;

        // Reiniciar el tiempo pasado
        tiempoPasado = 0.0f;
    }

}
