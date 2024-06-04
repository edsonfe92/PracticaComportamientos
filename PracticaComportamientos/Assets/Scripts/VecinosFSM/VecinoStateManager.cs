using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VecinoStateManager : MonoBehaviour
{
    public List<Transform> PivotTransforms = new List<Transform>();
    VecinoBaseState currentState;
    public VecinoWalkState WalkingState = new VecinoWalkState();
    public VecinoPanicState PanicState = new VecinoPanicState();
    public Location spawn;
    // Start is called before the first frame update
    void Start()
    {
        PivotTransforms = PivotList.Instance.Pivots;
        currentState = WalkingState;
        currentState.EnterState(this);
    }

    // Update is called once per frame
    void Update()
    {
        currentState.UpdateState(this);
    }

    public void SwitchState(VecinoBaseState state)
    {
        currentState = state;
        state.EnterState(this);
    }
}
