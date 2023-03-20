using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityHUD : MonoBehaviour
{
    [SerializeField] private AbilityJoyStick attackJoystick;
    [SerializeField] private AbilityJoyStick[] abilityJoysticks;

    // Start is called before the first frame update
    void Start()
    {
        UnitAbilityController unitAbility = PlayerManager.Instance.CurrentUnit.UnitAbility;
        attackJoystick.SetAbility(unitAbility.AttackAbility);

        for (int i = 0; i < unitAbility.Abilities.Count; i++)
        {
            if (i >= abilityJoysticks.Length) break;
            abilityJoysticks[i].SetAbility(unitAbility.Abilities[i]);
        }
    }
}
