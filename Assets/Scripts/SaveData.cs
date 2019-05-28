using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.SceneManagement;

[Serializable]
public class SaveData : IComparable
{
    public float positionX;
    public float positionY;
    public float positionZ;
    public int currentSceneID;
    public string name;
    public DateTime dataTime;

    public SaveData(Vector3 position, string namePerson)
    {
        positionX = position.x;
        positionY = position.y;
        positionZ = position.z;
        currentSceneID = SceneManager.GetActiveScene().buildIndex;
        name = namePerson;
        dataTime = DateTime.Now;
        SaveGame();
    }

    public void SaveGame()
    {
        if (!Directory.Exists(Application.dataPath + "/Data/Saves")) Directory.CreateDirectory(Application.dataPath + "/saves");
        FileStream fs = new FileStream(Application.dataPath + "/Data/Saves/" + name + ".sv", FileMode.Create);
        BinaryFormatter bformatter = new BinaryFormatter();
        bformatter.Serialize(fs, this);
        fs.Close();
    }

    public int CompareTo(object o)
    {
        SaveData rec = (SaveData)o;
        return this.dataTime.CompareTo(rec.dataTime);
    }
}