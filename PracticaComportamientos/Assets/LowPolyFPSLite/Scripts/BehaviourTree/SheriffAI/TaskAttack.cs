
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskAttack : Node
{

    private Transform _lastTarget;
    private Transform _transform;
    
    private SheriffBT _sherif;
    private EnemyManager _enemyManager;

    private float _attackTime = 1f;

    private float _attackCounter = 0f;    

    public TaskAttack(Transform transform)
    {        
        _transform = transform;
        _sherif = transform.gameObject.GetComponent<SheriffBT>();        
    }

    public override NodeState Evaluate()
    {
        Transform target = (Transform)GetData("target");
        if (target != _lastTarget)
        {
            _enemyManager = target.GetComponent<EnemyManager>();
            _lastTarget = target;
        }

        Vector3 direction = (target.position - _transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        _transform.rotation = Quaternion.Slerp(_transform.rotation, lookRotation, Time.deltaTime * 5f);

        _attackCounter += Time.deltaTime;
        if (_attackCounter >= _attackTime)
        {
            _sherif._healthManager.Shoot();
            
            _sherif.ShootBullet();

            
            bool enemyIsDead = target == null;

            if (enemyIsDead)
            {                
                ClearData("target");
            }
            else
            {
                _attackCounter = 0f;
            }
        }

        state = NodeState.RUNNING;
        return state;
    }

}
