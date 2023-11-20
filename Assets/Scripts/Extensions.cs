using System.Runtime.CompilerServices;
using Unity.Burst;
using Unity.Mathematics;

namespace SpaceShooter
{
    public static class Extensions
    {
        // https://forum.unity.com/threads/is-there-an-analogue-of-movetowards-in-entities.1416837/#post-8908944
        [BurstCompile]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float3 MoveTowards(float3 current, float3 target, float maxDistanceDelta)
        {
            float dirX = target.x - current.x;
            float dirY = target.y - current.y;
            float dirZ = target.z - current.z;

            float sqrLength = dirX * dirX + dirY * dirY + dirZ * dirZ;

            if (sqrLength == 0.0 || maxDistanceDelta >= 0.0 && sqrLength <= maxDistanceDelta * maxDistanceDelta)
                return target;

            float dist = math.sqrt(sqrLength);

            return new float3(current.x + dirX / dist * maxDistanceDelta,
                              current.y + dirY / dist * maxDistanceDelta,
                              current.z + dirZ / dist * maxDistanceDelta);
        }
    }
}
