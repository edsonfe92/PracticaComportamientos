using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

public class TaskPatrol : Node
{
    private Transform _transform;
    private Transform[] _waypoints;
    
        
    private int _currentWaypointIndex;

    private float _waitTime = 1f;
    private float _waitCounter = 0f;
    private bool _waiting = false;

    public TaskPatrol(Transform transform, Transform[] waypoints)
    {
        _transform = transform;
        _waypoints = waypoints;
        
    }


    public override NodeState Evaluate()
    {
        if (_waiting)
        {
            _waitCounter += Time.deltaTime;
            if (_waitCounter >= _waitTime)
                _waiting = false;
        }
        else
        {
            Transform wp = _waypoints[_currentWaypointIndex];
            if (Vector3.Distance(_transform.position, wp.position) < 3.0f)
            {
                _transform.position = wp.position;
                _waitCounter = 0f;
                _waiting = true;

                _currentWaypointIndex = (_currentWaypointIndex + 1) % _waypoints.Length;

            }
            else
            {
                //_transform.position = Vector3.MoveTowards(_transform.position, wp.position, SheriffBT.speed * Time.deltaTime);
                _transform.GetComponent<SheriffBT>().agentNavMesh.SetDestination(new Vector3(wp.position.x, _transform.position.y, wp.position.z));
                //_transform.LookAt(wp.position);
            }
        }

        state = NodeState.RUNNING;
        return state;

    }
}
