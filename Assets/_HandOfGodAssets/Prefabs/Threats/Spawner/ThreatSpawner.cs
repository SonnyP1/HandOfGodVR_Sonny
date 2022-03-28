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
    private ScoreKeeper _scoreKeeper;

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
        while(true)
        {
            difficultIndex = (int)(_scoreKeeper.GetTimeScore() / 3) + 1;
            yield return new WaitForSeconds(Mathf.Lerp(MinSpawnInterval, Mathf.Clamp(MaxSpawnInterval * (float)(1.0f/difficultIndex),1,float.MaxValue), Random.Range(0f,1f)));
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
