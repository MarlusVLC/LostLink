using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Aux_Classes
{
    public static class Extensions
    {
        public static int CountSetBits(this int n)
        {
            if (n == 0)
                return 0;

            return (n & 1) + CountSetBits(n >> 1);
        }

        public static void ForEach<T>(this IEnumerable<T> enumeration, Action<T> action)
        {
            foreach (var item in enumeration)
            {
                action(item);
            }
        }

        public static Tuple<Vector2, Vector2> AdaptToAboveEntity(this PositionCheckingBox checkingBox,
            float extensionSizeX, float extensionSizeY, float extensionOffsetX, float extensionOffsetY)
        {
            var originalSize = checkingBox.CollisionDetectorSize;
            var originalOffset = checkingBox.CollisionDetectorOffset;
            // var originalDimensions =
            //     new Tuple<Vector2, Vector2>(checkingBox.CollisionDetectorSize, checkingBox.CollisionDetectorOffset);

            checkingBox.CollisionDetectorSize = new Vector2(extensionSizeX, extensionSizeY);
            checkingBox.CollisionDetectorOffset = new Vector2(extensionOffsetX, extensionOffsetY);
            
            return new Tuple<Vector2, Vector2>(originalSize, originalOffset);
        }
        
        
        
    }
}