using System.Collections.Generic;
using NUnit.Framework;
using TMPro;
using UnityEngine;

public class TargetManager : MonoBehaviour
{
    public static TargetManager instance;
    SoundPlayerScript soundPlayer;

    [SerializeField] GameObject spawnPointsGameObject;
    [SerializeField] int maxTargets = 10;
    [SerializeField] int minTargets = 1;
    [SerializeField] AudioClip Music;
    [SerializeField] TMPro.TextMeshProUGUI winText;

    int leftTargets = 0;

    [SerializeField] GameObject targetPrefab;
    [SerializeField] TextMeshProUGUI scoreText;

    Transform[] spawnPointTransforms;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        winText.enabled = false;
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        soundPlayer = GetComponent<SoundPlayerScript>();
        soundPlayer.PlaySound(Music, transform, 1f);
        spawnPointTransforms = spawnPointsGameObject.GetComponentsInChildren<Transform>();
        SpawnAllTarget();
        scoreText.text = "Remaining Obects: " + leftTargets.ToString();
    }

    void Update()
    {
        soundPlayer.PlaySound(Music, transform, 1f);
    }


    public void TargetDestroyed()
    {
        leftTargets--;
        UpdateScore(leftTargets);
        if (leftTargets <= 0)
        {
            winText.enabled = true;
        }
    }

   

    public void UpdateScore(int score)
    {
        scoreText.text = "Remaining Obects: " + score.ToString();
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
