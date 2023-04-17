using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [Header("HUD")]
    public Slider gasCount; // slider showing how much gas there is in game
    public RectTransform winZone; // slider zone for winning
    public RectTransform loseZone; // slider zone for losing

    public PlayerMovement pm;

    // Update is called once per frame
    void Update()
    {
        gasCount.value = GasManager.main.GasRatio;
        winZone.sizeDelta = new Vector2(600 * GameManager.main.WinRatio, 10);
        loseZone.sizeDelta = new Vector2(600 * GameManager.main.LoseRatio, 10);
    }

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

    public void StartGameplay(string menuName)
    {
        GameObject.Find(menuName).SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        GameManager.main._isGameActive = true;
        pm.isPaused = false;
        pm.gameStarted = true;
        Time.timeScale = 1.0f;
    }

    #endregion
}
