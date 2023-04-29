using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VacuumBehaviour : MonoBehaviour
{
    public enum VacuumShape { Capsule, XZRange }

    [SerializeField] private VacuumShape _shape;
    [SerializeField] private LayerMask _gasMask;
    [SerializeField] private bool _activated;

    [SerializeField] private Transform _cameraObj;

    [Tooltip("The width of the handheld, the size of the stationary")]
    [SerializeField] private float _rangeRadius;
    [Tooltip("The length of the handheld, the height of the stationary")]
    [SerializeField] private float _rangeLength;
    [SerializeField] private float _disableRadius;
    [SerializeField] private float _strength;

    [SerializeField] private float _overchargeLength;
    [SerializeField] private float _overchargeStrength;

    [SerializeField] private Renderer _vacuumEffect;

    private PlayerMovement _plr;

    private void Start()
    {
        _plr = FindObjectOfType<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!_activated)
        {
            foreach (GasBehaviour curGas in GasManager.main.AllGas)
            {
                curGas.StopMovingToVacuum(this);
            }
            return;
        }

        switch (_shape)
        {
            case VacuumShape.Capsule:
                RaycastHit[] inRange = Physics.CapsuleCastAll(_cameraObj.position, _cameraObj.position + _cameraObj.forward, _rangeRadius / 2f, _cameraObj.forward, _plr.overcharged ? _overchargeLength : _rangeLength, _gasMask);

                List<Transform> inRangeList = new();
                foreach (RaycastHit ray in inRange)
                {
                    inRangeList.Add(ray.transform);
                }

                foreach (GasBehaviour curGas in GasManager.main.AllGas)
                {
                    if (inRangeList.Contains(curGas.transform))
                    {
                        curGas.MoveToVacuum(this);
                    }
                    else
                    {
                        curGas.StopMovingToVacuum(this);
                    }
                }
                break;
            case VacuumShape.XZRange:
                foreach (GasBehaviour curGas in GasManager.main.AllGas)
                {
                    float distance = Vector2.Distance(new Vector2(curGas.transform.position.x, curGas.transform.position.z), new Vector2(transform.position.x, transform.position.z));

                    if (distance <= _rangeRadius && curGas.transform.position.y - transform.position.y < _rangeLength)
                    {
                        curGas.MoveToVacuum(this);
                    }
                    else
                    {
                        curGas.StopMovingToVacuum(this);
                    }
                }
                break;
        }

        /*
        //print("let's check to see if im a handheld");
        if (_shape == VacuumShape.Capsule)
        {
            //print("turns out I am, let's get jiggy");
            RaycastHit[] inLargeRange = Physics.CapsuleCastAll(transform.position, transform.position + Vector3.up, _disableRadius, Vector3.up, 100, _gasMask);

            //print("Get ready, I'm about to spam test all the gas " + inLargeRange.Length + " times");
            foreach (RaycastHit curGas in inLargeRange)
            {
                //print("let's check gas O_o");
                curGas.transform.GetComponent<GasBehaviour>().MovingToVacuum(false, -1, transform);
            }

            //print("Finished the gas check for handheld");
        }

        if (_activated)
        {
            RaycastHit[] inRange;

            switch (_shape)
            {
                case VacuumShape.Capsule:
                    inRange = Physics.CapsuleCastAll(_cameraObj.position, _cameraObj.position + _cameraObj.forward, _rangeRadius / 2f, _cameraObj.forward, _plr.overcharged ? _overchargeLength : _rangeLength, _gasMask);
                    //print("guys guys I'm a handheld");
                    break;
                case VacuumShape.XZRange:
                    inRange = Physics.CapsuleCastAll(transform.position, transform.position + (Vector3.up * _rangeLength), _rangeRadius, Vector3.up, 1, _gasMask);
                    //print("guys guys I'm a standalone");
                    break;
                default:
                    inRange = Physics.SphereCastAll(transform.position, _rangeRadius, Vector3.up, _gasMask);
                    //print("how did we get here");
                    break;
            }

            //print("Get ready, I'm about to spam angry face about " + inRange.Length + " times");
            foreach (RaycastHit curGas in inRange)
            {
                //print("I'm about to get componenet, and no one can stop me!!! >:)");
                curGas.transform.GetComponent<GasBehaviour>().MovingToVacuum(true, _plr.overcharged ? _overchargeStrength : _strength, transform);
            }

            //print("my ssn is your mom");
        }
        */
    }

    public void SetActive(bool active)
    {
        _activated = active;
        _vacuumEffect.enabled = active;
    }
}
