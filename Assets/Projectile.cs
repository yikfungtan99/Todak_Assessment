using System;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [Header("Properties")]
    [SerializeField] private float projectileSpeed;
    [SerializeField] private float effectRadius;
    [SerializeField] private LayerMask affectedLayerMask;

    private Unit owner = null;

    private void Update()
    {
        transform.position += transform.forward * projectileSpeed * Time.deltaTime;

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