using UnityEngine;

public class SoundsPlay : MonoBehaviour
{
    public static AudioClip _grabPistoleto;
    public static AudioClip _shoot;
    static AudioSource _audioSource;
    void Start()
    {
        _grabPistoleto = Resources.Load<AudioClip>("Tuturu");
        _audioSource = GetComponent<AudioSource>();
    }

    public static void PlaySound()
    {
        _audioSource.PlayOneShot (_grabPistoleto);
    }
}
