using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
[Serializable]
public struct ThreatSpawningSettings
{
    public Threat ThreatToSpawn;
    public float weight;
    public int maxCount;
}
public class ThreatSpawner : MonoBehaviour
{
    [SerializeField] ThreatSpawningSettings[] Threats;
    [SerializeField] BoxCollider MetoriteSpawnBoxCollider;
    public BoxCollider GetMetoriteSpawnBoxCollider()
    {
        return MetoriteSpawnBoxCollider;
    }
    [SerializeField] float MinSpawnInterval = 1f;
    [SerializeField] float MaxSpawnInterval = 5f;
    private ScoreKeeper _scoreKeeper;
    private float _maxWeight;
    private List<Threat> _currentThreatsList = new List<Threat>();

    void Start()
    {
        _scoreKeeper = FindObjectOfType<ScoreKeeper>();
        if(_scoreKeeper == null)
        {
            Debug.Log("Score keeper is null");
            return;
        }

        StartCoroutine(StartSpawnThreat());
    }
    IEnumerator StartSpawnThreat()
    {
        int difficultIndex = 0;
        _maxWeight = GetMaxWeight();
        while (true)
        {
            difficultIndex = (int)(_scoreKeeper.GetTimeScore() / 3) + 1;
            yield return new WaitForSeconds(
                Mathf.Lerp(MinSpawnInterval, 
                Mathf.Clamp(MaxSpawnInterval * (float)(1.0f/difficultIndex),1,float.MaxValue), 
                UnityEngine.Random.Range(0f,1f)));
            SpawnRandomThreat();
        }
    }
    private void SpawnRandomThreat()
    {
        if (Threats.Length == 0)
        {
            return;
        }

        ThreatSpawningSettings threatSettings = ChooseNextThreatSettingsToSpawn(_maxWeight);

        if (threatSettings.ThreatToSpawn != null)
        {
            if(NumOfCurrentThreats(threatSettings.ThreatToSpawn) >= threatSettings.maxCount)
            {
                return;
            }
            Threat newThreat = Instantiate(threatSettings.ThreatToSpawn);
            newThreat.Init(this);
        }
    }

    private int NumOfCurrentThreats(Threat threat)
    {
        int num = 0;
        for(int i = 0; i < _currentThreatsList.Count; i++)
        {
            if(threat.GetType() == _currentThreatsList[i].GetType())
            {
                num++;
            }
        }
        return num;
    }

    private ThreatSpawningSettings ChooseNextThreatSettingsToSpawn(float maxWeight)
    {
        ThreatSpawningSettings threatToSpawn = new ThreatSpawningSettings();
        float randomWeight = UnityEngine.Random.Range(0, maxWeight);

        float weightToCheck = 0;
        foreach (ThreatSpawningSettings threatSettings in Threats)
        {
            weightToCheck += threatSettings.weight;
            if (weightToCheck > randomWeight)
            {
                threatToSpawn = threatSettings;
                break;
            }
        }

        return threatToSpawn;
    }

    private float GetMaxWeight()
    {
        float maxWeight = 0;
        foreach (ThreatSpawningSettings threatSettings in Threats)
        {
            maxWeight += threatSettings.weight;
        }

        return maxWeight;
    }

    internal void AddThreatToSpawnerList(Threat threat)
    {
        _currentThreatsList.Add(threat);
    }
    internal void RemoveThreatInSpawnerList(Threat threat)
    {
        _currentThreatsList.Remove(threat);
    }
}
