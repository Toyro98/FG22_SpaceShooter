using Unity.Entities;

namespace SpaceShooter
{
    public class PlayerBaker : Baker<Player>
    {
        public override void Bake(Player authoring)
        {
            var playerEntity = GetEntity(TransformUsageFlags.Dynamic);
            AddComponent<PlayerTag>(playerEntity);
        }
    }
}
