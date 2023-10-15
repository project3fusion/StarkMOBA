using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project3Fusion.Scriptable
{
    [CreateAssetMenu(fileName = "Projectile", menuName = "Project3Fusion/Projectile")]
    public class Projectile : ScriptableObject
    {
        public GameObject projectileVFXGameObject;
        public GameObject projectileHitVFXGameObject;
    }
}