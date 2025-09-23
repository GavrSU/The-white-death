using UnityEngine;
using UnityEngine.Assertions.Comparers;
using UnityEngine.InputSystem;

public class Go : MonoBehaviour
{
    public InputActionAsset inputActions;
    private InputAction a_move;
    private InputAction a_look;
    private InputAction a_jump;

    private Vector2 move;
    private Vector2 look;
    private Animator animator;
    private Rigidbody rigidbody;
    public float walkspeed = 5;
    public float rotatespeed = 5;
    public float jumpspeed = 5;

    private void Awake()
    {
        a_move = InputSystem.actions.FindAction("Move");
        a_look = InputSystem.actions.FindAction("Look");
        a_jump = InputSystem.actions.FindAction("Jump");

        animator = GetComponent<Animator>();
        rigidbody = GetComponent<Rigidbody>();
        Cursor.lockState = CursorLockMode.Locked;
    }
    private void Update()
    {
        move = a_move.ReadValue<Vector2>();
        look = a_look.ReadValue<Vector2>();

        if (a_jump.WasCompletedThisFrame())
        {
            Jump();
        }

    }


    public void Jump()
    {
        rigidbody.AddForceAtPosition(new Vector3(0, 5f, 0), Vector3.up, ForceMode.Impulse);
        //animator.SetTrigger("junp");

    }
    private void FixedUpdate()
    {
        Walking();
        Rotating();
    }
    private void Walking()
    {
    
    animator.SetFloat("Speed",move.y);
        rigidbody.MovePosition(rigidbody.position + transform.forward * move.y * walkspeed * Time.deltaTime);

    }
    private void Rotating()
    {
        if (move.y != 0)
        {
            float rotationAmount = look.x * rotatespeed * Time.deltaTime;
            Quaternion deltaRotation = Quaternion.Euler(0, rotationAmount, 0);
            rigidbody.MoveRotation(rigidbody.rotation * deltaRotation);


        }



}






}