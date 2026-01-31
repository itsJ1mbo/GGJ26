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

            if (collision.gameObject.GetComponentInChildren<Rigidbody2D>() != null)
            {
                JoinToObj(collision.gameObject.GetComponentInChildren<Rigidbody2D>());
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 7)
        {
            Debug.Log("QUEEEE");
           // Release();

        }
    }

    public void JoinToObj(Rigidbody2D rb) 
    {

        joint.connectedBody = rb;
    
    }

    public void Release()
    {
        joint.connectedBody = null;

    }
}
