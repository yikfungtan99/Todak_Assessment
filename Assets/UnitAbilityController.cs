using System;
using System.Collections.Generic;
using UnityEngine;

public partial class UnitAbilityController : UnitBehaviour
{
    [Header("Dependencies")]
    [SerializeField] private Projectile attackProjectilePrefab;

    [Header("Properties")]
    [SerializeField] private AbilitySO attackAbilityObject;
    [SerializeField] private AbilitySO[] abilitiesObject;

    private Ability attackAbility;
    public Ability AttackAbility { get => attackAbility; }

    private List<Ability> abilities = new List<Ability>();
    public List<Ability> Abilities { get => abilities; }

    private Ability currentAbility = null;
    public Projectile AttackProjectilePrefab { get => attackProjectilePrefab; }

    protected override void Awake()
    {
        base.Awake();

        attackAbility = attackAbilityObject.InitAbility(unit);

        foreach (AbilitySO abilitySO in abilitiesObject)
        {
            abilities.Add(abilitySO.InitAbility(unit));
        }
    }

    private void Update()
    {
        for (int i = 0; i < abilities.Count; i++)
        {
            abilities[i].UpdateAbility();
        }
    }

    public void SetAbility(int abilityIndex)
    {
        if (abilityIndex < 0 || abilityIndex >= abilitiesObject.Length)
        {
            Debug.LogError($"{unit} selected null ability!");
            return;
        }

        currentAbility = abilities[abilityIndex];
    }

    public void PerformAbility(Quaternion aim)
    {
        if (currentAbility == null)
        {
            Debug.LogError($"{unit}'s no ability selected!");
        }

        currentAbility.Perform(aim);
        currentAbility = null;
    }
}