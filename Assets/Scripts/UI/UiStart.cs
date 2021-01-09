using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiStart : MonoBehaviour
{
    public Button create;

    public Button destroy;
    
    // Start is called before the first frame update
    public Material material;
    
    void Start()
    {
        BuildCreate build_create = new BuildCreate();
        
        build_create.Init(material);
        
        BuildData build_data = new BuildData();
        build_data.floor_height = 3;
        build_data.floor_num = 30;
        build_data.panel_pos = new List<Vector3>();
        build_data.panel_pos.Add(new Vector3(0, 0, 0));
        build_data.panel_pos.Add(new Vector3(0, 0, 12));
        build_data.panel_pos.Add(new Vector3(22, 0, 12));
        build_data.panel_pos.Add(new Vector3(22, 0, 32));
        build_data.panel_pos.Add(new Vector3(42, 0, 32));
        build_data.panel_pos.Add(new Vector3(42, 0, 12));
        build_data.panel_pos.Add(new Vector3(64, 0, 12));
        build_data.panel_pos.Add(new Vector3(64, 0, 32));
        build_data.panel_pos.Add(new Vector3(84, 0, 32));
        build_data.panel_pos.Add(new Vector3(84, 0, 12));
        build_data.panel_pos.Add(new Vector3(102, 0, 12));
        build_data.panel_pos.Add(new Vector3(102, 0, 0));

        build_create.CreateBuild(build_data);

        Vector3 offset = new Vector3(0, 0 ,100);
        for (int i = 0; i < build_data.panel_pos.Count; i++)
        {
            build_data.panel_pos[i] = build_data.panel_pos[i] + offset;
        }
        
        build_create.CreateBuild(build_data);

        BuildData build_data1 = new BuildData();
        build_data1.floor_height = 3;
        build_data1.floor_num = 33;
        build_data1.panel_pos = new List<Vector3>();
        build_data1.panel_pos.Add(new Vector3(0, 0, 0));
        build_data1.panel_pos.Add(new Vector3(0, 0, 20));
        build_data1.panel_pos.Add(new Vector3(80 ,0, 20));
        build_data1.panel_pos.Add(new Vector3(80, 0, 0));
         offset = new Vector3(200, 0 ,0);
        for (int i = 0; i < build_data1.panel_pos.Count; i++)
        {
            build_data1.panel_pos[i] = build_data1.panel_pos[i] + offset;
        }
        build_create.CreateBuild(build_data1);
        
        offset = new Vector3(0, 0 ,100);
        for (int i = 0; i < build_data1.panel_pos.Count; i++)
        {
            build_data1.panel_pos[i] = build_data1.panel_pos[i] + offset;
        }
        build_create.CreateBuild(build_data1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
