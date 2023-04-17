using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GasBehaviour : MonoBehaviour
{
    private Vector3 _center;
    [SerializeField] private Vector3 _target;
    [SerializeField] private float _maxDistance;
    [SerializeField] private float _ambientSpeed;
    [SerializeField] private float _activeSpeed;
    private bool _movingToVacuum;
    private GasSpawner _associatedSpawner;

    private ParticleSystem _particles;
    [SerializeField] ParticleSystem _gasCenter;
    [SerializeField] Renderer _sphere;

    private void Awake()
    {
        _particles = GetComponent<ParticleSystem>();
    }

    public void Init(GasSpawner spawner)
    {
        _center = transform.position;
        _target = _center;
        _associatedSpawner = spawner;
    }

    public void Init(Vector3 target, GasSpawner spawner)
    {
        _center = transform.position;
        _target = target;
        _associatedSpawner = spawner;
    }

    void Update()
    {
        if (Vector3.Distance(transform.position, _target) < 0.1f)
        {
            if (_movingToVacuum)
            {
                StartCoroutine(TriggerDeath());
                return;
            }
            _target = (Random.insideUnitSphere * _maxDistance) + _center;
        }
        else
        {
            Vector3 direction = _target - transform.position;
            transform.position += (_movingToVacuum ? _activeSpeed : _ambientSpeed) * Time.deltaTime * direction.normalized;
        }
    }

    public void MovingToVacuum(bool isMoving, float strength = -1, Transform vacuum = null)
    {
        if (_movingToVacuum && isMoving)
        {
            return;
        }
        if (isMoving)
        {
            if (vacuum == null)
            {
                return;
            }
            _target = vacuum.position;
        }
        if (!isMoving)
        {
            _center = transform.position;
            _target = _center;
        }
        _movingToVacuum = isMoving;
        if (strength == -1)
        {
            strength = _ambientSpeed;
        }
        _activeSpeed = strength;
    }

    private IEnumerator TriggerDeath()
    {
        _sphere.enabled = false;
        _associatedSpawner.RemoveGas();
        _particles.Stop();
        _gasCenter.Stop();
        GetComponent<Collider>().enabled = false;
        transform.SetParent(null);
        yield return new WaitForSeconds(3.5f);
        Destroy(gameObject);
    }
}
