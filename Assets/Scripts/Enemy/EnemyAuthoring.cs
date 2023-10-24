using Unity.Entities;
using UnityEngine;

namespace SpaceShooter
{
    public class EnemyAuthoring : MonoBehaviour
    {
        public int Health = 1;

        private class Baker : Baker<EnemyAuthoring>
        {
            public override void Bake(EnemyAuthoring authoring)
            {
                var data = new Enemy
                {
                    Health = authoring.Health
                };

                AddComponent(GetEntity(TransformUsageFlags.None), data);
            }
        }
    }
}
