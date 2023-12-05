using System.Collections;
using System.Collections.Generic;
using BehaviourAPI.BehaviourTrees;
using BehaviourAPI.Core;
using BehaviourAPI.Core.Actions;
using BehaviourAPI.UnityToolkit;
using UnityEngine;
using UnityEngine.AI;

public class ForajidoBT : BehaviourRunner
{
    //BehaviourTree forajidobt = new BehaviourTree();

    //public Transform[] pivotArray;

    
    public List<Transform> PivotTransforms = new List<Transform>();

    public float tiempoDeEspera = 5.0f; // Tiempo en segundos antes de que el forajido aparezca nuevamente

    private float tiempoPasado = 0.0f;

    protected override void Init()
    {
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
        //var MinePos = PivotTransforms[(int)Location.BANCO].position;
        //var TavernPos = PivotTransforms[(int)Location.BANCO].position;

        var walkToBankAction = new WalkAction(BankPos); 
        var walkToEscapeAction = new WalkAction(Entrada1Pos);
        //var walkToMineAction = new WalkAction(MinePos);
        //var walkToTavernAction = new WalkAction(TavernPos);

        var walkToBank = forajidobt.CreateLeafNode("walkToBank", walkToBankAction); // Camina al banco
        var stealAction = new FunctionalAction(EnterBank);
        var walkToEscape = forajidobt.CreateLeafNode("walkToEscape", walkToEscapeAction); // Camina al banco
        var detainAction = new FunctionalAction(Detained);
        var Duel = new FunctionalAction(EnterBank, () => Status.Success);
        //var walkToMine = forajidobt.CreateLeafNode("walkToMine", walkToMineAction); // Camina a la mina
        //var walkToTavern = forajidobt.CreateLeafNode("walkToTavern", walkToTavernAction); // Camina a la taberna
        forajidobt.SetRootNode(walkToBank);
        return forajidobt;
    }
    private void Start()
    {
          
    }

    private void EnterBank()
    {
        DesaparecerForajido();
        //Tiene que pasar tiempo o algo aquí
        if (!gameObject.activeSelf)
        {
            tiempoPasado += Time.deltaTime;

            // Verificar si ha pasado el tiempo de espera
            if (tiempoPasado >= tiempoDeEspera)
            {
                // Ha pasado el tiempo de espera, hacer que la persona aparezca nuevamente
                AparecerForajido();
            }
        }
    }

    private void Escape()
    {
        //Logica en la que el forajido sale y corre a ala salida
    }
    private void Detained()
    {
        
    }

    private void DesaparecerForajido()
    {
        // Desactivar el GameObject del forajido
        Debug.Log("Desaparesco");
        gameObject.SetActive(false);
    }

    private void AparecerForajido()
    {
        // Activar el GameObject de la persona
        gameObject.SetActive(true);

        // Reiniciar el tiempo pasado
        tiempoPasado = 0.0f;
    }

    private void Update()
    {

    }

}
