using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Slider gasCount;
    public RectTransform winZone;
    public RectTransform loseZone;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        gasCount.value = GasManager.main.GasRatio;
        winZone.sizeDelta = new Vector2(600 * GameManager.main.WinRatio, 10);
        loseZone.sizeDelta = new Vector2(600 * GameManager.main.LoseRatio, 10);
    }
}
