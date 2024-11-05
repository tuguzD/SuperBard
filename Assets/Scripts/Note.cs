using UnityEngine;

public class Note : MonoBehaviour
{
    [Tooltip("Time when it's gonna be tapped by the player")]
    public float assignTime;

    [Tooltip("Time when it's gonna be instantiated")]
    private double _instantiateTime;
    [Tooltip("Sprite renderer of the note")]
    private SpriteRenderer _spriteRenderer;

    private void Start()
    {
        _instantiateTime = SongManager.GetAudioSourceTime();
        
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        var sinceInstantiate = SongManager.GetAudioSourceTime() - _instantiateTime;
        var t = (float)(sinceInstantiate / (SongManager.Instance.noteTime * 2));

        if (t > 1) Destroy(gameObject);
        else
        {
            transform.localPosition = Vector3.Lerp(
                Vector3.up * SongManager.Instance.noteSpawnY,
                Vector3.up * SongManager.Instance.noteDespawnY, t);
            _spriteRenderer.enabled = true;
        }
    }
}