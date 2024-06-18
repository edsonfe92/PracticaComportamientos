using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TaskGoToTarget : Node
{
    private Transform _transform;

    public TaskGoToTarget(Transform transform)
    {
        _transform = transform;
    }

    public override NodeState Evaluate()
    {
        Transform target = (Transform)GetData("target");

        if (Vector3.Distance(_transform.position, target.position) > 0.5f)
        {
            _transform.GetComponent<SheriffBT>().agentNavMesh.SetDestination(new Vector3(target.position.x, _transform.position.y, target.position.z));

        }

        state = NodeState.RUNNING;
        return state;
    }

}