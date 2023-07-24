using System.Collections.Generic;
using UnityEngine;

public abstract class MovementController : MonoBehaviour
{
    
    public float moveDistance = 1f;

    public abstract void Move();
    
}