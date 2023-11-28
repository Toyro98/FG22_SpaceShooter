using Unity.Entities;

namespace SpaceShooter
{
    public class ProjectileBaker : Baker<Projectile>
    {
        public override void Bake(Projectile authoring)
        {
            var projectileEntity = GetEntity(TransformUsageFlags.Dynamic);

            AddComponent(projectileEntity, new ProjectileProperties
            {
                Speed = authoring.Speed,
            });
        }
    }
}
