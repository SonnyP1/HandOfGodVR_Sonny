using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionScriptDestorySelf : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(DestorySelf());
    }

    IEnumerator DestorySelf()
    {
        yield return new WaitForSeconds(10f);
        Destroy(gameObject);
    }
}
