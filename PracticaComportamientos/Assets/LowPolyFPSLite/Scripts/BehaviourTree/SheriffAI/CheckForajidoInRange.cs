using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CheckForajidoInRange : Node
{
    private static int _forajidoLayerMask = 1 << 6;

    private Transform _transform;

    public CheckForajidoInRange(Transform transform)
    {
        _transform = transform;
    }

    public override NodeState Evaluate()
    {
        object t = GetData("target");
        if (t == null)
        {
            Collider[] colliders = Physics.OverlapSphere(
                _transform.position, SheriffBT.fovRange, _forajidoLayerMask);

            if (colliders.Length > 0)
            {
                parent.parent.SetData("target", colliders[0].transform);
                
                state = NodeState.SUCCESS;
                return state;
            }

            state = NodeState.FAILURE;
            return state;
        }

        state = NodeState.SUCCESS;
        return state;
    }

}
