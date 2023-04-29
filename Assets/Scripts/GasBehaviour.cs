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
    [SerializeField] private bool _movingToVacuum;
    [SerializeField] private VacuumBehaviour _vacuumTarget;
    private GasSpawner _associatedSpawner;

    private ParticleSystem _particles;
    [SerializeField] ParticleSystem _gasCenter;
    [SerializeField] Renderer _sphere;

    private bool _imFlyinIn;

    private void Awake()
    {
        _particles = GetComponent<ParticleSystem>();
        _imFlyinIn = false;
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
        if (Vector3.Distance(transform.position, _target) < 0.01f)
        {
            if (_vacuumTarget != null)
            {
                StartCoroutine(TriggerDeath());
                return;
            }
            _target = (Random.insideUnitSphere * _maxDistance) + _center;
        }
        else
        {
            Vector3 direction = _target - transform.position;
            transform.position += (_vacuumTarget != null ? _activeSpeed : _ambientSpeed) * Time.deltaTime * direction.normalized;
        }
    }

    public void MoveToVacuum(VacuumBehaviour vacuum)
    {
        if (_vacuumTarget != null)
        {
            return;
        }

        print("I'm moving to a vacuum");
        _vacuumTarget = vacuum;
        _target = _vacuumTarget.transform.position;

        GetComponentInChildren<Renderer>().material.color = Color.blue;
    }

    public void StopMovingToVacuum(VacuumBehaviour vacuum)
    {
        if (_vacuumTarget != vacuum)
        {
            return;
        }

        print("I'm no longer moving to a vacuum");
        _vacuumTarget = null;
        _center = transform.position;
        _target = _center;

        GetComponentInChildren<Renderer>().material.color = Color.white;
    }

    /*public void MovingToVacuum(bool isMoving, float strength = -1, Transform vacuum = null)
    {
        //print("Wowee zowee I've been found by a vacuum");
        if (_movingToVacuum && isMoving)
        {
            //print("Turns out I'm currently moving at a vacuum and should be");
            return;
        }
        //print("Do they want me to move?");
        if (isMoving)
        {
            //print("They want me to move");
            if (vacuum == null)
            {
                //print("There's no vacuum, so I'm not going to do anything");
                return;
            }
            //print("Set imFlyinIn to true");
            _imFlyinIn = true;
            //print("Set the target to the vacuum");
            _target = vacuum.position;
        }
        //print("Do they want me to stop and I'm flyin in?");
        if (!isMoving && _imFlyinIn)
        {
            //print("I'm no longer flyin in");
            _imFlyinIn = false;
            //print("My current position is now my center");
            _center = transform.position;
            //print("I'm now targeting my center");
            _target = _center;
        }
        //print("Setting movingToVacuum to isMoving, " + isMoving);
        _movingToVacuum = isMoving;
        //print("Is strength -1?");
        if (strength == -1)
        {
            //print("Yes it is >:)");
            strength = _ambientSpeed;
        }
        //print("Setting activeSpeed to strength, far be it from me to know what that does");
        _activeSpeed = strength;

        //print("uwu xd I want to die lol");
    }*/

    private IEnumerator TriggerDeath()
    {
        _sphere.enabled = false;
        _particles.Stop();
        _gasCenter.Stop();
        GetComponent<Collider>().enabled = false;
        transform.SetParent(null);
        yield return new WaitForSeconds(3.5f);
        _associatedSpawner.RemoveGas();
        Destroy(gameObject);
    }
}
