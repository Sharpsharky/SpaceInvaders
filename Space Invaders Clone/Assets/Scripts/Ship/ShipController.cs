using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipController : MonoBehaviour
{
    private float moveSpeed;
    private ShipInput shipInput;
    private PlayerConfigHolder playerConfigHolder;
    private float maxOffsetMovement;

    private void Awake()
    {
        shipInput = GetComponent<ShipInput>();
        playerConfigHolder = GetComponent<PlayerConfigHolder>();
        moveSpeed = playerConfigHolder.ShipSettings.MoveSpeed;
        maxOffsetMovement = playerConfigHolder.ShipSettings.MaxMoveOffset;
    }

    private void Update()
    {
        float thrust = shipInput.Thrust;
        Vector3 tempPos = transform.position;
        tempPos -= thrust * transform.right * Time.deltaTime * moveSpeed;
        tempPos.x = Mathf.Clamp(tempPos.x, -maxOffsetMovement, maxOffsetMovement);
        transform.position = tempPos;
    }

}
