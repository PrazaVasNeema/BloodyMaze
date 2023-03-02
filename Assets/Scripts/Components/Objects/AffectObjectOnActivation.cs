using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace BloodyMaze.Components
{
    public class AffectObjectOnActivation : MonoBehaviour
    {
        [SerializeField] private Transform m_positionWhereToMove;

        public void Move()
        {
            Debug.Log("Move");
            transform.DOMove(m_positionWhereToMove.position, 2f);
        }
    }
}
