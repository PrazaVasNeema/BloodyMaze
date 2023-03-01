using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BloodyMaze
{
    [System.Serializable]
    public class PickableItem
    {
        public string name;
        public Sprite displaySprite;
        public GameObject modelPrefab;
    }
}
