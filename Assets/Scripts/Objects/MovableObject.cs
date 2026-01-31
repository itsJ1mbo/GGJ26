using UnityEngine;

public class MovableObject : MonoBehaviour
{
    [SerializeField]
    HingeJoint2D joint;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        


    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 7)
        {
            if (collision.gameObject.GetComponentInParent<PlayerMovementComponent>() != null)
            {
                collision.gameObject.GetComponentInParent<PlayerMovementComponent>().canInteract = true;
                collision.gameObject.GetComponentInParent<PlayerMovementComponent>().joint = joint;


            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 7)
        {
            if (collision.gameObject.GetComponentInParent<Rigidbody2D>() != null)
            {

                collision.gameObject.GetComponentInParent<PlayerMovementComponent>().canInteract = false;
            }
        }
    }

 

    public void JoinToObj(Rigidbody2D rb) 
    {
        joint.connectedBody = rb;

        Rigidbody2D thisRb = GetComponentInParent<Rigidbody2D>();
        thisRb.bodyType = RigidbodyType2D.Dynamic;
    }

    public void ReleaseObj()
    {
        joint.connectedBody = null;
        Rigidbody2D thisRb = GetComponentInParent<Rigidbody2D>();
        thisRb.bodyType = RigidbodyType2D.Kinematic;
    }
}
