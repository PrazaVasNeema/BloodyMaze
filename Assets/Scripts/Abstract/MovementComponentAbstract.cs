using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BloodyMaze
{
    public abstract class MovementComponentAbstract : MonoBehaviour
    {
        public abstract void Init(float speed);

        public abstract void Move(Vector3 dir);

        public abstract void MoveTo(Vector3 position);

        public virtual void Look(Vector3 dir)
        {
            transform.rotation = Quaternion.Euler(0f, Mathf.Atan2(-dir.z, dir.x) * Mathf.Rad2Deg + 90f, 0f);
        }
    }
}
