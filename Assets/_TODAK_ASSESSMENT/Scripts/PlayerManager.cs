using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : SingletonBehaviour<PlayerManager>
{
    [SerializeField] private Unit currentUnit;

    public Unit CurrentUnit { get => currentUnit; }
}
