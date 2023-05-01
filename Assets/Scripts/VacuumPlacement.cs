using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class VacuumPlacement : MonoBehaviour
{
    //PlayerMovement pm;

    //public GameObject vacuum;

    // Start is called before the first frame update
    void Start()
    {
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out RaycastHit hit, 100f) && hit.transform.tag == "Floor")
        {
            transform.position = hit.point;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out RaycastHit hit, 100f) && hit.transform.tag == "Floor")
        {
            transform.position = hit.point;
        }
    }

    //public void OnPlace(InputValue value)
    //{
    //    if (value.isPressed && pm.placeVacuum == true && pm.placeMaxVac == false)
    //    {
    //        Debug.Log("urmom");
    //        Instantiate(vacuum, transform.position, transform.rotation);
    //        gameObject.SetActive(false);
    //        pm.placeVacuum = false;
    //        pm.vacuumPlaced = true;
    //    }
    //}
}
