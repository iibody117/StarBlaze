using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    [Header("Shooting SFX")]
    [SerializeField] AudioClip shootingClip;
    [SerializeField][Range(0, 1)] float shootingVolume = 1f;
    [SerializeField] AudioClip damageClip;
    [SerializeField][Range(0, 1)] float damageVolume = 1f;


    public void PlayShootingSFX() // Playing the Shoot Sound
    {
        PlayAudioClip(shootingClip, shootingVolume);
    }
    public void PlayDamageSFX() //Play the Damage Sound
    {
        PlayAudioClip(damageClip, damageVolume);
    }
    void PlayAudioClip(AudioClip clip, float volume) //Play Music depends on the Scene
    {
        if (clip != null)
        {
            AudioSource.PlayClipAtPoint(clip, Camera.main.transform.position, volume);
        }
    }
}
