using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
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
        _audioSource.PlayOneShot(Random.Range(0.0f, 1.0f) < 0.5 ? capa_impacto_grande_final : capa_transicao);
    } 
    
    public IEnumerator StopLightSFX(){
        if (_audioSource.isPlaying)
            yield return null;
        yield return new WaitWhile(isPlaying);
        _audioSource.Stop();
    }

    private bool isPlaying()
    {
        return _audioSource.volume > 0f;
    }
}