using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    #region Variables
    [Header("Main Menu")]
    public GameObject mainMenu;
    public GameObject htpMenu;
    [Header("How to Play")]
    public GameObject page1;
    public GameObject page2;
    public GameObject page3;
    #endregion

    #region Button Functions

    /// <summary>
    /// In inspector, pass in the Build Index value of the scene to be loaded
    /// on the button you use to progress to a new scene
    /// </summary>
    /// <param name="sceneNum"></param>
    public void LoadScene(int sceneNum)
    {
        SceneManager.LoadScene(sceneNum);
    }

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    /// <summary>
    /// View the Main Menu
    /// </summary>
    public void ViewMain()
    {
        mainMenu.SetActive(true);
        htpMenu.SetActive(false);
    }

    /// <summary>
    /// View the How to Play Menu
    /// </summary>
    public void ViewHTP()
    {
        mainMenu.SetActive(false);
        htpMenu.SetActive(true);
    }

    /// <summary>
    /// View the next page of the How to Play Menu
    /// </summary>
    public void ViewPage1()
    {
        page2.SetActive(false);
        page1.SetActive(true);
    }

    /// <summary>
    /// View the next page of the How to Play Menu
    /// </summary>
    public void ViewPage2()
    {
        page2.SetActive(true);
        page1.SetActive(false);
        page3.SetActive(false);
    }

    /// <summary>
    /// View the next page of the How to Play Menu
    /// </summary>
    public void ViewPage3()
    {
        page3.SetActive(true);
        page2.SetActive(false);
    }

    /// <summary>
    /// Quits the game!
    /// </summary>
    public void QuitGame()
    {
        print("Quit!");
        Application.Quit();
    }
    #endregion

    void Start()
    {
        Time.timeScale = 1;
    }
}
