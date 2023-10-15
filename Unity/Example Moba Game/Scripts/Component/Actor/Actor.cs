using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public abstract class Actor : Component, ITarget, ITargeter
{
    public enum Type { Player, Minion, Tower };

    [NonSerialized] public NetworkVariable<bool> isDead = new NetworkVariable<bool>(false);
    [NonSerialized] public Transform selfTargetPointTransform;
    [NonSerialized] public Type type;
    [NonSerialized] public int id;

    public abstract void OnDataGenerated();

    public abstract void RecieveDamage(float adDamage, float apDamage);

    public abstract void SetSelfTargetPointTransform();

    public abstract void SendDamage(float adDamage, float apDamage, Actor target, Transform myTransform, string key);
}
