using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BloodyMaze
{
    public abstract class SpawnableComponentAbstract : MonoBehaviour
    {
        public abstract void Activate(float damage, float force);
    }
}
