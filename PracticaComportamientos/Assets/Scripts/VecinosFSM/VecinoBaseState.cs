using UnityEngine;

public abstract class VecinoBaseState
{
    public abstract void EnterState(VecinoStateManager vecino);

    public abstract void UpdateState(VecinoStateManager vecino);
}
