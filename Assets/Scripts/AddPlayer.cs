using UnityEngine;
using UnityEngine.InputSystem;

public class AddPlayer : MonoBehaviour
{
    [SerializeField] GameObject player1Prefab;
    [SerializeField] GameObject player2Prefab;
    [SerializeField] Transform[] spawn;

    void Start()
    {
        if (Keyboard.current == null) return;

        int gamepadCount = Gamepad.all.Count;

        PlayerInput player1;
        PlayerInput player2;

        if (gamepadCount >= 2)
        {
            // 2+ mandos: cada jugador usa su propio mando con leftStick + cuadrado
            // Incluimos Keyboard para satisfacer el control scheme, pero cada jugador usa su gamepad
            player1 = PlayerInput.Instantiate(player1Prefab,
                controlScheme: "WASD",
                pairWithDevices: new InputDevice[] { Keyboard.current, Gamepad.all[0] });

            player2 = PlayerInput.Instantiate(player2Prefab,
                controlScheme: "ARROW",
                pairWithDevices: new InputDevice[] { Keyboard.current, Gamepad.all[1] });

            // Override bindings de P2 para usar leftStick en lugar de rightStick
            var moveActionP2 = player2.actions.FindAction("Move");
            if (moveActionP2 != null)
            {
                for (int i = 0; i < moveActionP2.bindings.Count; i++)
                {
                    var binding = moveActionP2.bindings[i];
                    if (!string.IsNullOrEmpty(binding.path) && binding.path.Contains("rightStick"))
                    {
                        string newPath = binding.path.Replace("rightStick", "leftStick");
                        moveActionP2.ApplyBindingOverride(i, newPath);
                    }
                }
            }

            // Override Interact de P1 para usar buttonWest (cuadrado) en lugar de leftShoulder
            var interactActionP1 = player1.actions.FindAction("Interact");
            if (interactActionP1 != null)
            {
                for (int i = 0; i < interactActionP1.bindings.Count; i++)
                {
                    var binding = interactActionP1.bindings[i];
                    if (!string.IsNullOrEmpty(binding.path) && binding.path.Contains("leftShoulder"))
                    {
                        interactActionP1.ApplyBindingOverride(i, "<Gamepad>/buttonWest");
                    }
                }
            }

            // Override Interact de P2 para usar buttonWest (cuadrado) en lugar de rightShoulder
            var interactActionP2 = player2.actions.FindAction("Interact");
            if (interactActionP2 != null)
            {
                for (int i = 0; i < interactActionP2.bindings.Count; i++)
                {
                    var binding = interactActionP2.bindings[i];
                    if (!string.IsNullOrEmpty(binding.path) && binding.path.Contains("rightShoulder"))
                    {
                        interactActionP2.ApplyBindingOverride(i, "<Gamepad>/buttonWest");
                    }
                }
            }

            GameManager.Instance._gamepad = true;
        }
        else if (gamepadCount == 1)
        {
            // 1 mando compartido: P1 con leftStick+LB, P2 con rightStick+RB
            var devices = new InputDevice[] { Keyboard.current, Gamepad.all[0] };

            player1 = PlayerInput.Instantiate(player1Prefab,
                controlScheme: "WASD",
                pairWithDevices: devices);

            player2 = PlayerInput.Instantiate(player2Prefab,
                controlScheme: "ARROW",
                pairWithDevices: devices);

            GameManager.Instance._gamepad = true;
        }
        else
        {
            // 0 mandos: ambos jugadores con teclado
            player1 = PlayerInput.Instantiate(player1Prefab,
                controlScheme: "WASD",
                pairWithDevices: new InputDevice[] { Keyboard.current });

            player2 = PlayerInput.Instantiate(player2Prefab,
                controlScheme: "ARROW",
                pairWithDevices: new InputDevice[] { Keyboard.current });

            GameManager.Instance._gamepad = false;
        }

        // Posicionar jugadores en spawns
        if (spawn.Length > 0)
        {
            player1.transform.parent.position = spawn[0].position;
        }

        if (spawn.Length > 1)
        {
            player2.transform.parent.position = spawn[1].position;
        }

        // Configurar referencias
        player1.gameObject.GetComponent<PlayerMovementComponent>()._player1 = true;
        player2.gameObject.GetComponent<PlayerMovementComponent>()._player1 = false;
        GameManager.Instance.SetUpPlayers(player1.gameObject, player2.gameObject);
    }
}
