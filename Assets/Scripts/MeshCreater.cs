using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshCreater : MonoBehaviour
{
    Mesh mesh;
    public Material mat;//mesh材质
    public GameObject game;

    // Start is called before the first frame update
    void Start()
    {
        return;
        mesh = new Mesh();
        mesh.Clear();
        SetVertivesUV();
        SetTriangles();
        mesh.vertices = vertices.ToArray();
        mesh.colors = colors.ToArray();
        mesh.triangles = triangles;
        GameObject obj_cell = new GameObject();
        obj_cell.name = "cell";

        mesh.RecalculateNormals();//重置法线

        mesh.RecalculateBounds();   //重置范围

        obj_cell.AddComponent<MeshFilter>().mesh = mesh;
        obj_cell.AddComponent<MeshRenderer>();
        obj_cell.GetComponent<MeshRenderer>().material = mat;
        
        MeshCaluate mesh_caluate = new MeshCaluate();
        mesh_caluate.CalculateMesh(mesh);
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }

    public List<Vector3> vertices = new List<Vector3>();
    public List<Color> colors = new List<Color>();

    private float angle = 10;

    private float max_angle = 120;
    // 设置顶点信息
    void SetVertivesUV()
    {
        Vector3 dir1 = new Vector3(Mathf.Sqrt(3f), -1, 0);
        Vector3 dir2 = dir1 * 0.8f;
        
        List<Vector3> points1 = new List<Vector3>();
        List<Vector3> points2 = new List<Vector3>();

        int count = (int)((360 - max_angle) / angle);

        for (int i = 0; i < count; i++)
        {
            Quaternion q= Quaternion.AngleAxis(i * angle, Vector3.forward);
            
            Vector3 point1 = q* dir1;
            Vector3 point2 = q* dir2;
            points1.Add(point1);
            points2.Add(point2);
        }
        points1.Add(points1[0]);
        points2.Add(points2[0]);

        for (int i = 0; i < points1.Count; i++)
        {
            var v1 = points1[i];
            var v2 = points2[i];
            var v3 = points1[i];
            v3.z = 10;
            var v4 = points2[i];
            v4.z = 10;
            
            vertices.Add(v1);
            vertices.Add(v3);
            vertices.Add(v2);
            vertices.Add(v4);
            
            colors.Add(Random.ColorHSV(1, 255));
            colors.Add(Random.ColorHSV(1, 255));
            colors.Add(Random.ColorHSV(1, 255));
            colors.Add(Random.ColorHSV(1, 255));

        }
    }

    private int[] triangles;//索引
    // 设置索引
    void SetTriangles()
    {
        triangles = new int[vertices.Count * 3];
        int c = 0;
        for (int i = 0; i < triangles.Length -12 ; i += 12)
        {
            var v1 = c;
            var v2 = c + 1;
            var v3 = c + 4;
            var v4 = c + 5;
            
            var v5 = c + 2;
            var v6 = c + 3;
            var v7 = c + 6;
            var v8 = c + 7;
            
            triangles[i] = v4;
            triangles[i + 1] = v2;
            triangles[i + 2] = v1;

            triangles[i + 3] = v3;
            triangles[i + 4] = v4;
            triangles[i + 5] = v1;

            triangles[i + 6] = v5;
            triangles[i + 7] = v6;
            triangles[i + 8] = v8;
            
            triangles[i + 9] = v5;
            triangles[i + 10] = v8;
            triangles[i + 11] = v7;

            c += 4;
        }
    }

    class graph_vertex
    {
        public Vector3 pos;
        public int index;
        public bool visit = false;
        public List<int> edges = new List<int>();
    }

    public Mesh CreateSkeleton()
    {
        Mesh m = new Mesh();

        List<Vector3> panel =SegragatePoints(mesh);
        List<Vector3> copy_panel = new List<Vector3>(panel);

        for (int i = 0; i < 3; i++)
        {
            var p1 = new Vector3(0, 0, i);

            for (int j = 0; j < panel.Count; j++)
            {
                var pos = panel[j] + p1;
                if (i > 0)
                {
                   // DrawLine(copy_panel[j], pos);
                }
            }

            //DrawLine(copy_panel);
        }
        return m;
    }
    
    private List<Vector3> SegragatePoints(Mesh mesh)
    {
        List<graph_vertex> graph = new List<graph_vertex>();

        // 构建图
        for (int i = 0; i < mesh.tangents.Length; i += 3)
        {
            int i1 = mesh.triangles[i];
            int i2 = mesh.triangles[i + 1];
            int i3 = mesh.triangles[i + 2];

            Vector3 p1 = mesh.vertices[i1];
            Vector3 p2 = mesh.vertices[i2];
            Vector3 p3 = mesh.vertices[i3];

            if (p1.z <= 0 && p3.z <= 0)
            {
                AddEdge(graph, p1, p2, i1, i2);
            }

            if (p2.z <= 0 && p3.z <= 0)
            {
                AddEdge(graph, p2, p3, i2, i3);

            }

            if (p3.z <= 0 && p1.z <= 0)
            {
                AddEdge(graph, p3, p1, i3, i1);
            }
            
        }
        
        // 遍历图，查找环
        Stack<graph_vertex> stack = new Stack<graph_vertex>();
        stack.Push(graph[0]);
        
        List<Vector3> list = new List<Vector3>();

        while (stack.Count > 0)
        {
            var g = stack.Pop();
            g.visit = true;

            var position = mesh.vertices[g.index];

            if (g.edges.Count > 0)
            {
                for (int i = 0; i < g.edges.Count; i++)
                {
                    var k = g.edges[i];
                    if (!graph[k].visit)
                    {
                        stack.Push(graph[k]);
                    }
                }
            }

            position.z = 0;
            list.Add(position);
        }
        
        return list;
    }

    private void AddEdge(List<graph_vertex> graph, Vector3 p1, Vector3 p2, int s, int e)
    {
        int index_s = -1;
        int index_e = -1;

        for (int i = 0; i < graph.Count; i++)
        {
            var g = graph[i];

            if (g.index == s || Vector3.Distance(p1, g.pos) < 0.001f)
            {
                index_s = i;
            }
            
            if (g.index == e || Vector3.Distance(p2, g.pos) < 0.001f)
            {
                index_e = i;
            }    
        }

        if (index_s == -1)
        {
            var g_vertex = new graph_vertex();
            g_vertex.pos = p1;
            g_vertex.index = s;
            graph.Add(g_vertex);
            index_s = graph.Count - 1;
        }

        if (index_e == -1)
        {
            var g_vertex = new graph_vertex();
            g_vertex.pos = p2;
            g_vertex.index = e;
            graph.Add(g_vertex);
            index_s = graph.Count - 1;      
        }
        
        graph[index_s].edges.Add(index_e);
        graph[index_e].edges.Add(index_s);
    }
}
