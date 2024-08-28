using Content.Server.Botany.Components;
using Robust.Shared.Random;
using Robust.Shared.Timing;

namespace Content.Server.Botany.Systems
{
    public sealed class UnviableGrowthSystem : PlantGrowthSystem
    {
        [Dependency] private readonly IRobustRandom _random = default!;
        [Dependency] private readonly IGameTiming _gameTiming = default!;
        public override void Initialize()
        {
            base.Initialize();
        }

        public override void Update(float frameTime)
        {
            if (nextUpdate > _gameTiming.CurTime)
                return;

            var query = EntityQueryEnumerator<UnviableGrowthComponent>();
            while (query.MoveNext(out var uid, out var unviableGrowthComponent))
            {
                Update(uid, unviableGrowthComponent);
            }
            nextUpdate = _gameTiming.CurTime + updateDelay;
        }

        public void Update(EntityUid uid, UnviableGrowthComponent component)
        {
            PlantHolderComponent? holder = null;
            Resolve<PlantHolderComponent>(uid, ref holder);

            if (holder == null || holder.Seed == null || holder.Dead)
                return;

            holder.Health -= 6 * _random.Next(1, 3) * HydroponicsSpeedMultiplier;
        }
    }
}
