using UnityEngine;

[CreateAssetMenu(fileName = "Shoot Config", menuName = "Weapons/Gun Shoot Config", order = 2)]
public class ShootConfig : ScriptableObject
{
    public Bullet bulletPrefab;
    public float bulletSpawnForce = 1000f;
    public Vector3 spread = new(0.01f, 0.01f, 0.01f);
}