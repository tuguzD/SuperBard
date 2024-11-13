using UnityEngine;

public class GunSelector : MonoBehaviour
{
    [SerializeField] private Transform gunParent;

    [Space] public Gun activeGun;

    private void SetupGun(Gun gun)
    {
        activeGun = gun;
        gun.Spawn(gunParent, this);
    }

    public void ThrowActiveGun()
    {
        // activeGun.Despawn();
        // Destroy(activeGun);
    }

    public void PickUpGun(Gun gun)
    {
        ThrowActiveGun();
        SetupGun(gun);
    }
}