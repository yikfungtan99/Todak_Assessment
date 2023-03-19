using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchManager : SingletonBehaviour<TouchManager>
{
    private int currentTouchCount = 1;
    public int CurrentTouchCount { get => currentTouchCount; set => currentTouchCount = value; }
}
