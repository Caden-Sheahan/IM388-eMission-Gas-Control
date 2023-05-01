using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [Header("HUD")]
    public Slider gasCount; // slider showing how much gas there is in game
    public RectTransform winZone; // slider zone for winning
    public RectTransform loseZone; // slider zone for losing
    public GameObject endTimer;
    public TMP_Text endTimerText;

    [Header("Machines")]
    private PlayerMovement _plr;
    public GameObject vacuumInput;
    public GameObject maxVacInput;
    public TMP_Text vacuumTimer;
    public TMP_Text maxVacTimer;
    public Image vacuumImage;
    public Image maxVacImage;
    public Image selectVacuum;
    public Image selectMaxVac;
    public Color vacuumColor;
    public Color maxVacColor;
    public GameObject _maxVacOutline;

    public PlayerMovement pm;

    private void Start()
    {
        _plr = FindObjectOfType<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        _maxVacOutline.SetActive(_plr.overcharged);

        gasCount.value = 1 - GasManager.main.GasRatio;
        winZone.sizeDelta = new Vector2(600 * GameManager.main.WinRatio, 10);
        loseZone.sizeDelta = new Vector2(600 * GameManager.main.LoseRatio, 10);

        switch (GameManager.main.Status)
        {
            case 0: // middle
                {
                    endTimer.SetActive(false);
                    break;
                }
            case 1: // winning
                {
                    endTimer.SetActive(true);
                    endTimerText.text = "You will win in " + (Mathf.FloorToInt(GameManager.main.CurTime) + 1) + " seconds";
                    endTimerText.color = new Color(0.639f, 0.866f, 0.608f);
                    break;
                }
            case -1: // losing
                {
                    endTimer.SetActive(true);
                    endTimerText.text = "You will lose in " + (Mathf.FloorToInt(GameManager.main.CurTime) + 1) + " seconds";
                    endTimerText.color = new Color(0.866f, 0.624f, 0.608f);
                    break;
                }
            default:
                {
                    print("End text brokey");
                    break;
                }
        }

        if (GameManager.main.CurTime <= 0)
        {
            endTimer.SetActive(false);
        }

        vacuumTimer.text = _plr.curVacuumCooldown <= 0 && !_plr.vacuumPlaced ? "Q" : _plr.curVacuumCooldown <= 0 ? "" : Mathf.Ceil(_plr.curVacuumCooldown) + "";
        vacuumImage.color = (_plr.curVacuumCooldown <= 0 && !_plr.vacuumPlaced) || _plr.vacuumPlaced ? vacuumImage.color : Color.gray;
        maxVacTimer.text = _plr.curMaxVacCooldown <= 0 ? "E" : Mathf.Ceil(_plr.curMaxVacCooldown) + "";
        maxVacImage.color = (_plr.curMaxVacCooldown <= 0 && !_plr.maxVacPlaced) || (_plr.curMaxVacCooldown > 0 && _plr.maxVacPlaced) ? maxVacImage.color : Color.gray;
        selectVacuum.color = _plr.placeVacuum ? Color.white : Color.clear;
        selectMaxVac.color = _plr.placeMaxVac ? Color.white : Color.clear;
        if (_plr.curVacuumCooldown <= 0)
        {
            vacuumImage.color = vacuumColor;
        }
        if (_plr.curMaxVacCooldown <= 0)
        {
            maxVacImage.color = maxVacColor;
        }

        //if (_plr.placeVacuum)
        //{
        //    vacuumInput.SetActive(false);
        //}
        //else if (_plr.placeMaxVac)
        //{
        //    maxVacInput.SetActive(false);
        //}
        //if (_plr.curVacuumCooldown <= 0)
        //{
        //    vacuumInput.SetActive(true);
        //}
        //else if (_plr.curMaxVacCooldown <= 0)
        //{
        //    maxVacInput.SetActive(true);
        //}
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

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void StartGameplay(string menuName)
    {
        UnpauseGame(menuName);
        GameManager.main.StartGame();
    }

    public void UnpauseGame(string menuName)
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
