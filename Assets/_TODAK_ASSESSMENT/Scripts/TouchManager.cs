using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchManager : SingletonBehaviour<TouchManager>
{
    [HideInInspector] public List<int> FingerIDs = new List<int>();
}
