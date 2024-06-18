using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckAmmo : Node
{
    
    private Transform _transform;
    private float timer = 0f;
    private float ammoReloadTime = 3f;
    public CheckAmmo(Transform transform)
    {
        _transform = transform;
    }

    public override NodeState Evaluate()
    {
        if(!_transform.GetComponent<SheriffBT>()._healthManager.NoBullets())
        {            
            state = NodeState.FAILURE;
            return state;
        }

        timer += Time.deltaTime;        
        if(_transform.GetComponent<SheriffBT>()._healthManager.NoBullets())
        {
                   
            if (timer >= ammoReloadTime)
            {                
                _transform.GetComponent<SheriffBT>()._healthManager.Reload();
                timer = 0;                
                state = NodeState.SUCCESS;
                return state;
            }
        }
        
        state = NodeState.RUNNING;
        return state;
    }
}
