using UnityEngine;

public class GameManager : MonoBehaviour
{
    //싱글톤 기본형
    public static GameManager Instance { get; private set; }

    public UserData userData;
    public GameObject popupNoMoneyError;
    public GameObject popupWrongInputError;
    public GameObject popupCantFindTargetError;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }

        userData = UserDataManager.Load("Sebong") ?? new UserData("Sebong", "password", "김세봉", 10000, 99990000);
        SaveUser();
    }

    public void SaveUser()
    {
        UserDataManager.Save(userData);
    }
}
