public class RetainedAudioManager : AudioManager
{
    protected override void Awake()
    {
        DontDestroyOnLoad(this);
        base.Awake();
    }

}
