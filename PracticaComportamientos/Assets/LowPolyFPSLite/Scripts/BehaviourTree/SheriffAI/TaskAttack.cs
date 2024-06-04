
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskAttack : Node
{

    private Transform _lastTarget;
    private BasicStats _enemyManager;

    private float _attackTime = 1f;
    private float _attackCounter = 0f;

    public TaskAttack(Transform transform)
    {
        Debug.Log("ataco");
    }

    public override NodeState Evaluate()
    {
        Transform target = (Transform)GetData("target");
        if (target != _lastTarget)
        {
            _enemyManager = target.GetComponent<BasicStats>();
            _lastTarget = target;
        }

        _attackCounter += Time.deltaTime;
        if (_attackCounter >= _attackTime)
        {
            bool enemyIsDead = _enemyManager.UpdateHP(50);
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
