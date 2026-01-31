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

    private void OnTriggerStay2D(Collider2D collision)
    {

        if (collision.gameObject.layer == 7)
        {
            if (collision.gameObject.GetComponentInParent<Rigidbody2D>() != null)
            {
                Debug.Log("BLESSED CURSED");
                Debug.Log(collision.gameObject.GetComponentInParent<PlayerMovementComponent>().interact);

                if (collision.gameObject.GetComponentInParent<PlayerMovementComponent>().interact && joint.connectedBody == null)
                    JoinToObj(collision.gameObject.GetComponentInParent<Rigidbody2D>());
            }

            if (!collision.gameObject.GetComponentInParent<PlayerMovementComponent>().interact && joint.connectedBody != null)
                ReleaseObj();
        }   

        Debug.Log("no jodas eh");

        


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
