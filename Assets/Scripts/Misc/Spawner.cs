using System.Collections;
using System;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class Spawner : MonoBehaviour
{
    public Transform target;

    [HideInInspector]
    public enum State {Initializing, Spawning, AwaitingClear,
        WaitingForNextWave, Complete}

    [HideInInspector]
    public State currentState;

    private IEnumerator spawnRoutine;

    [SerializeField]
    private Transform[] spawnOrigins;

    [SerializeField]
    private SpawnObject[] spawnObjects;

    private GameObject[] gameObjects;
    private float[] probabilities;


    [SerializeField]
    private int numWaves;

    [SerializeField]
    private float timeBetweenWaves;


    [SerializeField]
    private int initialNumObjectsPerWave;

    [SerializeField]
    private int numObjectsPerWaveIncrement;

    [SerializeField]
    private int numObjectsPerWaveMaximum;

    private int numObjectsPerWave;



    [SerializeField]
    private float initialSpeed;

    [SerializeField]
    private float speedIncrement;

    [SerializeField]
    private float speedMaximum;

    private float speed;


    [SerializeField]
    private float initialDistanceBetweenObjects;

    [SerializeField]
    private float distanceIncrement;

    [SerializeField]
    private float distanceMinimum;

    private float distanceBetweenObjects;


    private int waveCompletionNumber;

    private int numSpawnEntitiesRemaining;


    private void Awake()
    {
        currentState = State.Initializing;

        speed = initialSpeed;
        distanceBetweenObjects = initialDistanceBetweenObjects;
        numObjectsPerWave = initialNumObjectsPerWave;

        ConfigureSpawnObjects();
    }


    private void OnEnable()
    {
        EventManager.main.spawnEntityDestroyEvent +=
            IncrementSpawnEntitysRemaining;

        EventManager.main.playerKillEvent += TerminateRoutine;
    }

    private void ConfigureSpawnObjects()
    {
        spawnObjects = SpawnObject.Normalize(spawnObjects);
        gameObjects = spawnObjects.Select(s => s.spawnObject).ToArray();
        probabilities = spawnObjects.Select(s => s.probability).ToArray();
    }


    private void Start()
    {
        
        spawnRoutine = SpawnWaves(numWaves, timeBetweenWaves);
        StartCoroutine(spawnRoutine);
    }

    private IEnumerator SpawnWaves(int numWaves, float timeBetweenWaves)
    {
        while (numWaves > 0)
        {
            float timeBetweenObjects = distanceBetweenObjects / speed;

            ChangeState(State.Spawning);

            yield return StartCoroutine(SpawnOverOrigins(numObjectsPerWave,
                timeBetweenObjects));

            ChangeState(State.AwaitingClear);

            numWaves--;

            Incrementations();
            
            yield return new WaitUntil(() => numSpawnEntitiesRemaining <= 0);
            numSpawnEntitiesRemaining = 0;

            if (numWaves > 0)
            {
                ChangeState(State.WaitingForNextWave);
                yield return new WaitForSeconds(timeBetweenWaves);
            }
            
        }

        ChangeState(State.Complete);

        yield break;
    }

    private IEnumerator SpawnOverOrigins(int numObjectsPerWave,
        float timeBetweenObjects)
    {

        for(int i = 0; i < spawnOrigins.Length; i++)
        {
            Transform spawnOrigin = spawnOrigins[i];
            StartCoroutine(SpawnWave(numObjectsPerWave,
                timeBetweenObjects, spawnOrigin));

        }
 
        yield return new WaitUntil(() =>
        waveCompletionNumber == spawnOrigins.Length);

        waveCompletionNumber = 0;

        yield break;
    }

    private IEnumerator SpawnWave(int numObjects, float timeBetween,
        Transform spawnOrigin)
    {

        float timeMinimum = distanceMinimum / speed;

        //float initialWaveWaitTime = Random.Range(0, timeBetween / 2f);

        //yield return new WaitForSeconds(initialWaveWaitTime);

        Vector3 direction = (target.position - spawnOrigin.position).normalized;

        while (numObjects > 0)
        {
            GameObject randObject = SpawnObject.GetRandom(gameObjects,
                probabilities);

            GameObject spawned = Instantiate(randObject, spawnOrigin.position,
                spawnOrigin.rotation, spawnOrigin);

            if(spawned.TryGetComponent(out SpawnEntity spawnEntity))
            { 
                spawnEntity.Shove(speed, direction); // shoves forward
                numSpawnEntitiesRemaining++;
            }
            else
                Debug.LogWarning("Spawned object not of type SpawnEntity");

            numObjects--;

            if (numObjects > 0)
            {
                // essentially excess time not required
                float excessTimeBetween = timeBetween - timeMinimum;
                float offset = Random.Range(0f, 2f * excessTimeBetween);
                yield return new WaitForSeconds(timeMinimum + offset);
            }

        }

        waveCompletionNumber++;

        yield break;
    }

    private void ChangeState(State toState)
    {
        switch (toState)
        {

            case State.Spawning:
                EventManager.main.OnSpawningNewWaves();
                break;
                
            case State.AwaitingClear:
                EventManager.main.OnFinishedSpawningWaves();
                break;

            case State.WaitingForNextWave:
                EventManager.main.OnWavesCleared();
                break;

            default:
                break;
        }

        currentState = toState;

    }


    private void Incrementations()
    {
        speed = Increment(speed, speedIncrement, speedMaximum);

        distanceBetweenObjects = Increment(distanceBetweenObjects,
            distanceIncrement, distanceMinimum, false);

        numObjectsPerWave = (int)Increment(numObjectsPerWave,
            numObjectsPerWaveIncrement, numObjectsPerWaveMaximum);
    }

    private void IncrementSpawnEntitysRemaining()
    {
        numSpawnEntitiesRemaining = (int)Increment(numSpawnEntitiesRemaining,
            -1, 0, false);
    }

    private static float Increment(float baseValue, float increment,
        float threshold, bool isMax = true)
    {
        float newValue = baseValue + increment;

        Func<float, float, bool> eq;

        if (isMax)
            eq = (float nv, float t) => nv <= t;
        else
            eq = (float nv, float t) => nv >= t;

        if (eq(newValue, threshold))
            return newValue;
        else
            return threshold;

    }

    private void TerminateRoutine() => StopCoroutine(spawnRoutine);

    private void OnDisable()
    {
        EventManager.main.spawnEntityDestroyEvent -=
            IncrementSpawnEntitysRemaining;
    }
}
