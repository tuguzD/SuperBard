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
        if (!other.TryGetComponent(
                out InstrumentManager manager)) return;

        if (manager.PickUpGun(gun))
            Destroy(gameObject);
    }
}