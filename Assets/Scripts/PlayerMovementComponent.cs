using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovementComponent : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void MovePlayer1(InputAction.CallbackContext obj)
    {

        Vector2 dir = obj.ReadValue<Vector2>();

        Debug.Log("player 1 goes to " + dir);


    }

    public void MovePlayer2(InputAction.CallbackContext obj)
    {
        Vector2 dir = obj.ReadValue<Vector2>();


        Debug.Log("player 2 goes to " + dir);

    }

}
