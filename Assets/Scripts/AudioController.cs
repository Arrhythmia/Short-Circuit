using UnityEngine;

public class AudioController : MonoBehaviour
{
    public AudioSource slam;
    public AudioSource jump;
    public AudioSource death;
    public void PlaySlamSound()
    {
        slam.Play();
    }
    public void PlayJumpSound()
    {
        jump.Play();
    }
    public void PlayDeathSound()
    {
        death.Play();
    }
}
