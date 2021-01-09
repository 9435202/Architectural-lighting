using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 建筑数据
public class BuildData
{
    public string name; // 建筑名称
    public float angle; // 旋转角度
    public List<Vector3> panel_pos; // 平面上的点
    public double floor_height; // 层高
    public double floor_num; // 层数
}
