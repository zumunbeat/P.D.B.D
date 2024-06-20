using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class Manager : MonoBehaviour
{
    public TMP_InputField idInputField; // Inspector���� ����
    public TMP_InputField pwInputField; // Inspector���� ����
    public Button enterButton;      // Inspector���� ����

    private string correctId = "Admin";
    private string correctPw = "1q2w3e4r";

    void Start()
    {
        enterButton.onClick.AddListener(OnEnterButtonClick);
    }

    void OnEnterButtonClick()
    {
        string enteredId = idInputField.text;
        string enteredPw = pwInputField.text;

        if (enteredId == correctId && enteredPw == correctPw)
        {
            SceneManager.LoadScene("Field");
        }
        else
        {
            Debug.Log("ID �Ǵ� ��й�ȣ�� �߸��Ǿ����ϴ�.");
        }
    }
}
