using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private string nextSceneName;

    public void StartGame()
    {
        SceneManager.LoadScene(nextSceneName);
    }
}
