using UnityEngine;
using UnityEngine.UI;

public class NoteManager : MonoBehaviour
{
    [Tooltip("Time when it's gonna be tapped by the player")]
    public float assignTime;

    [Tooltip("Time when it's gonna be instantiated")]
    private double _instantiateTime;
    [Tooltip("Sprite renderer of the note")]
    private Image _image;

    private void Start()
    {
        _instantiateTime = GameManager.GetAudioSourceTime();
        
        _image = GetComponent<Image>();
    }

    private void Update()
    {
        var sinceInstantiate = GameManager.GetAudioSourceTime() - _instantiateTime;
        var t = (float)(sinceInstantiate / (GameManager.Instance.aliveTime * 2));

        if (t > 1) Destroy(gameObject);
        else
        {
            transform.localPosition = Vector3.Lerp(
                Vector3.right * GameManager.Instance.spawnPosition,
                Vector3.right * GameManager.Instance.despawnPosition, t);
            _image.enabled = true;
        }
    }
}