using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class PopupBank : MonoBehaviour
{
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI cashText;
    public TextMeshProUGUI balanceText;

    public GameObject depositPanel;
    public GameObject withdrawalPanel;
    public GameObject popupErrorPanel;
    public GameObject RemittancePanel;

    public Button depositOpenBtn;
    public Button depositCloseBtn;
    public Button withdrawalOpenBtn;
    public Button withdrawalCloseBtn;
    public Button RemittanceOpenBtn;
    public Button RemittanceCloseBtn;

    public Button depositInputButton;
    public TMP_InputField depositInputField;
    public Button withdrawalInputButton;
    public TMP_InputField withdrawalInputField;
    public Button RemittanceInputButton;
    public TMP_InputField RemittanceInputField;


    GameManager gameManager;

    void Start()
    {
        gameManager = GameManager.Instance;

        depositOpenBtn.onClick.AddListener(OpenDepositPanel);
        depositCloseBtn.onClick.AddListener(CloseDepositPanel);
        withdrawalOpenBtn.onClick.AddListener(OpenWithdrawalPanel);
        withdrawalCloseBtn.onClick.AddListener(CloseWithdrawalPanel);
        RemittanceOpenBtn.onClick.AddListener(OpenRemittancePanel);
        RemittanceCloseBtn.onClick.AddListener(CloseRemittancePanel);

        depositPanel.SetActive(false);
        withdrawalPanel.SetActive(false);
        popupErrorPanel.SetActive(false);
        RemittancePanel.SetActive(false);

        Refresh();
    }

    void OpenDepositPanel()
    {
        depositPanel.SetActive(true);
    }

    void CloseDepositPanel()
    {
        depositPanel.SetActive(false);
    }

    void OpenWithdrawalPanel()
    {
        withdrawalPanel.SetActive(true);
    }

    void CloseWithdrawalPanel()
    {
        withdrawalPanel.SetActive(false);
    }

    void OpenRemittancePanel()
    {
        RemittancePanel.SetActive(true);
    }

    void CloseRemittancePanel()
    {
        RemittancePanel.SetActive(false);
    }

    public void Refresh()
    {   
        gameManager.SaveUserData();
        nameText.text = gameManager.userData.name;
        cashText.text = string.Format("{0:#,0}", gameManager.userData.cash);
        balanceText.text = string.Format("{0:#,0}", gameManager.userData.balance);
    }

    public void Deposit(int money)
    {
        //gameManager.userData.cash 보유현금 / money 입력된 돈
        if (gameManager.userData.cash < money)
        {
            Debug.Log("잔액이 부족합니다.");
            popupErrorPanel.SetActive(true);
            return;
        }

        //현금 - 입력한 만큼 = cash(매개변수, 입력받은 값)
        gameManager.userData.cash -= money;
        gameManager.userData.balance += money;

        //UI 텍스트 데이터에 맞게 반영
        Refresh();
    }

    public void DepositInput()
    {
        if (int.TryParse(depositInputField.text, out int money))
        {
            Deposit(money);

            depositInputField.text = string.Empty;
        }
    }

    public void Withdrawal(int money)
    {
        if (gameManager.userData.balance < money)
        {
            Debug.Log("잔액이 부족합니다.");
            popupErrorPanel.SetActive(true);
            return;
        }
        gameManager.userData.balance -= money;
        gameManager.userData.cash += money;
        Refresh();
    }

    public void WithdrawalInput()
    {
        if (int.TryParse(withdrawalInputField.text, out int money))
        {
            Withdrawal(money);

            withdrawalInputField.text = string.Empty;
        }
    }
}
