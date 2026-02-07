using UnityEngine;
using System.Collections.Generic;
using System.IO;
using System;


public class CharacterHelper
{
    private readonly static string savePath = Application.persistentDataPath + "/Save_Data/";

    public static void SaveCharacter(Character character)
    {
        if (Directory.Exists(savePath) == false)
            Directory.CreateDirectory(savePath);

        var safeName = string.Join("_", character.Name.Split(Path.GetInvalidFileNameChars()));
        var fileLocation = Path.Combine(savePath, $"{safeName}_Save.json");
        string json = JsonUtility.ToJson(character);
        File.WriteAllText(fileLocation, json);
        Debug.Log("Wrote to " + fileLocation);
        Debug.Log("Saved: " + json);
    }

    public static Character LoadCharacter(string json)
    {
        return JsonUtility.FromJson<Character>(json);
    }

    public static List<Character> GetCharacters()
    {
        var list = new List<Character>();

        if (!Directory.Exists(savePath))
            return list;

        foreach (var filePath in Directory.GetFiles(savePath, "*_Save.json"))
        {
            try
            {
                string json = File.ReadAllText(filePath);
                list.Add(LoadCharacter(json));
            }
            catch (Exception e)
            {
                Debug.LogError($"Failed to load character file '{filePath}': {e}");
            }
        }

        return list;
    }
}

[Serializable]
public class Character
{
    public string Name;
    public int Level;
    public float XP;
    public int Health;
    public int MaxHealth;
    public int Mana;
    public int MaxMana;
    public int Strength;
    public int Dexterity;
    public int Constitution;
    public int Intelligence;
    public int Charisma;
    public int Awareness;
    public List<Skill> Skills = new List<Skill>();
    public int EntropyTokens;
}

[Serializable]
public class Skill
{
    public string Name;
    public int Score;
    public float XP;
}
