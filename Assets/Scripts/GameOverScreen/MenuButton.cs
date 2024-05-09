using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButton : MonoBehaviour
{
    public void MainMenu() {
        Debug.Log("Loading Main Menu...");
        SceneManager.LoadScene("MainMenu");
    }
}
