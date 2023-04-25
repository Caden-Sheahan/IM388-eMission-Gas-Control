using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MaxVacPlacement : MonoBehaviour
{
    public Transform camera;
    //public Camera main;
    //PlayerMovement pm;

    //public GameObject maxVac;

    // Start is called before the first frame update
    void Start()
    {
        if (Physics.Raycast(camera.position, camera.forward, out RaycastHit hit, 3f) && hit.transform.tag == "Floor")
        {
            transform.position = hit.point;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Physics.Raycast(camera.position, camera.forward, out RaycastHit hit, 3f) && hit.transform.tag == "Floor")
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
