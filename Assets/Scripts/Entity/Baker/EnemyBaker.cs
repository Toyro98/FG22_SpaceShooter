using Unity.Entities;

namespace SpaceShooter
{
    public class EnemyBaker : Baker<Enemy>
    {
        public override void Bake(Enemy authoring)
        {
            var enemyEntity = GetEntity(TransformUsageFlags.Dynamic);

            AddComponent(enemyEntity, new EnemyProperties
            { 
                Health = authoring.Health,
                MoveSpeed = authoring.MoveSpeed,
            });
        }
    }
}
