using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;



public class SheriffBT : Tree
{
    public UnityEngine.Transform[] waypoints;

    public static float speed = 15f;
    public static float fovRange = 15f;
    public static float attackRange = 10f;
    public static float bulletSpeed = 20f; 
    
    public NavMeshAgent agentNavMesh;

    public EnemyManager _healthManager;

    public GameObject _bulletPrefab;

    public Transform _bulletSpawn;

    private void Awake() 
    {
        agentNavMesh.speed = speed;   
        _healthManager = GetComponent<EnemyManager>();
    }
    protected override Node SetupTree()
    {
        Node root = new Selector(new List<Node>
        {
            new Sequence(new List<Node>
            {
                new CheckAmmo(transform),                
            }),
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

    public void ShootBullet()
    {
        GameObject bullet = Instantiate(_bulletPrefab, _bulletSpawn.position, _bulletSpawn.rotation);
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        rb.velocity = _bulletSpawn.forward * bulletSpeed; 
    }
}
