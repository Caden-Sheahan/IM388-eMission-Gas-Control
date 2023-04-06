using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VacuumBehaviour : MonoBehaviour
{
    public enum VacuumShape { Capsule, Sphere }

    [SerializeField] private VacuumShape _shape;
    [SerializeField] private LayerMask _gasMask;
    [SerializeField] private bool _activated;

    [SerializeField] private float _rangeSize;
    [SerializeField] private float _rangeLength;
    [SerializeField] private float _disableRadius;
    [SerializeField] private float _strength;

    // Update is called once per frame
    void Update()
    {
        if (_activated)
        {
            RaycastHit[] inRange;

            switch (_shape)
            {
                case VacuumShape.Capsule:
                    inRange = Physics.CapsuleCastAll(transform.position, transform.position + transform.forward, _rangeSize / 2f, transform.forward, _rangeLength, _gasMask);
                    break;
                default:
                    inRange = Physics.SphereCastAll(transform.position, _rangeLength, Vector3.up, _gasMask);
                    break;
            }

            RaycastHit[] inSphere = Physics.SphereCastAll(transform.position, _disableRadius, Vector3.up, _gasMask);

            foreach (RaycastHit curGas in inSphere)
            {
                curGas.transform.GetComponent<GasBehaviour>().MovingToVacuum(false);
            }

            foreach (RaycastHit curGas in inRange)
            {
                curGas.transform.GetComponent<GasBehaviour>().MovingToVacuum(true, _strength, transform);
            }
        }
    }
}
