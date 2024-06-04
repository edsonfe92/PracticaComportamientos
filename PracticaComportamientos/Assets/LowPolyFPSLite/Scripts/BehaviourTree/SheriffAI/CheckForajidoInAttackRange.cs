using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CheckForajidoInAttackRange : Node
{
    private Transform _transform;

    public CheckForajidoInAttackRange(Transform transform)
    {
        _transform = transform;
    }

    public override NodeState Evaluate()
    {
        object t = GetData("target");
        if (t == null)
        {
            state = NodeState.FAILURE;
            return state;
        }

        Transform target = (Transform)t;
        if (Vector3.Distance(_transform.position, target.position) <= SheriffBT.attackRange)
        {

            state = NodeState.SUCCESS;
            return state;
        }

        state = NodeState.FAILURE;
        return state;
    }

}
