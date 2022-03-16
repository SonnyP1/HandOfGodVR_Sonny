using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThreatSpawner : MonoBehaviour
{
    [SerializeField] Threat[] Threats;
    [SerializeField] BoxCollider MetoriteSpawnBoxCollider;
    public BoxCollider GetMetoriteSpawnBoxCollider()
    {
        return MetoriteSpawnBoxCollider;
    }
    [SerializeField] float MinSpawnInterval = 1f;
    [SerializeField] float MaxSpawnInterval = 5f;
    void Start()
    {
        StartCoroutine(StartSpawnThreat());
    }
    IEnumerator StartSpawnThreat()
    {
        while(true)
        {
            yield return new WaitForSeconds(Mathf.Lerp(MinSpawnInterval, MaxSpawnInterval, Random.Range(0f,1f)));
            SpawnRandomThreat();
        }
    }
    private void SpawnRandomThreat()
    {
        if(Threats.Length == 0)
        {
            return;
        }

        int RandomIndex = Random.Range(0, Threats.Length);
        Threat newThreat = Instantiate(Threats[RandomIndex]);
        newThreat.Init(this);
    }
}
