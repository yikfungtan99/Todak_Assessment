using UnityEngine;

[System.Serializable]
public class SphereAbility : Ability
{
    public float skillDuration;
    public GameObject sphereAbilityPrefab;
    private GameObject sphereInstance = null;

    private float currentSkillTime = 0;

    public SphereAbility(Unit unit) : base(unit)
    {
    }

    public SphereAbility(SphereAbility ability, Unit unit) : base(ability, unit)
    {
        sphereAbilityPrefab = ability.sphereAbilityPrefab;
        skillDuration = ability.skillDuration;
    }

    public override void Perform()
    {
        if (sphereInstance != null) return;
        sphereInstance = GameObject.Instantiate(sphereAbilityPrefab, unit.transform.position, Quaternion.identity);
    }

    public override void UpdateAbility()
    {
        if (sphereInstance == null) return;

        if(currentSkillTime >= skillDuration)
        {
            currentSkillTime = 0;
            GameObject.Destroy(sphereInstance);
            return;
        }
        else
        {
            currentSkillTime += Time.deltaTime;
        }

        sphereInstance.transform.position = unit.transform.position;
    }
}