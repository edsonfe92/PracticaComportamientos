using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using BehaviourAPI.BehaviourTrees;
using BehaviourAPI.Core;
using BehaviourAPI.Core.Actions;
using BehaviourAPI.Core.Perceptions;
using BehaviourAPI.StateMachines;
using BehaviourAPI.UnityToolkit;
using BehaviourAPI.UnityToolkit.Demos;
using BehaviourAPI.UnityToolkit.GUIDesigner.Runtime;
using UnityEngine;
using UnityEngine.AI;

public class ForajidoBT : BehaviourRunner
{
    #region Variables

    public List<Transform> PivotTransforms = new List<Transform>();

    public GameObject forajido;
    private int destination;

    private int _sherifLayerMask = 1 << 7;
    BSRuntimeDebugger _debugger;

    private EnemyManager _healthController;

    private NavmeshAgentMovement _navMeshAgentMovement;
    [Header("Duelo")]
    [SerializeField] private Transform target;
    public GameObject bulletPrefab;
    public Transform bulletSpawnPoint;        
    public float bulletSpeed = 20f; 
    public float fireRate = 1f;          
    private float nextTimeToFire = 0f; 

    private float timeElapsed = 0;

    [Header("Atributos Forajido")]
    public float range = 12f;
    
    #endregion

    protected override void Init()
    {
        _debugger = GetComponent<BSRuntimeDebugger>();
        _healthController = GetComponent<EnemyManager>();
        _navMeshAgentMovement = GetComponent<NavmeshAgentMovement>();
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
        FSM fsm = dueloFSM();
        
        
        var Destino = new Vector3(PivotTransforms[destination].position.x, transform.position.y, PivotTransforms[destination].position.z);        
        var Salida = new Vector3(PivotTransforms[Escape].position.x, transform.position.y, PivotTransforms[Escape].position.z);
        var Fuera = new Vector3(PivotTransforms[(int)Location.FUERA].position.x, transform.position.y, PivotTransforms[(int)Location.FUERA].position.z);

        
        #region Percepciones
        var sherifInRangePerception = new ConditionPerception(sherifInRange);
        var noSherifInRangePerception = new ConditionPerception(noSherifInRange);
        #endregion

        #region Acciones

        var walkToDestAction = new WalkAction(Destino); 
        var walkToEscapeAction = new WalkAction(Salida);
        var stealAction = new FunctionalAction(EnterBank, () => Status.Success);
        var salirAction = new FunctionalAction(OuterBank, () => Status.Success);
        var detainAction = new FunctionalAction(Detained, () => Status.Success); 

        //var dueloAction= new FunctionalAction(Duelo, ()=> Status.Success);     

        #endregion

        #region Nodos

        // Tarea principal forajido
        var walkToDestination = forajidobt.CreateLeafNode("walkToDestination", walkToDestAction); // Camina al destino
        var steal = forajidobt.CreateLeafNode("steal", stealAction);
        var salir = forajidobt.CreateLeafNode("salir", salirAction);
        var walkToEscape = forajidobt.CreateLeafNode("walkToEscape", walkToEscapeAction); // Camina al banco
        var timer = forajidobt.CreateDecorator<UnityTimerDecorator>("Timer", salir).SetTotalTime(5f);

        var seqTarea = forajidobt.CreateComposite<SequencerNode>("key seq", false, walkToDestination, steal, timer, walkToEscape);

        // Tarea Duelo
        var duelo = forajidobt.CreateLeafNode("duelo", new SubsystemAction(fsm)); 
        //var duelo = forajidobt.CreateLeafNode("duelo", dueloAction); 

        // Condicionales
        var condSherif = forajidobt.CreateDecorator<ConditionNode>(duelo);
        condSherif.SetPerception(sherifInRangePerception);

        var reactiveCond = forajidobt.CreateDecorator<ReactiveConditionNode>(seqTarea);
        reactiveCond.SetPerception(noSherifInRangePerception);
        reactiveCond.InterruptOptions = ExecutionInterruptOptions.Pause;
                
        // Cabecera principal
        var sel = forajidobt.CreateComposite<SelectorNode>("main sel", false,condSherif, reactiveCond);
        var loop = forajidobt.CreateDecorator<LoopNode>("loop", sel).SetIterations(-1);
        
        #endregion

        #region Debug & Setup        
        forajidobt.SetRootNode(loop);
        _debugger.RegisterGraph(forajidobt, "main");
        _debugger.RegisterGraph(fsm, "duelo");
        return forajidobt;
        #endregion
    }
    private FSM dueloFSM()
    {
        var fsm = new FSM();

        //Perceptions

        var getHit = new ConditionPerception(() => _healthController.GotHit());                
        var dodgeComplete = new ConditionPerception(() => _navMeshAgentMovement.HasArrived());

        var targetNotInRangePerception = new ConditionPerception(TargetNotInRange);


        //Actions
        var dodgeAction = new FunctionalAction(dodge, ()=> Status.Success);
        var moveTowarsTarget = new FunctionalAction(MoveToTarget, ()=> Status.Success);

        //States
        FSM subFSM = SubDuel();
        var fightState = fsm.CreateState("fightState", new SubsystemAction(subFSM));
        var moveState = fsm.CreateState("moveState", dodgeAction);
        var chaseState = fsm.CreateState("chaseState", moveTowarsTarget);


        //Transitions
        fsm.CreateTransition("got hit", fightState, moveState, getHit);
        fsm.CreateTransition("dodged", moveState, fightState, dodgeComplete);

        fsm.CreateTransition("out of range", fightState,chaseState,targetNotInRangePerception);
        fsm.CreateTransition("target in range", chaseState,fightState,dodgeComplete);

        fsm.CreateExitTransition(fightState, Status.Success, statusFlags: StatusFlags.Success);

        _debugger.RegisterGraph(subFSM, "subduelo");
        return fsm;
    }
    private FSM SubDuel()
    {
        var fsmDuel = new FSM();
        
        //Perceptions
        var noBullets = new ConditionPerception(() => _healthController.NoBullets());
        var fullAmmo = new ConditionPerception(() => _healthController.Reload());

        var duelWon = new ConditionPerception(() => target == null);
        
        //Actions
        var shootAction = new FunctionalAction(Shoot, ShootUpdate);
        var reloadTimeAction = new DelayAction(10);

        //States
        var shootState = fsmDuel.CreateState("shoot", shootAction);
        var realoadState = fsmDuel.CreateState("reloadState", reloadTimeAction);

        //Transitions        
        fsmDuel.CreateTransition("no bullets", shootState,realoadState,noBullets);
        fsmDuel.CreateTransition("reload", realoadState,shootState,fullAmmo);

        fsmDuel.CreateExitTransition(shootState,Status.Success,duelWon);

        return fsmDuel;
    }

    #region Implementacion Funcionalidades

    #region Main Sequence
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

    #endregion
    
    #region Duelo

    private void Duelo()
    {
        Debug.Log("DUELO AAAAAAAAAAAAAAAAAAAAAAAAA");
    }

    private void Shoot()
    {
        //_healthController.Shoot();
        //TODO SALGA LA BALA E IMPACTE

    }
    private Status ShootUpdate()
    {                
        timeElapsed += Time.deltaTime;
        if(_healthController.NoBullets())
        {
            Debug.Log("No bullets");
            return Status.Success;
        }   

        if (target != null && Vector3.Distance(transform.position, target.position) <= range+1.5f)
        {            

            Vector3 direction = (target.position - transform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);

            if(timeElapsed >= fireRate)
            {              
                _healthController.Shoot();
                GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
                Rigidbody rb = bullet.GetComponent<Rigidbody>();
                rb.velocity = bulletSpawnPoint.forward * bulletSpeed;  
                timeElapsed = 0f;                            
            }
        }
        return Status.Running;
        
    }
    private void MoveToTarget()
    {
        
        
        if(target != null)
        {
            Vector3 direction = (target.position - transform.position).normalized;            
            Vector3 newPos =  transform.position + direction * 3f;            
            _navMeshAgentMovement.SetTarget(newPos);

        }
    }

    private void dodge()
    {        
        _navMeshAgentMovement.SetTarget(GetRandomPositionAtDistance());            

    }    

    public Vector3 GetRandomPositionAtDistance(float distance = 2f)
    {        
        Debug.Log("DODGE POSITION CALCULATED?");
        Vector2 randomDirection = Random.insideUnitCircle.normalized;
        
        Vector3 newPosition = transform.position + new Vector3(randomDirection.x, 0, randomDirection.y) * distance;

        return newPosition;
    }


    #endregion

    #region Percepciones
    
    private bool TargetNotInRange()
    {
        return (target != null) && (Vector3.Distance(transform.position, target.position) > range+1.5f);
        
    }

    private bool sherifInRange()
    {
        Collider[] colliders = Physics.OverlapSphere(  // comprobar en un rango si hay un sherif cerca
                transform.position, range, _sherifLayerMask);
            
        if (colliders.Length > 0) 
        {
            target = colliders[0].transform;
            return true;   
        }
        else return false;
    
    }

    private bool noSherifInRange()
    {
        Collider[] colliders = Physics.OverlapSphere(  // comprobar en un rango si hay un sherif cerca
                transform.position, range, _sherifLayerMask);
            
        if (colliders.Length > 0) return false;
        else return true;
        
    }

    #endregion
    
    #endregion

 

}
