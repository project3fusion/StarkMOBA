using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITargeter
{
    public void SendDamage(float adDamage, float apDamage, Actor target, Transform transform, string key);
}
