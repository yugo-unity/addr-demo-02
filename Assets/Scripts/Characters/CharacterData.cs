using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/CharacterData", order = 1)]
public class CharacterData : ScriptableObject
{
    public string Name;
    public bool IsOwnOnStart;

    public string GetObjectAddress()
    {
        return $"{Name}/Prefab/character.prefab";
    }
}
