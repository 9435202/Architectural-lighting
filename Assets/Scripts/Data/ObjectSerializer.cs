using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public static class ObjectSerializer 
{
    public static bool Save<T>(T obj, string file)
    {
        try
        {
            XmlSerializer writer = new XmlSerializer(typeof(T));
            
            var w = new System.IO.StreamWriter(file);

            writer.Serialize(w, obj);
            w.Close();
        }
        catch (Exception e)
        {
            Debug.Log("Save Error" + e.Message);
            Debug.Log("Save Error" + e.StackTrace);
        }

        return true;
    }

    public static T Load<T>(string file)
    {
        T t = default(T);
        try
        {
            XmlSerializer read = new XmlSerializer(typeof(T));
            
            var r = new System.IO.StreamReader(file);

            t = (T) read.Deserialize(r);
            r.Close();
        }
        catch (Exception e)
        {
            Debug.Log("Load Error" + e.Message);
            Debug.Log("Load Error" + e.StackTrace);
            throw;
        }

        return t;
    }
}
