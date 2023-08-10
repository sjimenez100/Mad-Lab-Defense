using UnityEngine;
using TMPro;
using UnityEngine.UI;

[RequireComponent(typeof(Animator))]
public class UIManager : MonoBehaviour
{
    public static UIManager main;
    private Animator animator;


    [SerializeField]
    private GameObject endCover;

    [SerializeField]
    private GameObject pauseCover;

    [SerializeField]
    private Slider healthSlider;

    [SerializeField]
    private TextMeshProUGUI playerScore;

    [SerializeField]
    private TextMeshProUGUI errorText;

    [SerializeField]
    private TextMeshProUGUI healthText;

    [SerializeField]
    private TMP_Text waveText;
    [SerializeField]
    private TMP_Text killText;

    [HideInInspector]
    public int enemiesKilledNum;
    [HideInInspector]
    public int waveNum;


    private void Awake() => main = this;

    private void OnEnable()
    {
        EventManager.main.enemyKillEvent += UpdateEnemiesKilled;
        EventManager.main.spawningNewWavesEvent += UpdateWavesBegun;
        EventManager.main.wavesClearedEvent += WaveTextAnimationOn;
        EventManager.main.spawningNewWavesEvent += WaveTextAnimationOff;
        EventManager.main.gameOverEvent += ActivateEndCover;
        EventManager.main.gameOverEvent += UpdateplayerScore;
        EventManager.main.playerDamagedEvent += UpdateHealthBar;
        EventManager.main.gamePlayEvent += DeactivatePauseCover;
        EventManager.main.gamePauseEvent += ActivatePauseCover;
    }

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void ShowErrorMessage(string msg)
    {
        errorText.gameObject.SetActive(true);
        errorText.text = msg;
    }

    private void UpdateplayerScore()
    {
        playerScore.text = killText.text;
    }

    private void UpdateHealthBar(float damage)
    {
        healthSlider.value -= (int)damage;
        healthText.text = $"({healthSlider.value}/{healthSlider.maxValue})";
    }
    private void ActivateEndCover() => endCover.SetActive(true);

    private void ActivatePauseCover() => pauseCover.SetActive(true);

    private void DeactivatePauseCover() => pauseCover.SetActive(false);

    private void UpdateEnemiesKilled()
    {
        enemiesKilledNum++;
        UIManager.main.SetKillText(enemiesKilledNum);
    }

    private void UpdateWavesBegun()
    {
        waveNum++;
        UIManager.main.SetWaveText(waveNum);
    }


    public void SetWaveText(int waveNum) => waveText.text = $"Wave {waveNum}";

    public void SetKillText(int killNum) => killText.text = $"{killNum} Kills";

    private void WaveTextAnimationOff() => animator.SetBool("FadeBool", false);

    private void WaveTextAnimationOn() => animator.SetBool("FadeBool", true);

    private void OnDisable()
    {
        EventManager.main.wavesClearedEvent -= WaveTextAnimationOn;
        EventManager.main.spawningNewWavesEvent -= WaveTextAnimationOff;
        EventManager.main.gameOverEvent -= ActivateEndCover;
        EventManager.main.gameOverEvent -= UpdateplayerScore;
        EventManager.main.playerDamagedEvent -= UpdateHealthBar;
        EventManager.main.enemyKillEvent -= UpdateEnemiesKilled;
        EventManager.main.spawningNewWavesEvent -= UpdateWavesBegun;
    }
}
