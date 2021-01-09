using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunCalculator
{

    //太阳高度角计算
    public double SunPitch(float lat, float lon, int day, int beijing_time)
    {
        double declination = SunDeclination(day);

        double dlon = lon * Math.PI / 180;
        
        double t = TimeAngle(day, lat ,beijing_time);
        Debug.LogWarning($"declination:{declination} t:{t}");
        double s = Math.Sin(dlon) * Math.Sin(declination) + Math.Cos(dlon) * Math.Cos(declination) * Math.Cos(t);

        double r = Math.Asin(s);
        
       // Debug.Log($"高度角 {r * 180 / Math.PI}");
        return r;
    }
    
    // 太阳方位角计算
    public double SunAzimuthAngle(float lat, float lon, int day, int beijing_time)
    {
        double angle = SunPitch(lat, lon, day, beijing_time);
        double declination = SunDeclination(day);

        double dlon = lon * Math.PI / 180;

        double c = (Math.Sin(angle) * Math.Sin(dlon) - Math.Sin(declination))
            / (Math.Cos(angle) * Math.Cos(dlon));

        double r = Math.Acos(c);

        int real_time = RealSumTime(day, lat, beijing_time);
        
        if (real_time > 43200) r = -r;
        //Debug.Log($"方位角 {r * 180 / Math.PI}");
        return r;
    }

    // 太阳赤纬计算
    public double SunDeclination( int day)
    {
       double b = 2 * Math.PI * (day - 1) / 365;
       b = b * Math.PI / 180;
       double r = 0.006918 - 0.399912 * Math.Cos(b)
                  + 0.070257 * Math.Sin(b)
                  - 0.006758 * Math.Cos(2 * b)
                  + 0.000907 * Math.Sin(2 * b)
                  - 0.002697 * Math.Cos(3 * b)
                  + 0.00148 * Math.Sin(3 * b);
       //double r = 23.45 * Math.Sin(360 * (284 + day) / 365.0);
       //   r = r * Math.PI / 180;
       var N0 = 79.6764+0.2422*(2020-1985)-(int)((2020-1985)/4);
       var t = day - N0;
       double c = 2* Math.PI * t / 365.2422;
       r = 0.3723+23.2567* Math.Sin(c)+0.1149 * Math.Sin(2 *c)-0.1712 * Math.Sin(3 * c)-0.758 * Math.Cos(c) + 0.3656 *
           Math.Cos
               (2 * c) + 0.0201 * Math.Cos(3 * c);
       
       Debug.Log($"r {r}");
       r = r * Math.PI / 180;
       return r;
    }

    public int RealSumTime(int day,  float lat, int beijing_time)
    {
        int plane_sun_time = PlaneSunTime(lat, beijing_time);

        int real_sun_time = plane_sun_time + TimeTable.GetTableTime(day);

        return real_sun_time;
    }
    
    // 时角计算
    public double TimeAngle(int day,  float lat, int beijing_time)
    {
        double real_sun_time = RealSumTime(day, lat, beijing_time);

        double d = (real_sun_time / 3600.0 - 12) * 15;

        //Debug.LogError($" d: {d}");

        d = d * Math.PI / 180;

        return d;
    }
    
    // 真太阳时计算
    // time 秒钟
    // lat 经度
    public int PlaneSunTime(float lat, int beijing_time)
    {
        double r = beijing_time + 4 * (120 - lat) * 60;

        return (int)r;
    }
    
    
}
