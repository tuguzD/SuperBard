using System;
using UnityEngine;
using UnityEngine.InputSystem;
using NaughtyAttributes;

[Serializable] public struct GunSlot
{
    [SerializeField] public Transform parent;
    [SerializeField] [ReadOnly] public Gun gun;
}

public class InstrumentManager : MonoBehaviour
{
    public Camera playerCamera;

    [Header("Left arm configs")] [SerializeField] [Tooltip("Input action for instrument in left hand")]
    private InputAction inputLeft;
    [SerializeField] private GunSlot leftPrimary;
    [SerializeField] private GunSlot leftSecondary;

    [Header("Right arm configs")] [SerializeField] [Tooltip("Input action for instrument in right hand")]
    private InputAction inputRight;
    [SerializeField] private GunSlot rightPrimary;
    [SerializeField] private GunSlot rightSecondary;

    private void SetupGun(Gun gun, ref GunSlot parentSlot)
    {
        parentSlot.gun = gun;
        gun.Spawn(parentSlot.parent, this);
    }

    public bool PickUpGun(Gun gun)
    {
        // ThrowActiveGun();
        SetupGun(gun, ref leftPrimary);
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
            if (leftPrimary.gun) leftPrimary.gun.Shoot(direction);
            if (leftSecondary.gun) leftSecondary.gun.Shoot(direction);
        }
        if (inputRight.triggered)
        {
            if (rightPrimary.gun) rightPrimary.gun.Shoot(direction);
            if (rightSecondary.gun) rightSecondary.gun.Shoot(direction);
        }
    }
}