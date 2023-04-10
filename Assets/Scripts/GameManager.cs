using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager main;

    [Header("Gas Management")]
    [SerializeField] [Tooltip("0 = Easiest, 1 = Hardest")] [Range(0, 1)] private float _difficulty;
    [SerializeField] private AnimationCurve _spawnDelay;
    [SerializeField] [Range(0, 0.5f)] private float _winRatio;
    [SerializeField] [Range(0, 0.5f)] private float _loseRatio;
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

    public float GetCooldownTime()
    {
        return _spawnDelay.Evaluate(_difficulty);
    }
}
