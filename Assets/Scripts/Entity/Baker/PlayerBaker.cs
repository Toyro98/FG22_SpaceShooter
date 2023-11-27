using Unity.Entities;

namespace SpaceShooter
{
    public class PlayerBaker : Baker<Player>
    {
        public override void Bake(Player authoring)
        {
            var playerEntity = GetEntity(TransformUsageFlags.Dynamic);

            AddComponent(playerEntity, new PlayerProperties
            {
                MoveSpeed = authoring.MoveSpeed,
                RotationSpeed = authoring.RotationSpeed
            });

            AddComponent<PlayerTag>(playerEntity);
        }
    }
}
