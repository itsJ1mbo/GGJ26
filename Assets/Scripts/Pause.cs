using UnityEngine;
using UnityEngine.InputSystem;

public class Pause : MonoBehaviour
{
    [SerializeField] GameObject menuPausa;

    bool juegoPausado;
    InputAction pauseAction;

    void Awake()
    {
        pauseAction = new InputAction(
            "Pause",
            InputActionType.Button,
            "<Keyboard>/escape"
        );
    }

    void OnEnable()
    {
        pauseAction.Enable();
        pauseAction.performed += OnPause;
    }

    void OnDisable()
    {
        pauseAction.performed -= OnPause;
        pauseAction.Disable();
    }

    void OnPause(InputAction.CallbackContext ctx)
    {
        if (juegoPausado)
            Reanudar();
        else
            Pausar();
    }

    public void Reanudar()
    {
        menuPausa.SetActive(false);
        Time.timeScale = 1f;
        juegoPausado = false;
    }

    public void Pausar()
    {
        menuPausa.SetActive(true);
        Time.timeScale = 0f;
        juegoPausado = true;
    }
}
