using UnityEngine;

[RequireComponent(typeof(Collider))]
public class GunPickUp : MonoBehaviour
{
    public Gun gun;
    public Vector3 spinDirection = Vector3.up;

    private void Update()
    {
        transform.Rotate(spinDirection);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.TryGetComponent(out InstrumentManager gunSelector)) return;

        gunSelector.PickUpGun(gun);
        Destroy(gameObject);
    }
}