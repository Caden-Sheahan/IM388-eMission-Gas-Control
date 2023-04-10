using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager main;

    [SerializeField] private float _winRatio;
    [SerializeField] private float _loseRatio;
    [SerializeField] private int _maxGas;

    public float WinRatio => _winRatio;
    public float LoseRatio => _loseRatio;
    public int MaxGas => _maxGas;

    private void Awake()
    {
        if (main == null)
        {
            main = this;
        }
        else
        {
            Destroy(this);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
