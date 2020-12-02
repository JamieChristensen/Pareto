using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MathUtility;

public class CurveToMeshGeneration : MonoBehaviour
{
    public bool generateBumpyMapOnStart = false;
    public AnimationCurve curve;
    public int size;


    public Mesh mesh;

    private Vector3[] vertices;
    private const int bumps = 25;


    private void Awake()
    {
        if (generateBumpyMapOnStart)
        {
            Keyframe[] keys = new Keyframe[bumps];
            for (int i = 0; i < bumps; i++)
            {
                float value = (((float)(i % 2) * 2) - 1) * 0.2f + 0.5f;
                if (i < (3) || i > bumps - (3)) value = 0.5f;
                Keyframe key = new Keyframe(i, value);
                //int keyInt = curve.AddKey(key);
                keys[i] = key;
            }
            curve.keys = keys;
        }
        Generate();
    }

    private void Generate()
    {
        //WaitForSeconds wait = new WaitForSeconds(0.0f);
        int xSize = size;
        int ySize = size;


        GetComponent<MeshFilter>().mesh = mesh = new Mesh();
        mesh.name = "Procedural Grid";

        vertices = new Vector3[(xSize + 1) * (ySize + 1)];
        for (int i = 0, y = 0; y <= ySize; y++)
        {
            float mapMax = generateBumpyMapOnStart ? bumps : 1;
            float yReal = MathUtils.map(curve.Evaluate(MathUtils.map(y, 0, ySize, 0, mapMax)), 0, mapMax, 0, size);
            for (int x = 0; x <= xSize; x++, i++)
            {

                vertices[i] = new Vector3(x, yReal, y);
                //yield return wait;
            }
        }
        mesh.vertices = vertices;

        int[] triangles = new int[xSize * ySize * 6 * 2];
        for (int ti = 0, vi = 0, y = 0; y < ySize; y++, vi++)
        {
            for (int x = 0; x < xSize; x++, ti += 6, vi++)
            {
                triangles[ti] = vi;
                triangles[ti + 4] = triangles[ti + 1] = vi + 1;
                triangles[ti + 3] = triangles[ti + 2] = vi + xSize + 1;
                triangles[ti + 5] = vi + xSize + 2;
            }
        }
        mesh.triangles = triangles;
        mesh.RecalculateNormals();
        GetComponent<MeshCollider>().sharedMesh = mesh;
    }

    /*
        private void OnDrawGizmos()
        {
            if (vertices == null)
            {
                return;
            }
            Gizmos.color = Color.black;
            for (int i = 0; i < vertices.Length; i++)
            {
                Gizmos.DrawSphere(vertices[i], 0.1f);
            }
        }
        */
}
