using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grabable : MonoBehaviour , IDragable
{
    [SerializeField] float MaxTimeToGoToHand = 1f;
    [SerializeField] float ThrowForceMultiplier = 10f;
    Coroutine MoveToGrabberTransformCor = null;
    Rigidbody _rigidbody;
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }
    public void Grab(GameObject grabber, Vector3 grabPoint)
    {
        MoveToGrabberTransformCor = StartCoroutine(MoveToGrabberTransform(grabber, MaxTimeToGoToHand));
        if(_rigidbody)
        {
            _rigidbody.isKinematic = true;
            _rigidbody.velocity = Vector3.zero;
        }
    }

    public void Release(Vector3 ThrowVelocity)
    {
        if(MoveToGrabberTransformCor != null)
        {
            StopCoroutine(MoveToGrabberTransformCor);
            transform.parent = null;
            if(_rigidbody)
            {
                _rigidbody.isKinematic = false;
                _rigidbody.velocity = ThrowVelocity * ThrowForceMultiplier;
            }
        }
        MoveToGrabberTransformCor = null;
    }


    IEnumerator MoveToGrabberTransform(GameObject grabber,float MaxTime)
    {
        float timer = 0;
        while(timer < MaxTime)
        {
            timer += Time.deltaTime;
            float timerPercent = timer / MaxTime;
            transform.position = Vector3.Lerp(transform.position,grabber.transform.position, timerPercent);
            yield return new WaitForEndOfFrame();
        }
        transform.position = grabber.transform.position;
        transform.parent = grabber.transform;
    }


}
