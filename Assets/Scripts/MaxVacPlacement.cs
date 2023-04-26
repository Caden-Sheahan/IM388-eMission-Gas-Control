using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MaxVacPlacement : MonoBehaviour
{
    //public Camera main;
    //PlayerMovement pm;

    //public GameObject maxVac;

    public LayerMask floorMask;

    // Update is called once per frame
    void Update()
    {
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out RaycastHit hit, 100f, floorMask))
        {
            transform.position = hit.point;
        }
    }

    //public void OnFire(InputValue value)
    //{
    //    if (value.isPressed && pm.placeVacuum == false && pm.placeMaxVac == true)
    //    {
    //        Instantiate(maxVac, transform.position, transform.rotation);
    //        Destroy(gameObject);
    //    }
    //}
}
