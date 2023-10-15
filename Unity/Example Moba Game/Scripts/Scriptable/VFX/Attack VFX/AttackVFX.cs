using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Attack VFX", menuName = "Project3Fusion/VFX/Attack VFX")]
public class AttackVFX : VFX
{
    public GameObject vfxGameObject, vfxHitGameObject;
    public float vfxDestroySeconds, vfxHitDestroySeconds;
}
