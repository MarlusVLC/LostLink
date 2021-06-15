using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioLib : MonoBehaviour
{
    [Header("Character movement sfx:")]

    [Space(10)][Header("Audio")]
    [SerializeField] private AudioClip capa_impacto_grande_finalSFX;
    [SerializeField] private AudioClip capa_transicaoSFX;

    [Header("Door sfx:")]
    [SerializeField] private AudioClip sliding_doorSFX;

    [Header("Lights sfx:")]
    [SerializeField] private AudioClip lightSFX;
    [SerializeField] private AudioClip light_activateSFX;
    private AudioSource _audioSource;

    public void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    // character movement sfx
    public void RunSFX()
    {
        _audioSource.PlayOneShot(Random.Range(0.0f, 1.0f) < 0.5 ? capa_impacto_grande_finalSFX : capa_transicaoSFX);
    } 

    // sliding door sfx
    public void SlidingDoorOpeningSFX()
    {
        _audioSource.PlayOneShot(sliding_doorSFX);
    }

    // lights sfx
    public void LightSFX()
    {
        _audioSource.Play(0);
    }

    public IEnumerator StopLightSFX()
    {
        if (_audioSource.isPlaying)
            yield return null;
        yield return new WaitWhile(isPlaying);
        _audioSource.Stop();
    }

    private bool isPlaying()
    {
        return _audioSource.volume > 0f;
    }

    public void LightActivateSFX()
    {
        _audioSource.PlayOneShot(light_activateSFX);
    }
}
