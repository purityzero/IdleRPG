
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public bool GetKeyDown(KeyCode key) => Input.GetKeyDown(key);
    public bool GetKeyUp(KeyCode key) => Input.GetKeyUp(key);
    public bool GetKey(KeyCode key) => Input.GetKey(key);
}
