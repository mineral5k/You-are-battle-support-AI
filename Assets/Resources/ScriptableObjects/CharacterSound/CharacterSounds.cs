using UnityEngine;

[CreateAssetMenu(fileName = "CharacterSounds", menuName = "Scriptable Objects/CharacterSounds")]
public class CharacterSounds : ScriptableObject
{
    public AudioClip attack;
    public AudioClip hurt;
    public AudioClip nonAttack;
    public AudioClip death;
}
