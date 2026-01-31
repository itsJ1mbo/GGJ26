using UnityEngine;
using UnityEngine.InputSystem;

public class AddPlayer : MonoBehaviour
{
    [SerializeField] GameObject player1Prefab;
    [SerializeField] GameObject player2Prefab;
    [SerializeField] Transform[] spawn;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (Keyboard.current == null) return;

        // mete al player1
        var player1 = PlayerInput.Instantiate(player1Prefab,
            controlScheme: "WASD",
            pairWithDevice: Keyboard.current);

        if (spawn.Length > 0)
        {
            player1.transform.parent.position = spawn[0].position;
        }

        var player2 = PlayerInput.Instantiate(player2Prefab,
            controlScheme: "ARROW",
            pairWithDevice: Keyboard.current);

        if (spawn.Length > 1)
        {
            player2.transform.parent.position = spawn[1].position;
        }
        
        GameManager.Instance.SetUpPlayers(player1.gameObject, player2.gameObject);
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
