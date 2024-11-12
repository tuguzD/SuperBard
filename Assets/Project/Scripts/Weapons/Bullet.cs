using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Bullet : MonoBehaviour
{
    [field: SerializeField]
    public Vector3 SpawnLocation { get; private set; }
    [SerializeField] private float delayedDisableTime = 2f;
    
    public delegate void CollisionEvent(Bullet bullet, Collision collision);
    public event CollisionEvent OnCollision;
    
    private Rigidbody _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }
    
    public void Spawn(Vector3 spawnForce)
    {
        SpawnLocation = transform.position;
        transform.forward = spawnForce.normalized;
        _rigidbody.AddForce(spawnForce);
        StartCoroutine(DelayedDisable(delayedDisableTime));
    }
    
    private IEnumerator DelayedDisable(float time)
    {
        yield return new WaitForSeconds(time);
        OnCollisionEnter(null);
    }
    
    private void OnCollisionEnter(Collision collision)
    {
        OnCollision?.Invoke(this, collision);
    }
    
    private void OnDisable()
    {
        StopAllCoroutines();
        _rigidbody.velocity = Vector3.zero;
        _rigidbody.angularVelocity = Vector3.zero;
        OnCollision = null;
    }
}