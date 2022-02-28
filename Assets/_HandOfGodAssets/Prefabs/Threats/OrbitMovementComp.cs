using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbitMovementComp : MonoBehaviour
{
    [SerializeField] float OrbitAgularSpeed;
    [SerializeField] Transform OrbitAround;
    [SerializeField] Vector3 OrbitAxis;

    void Start()
    {
        if(OrbitAround == null)
        {
            OrbitAround = FindObjectOfType<Earth>()?.transform;
        }
        transform.parent = OrbitAround;
        transform.position = OrbitAround.position;
        transform.localPosition = Vector3.zero;
    }

    internal void SetRotation(Quaternion spawnRot)
    {
        transform.rotation = spawnRot;
    }

    void Update()
    {
        transform.Rotate(
            OrbitAxis.x* OrbitAgularSpeed* Time.deltaTime,
            OrbitAxis.y * OrbitAgularSpeed * Time.deltaTime,
            OrbitAxis.z * OrbitAgularSpeed * Time.deltaTime);
    }
}
