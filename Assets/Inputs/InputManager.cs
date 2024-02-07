using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance;

    public InputActionReference jumpActionReference;
    public InputActionReference moveActionReference;

    private void Awake()
    {
        if (Instance != null)
        {
            if (Instance != this)
            {
                Destroy(Instance.gameObject);
            }
        }
        Instance = this;
    }
}
