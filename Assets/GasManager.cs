using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GasManager : MonoBehaviour
{
    [SerializeField] private GasSpawner[] _spawners;
    [SerializeField] private Transform _gasParent;
    [SerializeField] private GasBehaviour _gasObj;

    [Header("Gas Spawning")]
    [SerializeField][Tooltip("0 = Easiest, 1 = Hardest")][Range(0, 1)] private float _difficulty;
    [SerializeField] private AnimationCurve _spawnDelay;
    [SerializeField] private int _maxGasCount;

    [Header("Timing")]
    private float _curCooldownTime;

    void Update()
    {
        if (_curCooldownTime > 0)
        {
            _curCooldownTime -= Time.deltaTime;
        }

        if (_curCooldownTime <= 0 && GasCount() < _maxGasCount)
        {
            SpawnGas();
        }
    }

    private int GasCount()
    {
        return _gasParent.childCount;
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
        _curCooldownTime = _spawnDelay.Evaluate(_difficulty);
    }
}
