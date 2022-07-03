using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWaveMovement : MonoBehaviour
{

    //[SerializeField] private float maxOffsetMovement = 5; //const
    [SerializeField] private float delayTime = 1f;
    [SerializeField] private float enemyInitialSpeed = 2; //const
    [SerializeField] private float maxSpeed = 10; //const
    [SerializeField] private float speedFactor = 0.2f;
    [SerializeField] private WaveManager waveManager;
    [SerializeField] float moveDownDistance = 1;


    private float currentSpeed;
    private Vector3 initialPosition = new Vector3(0,0,0); //const
    private bool isDelay = true;
    private int directionSign = 1;
    private bool isGoingDown = false;
    private float lastZCoord;
    private float newZCoord;


    private void Awake()
    {
        waveManager.OnStartWave += StartWave;
        WallLogic.OnEnemyCollide += ChangeSideMovement;
    }
    private void Start()
    {
        Enemy.OnEnemyKilled += IncrementSpeed;
    }
    private void StartWave()
    {
        lastZCoord = initialPosition.y;
        newZCoord = lastZCoord;
        currentSpeed = enemyInitialSpeed;
        isDelay = true;
        StartCoroutine(StartWithDelay());
        initialPosition = transform.position;
    }

    private void IncrementSpeed()
    {
        currentSpeed += speedFactor;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        SideMovementLogic();
        VerticalMovementLogic();
    }

    private void SideMovementLogic()
    {
        if (!isDelay)
        {
            transform.Translate(transform.right * directionSign * currentSpeed * Time.deltaTime);
        }

        /* Side movement logic in case 
        if (directionSign > 0 && (transform.localPosition.x - initialPosition.x) >= maxOffsetMovement)
        {
            MoveDown(-1);
        }
        else if (directionSign < 0 && (initialPosition.x - transform.localPosition.x) >= maxOffsetMovement)
        {
            MoveDown(1);
        }*/


    }

    private void VerticalMovementLogic()
    {
        if (isGoingDown)
        {
            transform.Translate(transform.forward * currentSpeed * Time.deltaTime);
            if (transform.position.z >= newZCoord) isGoingDown = false;
        }
    }

    
    private void ChangeSideMovement(string wallTag)
    {
        if (wallTag == "Left Wall" && directionSign == 1)
        {
            MoveDown(-1);
        }
        else if (wallTag == "Right Wall" && directionSign == -1)
        {
            MoveDown(1);
        }
    }

    private void MoveDown(int dir)
    {
        directionSign = dir;
        if (currentSpeed > maxSpeed) currentSpeed = maxSpeed;
        newZCoord = lastZCoord + moveDownDistance;
        lastZCoord = newZCoord;
        isGoingDown = true;
    }

    private IEnumerator StartWithDelay()

    {
        yield return new WaitForSeconds(delayTime);
        isDelay = false;
        yield return null;
    }

    
}
