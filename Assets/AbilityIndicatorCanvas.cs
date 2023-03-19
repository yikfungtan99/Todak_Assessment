using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityIndicatorCanvas : MonoBehaviour
{
    [SerializeField] private Vector3 offset;

    private Unit unit;

    // Start is called before the first frame update
    void Start()
    {
        unit = PlayerManager.Instance.CurrentUnit;
    }

    private void Update()
    {
        transform.position = unit.transform.position + offset;
    }
}
