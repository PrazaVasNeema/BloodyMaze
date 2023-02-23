using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BloodyMaze
{
    public interface ISpawnableComponent
    {
        public void Activate(float damage, float force);
    }
}
