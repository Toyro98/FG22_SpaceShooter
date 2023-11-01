using UnityEngine;
using Unity.Jobs;
using Unity.Burst;
using Unity.Collections;

namespace SpaceShooter
{
    [BurstCompile]
    public struct EnemyJob : IJob
    {
        [ReadOnly] public Vector2 PlayerPosition;
        [ReadOnly] public int Health;
        [ReadOnly] public int Damage;
        [ReadOnly] public float Speed;
        [ReadOnly] public float DeltaTime;
        [ReadOnly] public Vector2 CurrentPosition;
        [WriteOnly] public NativeArray<Vector2> PositionResult;

        public void Execute()
        {
            PositionResult[0] = Vector3.MoveTowards(CurrentPosition, PlayerPosition, DeltaTime * Speed);
        }
    }
}
