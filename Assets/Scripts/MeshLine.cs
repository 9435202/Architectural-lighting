using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 画线
public class MeshLine 
{
    private Material material; // 材质
    private RepeatSegmentMesh segment_mesh;
    private RenderObject render_object;
    private MeshEnableFlag flag;

    public MeshLine()
    {
        this.material = GlobalAppData.Instance.Data.line_material;
        render_object = RenderObject.Create(null, null, Vector3.zero, Quaternion.identity);
        render_object.mesh_render.material = material;
        render_object.mesh_render.castShadows = true;
        render_object.mesh_render.receiveShadows = true;
        render_object.go.name = "MeshLine";

        //Light light = render_object.mesh_filter.GetComponent<Lighting>();
        flag = MeshEnableFlag.Vertice | MeshEnableFlag.Normal | MeshEnableFlag.Triangle | MeshEnableFlag.Color | MeshEnableFlag.UV;
        segment_mesh = new RepeatSegmentMesh(3,3, 100, flag);
        segment_mesh.ProcedureMesher.SetMesh32();

        segment_mesh.SegmentData.MarkTriangleTopology();       
    }
    
    public static Vector3 GetVerticalDir(Vector3 _dir)
    {
        if (_dir.z == 0)
        {
            return new Vector3(0, 0, -1);
        }
        else
        {
            return new Vector3(-_dir.z / _dir.x, 0, 1).normalized;
        }
    }

    // 增加线段
    public void AddLine(Vector3 start, Vector3 end, Color color , float radius = 0.1f)
    {
        int num = 6;
        int angle = 360 / num;
        Vector3 dir = (end - start).normalized;
        var vertical = GetVerticalDir(dir);
        
        for (int i = 1; i <= num; i++)
        {
             var r = Quaternion.AngleAxis(i * angle, dir) * vertical;
             var r1 = Quaternion.AngleAxis((i + 1) * angle, dir) * vertical;

             var p1 = (r * radius) + start;
             var p2 = (r1 * radius) + start;
             var p3 = (r * radius) + end;
             var p4 = (r1 * radius) + end;
             segment_mesh.SegmentData.SetTriangleVertice(0, start, p2, p1);
             segment_mesh.SegmentData.SetColor(color);
             segment_mesh.AddSegmentData(flag);
             
             segment_mesh.SegmentData.SetTriangleVertice(0, end, p3, p4);
             segment_mesh.SegmentData.SetColor(color);
             segment_mesh.AddSegmentData(flag);
             
             segment_mesh.SegmentData.SetTriangleVertice(0, p1, p2, p4);
             segment_mesh.SegmentData.SetColor(color);
             segment_mesh.AddSegmentData(flag);

             segment_mesh.SegmentData.SetTriangleVertice(0, p3, p1, p4);
             segment_mesh.SegmentData.SetColor(color);
             segment_mesh.AddSegmentData(flag);

        }
        
        segment_mesh.CopyToMesh(render_object.mesh_filter.mesh, true);
        render_object.mesh_filter.mesh.RecalculateNormals();
    }

    // 增加多段线
    public void AddLine(List<Vector3> points, Color color , float radius = 0.1f)
    {
        for (int i = 1; i < points.Count; i++)
        {
            AddLine(points[i -1], points[i], color, radius);
        }
    }

    // 清除
    public void Clear()
    {
        segment_mesh.SetSegmentCount(0);
        segment_mesh.CopyToMesh(render_object.mesh_filter.mesh, true);
        render_object.mesh_filter.mesh.RecalculateNormals();   
    }
}
