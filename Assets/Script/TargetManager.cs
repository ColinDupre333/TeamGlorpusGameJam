using System.Collections.Generic;
using NUnit.Framework;
using TMPro;
using UnityEngine;

public class TargetManager : MonoBehaviour
{
    [SerializeField] GameObject spawnPointsGameObject;
    [SerializeField] GameObject targetPrefab;
    [SerializeField] TextMeshProUGUI scoreText;

    Transform[] spawnPointTransforms;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spawnPointTransforms = spawnPointsGameObject.GetComponentsInChildren<Transform>();
    }



    // Update is called once per frame
    void Update()
    {
        
    }
}
