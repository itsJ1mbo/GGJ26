using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Windows;

public class PlayerMovementComponent : MonoBehaviour
{
    Vector2 direction;
    [SerializeField]
    float speed = 1;

    [SerializeField]
    int playerID = 1;

    [SerializeField]
    public bool canInteract = false;

    Rigidbody2D rb;
    public HingeJoint2D joint;


    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

        rb.gravityScale = 0;
        rb.freezeRotation = true;

        canInteract = false;

        joint = null;
    }

    void FixedUpdate()
    {
        rb.linearVelocity = direction * speed;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {



    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnMove(InputAction.CallbackContext obj)
    {
        direction = obj.ReadValue<Vector2>();

    }


    public void Interact(InputAction.CallbackContext value)
    {
        if (value.started)
            Debug.Log("started");
        else if (value.performed)
            Debug.Log("performed");
        else if (value.canceled)
            Debug.Log("canceled");


        if(joint.connectedBody != null)
        {
            canInteract = true;
        }

        if (value.canceled && canInteract)
        {
            if (joint.connectedBody == null)
            {
                joint.connectedBody = rb;
                Rigidbody2D thisRb = joint.gameObject.GetComponent<Rigidbody2D>();
                thisRb.bodyType = RigidbodyType2D.Dynamic;
            }
            else
            {
                Rigidbody2D thisRb = joint.gameObject.GetComponent<Rigidbody2D>();
                thisRb.bodyType = RigidbodyType2D.Kinematic;
                joint.connectedBody = null;
            }
        }

        //// se lo settea como bien pueda
        //if (joint.connectedBody == null)
        //{
        //    Debug.Log("se conecta???");
        //    joint.connectedBody = rb;
        //    Rigidbody2D thisRb = joint.gameObject.GetComponent<Rigidbody2D>();
        //    thisRb.bodyType = RigidbodyType2D.Dynamic;

        //}
        //else
        //{
        //    Debug.Log("se desconecta :)");

        //    Rigidbody2D thisRb = joint.gameObject.GetComponent<Rigidbody2D>();
        //    thisRb.bodyType = RigidbodyType2D.Kinematic;
        //    joint.connectedBody = null;
        //}

    }


    public void InteractRelease()
    {

    }
}
