using System.Collections.Generic;
using UnityEngine;

public class GunSelector : MonoBehaviour
{
    [SerializeField] private GunType gunType;
    [SerializeField] private Transform gunParent;
    [SerializeField] private List<Gun> guns;

    [Space] [Header("Runtime Filled")] public Gun activeGun;

    private void Start()
    {
        var gun = guns.Find(gun => gun.type == gunType);

        if (gun == null)
        {
            Debug.LogError($"No GunScriptableObject found for GunType: {gun}");
            return;
        }

        activeGun = gun;
        gun.Spawn(gunParent, this);
    }
}