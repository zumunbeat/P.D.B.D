using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class Manager : MonoBehaviour
{
    public TMP_InputField idInputField; // Inspector에서 설정
    public TMP_InputField pwInputField; // Inspector에서 설정
    public Button enterButton;      // Inspector에서 설정

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
            Debug.Log("ID 또는 비밀번호가 잘못되었습니다.");
        }
    }
}
