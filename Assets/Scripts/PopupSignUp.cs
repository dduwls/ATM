using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;


public class PopupSignUp : MonoBehaviour
{
    public GameObject popupLoginPanel;
    public GameObject popupBankPanel;

    public Button signUpBtn;
    void Start()
    {
        signUpBtn.onClick.AddListener(OpenPopupLoginPanel);

        gameObject.SetActive(false);

        popupBankPanel.SetActive(false);
    }

    void Update()
    {
        
    }

    void OpenPopupLoginPanel()
    {
        popupLoginPanel.SetActive(true);
        gameObject.SetActive(false);
    }
}
