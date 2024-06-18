using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskReload : Node
{
    Transform _transform;
    public TaskReload(Transform transform)
    {
        _transform = transform;
    }

    public override NodeState Evaluate()
    {                
        Debug.Log("Reload");
        _transform.GetComponent<SheriffBT>()._healthManager.Reload();
        state = NodeState.SUCCESS;
        return state;                
    }

}
