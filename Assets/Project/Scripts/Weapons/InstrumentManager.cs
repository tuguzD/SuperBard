using UnityEngine;
using UnityEngine.InputSystem;

public class InstrumentManager : MonoBehaviour
{
    public Camera playerCamera;

    [Header("Left arm configs")] [SerializeField] [Tooltip("Input action for instrument in left hand")]
    private InputAction inputLeft;

    [SerializeField] private Transform parentLeftPrimary;
    private Gun _gunLeftPrimary;
    [SerializeField] private Transform parentLeftSecondary;
    private Gun _gunLeftSecondary;

    [Header("Right arm configs")] [SerializeField] [Tooltip("Input action for instrument in right hand")]
    private InputAction inputRight;

    [SerializeField] private Transform parentRightPrimary;
    private Gun _gunRightPrimary;
    [SerializeField] private Transform parentRightSecondary;
    private Gun _gunRightSecondary;

    private void SetupGun(Gun gun)
    {
        _gunLeftPrimary = gun;
        gun.Spawn(parentLeftPrimary, this);
    }

    public void PickUpGun(Gun gun)
    {
        // ThrowActiveGun();
        SetupGun(gun);
    }

    // public void ThrowActiveGun()
    // {
    //     // activeGun.Despawn();
    //     // Destroy(activeGun);
    // }

    private void OnEnable()
    {
        inputLeft.Enable();
        inputRight.Enable();
    }

    private void OnDisable()
    {
        inputLeft.Disable();
        inputRight.Disable();
    }

    private void Update()
    {
        var direction = playerCamera.transform.forward;
        if (inputLeft.triggered)
        {
            if (_gunLeftPrimary) _gunLeftPrimary.Shoot(direction);
            if (_gunLeftSecondary) _gunLeftSecondary.Shoot(direction);
        }

        if (inputRight.triggered)
        {
            if (_gunRightPrimary) _gunRightPrimary.Shoot(direction);
            if (_gunRightSecondary) _gunRightSecondary.Shoot(direction);
        }
    }
}