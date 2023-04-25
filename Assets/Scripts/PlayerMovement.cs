using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody rb;
    public float moveSpeed;
    public float vacSpeed;
    private float _curSpeed;
    [SerializeField] private VacuumBehaviour _vacuum;

    Vector3 moveDirection = Vector3.zero;

    public float groundDistance;

    public float mouseSensitivity = 25f;
    public float xRot = 0f;
    public Transform playerCamera;

    public bool isPaused = false;
    public GameObject pauseMenu;
    public bool gameStarted = false;

    public GameObject vacuumBP;
    public GameObject maxVacBP;
    public GameObject vacuum;
    public GameObject maxVac;
    public bool placeVacuum = false;
    public bool placeMaxVac = false;
    public bool vacuumPlaced = false;
    public bool maxVacPlaced = false;
    VacuumPlacement vp;
    MaxVacPlacement mp;
    public Transform camera;

    private void Start()
    {
        isPaused = true;
        Cursor.lockState = CursorLockMode.None;
        Time.timeScale = 0.0f;
    }

    public void OnMove(InputValue value)
    {
        Vector2 valueVect = value.Get<Vector2>();
        moveDirection = new Vector3(valueVect.x, 0, valueVect.y);
    }

    public void OnJump(InputValue value)
    {

        if (IsGrounded() == true)
        {
            GetComponent<Rigidbody>().AddForce(transform.up * 5, ForceMode.Impulse);
        }
    }

    public void OnFire(InputValue value)
    {
        _vacuum.SetActive(value.isPressed && placeVacuum == false && placeMaxVac == false);

        if (value.isPressed && placeVacuum == false && placeMaxVac == false)
        {
            _curSpeed = vacSpeed;
        }
        else
        {
            _curSpeed = moveSpeed;
        }

        if (value.isPressed && placeVacuum == true && placeMaxVac == false && vacuumPlaced == false)
        {
            Instantiate(vacuum, vacuumBP.transform.position, vacuumBP.transform.rotation);
            vacuumBP.SetActive(false);
            placeVacuum = false;
            vacuumPlaced = true;
        }

        else if (value.isPressed && placeVacuum == false && placeMaxVac == true && maxVacPlaced == false)
        {
            Instantiate(maxVac, maxVacBP.transform.position, maxVacBP.transform.rotation);
            maxVacBP.SetActive(false);
            placeMaxVac = false;
            maxVacPlaced = true;
        }

        //if (value.isPressed && placeVacuum == true && placeMaxVac == false)
        //{
        //    Instantiate(vacuumBP, vacuumBP.transform.position, vacuumBP.transform.rotation);
        //    Destroy(vacuumBP);
        //}

        //if (value.isPressed && placeVacuum == false && placeMaxVac == true)
        //{
        //    Instantiate(maxVacBP, maxVacBP.transform.position, maxVacBP.transform.rotation);
        //    Destroy(maxVacBP);
        //}
    }

    public void OnLook(InputValue value)
    {
        if (!isPaused)
        {
            float mouseX = value.Get<Vector2>().x * mouseSensitivity * Time.deltaTime;
            float mouseY = value.Get<Vector2>().y * mouseSensitivity * Time.deltaTime;

            xRot -= mouseY;
            xRot = Mathf.Clamp(xRot, -90f, 90f);

            playerCamera.localRotation = Quaternion.Euler(xRot, 0f, 0f);
            transform.Rotate(Vector3.up * mouseX);
        }
    }

    public void OnSprint(InputValue value)
    {
        
    }

    private bool IsGrounded()
    {
        bool IsGrounded = Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit, 1.1f);
        return IsGrounded;
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = (transform.forward * moveDirection.z + transform.right * moveDirection.x) * _curSpeed + rb.velocity.y * Vector3.up;
    }

    public void OnVacuum(InputValue value)
    {
        if (placeVacuum == false && placeMaxVac == false && vacuumPlaced == false)
        {
            placeVacuum = true;
            vacuumBP.SetActive(true);
        }

        else if (placeVacuum == true && placeMaxVac == false && vacuumPlaced == false)
        {
            placeVacuum = false;
            vacuumBP.SetActive(false);
        }

        else if (vacuumPlaced == true)
        {
            Debug.Log("Already Placed Vacuum");
        }
    }

    public void OnMaxVac(InputValue value)
    {
        if (placeVacuum == false && placeMaxVac == false && maxVacPlaced == false)
        {
            placeMaxVac = true;
            maxVacBP.SetActive(true);
        }

        else if (placeVacuum == false && placeMaxVac == true && maxVacPlaced == false)
        {
            placeMaxVac = false;
            maxVacBP.SetActive(false);
        }

        else if (maxVacPlaced == true)
        {
            Debug.Log("Already Placed MaxVac");
        }
    }

    public void OnPickUp(InputValue value)
    {
        //Not Working, not sure why. If you get rid of the && and just leave the raycast it will allow you to delete anything you look at
        Debug.Log("Why");
        if (Physics.Raycast(camera.position, camera.forward, out RaycastHit hit, 3f) && hit.transform.tag == "Pickup")
        {
            Debug.Log("Not");
            Destroy(hit.transform.gameObject);
        }
    }

    public void OnPause(InputValue value)
    {
        if (gameStarted)
        {
            if (!isPaused)
            {
                Cursor.lockState = CursorLockMode.None;
                GameManager.main._isGameActive = false;
                Time.timeScale = 0.0f;
                isPaused = true;
                pauseMenu.SetActive(true);
            }

            else if (isPaused)
            {
                Cursor.lockState = CursorLockMode.Locked;
                GameManager.main._isGameActive = true;
                Time.timeScale = 1.0f;
                isPaused = false;
                pauseMenu.SetActive(false);
            }
        }
    }

    //public void OnPlace(InputValue value)
    //{
    //    if (value.isPressed && placeVacuum == true && placeMaxVac == false && vacuumPlaced == false)
    //    {
    //        Instantiate(vacuum, vacuumBP.transform.position, vacuumBP.transform.rotation);
    //        vacuumBP.SetActive(false);
    //        placeVacuum = false;
    //        vacuumPlaced = true;
    //    }

    //    else if (value.isPressed && placeVacuum == false && placeMaxVac == true && maxVacPlaced == false)
    //    {
    //        Instantiate(maxVac, maxVacBP.transform.position, maxVacBP.transform.rotation);
    //        maxVacBP.SetActive(false);
    //        placeMaxVac = false;
    //        maxVacPlaced = true;
    //    }
    //}
}
