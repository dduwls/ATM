using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;


public class PopupLogin : MonoBehaviour
{

    public GameObject popupLoginPanel;
    public GameObject popupSignUpPanel;
    public GameObject popupBankPanel;

    public Button loginBtn;
    public Button signUpBtn;

    public TMP_InputField idInputField;
    public TMP_InputField pwInputField;
    void Start()
    {

        loginBtn.onClick.AddListener(OpenpopupBankPanel);
        signUpBtn.onClick.AddListener(OpenSignUpPanel);

        popupBankPanel.SetActive(false);
        popupSignUpPanel.SetActive(false);
    }

    void Update()
    {
        
    }

    void OpenpopupBankPanel()
    {
        popupBankPanel.SetActive(true);
    }


    void OpenSignUpPanel()
    {
        popupSignUpPanel.SetActive(true);
    }
}
