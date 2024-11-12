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
    // private GameObject _model;
    private ParticleSystem _shootSystem;
    private ObjectPool<TrailRenderer> _trailPool;

    public void Spawn(Transform parent, MonoBehaviour activeMonoBehaviour)
    {
        _activeMonoBehaviour = activeMonoBehaviour;
        _trailPool = new ObjectPool<TrailRenderer>(CreateTrail);

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

        if (Physics.Raycast(
                _shootSystem.transform.position, shootDirection,
                out var hit, float.MaxValue, shootConfig.hitMask
            ))
        {
            _activeMonoBehaviour.StartCoroutine(PlayTrail(
                _shootSystem.transform.position, hit.point, hit
            ));
        }
        else
        {
            _activeMonoBehaviour.StartCoroutine(PlayTrail(
                _shootSystem.transform.position,
                _shootSystem.transform.position + (shootDirection * trailConfig.missDistance),
                new RaycastHit()
            ));
        }
    }

    private IEnumerator PlayTrail(Vector3 startPoint, Vector3 endPoint, RaycastHit hit)
    {
        var instance = _trailPool.Get();
        instance.gameObject.SetActive(true);
        instance.transform.position = startPoint;
        yield return null; // avoid position carry-over from last frame if reused

        instance.emitting = true;

        var distance = Vector3.Distance(startPoint, endPoint);
        var remainingDistance = distance;
        while (remainingDistance > 0)
        {
            instance.transform.position = Vector3.Lerp(
                startPoint,
                endPoint,
                Mathf.Clamp01(1 - (remainingDistance / distance))
            );
            remainingDistance -= trailConfig.simulationSpeed * Time.deltaTime;

            yield return null;
        }

        instance.transform.position = endPoint;

        yield return new WaitForSeconds(trailConfig.duration);
        yield return null;

        instance.emitting = false;
        instance.gameObject.SetActive(false);
        _trailPool.Release(instance);
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
}