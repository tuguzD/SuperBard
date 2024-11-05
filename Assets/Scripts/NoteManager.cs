using UnityEngine;

public class NoteManager : MonoBehaviour
{
    [Tooltip("Time when it's gonna be tapped by the player")]
    public float assignTime;

    [Tooltip("Time when it's gonna be instantiated")]
    private double _instantiateTime;
    [Tooltip("Sprite renderer of the note")]
    private SpriteRenderer _spriteRenderer;

    private void Start()
    {
        _instantiateTime = GameManager.GetAudioSourceTime();
        
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        var sinceInstantiate = GameManager.GetAudioSourceTime() - _instantiateTime;
        var t = (float)(sinceInstantiate / (GameManager.Instance.noteTime * 2));

        if (t > 1) Destroy(gameObject);
        else
        {
            transform.localPosition = Vector3.Lerp(
                Vector3.up * GameManager.Instance.noteSpawnY,
                Vector3.up * GameManager.Instance.noteDespawnY, t);
            _spriteRenderer.enabled = true;
        }
    }
}