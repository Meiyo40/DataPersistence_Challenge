using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEditor;
using TMPro;
using System;

public class ButtonHandler : MonoBehaviour
{
    [SerializeField]
    private GameObject playerNameInput;

    private void Start()
    {
        this.LoadData();
    }

    private void LoadData()
    {
        GameState.Instance.LoadData();
        GameState.Instance.SetMenuStats();

        if (GameState.Instance.PlayerName != string.Empty) //If a name is saved, we write it in the input
            playerNameInput.GetComponent<TMP_InputField>().text = GameState.Instance.PlayerName;
    }

    public void StartGame()
    {
        string text = playerNameInput.GetComponent<TMP_InputField>().text;

        if (text == string.Empty)
            text = "player"; //be sure to have a default value

        GameState.Instance.PlayerName = text;

        SceneManager.LoadScene("main");
    }

    public void ExitApp()
    {
        GameState.Instance.SaveData();

        #if UNITY_EDITOR
            EditorApplication.ExecuteMenuItem("Edit/Play");
        #else
            Application.Quit();
        #endif
    }

    public void OnValueChanged() //if we change the name, we must reset the present score.
    {
        GameState.Instance.Score = 0;
    }
}
