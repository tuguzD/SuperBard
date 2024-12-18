using Minimalist.Quantity;
using Minimalist.Utility;
using Minimalist.Utility.SampleScene;
using UnityEngine;

public class Audience : MonoBehaviour
{
    private QuantityBhv _happiness;

    private void ListenToMusic()
    {
        _happiness.PassiveDynamics.Type = QuantityDynamicsType.Depletion;
    }

    private void OnCollisionEnter(Collision other)
    {
        if (!other.gameObject.TryGetComponent(typeof(Bullet), out var bullet)) return;

        _happiness.Amount += 0.25f;
    }

    [Header("Audio clips to play at the end")]
    [SerializeField] private AudioClip cheering;
    [SerializeField] private AudioClip worried;
    [SerializeField] private AudioClip angry;

    private void PlayAudio()
    {
        _happiness.PassiveDynamics.Type = QuantityDynamicsType.None;

        var source = GetComponent<AudioSource>();
        source.clip = _happiness.FillAmount switch
        {
            >= 0.75f => cheering,
            <= 0.25f => angry,
            _ => worried
        };
        source.Play();
    }

    private void Start()
    {
        _happiness = transform.parent.GetComponentInChildren<QuantityBhv>();

        var gameManager = FindObjectOfType<GameManager>();
        gameManager.StartAction += ListenToMusic;
        gameManager.EndAction += PlayAudio;

        _gradient = FindObjectOfType<LabelBhv>()._fontColorGradient;
        _material = gameObject.GetComponent<Renderer>().material;
    }

    private DiscretizedGradient _gradient;
    private Material _material;
    
    private void FixedUpdate()
    {
        _material.color = _gradient.Evaluate(_happiness.FillAmount);
    }
}