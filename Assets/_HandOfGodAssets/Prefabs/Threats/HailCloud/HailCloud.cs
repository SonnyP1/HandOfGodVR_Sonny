using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HailCloud : Threat , IDragable
{
    [SerializeField] GameObject ExplosionEffect;
    [SerializeField] Transform ExplosionSpawnTransform;
    OrbitMovementComp _orbitMovementComp;
    Coroutine _blowUpCore;
    public void Grab(GameObject grabber, Vector3 grabPoint)
    {
        _orbitMovementComp.enabled = false;
    }
    public void Release(Vector3 ThrowVelocity)
    {
        _orbitMovementComp.enabled = true;
    }

    public override void Init(ThreatSpawner spawner)
    {
        base.Init(spawner);
        _orbitMovementComp = GetComponent<OrbitMovementComp>();
        Transform walkManTrans = GameplayStatics.GetWalkmanTransform();
        Vector3 SpawnRotUp = new Vector3(Random.Range(0,360),
            0,
            0);
        Vector3 SpawnRotForward = transform.forward * Random.Range(0, 50);

        Quaternion SpawnRot = Quaternion.LookRotation(SpawnRotForward, SpawnRotUp);
        _orbitMovementComp.SetRotation(SpawnRot);
        _blowUpCore = StartCoroutine(BlowUpTimer());
    }
    public override void BlowUp()
    {
        base.BlowUp();
        GameObject newEffect = Instantiate(ExplosionEffect, ExplosionSpawnTransform);
        newEffect.transform.parent = null;
        if (gameObject != null)
        {
            Destroy(gameObject);
        }
    }

    public void StopBlowUpTimer()
    {
        if (_blowUpCore != null)
        {
            StopCoroutine(_blowUpCore);
        }
    }
    public void StartBlowUpTimer()
    {
        if(_blowUpCore != null)
        {
            StopCoroutine(_blowUpCore);
        }
        _blowUpCore = StartCoroutine(BlowUpTimer());
    }
    public GameObject GetGameObject()
    {
        return gameObject;
    }

    IEnumerator BlowUpTimer()
    {
        yield return new WaitForSeconds(10f);
        BlowUp();
    }
}
