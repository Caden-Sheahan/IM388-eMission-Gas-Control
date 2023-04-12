using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GasSpawner : MonoBehaviour
{
    [SerializeField] private bool _activated;
    [SerializeField] private Boundary _boundary;

    public bool Activated => _activated;

    public void SpawnGas(GasBehaviour gasObj, Transform parent)
    {
        Vector3 spawnPoint = _boundary.RandomPoint();
        GasBehaviour curGas = Instantiate(gasObj, spawnPoint, Quaternion.identity);
        curGas.Init();

        curGas.transform.SetParent(parent);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(1, _activated ? 1 : 0, _activated ? 1 : 0, 0.5f);
        Gizmos.DrawCube(_boundary.GetCenter(), _boundary.GetSize());
    }

    public void Simplify()
    {
        Vector3 center = _boundary.GetCenter();
        Vector3 size = _boundary.GetSize();

        transform.position = center;

        _boundary.SetSize(-size / 2f, size / 2f);
    }
}

[System.Serializable]
public class Boundary
{
    [SerializeField]
    private Transform _center;

    [SerializeField]
    private Vector3 _min;
    [SerializeField]
    private Vector3 _max;

    public void SetSize(Vector3 minSize, Vector3 maxSize)
    {
        _min = minSize;
        _max = maxSize;
    }
    public Vector3 GetCenter()
    {
        

        Vector3 position = new(
            (_min.x + _max.x) / 2f,
            (_min.y + _max.y) / 2f,
            (_min.z + _max.z) / 2f);

        return position + _center.position;
    }
    public Vector3 GetSize()
    {
        Vector3 size = new(
            _max.x - _min.x,
            _max.y - _min.y,
            _max.z - _min.z);

        return size;
    }
    public Vector3 RandomPoint()
    {
        Vector3 position = new(
            Random.Range(_min.x, _max.x),
            Random.Range(_min.y, _max.y),
            Random.Range(_min.x, _max.z));

        return position + _center.position;
    }
}
