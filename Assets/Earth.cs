using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Earth : MonoBehaviour
{
    [SerializeField] float RotSpeedY;
    [SerializeField] float RotSpeedX;
    [SerializeField] Transform WalkMan;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(RotSpeedY * Time.deltaTime, 0f, 0f);
    }

    public void RotateBasedOnVelocity(float velocity)
    {
        WalkMan.Rotate(0f, 150 * -velocity * Time.deltaTime, 0f, Space.World);
        transform.Rotate(0f, 150 * -velocity * Time.deltaTime, 0f, Space.World);
    }
    public void RotateLeft()
    {
        WalkMan.Rotate(0f, RotSpeedX * Time.deltaTime, 0f, Space.World);
        transform.Rotate(0f, RotSpeedX * Time.deltaTime, 0f,Space.World);
    }
    public void RotateRight()
    {
        WalkMan.Rotate(0f, -RotSpeedX * Time.deltaTime, 0f, Space.World);
        transform.Rotate(0f, -RotSpeedX * Time.deltaTime, 0f, Space.World);
    }
}
