using System;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public static EventManager main;

    public Action<Vector3> singleInputMoveEvent;

    public Action<int> singleInputOptionEvent;

    public Action singleInputFireEvent;

    public Action<float> playerDamagedEvent;

    public Action<int> changeColorEvent;


    public Action spawningNewWavesEvent;

    public Action wavesClearedEvent;

    public Action finishedSpawningWavesEvent;


    public Action spawnEntityKillEvent;

    public Action spawnEntityDestroyEvent;

    public Action playerKillEvent;

    public Action powerUpKillEvent;

    public Action enemyKillEvent;

    public Action gameOverEvent;

    public Action gamePauseEvent;

    public Action gamePlayEvent;

    public Action gameStateToggleEvent;

    private void Awake() => main = this;


    public void OnSingleInputMove(Vector3 direction) =>
        singleInputMoveEvent?.Invoke(direction.normalized);

    public void OnSingleInputOption(int id) =>
        singleInputOptionEvent?.Invoke(id);

    public void OnSingleInputFire() => singleInputFireEvent?.Invoke();

    public void OnPlayerDamaged(float damage) =>
        playerDamagedEvent?.Invoke(damage);

    public void OnChangeColor(int id) => changeColorEvent?.Invoke(id);

    public void OnSpawningNewWaves() => spawningNewWavesEvent?.Invoke();

    public void OnWavesCleared() => wavesClearedEvent?.Invoke();

    public void OnFinishedSpawningWaves() =>
        finishedSpawningWavesEvent?.Invoke();

    public void OnEnemyKill() => enemyKillEvent?.Invoke();

    public void OnSpawnEntityKill() => spawnEntityKillEvent?.Invoke();

    public void OnPlayerKill() => playerKillEvent?.Invoke();

    public void OnPowerUpKill() => powerUpKillEvent?.Invoke();

    public void OnSpawnEntityDestroy() => spawnEntityDestroyEvent?.Invoke();

    public void OnGameOver() => gameOverEvent?.Invoke();
    public void OnGamePause() => gamePauseEvent?.Invoke();
    public void OnGamePlay() => gamePlayEvent?.Invoke();

    public void OnGameStateToggle() => gameStateToggleEvent?.Invoke();

}
