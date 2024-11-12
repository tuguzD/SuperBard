using UnityEngine;
using UnityEngine.InputSystem;

public class GunAction : MonoBehaviour
{
    [Tooltip("Input action to register by indicator")]
    public InputAction input;

    public Camera playerCamera;

    private void OnEnable()
    {
        input.Enable();
    }

    private void OnDisable()
    {
        input.Disable();
    }
    
    [SerializeField]
    private GunSelector gunSelector;

    private void Update()
    {
        if (input.triggered && gunSelector.activeGun != null)
            gunSelector.activeGun.Shoot(playerCamera.transform.forward);
    }
}