using Content.Server.Botany.Components;
using Robust.Shared.Random;
using Robust.Shared.Timing;

namespace Content.Server.Botany.Systems
{
    public sealed class AgeGrowthSystem : PlantGrowthSystem
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

            var query = EntityQueryEnumerator<AgeGrowthComponent>();
            while (query.MoveNext(out var uid, out var ageGrowthComponent))
            {
                Update(uid, ageGrowthComponent);
            }
            nextUpdate = _gameTiming.CurTime + updateDelay;
        }

        public void Update(EntityUid uid, AgeGrowthComponent component)
        {
            PlantHolderComponent? holder = null;
            Resolve<PlantHolderComponent>(uid, ref holder);

            if (holder == null || holder.Seed == null || holder.Dead)
                return;

            // Advance plant age here.
            if (holder.SkipAging > 0)
                holder.SkipAging--;
            else
            {
                if (_random.Prob(0.8f))
                {
                    holder.Age += (int)(1 * HydroponicsSpeedMultiplier);
                    holder.UpdateSpriteAfterUpdate = true;
                }
            }
        }
    }
}
