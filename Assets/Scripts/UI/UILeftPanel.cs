using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UILeftPanel : MonoBehaviour
{
    public InputField input_name;
    public InputField input_rotation;
    public InputField input_layer;
    public InputField input_layer_number;
    // Start is called before the first frame update
    void Start()
    {
        input_name.text = "一栋";
        input_rotation.text = "0";
        input_layer.text = "33";
        input_layer_number.text = "3";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
