using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovementComponent : MonoBehaviour
{
    Vector2 direction;
    [SerializeField]
    float speed = 1;

    [SerializeField]
    int playerID = 1;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
      
        

    }

    // Update is called once per frame
    void Update()
    {

        // 
        float xVel = speed * Time.deltaTime * direction.x;
        float yVel = speed * Time.deltaTime * direction.y;
        transform.position += new Vector3(xVel, yVel, 0 );

    }

    public void OnMove(InputAction.CallbackContext obj)
    {
        if(playerID == 1)
        {
            direction = obj.ReadValue<Vector2>();
            Debug.Log("Player 1 goes to " + direction);
        }
        else if (playerID == 2)
        {
            direction = obj.ReadValue<Vector2>();
            Debug.Log("Player 2 goes to " + direction);
        }




    }






}
