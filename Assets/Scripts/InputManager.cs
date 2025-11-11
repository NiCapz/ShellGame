using UnityEngine;

public class InputManager : MonoBehaviour
{
    
    public static InputManager Instance { get; private set; }

    private InputSystem_Actions playerInput;
    public Vector2 mousePos;
    
    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;

        playerInput = new InputSystem_Actions();
        playerInput.Player.Enable();
        playerInput.UI.Enable();
    }


}
