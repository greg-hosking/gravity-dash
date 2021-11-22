using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    // 0 for start screen state; 1 for in-game state; and 2 for end screen state
    public int state = 0;

    // Canvas variables
    public GameObject startCanvasGO;
    public GameObject endCanvasGO;

    // Score variables
    public TextMeshPro scoreText;
    public TextMeshPro highscoreText;
    public TextMeshPro endScoresText;
    float[] scoreIncrementPerSecIntervals = { 10.0f, 15.0f, 20.0f };
    public float scoreIncrementPerBit;
    [HideInInspector] public float score;
    [HideInInspector] public float highscore;
    [HideInInspector] public int bitsCollected;

    // Object spawn time variables
    int currentTimeInterval;
    public float timeBetweenObjectSpawns;
    float[,] timeIntervals = { { 3.5f, 2.25f }, { 2.25f, 1.5f }, { 1.5f, 1.0f } };
    float[] timeIntervalDecrements = { 0.1f, 0.025f, 0.0125f };
    float timeStarted;

    // Object spawn and destroy position variables
    public float objectSpawnX, objectDestroyX;

    // Object movement variables
    public float horizontalObjectSpeed, verticalObjectSpeed;

    void Start()
    {
        startCanvasGO.SetActive(true);
        endCanvasGO.SetActive(false);
    }

    public void SetupGame()
    {
        state = 1;
        timeStarted = Time.time;

        // Reset score and bitsCollected
        score = 0.0f;
        bitsCollected = 0;
        highscore = PlayerPrefs.GetFloat("highscore", 0.0f);

        // Reset currentTimeInterval and timeBetweenObjectSpawns 
        // and start decreasing timeBetweenObjectSpawns
        currentTimeInterval = 0;
        timeBetweenObjectSpawns = timeIntervals[currentTimeInterval, 0];
        StartCoroutine(DecreaseTimeBetweenObjectSpawns());

        startCanvasGO.SetActive(false);
        endCanvasGO.SetActive(false);
    }

    public void ResetGame()
    {
        state = 0;
        startCanvasGO.SetActive(true);
        endCanvasGO.SetActive(false);
    }

    public void EndGame()
    {
        state = 2;
        endCanvasGO.SetActive(true);

        PlayerPrefs.SetFloat("highscore", highscore);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    void Update()
    {
        if (state == 1)
        {
            // Determine score based on Time.time and bitsCollected
            score = Mathf.Round(((Time.time - timeStarted) * scoreIncrementPerSecIntervals[currentTimeInterval]) + (bitsCollected * scoreIncrementPerBit));
            if (score > highscore)
            {
                highscore = score;
                PlayerPrefs.SetFloat("highscore", highscore);
            }
            // Update scoreText and highscoreText
            scoreText.SetText("SCORE: " + score.ToString());
            highscoreText.SetText("HIGHSCORE: " + highscore.ToString());
        }
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

    void OnDestroy()
    {
        PlayerPrefs.SetFloat("highscore", highscore);
        PlayerPrefs.Save();
    }
}
