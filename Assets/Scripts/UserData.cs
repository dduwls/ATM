using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[System.Serializable]
public class UserData 
{
    public string id;
    public string password;
    public string name;
    public int cash;
    public int balance;

    public UserData(string id, string password, string name, int cash, int balance)
    {   
        this.id = id;
        this.password = password;
        this.name = name;
        this.cash = cash;
        this.balance = balance;
    }
}
