using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VacuumBehaviour : MonoBehaviour
{
    [SerializeField] private LayerMask _gasMask;
    [SerializeField] private bool _activated;

    [SerializeField] private float _strength;

    // Update is called once per frame
    void Update()
    {
        if (_activated)
        {
            RaycastHit[] inCapsule = Physics.CapsuleCastAll(transform.position, transform.position + transform.forward, 0.5f, transform.forward, 5, _gasMask);
            RaycastHit[] inSphere = Physics.SphereCastAll(transform.position, 10, Vector3.up, _gasMask);

            foreach (RaycastHit curGas in inSphere)
            {
                curGas.transform.GetComponent<GasBehaviour>().MovingToVacuum(false);
            }

            foreach (RaycastHit curGas in inCapsule)
            {
                curGas.transform.GetComponent<GasBehaviour>().MovingToVacuum(true, _strength, transform);
            }
        }
    }
}
