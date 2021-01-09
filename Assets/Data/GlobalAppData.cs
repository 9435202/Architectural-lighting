using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class GlobalAppData : MonoBehaviour
{
    [FormerlySerializedAs("beam_data")] public BeamData Data;
    
    static GlobalAppData _instance;

    void Awake () {
        _instance = this;
        // 防止载入新场景时被销毁
        DontDestroyOnLoad(_instance.gameObject);	
    }

    public static GlobalAppData Instance {
        get {
            return _instance;
        }
    }
}
