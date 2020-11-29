using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Mathematics;


public class VisualizeParetoFrontier : MonoBehaviour
{
    public bool useDefaultLoading;

    public GameObject nodePrefab;

    public List<Vector3> fitnessList;



    public Mesh mesh;

    private List<Vector3> verticesList;


    public void Start()
    {
        Debug.Log("sup lol");
        if (useDefaultLoading)
        {
            LoadFitnessInLocal();
            Generate();
        }
        else
        {
            //Something with actual fitness values.
        }
    }

    public void LoadFitnessInLocal()
    {
        var _fitnessList = new List<Vector3>(10);

        float mult = 5;
        float yMax = 2;
        float sizes = 5;


        for (int i = 0; i < 5; i++)
        {
            for (int j = 0; j < 5; j++)
            {
                _fitnessList.Add(new Vector3(i * mult, Mathf.Min(Mathf.Max(i, j), yMax) * mult, j * mult));
            }
        }

        fitnessList = _fitnessList;
        Debug.Log("lol");
    }

    public void LoadFitnessIn(List<Vector3> _fitnessList)
    {
        //Something with actual fitness values.
    }



    private void Generate()
    {
        float xMax, yMax, zMax;
        xMax = yMax = zMax = 0;

        foreach (Vector3 point in fitnessList)
        {
            xMax = Mathf.Max(xMax, point.x);
            yMax = Mathf.Max(yMax, point.y);
            zMax = Mathf.Max(zMax, point.z);
            Instantiate(nodePrefab, point + transform.position, quaternion.identity, transform);

            
        
        }

        GetComponent<MeshFilter>().mesh = mesh = new Mesh();
        mesh.name = "Pareto Mesh";

        var vertices = fitnessList.ToArray();

        //vertices = new Vector3[(xSize + 1) * (ySize + 1)];


        /*
        for (int i = 0, y = 0; y <= ySize; y++)
        {
            for (int x = 0; x <= xSize; x++, i++)
            {
                vertices[i] = new Vector3(x, y);
            }
        }
        mesh.vertices = vertices;

        int[] triangles = new int[xSize * ySize * 6];
        for (int ti = 0, vi = 0, y = 0; y < ySize; y++, vi++)
        {
            for (int x = 0; x < xSize; x++, ti += 6, vi++)
            {
                triangles[ti] = vi;
                triangles[ti + 3] = triangles[ti + 2] = vi + 1;
                triangles[ti + 4] = triangles[ti + 1] = vi + xSize + 1;
                triangles[ti + 5] = vi + xSize + 2;
            }
        }
        mesh.triangles = triangles;
        mesh.RecalculateNormals();
        */

    }


    private void Visualize()
    {
        var modadd = new ModAdd(4);
        modadd.pp(1, 7);



    }




    struct ModAdd
    {
        private int value;

        public int pp(int step, int cap)
        {
            return (value + step) % cap;
        }

        public int GetValue()
        {
            return value;
        }

        public ModAdd(int _value)
        {
            value = _value;
        }
    }

    public Vector3 DirFromAtoB(Vector3 a, Vector3 b)
    {
        return -a + b;
    }
}
