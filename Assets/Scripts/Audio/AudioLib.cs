using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioLib : MonoBehaviour
{
    [Space(10)][Header("Audio")]

    [SerializeField] private AudioClip capa_impacto_grande_final;
    [SerializeField] private AudioClip capa_transicao;

    [SerializeField] private AudioClip sliding_door;
    private AudioSource _audioSource;

    public void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    public void RunSFX()
    {
        _audioSource.PlayOneShot(Random.Range(0.0f, 1.0f) < 0.5 ? capa_impacto_grande_final : capa_transicao);
    } 

    public void SlidingDoorOpening()
    {
        _audioSource.PlayOneShot(sliding_door);
    }
}