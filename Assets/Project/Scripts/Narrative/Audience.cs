using Minimalist.Quantity;
using Minimalist.Utility;
using Minimalist.Utility.SampleScene;
using UnityEngine;

public class Audience : MonoBehaviour
{
    private QuantityBhv _happiness;
    private AudioSource _source;

    private void ListenToMusic()
    {
        _happiness.PassiveDynamics.Type = QuantityDynamicsType.Depletion;
    }

    private void OnCollisionEnter(Collision other)
    {
        if (!other.gameObject.TryGetComponent(typeof(Bullet), out var bullet)) return;

        var noCombo = Mathf.Approximately(ScoreManager.ComboPower, Mathf.Epsilon);
        if (noCombo)
        {
            _happiness.Amount -= 0.05f;
            PlayClip(upset, 0.25f);
        }
        else
        {
            _happiness.Amount += 0.15f * (1 + ScoreManager.ComboPower);
            PlayClip(happy, 0.5f);
        }
    }

    [Header("Audio clips to play at the end")]
    [SerializeField] private AudioClip cheering;
    [SerializeField] private AudioClip worried;
    [SerializeField] private AudioClip angry;
    
    [Header("Audio clips to play while music is playing")]
    [SerializeField] private AudioClip happy;
    [SerializeField] private AudioClip upset;

    private void PlayAudio()
    {
        _happiness.PassiveDynamics.Type = QuantityDynamicsType.None;

        PlayClip(
            _happiness.FillAmount switch
            {
                >= 0.75f => cheering,
                <= 0.25f => angry,
                _ => worried,
            },
            _source.clip switch
            {
                var value when value == cheering => 0.5f,
                var value when value == cheering => 0.25f,
                _ => 1f,
            }
        );
    }

    private void Start()
    {
        _happiness = transform.parent.GetComponentInChildren<QuantityBhv>();
        _source = GetComponent<AudioSource>();

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

    private void PlayClip(AudioClip clip, float volume)
    {
        _source.clip = clip;
        _source.volume = volume;
        _source.Play();
    }
}