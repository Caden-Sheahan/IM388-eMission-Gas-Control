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
        print("hello");

        if (_curCooldownTime > 0)
        {
            _curCooldownTime -= Time.deltaTime;
        }

        if (_curCooldownTime <= 0 && GasCount < GameManager.main.MaxGas && _spawners.Length != 0)
        {
            SpawnGas();
        }
    }

    public void InitialGas(int gasCount)
    {
        for (int i = 0; i < gasCount; i++)
        {
            SpawnGas();
        }
    }

    private void SpawnGas()
    {
        GasSpawner spawner = RandomWeightedSpawner();
        spawner.SpawnGas(_gasObj, _gasParent);
        _curCooldownTime = GameManager.main.GetCooldownTime();
    }

    private GasSpawner RandomWeightedSpawner()
    {
        Dictionary<GasSpawner, float> availableSpawners = new();
        float total = 0;

        foreach (GasSpawner curSpawner in _spawners)
        {
            if (curSpawner.Activated)
            {
                availableSpawners.Add(curSpawner, total += curSpawner.GetVolume());
            }
        }

        float value = Random.Range(0, total);

        foreach (KeyValuePair<GasSpawner,float> curSpawner in availableSpawners)
        {
            if (value < curSpawner.Value)
            {
                return curSpawner.Key;
            }
        }

        return _spawners[^1];
    }
}
