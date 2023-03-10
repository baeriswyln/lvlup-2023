using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode] [RequireComponent(typeof(Renderer))] 
public class DepthSortByY : MonoBehaviour
{

    private const int IsometricRangePerYUnit = 100;

    public float yOffset = 0f;

    void Update()
    {
        Renderer renderer = GetComponent<Renderer>();
        renderer.sortingOrder = -(int)((transform.position.y + yOffset) * IsometricRangePerYUnit);
    }
}
