using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class UiCaculute : MonoBehaviour
{
    public Button analysis_btn;
    public Button clear;
    public InputField lan;
    public InputField lat;
    [FormerlySerializedAs("slider")] public Slider slider_date;
    public InputField date; // 日期
    public Slider slider_time;
    public InputField time;

    private SunCalculator sun_calculator = new SunCalculator();
    
    private culator_data data = new culator_data();
    // Start is called before the first frame update
    void Start()
    {
        data = SceneData.Instance.data;
        lat.text = data.lat.ToString();
        lan.text = data.lon.ToString();
        slider_date.SetValueWithoutNotify(data.day);
        date.text = data.day.ToString();

        slider_time.SetValueWithoutNotify(data.beijing_time - init_time);
                
        int h = data.beijing_time / 3600;
        int min = (data.beijing_time % 3600) / 60;
        int ss =  (data.beijing_time % 3600) % 60;
        string text = $"{h}:{min}.{ss}";
        time.text = text;

        UpdateDate();
        SliderDate();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private static readonly int[] DaysToMonth365 = new int[13]
    {
        0,
        31,
        59,
        90,
        120,
        151,
        181,
        212,
        243,
        273,
        304,
        334,
        365
    };
    public void SliderDate()
    {
        int date_time = (int)slider_date.value;
        data.day = date_time;
        int i = 0;
        int month = 0;
        for (i = 1; i < DaysToMonth365.Length; i++)
        {
            if (DaysToMonth365[i - 1] <= date_time && DaysToMonth365[i] > date_time)
            {
                month = i;
                break;
            }
        }

        if (i == 13) month = 12;
        date.SetTextWithoutNotify($"{month}月{date_time - DaysToMonth365[month - 1] }日");
        
        UpdateDate();
    }

    private int init_time = 18000;
    public void SliderTime()
    {
        int beijing_time = (int)slider_time.value + init_time;
        data.beijing_time = beijing_time;
        
        int h = beijing_time / 3600;
        int min = (beijing_time % 3600) / 60;
        int ss =  (beijing_time % 3600) % 60;
        string text = $"{h}:{min}.{ss}";
        time.SetTextWithoutNotify(text);

        UpdateDate();
    }

    public void OnEndEdit()
    {
        float flat = data.lat;
        float flon = data.lon;
        float.TryParse(lan.text, out flon);
        float.TryParse(lat.text, out flat);
    }

    private void UpdateDate()
    {
        float  pitch = (float)(sun_calculator.SunPitch(data.lat, data.lon, data.day, data.beijing_time) *180 / Math.PI);
        float  azimuth = (float)(sun_calculator.SunAzimuthAngle(data.lat, data.lon, data.day, data.beijing_time) * 180/ Math.PI);    
        
        Debug.Log($"高度角：{pitch} 方位角：{azimuth}");
        
        LightControl.Instance.SetAngle(pitch, azimuth);
    }
}
