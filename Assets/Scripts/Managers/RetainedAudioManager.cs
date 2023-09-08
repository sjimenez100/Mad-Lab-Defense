using UnityEngine;

public class RetainedAudioManager : AudioManager
{
    [SerializeField]
    private RetainedAudioAssistant assistant;

    public static RetainedAudioManager retainedManager;
    
    protected override void Awake()
    {
        if (retainedManager == null)
            retainedManager = this;
        else
        {
            Destroy(gameObject);
            return;
        }


        DontDestroyOnLoad(this);
        assistant.playRetainedSound += PlaySound;

        base.Awake();
    }

    private void OnDisable()
    {
        assistant.playRetainedSound -= PlaySound;
    }
}

