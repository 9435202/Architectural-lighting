using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;

public class culator_data
{
     public float lat = 118;
     public float lon = 33;
     public int day = 0;
     public int beijing_time = 43200;
}

public class SceneData : Singleton<SceneData>
{

     public culator_data data = new culator_data();
     public List<BuildData> list = new List<BuildData>();

     public void Load(string file_name)
     {
          try
          {
               string path =IOPath.XMLpath + file_name;
               //IOPath.XMLpath 
               if (!File.Exists(path))
               {
                    return;
               }

               s_instance = ObjectSerializer.Load<SceneData>(path);
          }
          catch (Exception e)
          {
               
          }
     }

     public void Save(string file_name)
     {
          try
          {
               string path =IOPath.XMLpath + file_name;

               ObjectSerializer.Save(s_instance, path);
          }
          catch (Exception e)
          {

          }
     }
}
