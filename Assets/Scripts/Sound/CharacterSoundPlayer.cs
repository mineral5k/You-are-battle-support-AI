using UnityEngine;

public class CharacterSoundPlayer : MonoBehaviour
{
    [SerializeField] private CharacterSounds sounds;

    public void PlayAttack()
    {
        AudioManager.Instance.PlaySFX(sounds.attack);
    }

    public void PlayHurt()
    {
        AudioManager.Instance.PlaySFX(sounds.hurt);
    }
    public void PlayDeath()
    {
        AudioManager.Instance.PlaySFX(sounds.death);
    }
    public void PlayNonAttack()
    {
        AudioManager.Instance.PlaySFX(sounds.nonAttack);
    }
}
