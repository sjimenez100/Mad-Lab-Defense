using System;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(menuName = "RetainedAudioAssistant")]
public class RetainedAudioAssistant : ScriptableObject
{
    public Action<string> playRetainedSound;

    public void OnPlayRetainedSound(string str) => playRetainedSound?.Invoke(str);


}
