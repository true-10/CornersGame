using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Corners
{
    [CreateAssetMenu(menuName = "CornerGame/Game Settings")]
    public class GameSettingsSO : ScriptableObject
    {
        [Header("GameMode")]
        public GameMode GameMode;
        [Header("Grid Data")]
        public Vector3Int GridSize = Vector3Int.one * 5;
        public Vector3Int SectorSize = Vector3Int.one * 3;
        public Vector3 Offset = Vector3.zero;
        public Vector3 CellSize = Vector3.one;
    }


    public static class GlobalConstants
    {
        public static class LayersName
        {
            public const string GRID_LAYER = "Grid";
        }

        public static class SomeNumbers
        {
            public const float RAYCAST_DISTANCE = 1200f;
        }

        public class Chips
        {
            public static Vector3 SELECTED_SCALE = Vector3.one * 1.2f;
            public static float MOVE_DURATION = 0.3f;
        }
    }
}