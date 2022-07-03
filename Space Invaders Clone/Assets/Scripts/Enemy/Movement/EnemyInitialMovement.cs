using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyInitialMovement : MonoBehaviour
{
    private Vector3 startPosition = new Vector3(0,0,0);
    private Transform targetPosition;
    
    private Vector3 centerPosition;
    private Vector3 startRelCenter;
    private Vector3 endRelCenter;

    float startTime;
    float journeyTime = 1.0f;
    float speed = 0.52f;

    bool slerp = true;
    bool isParented = false;

    public Transform TargetPosition { get => targetPosition; set => targetPosition = value; }
    public Vector3 StartPosition { get => startPosition; set => startPosition = value; }

    private void Start()
    {
        isParented = false;
        startTime = Time.time;
        StartPosition = transform.position;
        slerp = true;

    }
    private void OnEnable()
    {
        //isParented = false;
        startTime = Time.time;
        //startPosition = transform.position;
        //Debug.Log("transform.position " + transform.position + "transform.localPosition " + transform.localPosition);
        //slerp = true;
    }

    private void OnDisable()
    {
        centerPosition = new Vector3(0, 0, 0);
        startRelCenter = new Vector3(0, 0, 0);
        endRelCenter = new Vector3(0, 0, 0);
        StartPosition = new Vector3(0, 0, 0);
        StartPosition = transform.position;
        slerp = true;
        isParented = false;

    }

    private void FixedUpdate()
    {
        if (slerp)
        {
            GetCenter(new Vector3(0, 0, -1));
            float fracComplete = (Time.time - startTime) / journeyTime * speed;
            transform.position = Vector3.Slerp(startRelCenter, endRelCenter, fracComplete * speed);
            transform.position += centerPosition;
            transform.rotation = Quaternion.LookRotation(transform.forward);
            if (Vector3.Distance(transform.position,targetPosition.position) <= 0.02f) slerp = false;
        }
        else
        {
            if (!isParented)
            {
                transform.parent = targetPosition;
                isParented = true;
            }
            //transform.position = Vector3.MoveTowards(transform.position, targetPosition.position, speed2 * Time.deltaTime);
        }



    }

    private void GetCenter(Vector3 dir)
    {
        centerPosition = (StartPosition + TargetPosition.position)*0.5f;
        centerPosition -= dir;
        startRelCenter = StartPosition - centerPosition;
        endRelCenter = TargetPosition.position - centerPosition;
    }
}
