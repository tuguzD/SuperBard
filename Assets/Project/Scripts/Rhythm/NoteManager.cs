using UnityEngine;
using UnityEngine.UI;

public class NoteManager : MonoBehaviour
{
    [Tooltip("Sprite renderer of the note")]
    public Image image;
    private Color _imageColor;

    [Tooltip("Time when it's gonna be tapped by the player")]
    public float assignTime;

    [Tooltip("Whether the note is colliding with the crosshair")]
    [HideInInspector] public bool isColliding;
    public float actualScale = 0.5f;

    [Tooltip("Time when it's gonna be instantiated")]
    private double _instantiateTime;

    [HideInInspector] public LaneManager lane;

    private void Start()
    {
        _instantiateTime = GameManager.GetAudioSourceTime();
        _imageColor = image.color;
    }

    private void Update()
    {
        MoveOrDestroyOnTime();

        transform.localScale = Vector3.Lerp(
            transform.localScale, Vector3.one * (actualScale * lane.priorityModifier), Time.deltaTime * 5);
        
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