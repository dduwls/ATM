using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PopupLogin : MonoBehaviour
{
    public GameObject popupSignUpPanel;
    public GameObject popupBankPanel;

    public Button loginBtn;
    public Button signUpBtn;

    public TMP_InputField idInputField;
    public TMP_InputField pwInputField;

    void Start()
    {
        loginBtn.onClick.AddListener(OnLogin);
        signUpBtn.onClick.AddListener(OpenSignUpPanel);

        popupBankPanel.SetActive(false);
        popupSignUpPanel.SetActive(false);
    }

    void OnLogin()
    {
        string id = idInputField.text.Trim();
        string pw = pwInputField.text.Trim();

        UserData loadedUser = UserDataManager.Load(id);

        if (loadedUser != null && loadedUser.password == pw)
        {
            Debug.Log($"로그인 성공! {loadedUser.name}님 환영합니다.");

            GameManager.Instance.userData = loadedUser; // 로그인 유저 덮어쓰기
            OpenpopupBankPanel();
        }
        else
        {
            Debug.LogWarning("로그인 실패: 잘못된 ID 또는 비밀번호입니다.");
            GameManager.Instance.popupWrongInputError.SetActive(true);
        }
    }

    void OpenpopupBankPanel()
    {
        gameObject.SetActive(false);
        popupBankPanel.SetActive(true);
    }


    void OpenSignUpPanel()
    {
        gameObject.SetActive(false);
        popupSignUpPanel.SetActive(true);
    }
}
