using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreKeeper : MonoBehaviour
{
    private float _timeScore;
    private bool _walkmanDead = false;

    public bool IsWalkmanDead()
    {
        return _walkmanDead;
    }
    public float GetTimeScore()
    {
        return _timeScore;
    }
    void Start()
    {
        _timeScore = 0;
        _walkmanDead = false;
        StartCoroutine(ScoreTime());
    }

    IEnumerator ScoreTime()
    {
        while(true)
        {
            WalkMan walkMan = FindObjectOfType<WalkMan>();
            if (!walkMan)
            {
                _walkmanDead = true;
                break;
            }
            _timeScore += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
    }
}
