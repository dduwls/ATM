using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class PopupBank : MonoBehaviour
{
    [Header("유저 데이터")]
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI cashText;
    public TextMeshProUGUI balanceText;

    [Header("UI 패널")]
    public GameObject depositPanel;
    public GameObject withdrawalPanel;
    public GameObject RemittancePanel;

    [Header("패널 온/오프 버튼")]
    public Button depositOpenBtn;
    public Button depositCloseBtn;
    public Button withdrawalOpenBtn;
    public Button withdrawalCloseBtn;
    public Button remittanceOpenBtn;
    public Button remittanceCloseBtn;

    [Header("직접 입력")]
    public Button depositInputButton;
    public TMP_InputField depositInputField;
    public Button withdrawalInputButton;
    public TMP_InputField withdrawalInputField;
    public Button remittanceInputButton;
    public TMP_InputField remittanceTargetInputField;
    public TMP_InputField remittanceMoneyInputField;

    GameManager gameManager;

    void Start()
    {
        gameManager = GameManager.Instance;

        depositOpenBtn.onClick.AddListener(OpenDepositPanel);
        depositCloseBtn.onClick.AddListener(CloseDepositPanel);
        withdrawalOpenBtn.onClick.AddListener(OpenWithdrawalPanel);
        withdrawalCloseBtn.onClick.AddListener(CloseWithdrawalPanel);
        remittanceOpenBtn.onClick.AddListener(OpenRemittancePanel);
        remittanceCloseBtn.onClick.AddListener(CloseRemittancePanel);

        depositPanel.SetActive(false);
        withdrawalPanel.SetActive(false);
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
        gameManager.SaveUser();
        nameText.text = gameManager.userData.name;
        cashText.text = string.Format("{0:#,0}", gameManager.userData.cash);
        balanceText.text = string.Format("{0:#,0}", gameManager.userData.balance);
    }

    // 입금
    public void Deposit(int money)
    {
        //gameManager.userData.cash 보유현금 / money 입력된 돈
        if (gameManager.userData.cash < money)
        {
            Debug.Log("잔액이 부족합니다.");
            gameManager.popupNoMoneyError.SetActive(true);
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
        if (int.TryParse(depositInputField.text.Trim(), out int money))
        {
            Deposit(money);

            depositInputField.text = string.Empty;
        }
    }

    // 출금
    public void Withdrawal(int money)
    {
        if (gameManager.userData.balance < money)
        {
            Debug.Log("잔액이 부족합니다.");
            gameManager.popupNoMoneyError.SetActive(true);
            return;
        }
        gameManager.userData.balance -= money;
        gameManager.userData.cash += money;
        Refresh();
    }

    public void WithdrawalInput()
    {
        if (int.TryParse(withdrawalInputField.text.Trim(), out int money))
        {
            Withdrawal(money);

            withdrawalInputField.text = string.Empty;
        }
    }
    
    // 송금
    public void Remittance()
    {
        if (int.TryParse(remittanceMoneyInputField.text.Trim(), out int money))
        {
            Remittance(money);

            remittanceMoneyInputField.text = string.Empty;
        }
    }

    public void Remittance(int money)
    {
        string targetId = remittanceTargetInputField.text.Trim();

        if (string.IsNullOrEmpty(targetId))
        {
            Debug.Log("송금 대상 ID를 입력해주세요.");
            return;
        }

        if (targetId == gameManager.userData.id)
        {
            Debug.Log("자기 자신에게는 송금할 수 없습니다.");
            return;
        }

        if (gameManager.userData.balance < money)
        {
            Debug.Log("잔액이 부족합니다.");
            gameManager.popupNoMoneyError.SetActive(true);
            return;
        }

        // 대상 유저 불러오기
        UserData targetUser = UserDataManager.Load(targetId);

        if (targetUser == null)
        {
            Debug.Log("해당 ID의 유저가 존재하지 않습니다.");
            return;
        }

        gameManager.userData.balance -= money;
        targetUser.balance += money;

        UserDataManager.Save(targetUser);
        Refresh();
    }
}
