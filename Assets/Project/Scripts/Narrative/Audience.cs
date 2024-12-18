using Minimalist.Quantity;
using UnityEngine;

public class Audience : MonoBehaviour
{
    private void OnCollisionEnter(Collision other)
    {
        if (!other.gameObject.TryGetComponent(typeof(Bullet), out var bullet)) return;

        _happiness.Amount += 0.25f;
    }

    private QuantityBhv _happiness;

    private void Start()
    {
        _happiness = transform.parent.GetComponentInChildren<QuantityBhv>();

        var gameManager = FindObjectOfType<GameManager>();
        gameManager.StartAction += () =>
            _happiness.PassiveDynamics.Type = QuantityDynamicsType.Depletion;
        gameManager.EndAction += () =>
            Debug.Log("Cheer or cry out");
    }
}