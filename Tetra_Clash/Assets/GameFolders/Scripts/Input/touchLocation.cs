using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class touchLocation
{
    public int touchId;
    public TouchArea circle;

    public touchLocation(int newTouchId, TouchArea newCircle)
    {
        touchId = newTouchId;
        circle = newCircle;
    }
}
