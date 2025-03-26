using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PopupSignUp : MonoBehaviour
{
    public GameObject popupLoginPanel;
    public GameObject popupBankPanel;

    public Button signUpBtn;

    public TMP_InputField nameInputField;
    public TMP_InputField idInputField;
    public TMP_InputField pwInputField;

    void Start()
    {
        signUpBtn.onClick.AddListener(OnSignUp);

        gameObject.SetActive(false);
        popupBankPanel.SetActive(false);
    }

    void OnSignUp()
    {
        // Trim() - 앞, 뒤 공백 제거
        string name = nameInputField.text.Trim();
        string id = idInputField.text.Trim();
        string pw = pwInputField.text.Trim();

        // ID와 비밀번호 중 하나라도 입력되지 않은게 있는지 확인
        if (string.IsNullOrEmpty(id) || string.IsNullOrEmpty(pw))
        {
            Debug.LogWarning("ID와 비밀번호를 입력해주세요.");
            GameManager.Instance.popupWrongInputError.SetActive(true);
            return;
        }

        // 이미 존재하는 ID인지 확인
        if (UserDataManager.IsUserExists(id))
        {
            Debug.LogWarning("이미 존재하는 ID입니다. 다른 ID를 입력해주세요.");
            return;
        }

        // 기본 데이터로 새 유저 생성
        UserData newUser = new UserData(id, pw, name, 10000, 0);
        UserDataManager.Save(newUser);

        Debug.Log($"회원가입 성공! ID: {id}");
        OpenPopupLoginPanel();
    }

    void OpenPopupLoginPanel()
    {
        gameObject.SetActive(false);
        popupLoginPanel.SetActive(true);
    }
}
