using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 创建网格
public class BuildCreate
{
    private Material material; // 材质
    private RepeatSegmentMesh segment_mesh;
    private RenderObject render_object;
    private MeshLine mesh_line;

    private MeshEnableFlag flag;
    // 初始化
    public void Init(Material material)
    {
        this.material = material;
        render_object = RenderObject.Create(null, null, Vector3.zero, Quaternion.identity);
        render_object.mesh_render.material = material;
        render_object.mesh_render.castShadows = true;
        render_object.mesh_render.receiveShadows = true;
        render_object.go.name = "Build";
        flag = MeshEnableFlag.Vertice | MeshEnableFlag.Normal | MeshEnableFlag.Triangle | MeshEnableFlag.Color | MeshEnableFlag.UV;
        segment_mesh = new RepeatSegmentMesh(3,3, 100, flag);
        
        segment_mesh.SegmentData.MarkTriangleTopology();
        segment_mesh.ProcedureMesher.SetMesh32();
        
        mesh_line = new MeshLine();
    }
    
    // 创建建筑
    public void CreateBuild(BuildData build_data)
    {
        float build_height = (float) (build_data.floor_height * build_data.floor_num);
        Vector3 h = new Vector3(0, build_height, 0);
        // 绘制底面
        List<int> triangleindex = Triangulation.WidelyTriangleIndex(build_data.panel_pos);
        for (int i = 2; i < triangleindex.Count; i += 3)
        {
            Vector3 vv1 = build_data.panel_pos[triangleindex[i - 2]];
            Vector3 vv2 = build_data.panel_pos[triangleindex[i - 1]];
            Vector3 vv3 = build_data.panel_pos[triangleindex[i]];
            
            segment_mesh.SegmentData.SetTriangleVertice(0, vv3, vv2, vv1);  
            segment_mesh.AddSegmentData(flag);
        }
   
        // 绘制柱子
        Vector3 v1 = Vector3.zero;
        Vector3 v2 = Vector3.zero;
        Vector3 v3 = Vector3.zero;
        Vector3 v4 = Vector3.zero;
        for (int i = 0; i < build_data.panel_pos.Count; i++)
        {
            if (i == 0)
            {
                v2 = build_data.panel_pos[i];
                v1 = build_data.panel_pos[build_data.panel_pos.Count - 1];
                v4 = build_data.panel_pos[i] + h;
                v3 = build_data.panel_pos[build_data.panel_pos.Count - 1] + h;
            }
            else
            {
                v1 = build_data.panel_pos[i - 1];
                v2 = build_data.panel_pos[i];
                v3 = build_data.panel_pos[i - 1] + h;
                v4 = build_data.panel_pos[i] + h;    
            }
            segment_mesh.SegmentData.SetTriangleVertice(0, v3, v2, v1);  
            segment_mesh.AddSegmentData(flag);
            segment_mesh.SegmentData.SetTriangleVertice(0, v2, v3, v4);  
            segment_mesh.AddSegmentData(flag);
            
            segment_mesh.SegmentData.SetTriangleVertice(0, v1, v2, v3);  
            segment_mesh.AddSegmentData(flag);
            segment_mesh.SegmentData.SetTriangleVertice(0, v4, v3, v2);  
            segment_mesh.AddSegmentData(flag);
        }
        
        // 绘制顶面
        for (int i = 2; i < triangleindex.Count; i += 3)
        {
            Vector3 vv1 = build_data.panel_pos[triangleindex[i - 2]] + h;
            Vector3 vv2 = build_data.panel_pos[triangleindex[i - 1]] + h;
            Vector3 vv3 = build_data.panel_pos[triangleindex[i]] + h;
            
            segment_mesh.SegmentData.SetTriangleVertice(0, vv1, vv2, vv3);  
            segment_mesh.AddSegmentData(flag);
        }
        
        segment_mesh.CopyToMesh(render_object.mesh_filter.mesh, true);
        render_object.mesh_filter.mesh.RecalculateNormals();

        CreateLayer(build_data);
    }

    public void CreateLayer(BuildData build_data)
    {
        List<Vector3> new_list = new List<Vector3>();
        for (int i = 0; i < build_data.panel_pos.Count; i++)
        {
            new_list.Add(build_data.panel_pos[i]);
        }
        
         for (int i = 1; i < build_data.floor_num; i++)
        {
            for (int j = 0; j < new_list.Count; j++)
            {
                var p = new_list[j];
                p.y = i * (float)build_data.floor_height;
                new_list[j] = p;
            }

            mesh_line.AddLine(new_list, Color.cyan, 0.3f);
        }
    }
}
