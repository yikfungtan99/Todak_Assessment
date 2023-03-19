using UnityEngine;

public class Unit : MonoBehaviour
{
    [SerializeField] private UnitAttack unitAttack;

    public UnitAttack UnitAttack { get => unitAttack; }
}