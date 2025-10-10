using System.Collections.Generic;
using NUnit.Framework;
using TMPro;
using UnityEngine;

public class TargetManager : MonoBehaviour
{
    public static TargetManager instance;

    [SerializeField] GameObject spawnPointsGameObject;
    [SerializeField] int maxTargets = 10;
    [SerializeField] int minTargets = 1;

    int leftTargets = 0;

    [SerializeField] GameObject targetPrefab;
    [SerializeField] TextMeshProUGUI scoreText;

    Transform[] spawnPointTransforms;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        spawnPointTransforms = spawnPointsGameObject.GetComponentsInChildren<Transform>();
        SpawnAllTarget();
        scoreText.text = "Capsule restante: " + leftTargets.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void TargetDestroyed()
    {
        leftTargets--;
        UpdateScore(leftTargets);
        if (leftTargets <= 0)
        {
            GameOver();
        }
    }

    private void GameOver()
    {
        Debug.Log("Game Over");
    }

    public void UpdateScore(int score)
    {
        scoreText.text = "Capsule restante: " + score.ToString();
    }

    void SpawnAllTarget()
    {
        int randomTargetCount = Random.Range(minTargets, maxTargets + 1);
        leftTargets = randomTargetCount;
        for (int i = 0; i < randomTargetCount; i++)
        {
            SpawnTarget();
        }
    }
    void SpawnTarget()
    {
        int randomIndex = Random.Range(1, spawnPointTransforms.Length);
        Instantiate(targetPrefab, spawnPointTransforms[randomIndex].position, Quaternion.identity);
    }
}
