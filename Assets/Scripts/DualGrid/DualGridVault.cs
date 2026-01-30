using System;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace DualGrid
{
    [CreateAssetMenu(fileName = "Dual_Grid_Vault", menuName = "ScriptableObjects/Dual Grid Vault")]
    public class DualGridVault : ScriptableObject
    {
        public Group[] groups;

        [Serializable]
        public struct Group
        {
            public TileBase baseTile;
            public TileBase[] tiles;
        }
    }
}
