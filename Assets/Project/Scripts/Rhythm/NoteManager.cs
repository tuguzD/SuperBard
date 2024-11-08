using UnityEngine;
using UnityEngine.UI;

public class NoteManager : MonoBehaviour
{
    [HideInInspector] public LaneManager lane;

    [Tooltip("Sprite renderer of the note")]
    public Image image;
    private Color _imageColor;

    [Tooltip("Time when it's gonna be tapped by the player")]
    public float assignTime;

    [Tooltip("Whether the note is colliding with the crosshair")]
    [HideInInspector] public bool isColliding;

    [Tooltip("Time when it's gonna be instantiated")]
    private double _instantiateTime;
    private float _scale;

    private void Start()
    {
        _scale = transform.localScale.x;
        transform.localScale = Vector3.zero;

        _imageColor = image.color;
        _instantiateTime = GameManager.GetAudioSourceTime();
    }

    private void Update()
    {
        MoveOrDestroyOnTime();

        transform.localScale = Vector3.Lerp(
            transform.localScale, Vector3.one * (_scale * lane.priorityModifier), Time.deltaTime * 5);
        
        UpdateColorByPriority();
    }

    private void MoveOrDestroyOnTime()
    {
        var sinceInstantiate = GameManager.GetAudioSourceTime() - _instantiateTime;
        var t = (float)(sinceInstantiate / (GameManager.Instance.aliveTime * 2));

        if (t > 1) Destroy(gameObject);
        else
        {
            transform.localPosition = Vector3.Lerp(
                Vector3.right * GameManager.Instance.spawnPosition,
                Vector3.right * GameManager.Instance.despawnPosition, t);
            image.enabled = true;
        }
    }

    private void UpdateColorByPriority()
    {
        if (lane.priorityModifier >= 1) 
            image.color = _imageColor;
        else
        {
            _imageColor = image.color;
            image.color = new Color(.5f, .5f, .5f, _imageColor.a);
        }
    }
}