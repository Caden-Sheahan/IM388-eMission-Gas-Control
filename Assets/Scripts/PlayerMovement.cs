using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody rb;
    public float moveSpeed = 5f;
    [SerializeField] private VacuumBehaviour _vacuum;

    Vector3 moveDirection = Vector3.zero;

    public float groundDistance;

    public float mouseSensitivity = 25f;
    public float xRot = 0f;
    public Transform playerCamera;


    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
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
        _vacuum.SetActive(value.isPressed);
    }

    public void OnLook(InputValue value)
    {
        float mouseX = value.Get<Vector2>().x * mouseSensitivity * Time.deltaTime;
        float mouseY = value.Get<Vector2>().y * mouseSensitivity * Time.deltaTime;

        xRot -= mouseY;
        xRot = Mathf.Clamp(xRot, -90f, 90f);

        playerCamera.localRotation = Quaternion.Euler(xRot, 0f, 0f);
        transform.Rotate(Vector3.up * mouseX);
    }

    public void OnSprint(InputValue value)
    {
        if (value.isPressed)
        {
            moveSpeed = 4f;
        }
        else
        {
            moveSpeed = 2.5f;
        }
    }

    private bool IsGrounded()
    {
        bool IsGrounded = Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit, 1.1f);
        return IsGrounded;
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = (transform.forward * moveDirection.z + transform.right * moveDirection.x) * moveSpeed + rb.velocity.y * Vector3.up;
    }
}
