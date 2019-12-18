using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetMovement : MonoBehaviour
{
    public Vector3 movementRange;
    public float smoothTime = 0.3F;

    private Vector3 velocity = Vector3.zero;
    private Transform followTransform;
    private Vector3 initPosition;

    void Start()
    {
        initPosition = transform.position;
        GameObject followGO = new GameObject();
        followTransform = followGO.transform;
        followTransform.position = initPosition;
        StartCoroutine(MoveTarget());
    }

    // Update is called once per frame
    void Update()
    { 
        transform.position = Vector3.SmoothDamp(transform.position, followTransform.position, ref velocity, smoothTime);
    }

    private IEnumerator MoveTarget()
    {
        while (true)
        {

            followTransform.position = new Vector3(
                initPosition.x - (movementRange.x * .5f) + Random.Range(0f, movementRange.x),
                initPosition.y - (movementRange.y * .5f) + Random.Range(0f, movementRange.y),
                initPosition.z - (movementRange.z * .5f) + Random.Range(0f, movementRange.z));

            yield return new WaitForSeconds(1f);
        }

    }
}
