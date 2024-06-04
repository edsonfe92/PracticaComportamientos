using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;



public class SheriffBT : Tree
{
    public UnityEngine.Transform[] waypoints;

    public static float speed = 10f;
    public static float fovRange = 6f;
    public static float attackRange = 1f;
    
    public NavMeshAgent agentNavMesh;

    private void Awake() 
    {
        agentNavMesh.speed = speed;    
    }
    protected override Node SetupTree()
    {
        Node root = new Selector(new List<Node>
        {
            new Sequence(new List<Node>
            {
                new CheckForajidoInAttackRange(transform),
                new TaskAttack(transform),
            }),
            new Sequence(new List<Node>
            {
                new CheckForajidoInRange(transform),
                new TaskGoToTarget(transform),
            }),
            new TaskPatrol(transform, waypoints),
        });

        return root;
    }
}
