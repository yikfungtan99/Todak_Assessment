using UnityEngine;

[RequireComponent(typeof(Unit))]
public class UnitBehaviour : MonoBehaviour
{
    protected Unit unit;

    protected virtual void Awake()
    {
        unit = GetComponent<Unit>();
    }
}
