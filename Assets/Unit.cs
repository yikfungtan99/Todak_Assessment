using UnityEngine;

public class Unit : MonoBehaviour
{
    [Header("Dependencies")]
    [SerializeField] private UnitMovement unitMovement;
    [SerializeField] private UnitAbilityController unitAbility;

    public UnitAbilityController UnitAbility { get => unitAbility; }
    public UnitMovement UnitMovement { get => unitMovement; }
}