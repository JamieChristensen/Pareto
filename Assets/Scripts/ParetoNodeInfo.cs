using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParetoNodeInfo : MonoBehaviour
{
    public float bestTrack1;
    public float bestTrack2;
    public float bestTrack3;

    public bool shouldDraw = false;
    public bool isAnimated = false;

    [Range(0.5f, 5f)]
    public float size = 0.5f;

    [Range(0.2f, 1f)]
    public float speed = 0.3f;

    void OnDrawGizmos()
    {
        if (!shouldDraw)
        {
            return;
        }
        // Draw a yellow sphere at the transform's position
        Color col = Color.yellow;
        col.a = 0.2f;
        Gizmos.color = col;


        float offset = size / 2f;

        Gizmos.DrawCube(transform.position + new Vector3(offset, offset, offset), new Vector3(size, size, size));

        if (!isAnimated)
        {
            return;
        }
        size += Time.deltaTime * speed;
        if (size > 4)
        {
            size = 0.5f;
        }
    }
}

