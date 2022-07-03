using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerConfigHolder : MonoBehaviour
{
    [SerializeField]
    private ShipSettings shipSettings;

    public ShipSettings ShipSettings { get => shipSettings; set => shipSettings = value; }
}
