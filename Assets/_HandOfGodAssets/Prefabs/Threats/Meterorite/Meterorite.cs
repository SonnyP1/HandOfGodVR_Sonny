using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meterorite : Threat, IDragable
{
    [SerializeField] float MinSpeed;
    [SerializeField] float MaxSpeed;

    [SerializeField] float MinRotSpeed;
    [SerializeField] float MaxRotSpeed;

    [SerializeField] GameObject ExplosionEffect;


    private BoxCollider _spawnBoundaryBox;
    private Rigidbody _rb;
    public void Grab(GameObject grabber, Vector3 grabPoint)
    {
        _rb.isKinematic = true;
        transform.parent = grabber.transform;
        transform.position = grabPoint;
        StopAllCoroutines();
    }
    public void Release(Vector3 ThrowVelocity)
    {
        transform.parent = null;
        _rb.isKinematic = false;
        _rb.velocity = -ThrowVelocity * 0.5f;
        StartCoroutine(BlowUpTimer());
    }
    public override void Init(ThreatSpawner spawner)
    {
        _spawnBoundaryBox = spawner.GetMetoriteSpawnBoxCollider();
        Transform walkManTrans = GameplayStatics.GetWalkmanTransform();

        Vector3 origin = _spawnBoundaryBox.transform.position;
        Vector3 range = _spawnBoundaryBox.size;
        Vector3 randomRange = new Vector3(Random.Range(-range.x, range.x),
            Random.Range(-range.y, range.y),
            Random.Range(-range.z, range.z));

        Vector3 randomCoordinateWithinBoundBox = origin + randomRange;

        transform.position = randomCoordinateWithinBoundBox;
        transform.LookAt(walkManTrans, Vector3.up);

        _rb = GetComponent<Rigidbody>();

        float randomSpeed = Random.Range(MinSpeed, MaxSpeed);
        float randomRotSpeed = Random.Range(MinRotSpeed, MaxRotSpeed);

        _rb.AddTorque(Vector3.right* randomRotSpeed, ForceMode.Force);
        _rb.AddForce((walkManTrans.position - transform.position)* randomSpeed, ForceMode.Force);
    }

    private void OnCollisionEnter(Collision collision)
    {
        BlowUp();
    }
    public override void BlowUp()
    {
        GameObject newEffect = Instantiate(ExplosionEffect,transform);
        newEffect.transform.parent = null;
        if(gameObject != null)
        {
            Destroy(gameObject);
        }
    }

    IEnumerator BlowUpTimer()
    {
        yield return new WaitForSeconds(5f);
        BlowUp();
    }

    public GameObject GetGameObject()
    {
        return gameObject;
    }
}
