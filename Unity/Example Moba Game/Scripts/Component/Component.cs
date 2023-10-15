using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class Component : NetworkBehaviour
{
    public enum Team { Blue, Red, Neutral };

    [NonSerialized] public Team team;
}
