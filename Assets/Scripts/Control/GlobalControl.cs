using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalControl : MonoBehaviour
{
    private GameObject ui_start;
    // Start is called before the first frame update
    void Start()
    {
        ui_start = GlobalAppData.Instance.Data.ui_start.ExInstantiate(Vector3.zero, Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
