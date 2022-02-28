using UnityEngine;
/// <summary>
/// SEのScript
/// </summary>
public class SE : MonoBehaviour
{
    private AudioSource _audio;
    private void Start()
    {
        _audio = GetComponent<AudioSource>();
    }
    public void StartSE()
    {
        _audio.Play();
    }
}
