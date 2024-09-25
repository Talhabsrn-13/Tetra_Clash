using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITouchArea
{
    Transform Transform { get; }
    public bool IsTouched { get; }
    void StartToDrag(Vector2 touchPosition);
    void StopToDrag(Vector2 lastPosition);
    void OnDrag(Vector2 touchPosition);

}