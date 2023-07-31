using UnityEngine;
using UnityEngine.AddressableAssets;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// This allows us to store a database of all characters currently in the bundles, indexed by name.
/// </summary>
public class CharacterDatabase
{
    static protected Dictionary<string, CharacterData> m_CharactersDict;

    static public Dictionary<string, CharacterData> dictionary {  get { return m_CharactersDict; } }

    static protected bool m_Loaded = false;
    static public bool loaded { get { return m_Loaded; } }

    static public CharacterData GetCharacter(string type)
    {
        CharacterData c;
        if (m_CharactersDict == null || !m_CharactersDict.TryGetValue(type, out c))
        {
            Debug.Log($"[CharacterDatabase]{type} is not found in dictionart.");
            return null;
        }

        return c;
    }

    static public IEnumerator LoadDatabase()
    {
        if (m_CharactersDict == null)
        {
            m_CharactersDict = new Dictionary<string, CharacterData>();

            yield return Addressables.LoadAssetsAsync<CharacterData>("characters", op =>
            {
                if (op != null)
                {
                    m_CharactersDict.Add(op.Name, op);
                }
            });

            m_Loaded = true;

            ReflectDefaultCharacterToPlayerData();

            Debug.Log("Loaded Character Database.");
        }
    }

    static public void ReflectDefaultCharacterToPlayerData()
    {
        foreach(var pair in m_CharactersDict)
        {
            var data = pair.Value;
            if (data.IsOwnOnStart && !PlayerData.instance.characters.Contains(data.Name))
            {
                PlayerData.instance.AddCharacter(data.Name);
            }
        }
    }
}