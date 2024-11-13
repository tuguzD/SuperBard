using System;
using System.Collections.Generic;
using System.Linq;
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

    [SerializeField] private GameObject laneLeft;
    [SerializeField] [ReadOnly] private List<LaneManager> laneManagersLeft;

    [Header("Right arm configs")] [SerializeField] [Tooltip("Input action for instrument in right hand")]
    private InputAction inputRight;
    [SerializeField] private GunSlot rightPrimary;
    [SerializeField] private GunSlot rightSecondary;

    [SerializeField] private GameObject laneRight;
    [SerializeField] [ReadOnly] private List<LaneManager> laneManagersRight;

    private void Start()
    {
        laneManagersLeft = laneLeft.GetComponents<LaneManager>().ToList();
        laneManagersLeft.Sort(
            (x, y) => x.priorityModifier.CompareTo(y.priorityModifier)
        );
        // foreach (var left in laneManagersLeft)
        //     Debug.LogWarning(left.instrument);
        
        laneManagersRight = laneRight.GetComponents<LaneManager>().ToList();
        laneManagersRight.Sort(
            (x, y) => x.priorityModifier.CompareTo(y.priorityModifier)
        );
        // foreach (var right in laneManagersRight)
        //     Debug.LogWarning(right.instrument);
    }

    private void SetupGun(Gun gun, ref GunSlot parentSlot)
    {
        parentSlot.gun = gun;
        gun.Spawn(parentSlot.parent, this);
    }

    public bool PickUpGun(Gun gun)
    {
        foreach (var left in laneManagersLeft)
            if (gun.type == left.instrument)
            {
                // ThrowActiveGun();
                ref var parentSlot = ref leftPrimary.gun == null
                    ? ref leftPrimary : ref leftSecondary;

                SetupGun(gun, ref parentSlot);
                return true;
            }
        foreach (var right in laneManagersRight)
            if (gun.type == right.instrument)
            {
                // ThrowActiveGun();
                ref var parentSlot = ref rightPrimary.gun == null
                    ? ref rightPrimary : ref rightSecondary;

                SetupGun(gun, ref parentSlot);
                return true;
            }

        return false;
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