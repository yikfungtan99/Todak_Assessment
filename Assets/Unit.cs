using UnityEngine;

public class Unit : MonoBehaviour
{
    [SerializeField] private UnitMovement unitMovement;
    [SerializeField] private UnitAbilityController unitAttack;

    public UnitAbilityController UnitAttack { get => unitAttack; }
    public UnitMovement UnitMovement { get => unitMovement; }
}