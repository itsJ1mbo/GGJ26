using UnityEngine;
using UnityEngine.InputSystem;

public class AddPlayer : MonoBehaviour
{
    [SerializeField] GameObject playerPrefabRed;
    [SerializeField] GameObject playerPrefabBlue;
    [SerializeField] GameObject playerPrefabGreen;
    [SerializeField] Transform[] spawn;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (Keyboard.current == null) return;

        Debug.Log("si o que");

        // mete al player1
        var player1 = PlayerInput.Instantiate(playerPrefabRed,
            controlScheme: "WASD",
            pairWithDevice: Keyboard.current);

        if (spawn.Length > 0)
        {
            player1.transform.position = spawn[0].position;
            player1.GetComponent<AuraComponent>().SetInitialColor(1);
        }

        Debug.Log("hmmmm");

        var player2 = PlayerInput.Instantiate(playerPrefabBlue,
            controlScheme: "ARROW",
            pairWithDevice: Keyboard.current);

        if (spawn.Length > 1)
        {
            player2.transform.position = spawn[1].position;
            player2.GetComponent<AuraComponent>().SetInitialColor(2);
        }


        Debug.Log("KJGHWFKJHLSDHFKSLDJFBGLA");
        GameManager.Instance.AddPlayers(player1.gameObject, player2.gameObject);
    }

    // Update is called once per frame
    void Update()
    {

    }


    static public void setActionMap()
    {
        GameObject pf;

        //PlayerInputManager pim = getComponent

       

    }
}
