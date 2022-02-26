using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDragable
{
    public void Grab(GameObject grabber, Vector3 grabPoint);
    public void Release(Vector3 ThrowVelocity);
}
