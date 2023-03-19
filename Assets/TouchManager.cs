using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchManager : SingletonBehaviour<TouchManager>
{
    public List<int> FingerIDs = new List<int>();
}
