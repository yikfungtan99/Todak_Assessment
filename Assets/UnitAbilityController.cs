using System;
using UnityEngine;

public partial class UnitAbilityController : UnitBehaviour
{
    [Header("Dependencies")]
    [SerializeField] private Projectile attackProjectilePrefab;
    [SerializeField] private CanvasGroup straightRangeIndicator;
    [SerializeField] private CanvasGroup pointRangeIndicator;
    [SerializeField] private CanvasGroup rangeRadius;

    [Header("Properties")]
    [SerializeField] private float showTime;
    [SerializeField] private float pointRangeAimSpeed;

    private float curShowTime;
    private float curFadeTime;

    private bool isAiming;
    private bool isStraightAim = false;
    private bool isPointAim = false;

    private Quaternion straightAimDirection;
    private Quaternion pointAimDirection;

    private Ability[] abilities;

    private Ability currentAbility = null;

    public Projectile AttackProjectilePrefab { get => attackProjectilePrefab; }

    private void Start()
    {
        abilities = new Ability[3]
        {
            new TeleportAbility(unit),
            new PositionalAbility(unit),
            new SphereAbility(unit),
        };
    }

    private void Update()
    {
        for (int i = 0; i < abilities.Length; i++)
        {
            abilities[i].UpdateAbility();
        }

        if (isAiming)
        {
            curFadeTime = 0;
            curShowTime += Time.deltaTime;

            float t = Mathf.Clamp01(curShowTime / showTime);

            float alpha = Mathf.Lerp(0f, 1f, t);
            rangeRadius.alpha = alpha;

            if (isStraightAim)
            {
                straightRangeIndicator.alpha = alpha;
                Quaternion newRotation = Quaternion.Euler(90f, straightAimDirection.eulerAngles.y, 0);
                straightRangeIndicator.transform.rotation = newRotation;
            }

            if (isPointAim)
            {
                pointRangeIndicator.alpha = alpha;
                Vector3 forwardDirection = pointAimDirection * Vector3.forward;
                pointRangeIndicator.transform.position += forwardDirection * Time.deltaTime * pointRangeAimSpeed;
            }
        }
        else
        {
            straightRangeIndicator.alpha = 0;
            pointRangeIndicator.alpha = 0;
            rangeRadius.alpha = 0;
        }
    }

    public void Perform()
    {
        if (currentAbility != null)
        {
            PerformAbility(transform.rotation);
        }
        else
        {
            Attack(transform.rotation);
        }
    }

    public void Perform(Quaternion rotation)
    {
        if (currentAbility != null)
        {
            PerformAbility(rotation);
        }
        else
        {
            Attack(rotation);
        }
    }

    public void Attack(Quaternion rotation)
    {
        Projectile projInstance = Instantiate(attackProjectilePrefab, transform.position, rotation);
        projInstance.SetOwner(unit);
    }

    public void StraightAim(Quaternion aim)
    {
        isAiming = true;
        isStraightAim = true;
        straightAimDirection = aim;
    }

    public void PointAim(Quaternion aim)
    {
        isAiming = true;
        isPointAim = true;
        pointAimDirection = aim;
    }

    public void EndAim()
    {
        isAiming = false;
        isStraightAim = false;
        isPointAim = false;
        pointRangeIndicator.transform.position = new Vector3(transform.position.x, pointRangeIndicator.transform.position.y, transform.position.z);
    }

    public void SetAbility(int abilityIndex)
    {
        if (abilityIndex < 0 || abilityIndex >= abilities.Length)
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