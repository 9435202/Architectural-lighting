using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshCaluate
{
    public List<Vector3> points = new List<Vector3>();
    
    public void CalculateMesh(Mesh mesh)
    {
        for (int i = 0; i < mesh.vertices.Length; i++)
        {
            if (mesh.vertices[i].z == 0)
            {
                points.Add(mesh.vertices[i]);
            }
        }
        
        // 取点
        
    }
}
