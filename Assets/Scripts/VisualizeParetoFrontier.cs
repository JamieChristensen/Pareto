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

    public LineRenderer lineRenderer;

    public void Start()
    {
        Debug.Log("sup lol");
        if (useDefaultLoading)
        {
            LoadFitnessInLocal();
            StartCoroutine(Generate());
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

    private int[] TrianglesFromVertices(Vector3[] vertices)
    {
        List<int> triangles = new List<int>();

        //Take a pivot, figure out closest points within certain angles, add triangles to a list, create triangles[] from this.
        var points = new Point[vertices.Length];

        for (int j = 0; j < vertices.Length; j++)
        {
            points[j] = new Point
            {
                value = new float3(vertices[j].x, vertices[j].y, vertices[j].z),
                anglesOccupied = new List<float>(),
                isEdge = false,
                isCompletedPivot = false,
                index = j,
                connectedVerts = new List<Point>()
            };
        }

        //While-loop instead of recursion. Couldn't be arsed to do proper recursion. 
        Point currentPivot = points[0];
        int i = 0;
        while (i == 0)
        {
            i++;

            //Find closest point (on X & Z), check if it is a pivot or edge, if not so:
            float minDist = Mathf.Infinity;
            Point focus = null;
            for (int j = 0; j < points.Length; j++)
            {
                if (points[j] == currentPivot) continue;

                var dist = math.distance(points[j].value, currentPivot.value);
                if (dist < minDist)
                {
                    minDist = dist;
                    focus = points[j];
                }
            }
            currentPivot.focus = focus;
            //TODO: FIND ANGLES OCCUPIED AND NEST ANOTHER WHILE LOOP?????
            Point lastVertForTriangle = ClosestPointInAngle(points, currentPivot, 180);

            int firstVert = currentPivot.index;
            int secondVert = currentPivot.focus.value.x < lastVertForTriangle.value.x ? currentPivot.focus.index : lastVertForTriangle.index;
            int thirdVert = currentPivot.focus.value.x < lastVertForTriangle.value.x ? lastVertForTriangle.index : currentPivot.focus.index;
            triangles.Add(firstVert);
            triangles.Add(secondVert);
            triangles.Add(thirdVert);
            //Then add it to connectedVerts or trianglearray. figure it out.



            // TODO: FINISH PIVOT, THEN FIND NEW PIVOT. NEVER USE PIVOTS THAT HAVE BEEN USED BEFORE
        }
        Debug.Log("Number of iterations: " + i);

        int[] trianglearr = triangles.ToArray();

        return trianglearr;
    }

    private Point ClosestPointInAngle(Point[] points, Point pivot, float2 withinAngle)
    {
        Point closest = null;
        float distance = Mathf.Infinity;

        Vector3 dir = (-pivot.focus.value + pivot.value);
        dir = dir.normalized;

        foreach (Point point in points)
        {
            if (point == pivot)
            {
                continue;
            }

            if (point == pivot.focus)
            {
                continue;
            }

            //if the point is further away than a current point:
            var dist = math.distance(pivot.value, point.value);
            if (dist > distance)
            {
                continue;
            }

            Vector3 dirToPoint = -point.value + pivot.value;
            if (Vector3.Angle(dir, dirToPoint) < 180)
            {
                // NEED TO CHECK IF WITHIN THE ANGLE TOO!

                if (dist < distance)
                {
                    closest = point;
                    distance = dist;
                }
            }
            else
            {
                continue;
            }
        }

        Debug.Assert(pivot != closest && closest != null);

        return closest;
    }

    bool IsPointInAngleOfPoint()
    {
        return true;
    }


    bool AreAllPointsPivots(Point[] points)
    {
        foreach (Point point in points)
        {
            if (!point.isCompletedPivot)
            {
                return false;
            }
        }
        return true;
    }



    class Point
    {
        public float3 value;
        public Point focus; //Used when the point is a pivot.
        public List<float> anglesOccupied; //0 = baseline, 360 = full. Never above 360, as isEdge or isCompleted will be finished at 360.

        public List<Point> connectedVerts;
        public bool isEdge;
        public bool isCompletedPivot;

        public int index;
    }


    private IEnumerator Generate()
    {
        float xMax, yMax, zMax;
        xMax = yMax = zMax = 0;

        foreach (Vector3 point in fitnessList)
        {
            xMax = Mathf.Max(xMax, point.x);
            yMax = Mathf.Max(yMax, point.y);
            zMax = Mathf.Max(zMax, point.z);
            Instantiate(nodePrefab, point + transform.position, quaternion.identity, transform);

            yield return new WaitForSeconds(0.2f);
        }

        GetComponent<MeshFilter>().mesh = mesh = new Mesh();
        mesh.name = "Pareto Mesh";

        var vertices = fitnessList.ToArray();

        int[] triangles = TrianglesFromVertices(vertices);


        mesh.vertices = vertices;
        mesh.triangles = triangles;

        lineRenderer.SetPosition(0, vertices[mesh.triangles[0]]);
        lineRenderer.SetPosition(1, vertices[mesh.triangles[1]]);
        lineRenderer.SetPosition(2, vertices[mesh.triangles[2]]);



        mesh.RecalculateNormals();


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
        */

        /*

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
