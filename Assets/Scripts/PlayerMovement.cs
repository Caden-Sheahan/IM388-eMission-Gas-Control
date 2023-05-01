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

    public float overchargeSpeed;
    public float overchargeVacSpeed;

    Vector3 moveDirection = Vector3.zero;

    public float groundDistance;

    public float mouseSensitivity = 25f;
    public float xRot = 0f;
    public Transform playerCamera;

    public bool isPaused = false;
    public GameObject pauseMenu;
    public bool gameStarted = false;

    //public GameObject vacuumBP;
    //public GameObject maxVacBP;
    public GameObject bP;
    public GameObject vacuum;
    public GameObject maxVac;
    public bool placeVacuum = false;
    public bool placeMaxVac = false;
    public bool vacuumPlaced = false;
    public bool maxVacPlaced = false;
    public float vacuumCooldown;
    public float curVacuumCooldown;
    public float maxVacCooldown;
    public float maxVacRuntime;
    public float curMaxVacCooldown;
    VacuumPlacement vp;
    MaxVacPlacement mp;

    public bool overcharged;
    [SerializeField] private bool _isSucking;

    private void Start()
    {
        isPaused = true;
        Cursor.lockState = CursorLockMode.None;
        Time.timeScale = 0.0f;

        curVacuumCooldown = vacuumCooldown;
        curMaxVacCooldown = maxVacCooldown;
    }

    public void OnMove(InputValue value)
    {
        Vector2 valueVect = value.Get<Vector2>();
        moveDirection = new Vector3(valueVect.x, 0, valueVect.y);
    }

    public void OnJump(InputValue value)
    {

        if (IsGrounded())
        {
            GetComponent<Rigidbody>().AddForce(transform.up * 5, ForceMode.Impulse);
        }
    }

    public void OnFire(InputValue value)
    {
        if (!placeMaxVac && !placeVacuum)
        {
            _isSucking = value.isPressed;
            return;
        }

        if (value.isPressed)
        {
            if (placeVacuum && !placeMaxVac && !vacuumPlaced)
            {
                vacuum.transform.position = bP.transform.position;
                bP.SetActive(false);
                placeVacuum = false;
                vacuumPlaced = true;
            }

            else if (!placeVacuum && placeMaxVac && !maxVacPlaced)
            {
                maxVac.transform.position = bP.transform.position;
                bP.SetActive(false);
                placeMaxVac = false;
                maxVacPlaced = true;
                curMaxVacCooldown = maxVacRuntime;
            }
        }
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
        bool IsGrounded = Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit, 0.95f);
        return IsGrounded;
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = (transform.forward * moveDirection.z + transform.right * moveDirection.x) * _curSpeed + rb.velocity.y * Vector3.up;

        _vacuum.SetActive(_isSucking && !placeVacuum && !placeMaxVac);

        if (_isSucking || placeVacuum || placeMaxVac)
        {
            _curSpeed = overcharged ? overchargeVacSpeed : vacSpeed;
        }
        else
        {
            _curSpeed = overcharged ? overchargeSpeed : moveSpeed;
        }

        if (curVacuumCooldown > 0)
        {
            curVacuumCooldown -= Time.deltaTime;
        }
        if (curMaxVacCooldown > 0)
        {
            curMaxVacCooldown -= Time.deltaTime;
        }

        if (curMaxVacCooldown <= 0 && maxVacPlaced)
        {
            maxVac.transform.position = 100 * Vector3.up;
            maxVacPlaced = false;
            curMaxVacCooldown = maxVacCooldown;
        }
    }

    public void OnVacuum(InputValue value)
    {
        if (vacuumPlaced || curVacuumCooldown > 0)
        {
            return;
        }

        placeVacuum = !placeVacuum;
        bP.SetActive(placeVacuum);
        bP.GetComponentInChildren<Renderer>().material.color = Color.red * 0.5f;

        if (placeMaxVac && placeVacuum)
        {
            placeMaxVac = false;
        }
    }

    public void OnMaxVac(InputValue value)
    {
        if (maxVacPlaced || curMaxVacCooldown > 0)
        {
            return;
        }

        placeMaxVac = !placeMaxVac;
        bP.SetActive(placeMaxVac);
        bP.GetComponentInChildren<Renderer>().material.color = Color.yellow * 0.5f;

        if (placeMaxVac && placeVacuum)
        {
            placeVacuum = false;
        }
    }

    public void OnPickUp(InputValue value)
    {
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out RaycastHit hit, 3f) && hit.transform.CompareTag("Pickup"))
        {
            if (hit.transform.parent.GetComponent<VacuumBehaviour>())
            {
                vacuumPlaced = false;
                curVacuumCooldown = vacuumCooldown;
            }
            else if (hit.transform.parent.GetComponent<MaxVacBehaviour>())
            {
                maxVacPlaced = false;
                curMaxVacCooldown = maxVacCooldown;
            }
            hit.transform.parent.position = 100 * Vector3.up;
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

    public void OnPlace(InputValue value)
    {
        if (value.isPressed)
        {
            if (placeVacuum && !placeMaxVac && !vacuumPlaced)
            {
                vacuum.transform.position = bP.transform.position;
                bP.SetActive(false);
                placeVacuum = false;
                vacuumPlaced = true;
            }

            else if (!placeVacuum && placeMaxVac && !maxVacPlaced)
            {
                maxVac.transform.position = bP.transform.position;
                bP.SetActive(false);
                placeMaxVac = false;
                maxVacPlaced = true;
                curMaxVacCooldown = maxVacRuntime;
            }
        }
    }
}
