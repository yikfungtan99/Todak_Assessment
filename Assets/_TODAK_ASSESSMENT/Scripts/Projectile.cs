using System;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [Header("Properties")]
    [SerializeField] private float projectileSpeed;
    [SerializeField] private float effectRadius;
    [SerializeField] private LayerMask affectedLayerMask;
    [SerializeField] private float decayTime = 3f;

    private Unit owner = null;
    public bool CanDestroyOtherUnits = true;

    public float DecayTime { get => decayTime; set => decayTime = value; }

    private void Start()
    {
        Destroy(gameObject, decayTime);
    }

    private void Update()
    {
        MoveProjectile();
        DestroyOtherUnits();
    }

    private void MoveProjectile()
    {
        transform.position += transform.forward * projectileSpeed * Time.deltaTime;
    }

    private void DestroyOtherUnits()
    {
        if (!CanDestroyOtherUnits) return;
        Collider[] colliders = Physics.OverlapSphere(transform.position, effectRadius, affectedLayerMask);

        if (colliders.Length > 0)
        {
            for (int i = 0; i < colliders.Length; i++)
            {
                Unit hitUnit = colliders[i].GetComponent<Unit>();

                if (hitUnit != null && hitUnit != owner)
                {
                    Destroy(hitUnit.gameObject);
                    Destroy(gameObject);
                }
            }
        }
    }

    public void SetOwner(Unit unit)
    {
        owner = unit;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, effectRadius);
    }
}