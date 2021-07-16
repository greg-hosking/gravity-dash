using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    // Score variables
    public TextMeshPro scoreText;
    private float[] scoreIncrementPerSecIntervals = { 10.0f, 15.0f, 20.0f };
    [SerializeField] private float scoreIncrementPerBit;
    [HideInInspector] public float score;
    [HideInInspector] public int bitsCollected;

    // Object spawn time variables
    private int currentTimeInterval;
    public float timeBetweenObjectSpawns;
    private float[,] timeIntervals = { { 3.5f, 2.25f }, { 2.25f, 1.5f }, { 1.5f, 1.0f } };
    private float[] timeIntervalDecrements = { 0.1f, 0.025f, 0.0125f };

    // Object spawn and destroy position variables
    public float objectSpawnX, objectDestroyX;

    // Object movement variables
    public float objectSpeedX, objectSpeedY;

    void Start()
    {
        // Reset score and bitsCollected
        score = 0.0f;
        bitsCollected = 0;

        // Reset currentTimeInterval and timeBetweenObjectSpawns 
        // and start decreasing timeBetweenObjectSpawns
        currentTimeInterval = 0;
        timeBetweenObjectSpawns = timeIntervals[currentTimeInterval, 0];
        StartCoroutine(DecreaseTimeBetweenObjectSpawns());
    }

    void Update()
    {
        // Determine score based on Time.time and bitsCollected
        score = Mathf.Round((Time.time * scoreIncrementPerSecIntervals[currentTimeInterval]) + (bitsCollected * scoreIncrementPerBit));
        // Update scoreText
        scoreText.SetText(score.ToString());
    }

    IEnumerator DecreaseTimeBetweenObjectSpawns()
    {
        while (timeBetweenObjectSpawns > timeIntervals[timeIntervals.GetLength(0) - 1, 1])
        {
            yield return new WaitForSeconds(timeBetweenObjectSpawns);
            // Determine currentTimeInterval based on timeBetweenObjectSpawns
            for (int i = 0; i < timeIntervals.GetLength(0); i++)
                if (timeBetweenObjectSpawns <= timeIntervals[i, 0] && timeBetweenObjectSpawns > timeIntervals[i, 1])
                    currentTimeInterval = i;
            
            // Decrease timeBetweenObjectSpawns based on currentTimeInterval
            timeBetweenObjectSpawns -= timeIntervalDecrements[currentTimeInterval];
            timeBetweenObjectSpawns = Mathf.Round(timeBetweenObjectSpawns * 10000.0f) / 10000.0f;
        }
    }
}
