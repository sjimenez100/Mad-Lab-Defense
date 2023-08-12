using UnityEngine;

public class RetainedAudioAssistant : MonoBehaviour
{
    public static void PlaySoundOnRetainedManager(string sound)
    {
        RetainedAudioManager retainedManager = FindObjectOfType<RetainedAudioManager>();
        if(retainedManager != null)
            retainedManager.PlaySound(sound);

    }
}
