using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/ThemeData", order = 1)]
public class ThemeData : ScriptableObject
{
    public string Name;
    public bool IsOwnOnStart;

    public string GetSettingAddress()
    {
        return $"{Name}/setting.asset";
    }
}
