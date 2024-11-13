using UnityEngine;
using UnityEngine.InputSystem;
using NaughtyAttributes;

public class InstrumentManager : MonoBehaviour
{
    public Camera playerCamera;

    [Header("Left arm configs")] [SerializeField] [Tooltip("Input action for instrument in left hand")]
    private InputAction inputLeft;

    [SerializeField] private Transform parentLeftPrimary;
    [SerializeField] [ReadOnly] private Gun gunLeftPrimary;
    [SerializeField] private Transform parentLeftSecondary;
    [SerializeField] [ReadOnly] private Gun gunLeftSecondary;

    [Header("Right arm configs")] [SerializeField] [Tooltip("Input action for instrument in right hand")]
    private InputAction inputRight;

    [SerializeField] private Transform parentRightPrimary;
    [SerializeField] [ReadOnly] private Gun gunRightPrimary;
    [SerializeField] private Transform parentRightSecondary;
    [SerializeField] [ReadOnly] private Gun gunRightSecondary;

    private void SetupGun(Gun gun)
    {
        gunLeftPrimary = gun;
        gun.Spawn(parentLeftPrimary, this);
    }

    public bool PickUpGun(Gun gun)
    {
        // ThrowActiveGun();
        SetupGun(gun);
        return true;
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
            if (gunLeftPrimary) gunLeftPrimary.Shoot(direction);
            if (gunLeftSecondary) gunLeftSecondary.Shoot(direction);
        }
        if (inputRight.triggered)
        {
            if (gunRightPrimary) gunRightPrimary.Shoot(direction);
            if (gunRightSecondary) gunRightSecondary.Shoot(direction);
        }
    }
}