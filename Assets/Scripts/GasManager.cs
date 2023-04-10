using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GasManager : MonoBehaviour
{
    public static GasManager main;

    [SerializeField] private GasSpawner[] _spawners;
    [SerializeField] private Transform _gasParent;
    [SerializeField] private GasBehaviour _gasObj;

    [Header("Timing")]
    private float _curCooldownTime;

    public int GasCount => _gasParent.childCount;
    public float GasRatio => (float)_gasParent.childCount / GameManager.main.MaxGas;

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

    void Update()
    {
        if (_curCooldownTime > 0)
        {
            _curCooldownTime -= Time.deltaTime;
        }

        if (_curCooldownTime <= 0 && GasCount < GameManager.main.MaxGas && _spawners.Length != 0)
        {
            SpawnGas();
        }
    }

    private void SpawnGas()
    {
        List<GasSpawner> availableSpawners = new();

        foreach (GasSpawner curSpawner in _spawners)
        {
            if (curSpawner.Activated)
            {
                availableSpawners.Add(curSpawner);
            }
        }

        GasSpawner spawner = availableSpawners[Random.Range(0, availableSpawners.Count)];
        spawner.SpawnGas(_gasObj, _gasParent);
        _curCooldownTime = GameManager.main.GetCooldownTime();
    }
}
