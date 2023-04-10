using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VacuumBehaviour : MonoBehaviour
{
    public enum VacuumShape { Capsule, XZRange }

    [SerializeField] private VacuumShape _shape;
    [SerializeField] private LayerMask _gasMask;
    [SerializeField] private bool _activated;

    [Tooltip("The width of the handheld, the size of the stationary")]
    [SerializeField] private float _rangeRadius;
    [Tooltip("Only used for the handheld vacuum")]
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
                    inRange = Physics.CapsuleCastAll(transform.position, transform.position + transform.forward, _rangeRadius / 2f, transform.forward, _rangeLength, _gasMask);
                    break;
                case VacuumShape.XZRange:
                    inRange = Physics.CapsuleCastAll(transform.position, transform.position + Vector3.up, _rangeRadius, Vector3.up, 100, _gasMask);
                    break;
                default:
                    inRange = Physics.SphereCastAll(transform.position, _rangeRadius, Vector3.up, _gasMask);
                    break;
            }

            RaycastHit[] inLargeRange = Physics.CapsuleCastAll(transform.position, transform.position + Vector3.up, _disableRadius, Vector3.up, 100, _gasMask);

            foreach (RaycastHit curGas in inLargeRange)
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
