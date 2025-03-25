using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{ 
    //싱글톤 기본형
    public static GameManager Instance { get; private set; }

    public UserData userData;
    string fileName = "UserData.json";

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }

        userData = new UserData("12345","12345", "김세봉", 1234200, 623443);
        LoadUserData();
    }
    public void SaveUser()
    {
        UserDataManager.SaveUserData(userData);
    }
    public void SaveUserData()
    {
        // PlayerPrefs.SetString("Name", userData.name);
        // PlayerPrefs.SetInt("Balance", userData.balance);
        // PlayerPrefs.SetInt("Cash", userData.cash);
        // PlayerPrefs.Save();

        string json = JsonUtility.ToJson(userData, true);
        string path = Path.Combine(Application.persistentDataPath, fileName);
        File.WriteAllText(path, json);
    }

    public void LoadUserData()
    {
        // userData.name = PlayerPrefs.GetString("Name", userData.name);
        // userData.balance = PlayerPrefs.GetInt("Balance", userData.balance);
        // userData.cash = PlayerPrefs.GetInt("Cash", userData.cash);

        string path = Path.Combine(Application.persistentDataPath, fileName);
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            userData = JsonUtility.FromJson<UserData>(json);
        }
    }
}
