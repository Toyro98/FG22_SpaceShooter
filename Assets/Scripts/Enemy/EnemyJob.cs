using UnityEngine;
using Unity.Jobs;
using Unity.Burst;
using Unity.Collections;
using Unity.Mathematics;

namespace SpaceShooter
{
    [BurstCompile]
    public struct EnemyJob : IJobParallelFor
    {
        [ReadOnly] public float Speed;
        [ReadOnly] public float DeltaTime;
        [ReadOnly] public Vector3 PlayerPosition;

        public NativeArray<Vector3> CurrentPosition;
        public NativeArray<float> DistanceToPlayer;

        public void Execute(int i)
        {
            CurrentPosition[i] = Vector3.MoveTowards(CurrentPosition[i], PlayerPosition, DeltaTime * Speed);
            DistanceToPlayer[i] = Vector3.Distance(CurrentPosition[i], PlayerPosition);
        }
    }
}