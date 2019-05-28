using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserData : MonoBehaviour {
    public string namePlayer;
    public Vector3 positionBall;
    public static UserData instance = null;
    // Use this for initialization
    void Start()
    {
        if (instance == null)
        {
            namePlayer = "UnknownPlayer";
            positionBall = new Vector3(0, 4f, 0);
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(this.gameObject);
    }

    public void SaveDataToUserData(SaveData saveData)
    {
        namePlayer = saveData.name;
        positionBall = new Vector3(saveData.positionX, saveData.positionY, saveData.positionZ);
    }
}