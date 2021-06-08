using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioLib : MonoBehaviour
{
    [Space(10)][Header("Audio")]
    [SerializeField] private AudioClip capa_impacto_grande_final;
    [SerializeField] private AudioClip capa_transicao;
    private AudioSource _audioSource;

    public void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    public void RunSFX()
    {
        _audioSource.PlayOneShot(capa_impacto_grande_final);
    } 
}