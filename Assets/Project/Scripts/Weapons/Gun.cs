using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

[CreateAssetMenu(fileName = "Gun", menuName = "Weapons/Gun", order = 0)]
public class Gun : ScriptableObject
{
    public GunType type;
    // public string gunName;
    public GameObject modelPrefab;

    public Vector3 spawnPoint;
    public Vector3 spawnRotation;
    public Vector3 spawnScale;

    public ShootConfig shootConfig;
    public TrailConfig trailConfig;

    private MonoBehaviour _activeMonoBehaviour;
    private ParticleSystem _shootSystem;
    private ObjectPool<Bullet> _bulletPool;
    private ObjectPool<TrailRenderer> _trailPool;

    public void Spawn(Transform parent, MonoBehaviour activeMonoBehaviour)
    {
        _activeMonoBehaviour = activeMonoBehaviour;

        _trailPool = new ObjectPool<TrailRenderer>(CreateTrail);
        _bulletPool = new ObjectPool<Bullet>(CreateBullet);

        var model = Instantiate(modelPrefab, parent, false);
        model.transform.localPosition = spawnPoint;
        model.transform.localRotation = Quaternion.Euler(spawnRotation);
        model.transform.localScale = spawnScale;

        _shootSystem = model.GetComponentInChildren<ParticleSystem>();
    }

    public void Shoot(Vector3 forward)
    {
        _shootSystem.Play();

        var shootDirection = forward + new Vector3(
            Random.Range(-shootConfig.spread.x, shootConfig.spread.x),
            Random.Range(-shootConfig.spread.y, shootConfig.spread.y),
            Random.Range(-shootConfig.spread.z, shootConfig.spread.z)
        );
        shootDirection.Normalize();

        DoProjectileShoot(shootDirection);
    }

    private void DoProjectileShoot(Vector3 shootDirection)
    {
        var bullet = _bulletPool.Get();
        bullet.gameObject.SetActive(true);
        bullet.OnCollision += HandleBulletCollision;
        bullet.transform.position = _shootSystem.transform.position;
        bullet.Spawn(shootDirection * shootConfig.bulletSpawnForce);

        var trail = _trailPool.Get();
        if (!trail) return;
        trail.transform.SetParent(bullet.transform, false);
        trail.transform.localPosition = Vector3.zero;
        trail.emitting = true;
        trail.gameObject.SetActive(true);
    }

    private void HandleBulletCollision(Bullet bullet, Collision collision)
    {
        var trail = bullet.GetComponentInChildren<TrailRenderer>();
        if (trail)
        {
            trail.transform.SetParent(null, true);
            _activeMonoBehaviour.StartCoroutine(DelayedDisableTrail(trail));
        }

        bullet.gameObject.SetActive(false);
        _bulletPool.Release(bullet);
    }

    private IEnumerator DelayedDisableTrail(TrailRenderer trail)
    {
        yield return new WaitForSeconds(trailConfig.duration);
        yield return null;

        trail.emitting = false;
        trail.gameObject.SetActive(false);

        _trailPool.Release(trail);
    }

    private TrailRenderer CreateTrail()
    {
        var instance = new GameObject("Bullet Trail");
        var trail = instance.AddComponent<TrailRenderer>();

        trail.colorGradient = trailConfig.color;
        trail.material = trailConfig.material;
        trail.widthCurve = trailConfig.widthCurve;
        trail.time = trailConfig.duration;
        trail.minVertexDistance = trailConfig.minVertexDistance;

        trail.emitting = false;
        trail.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;

        return trail;
    }

    private Bullet CreateBullet()
    {
        return Instantiate(shootConfig.bulletPrefab);
    }
}