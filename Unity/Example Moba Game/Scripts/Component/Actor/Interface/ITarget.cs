using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITarget
{
    public void RecieveDamage(float adDamage, float apDamage);
    public void SetSelfTargetPointTransform();
}
