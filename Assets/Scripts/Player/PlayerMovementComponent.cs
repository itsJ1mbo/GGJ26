using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;
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

    [HideInInspector] public bool _player1;

    private bool _firstMove = true;

    Animator animator;
    SpriteRenderer spriteRenderer;


    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();

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
        if (_firstMove)
        {
            GameManager.Instance.SetFirstMove(_player1);
            _firstMove = false;
        }

        direction = obj.ReadValue<Vector2>();

        if (direction.x > 0)
        {
            GetComponent<SpriteRenderer>().flipX = false;
        }
        else if (direction.x < 0)
        {
            GetComponent<SpriteRenderer>().flipX = true;
        }

        if(animator != null)
        {
            if (obj.canceled)
                animator.SetBool("walking", false);
            else
                animator.SetBool("walking", true);
        }

    }

    public void Interact(InputAction.CallbackContext value)
    {
        

        if(joint != null && joint.connectedBody != null)
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
