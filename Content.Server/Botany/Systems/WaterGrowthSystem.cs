using Content.Server.Botany.Components;
using Robust.Shared.Random;
using Robust.Shared.Timing;

namespace Content.Server.Botany.Systems
{
    public sealed class WaterGrowthSystem : PlantGrowthSystem
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

            var query = EntityQueryEnumerator<WaterGrowthComponent>();
            while (query.MoveNext(out var uid, out var waterGrowthComponent))
            {
                Update(uid, waterGrowthComponent);
            }
            nextUpdate = _gameTiming.CurTime + updateDelay;
        }

        public void Update(EntityUid uid, WaterGrowthComponent component)
        {
            PlantHolderComponent? holder = null;
            Resolve<PlantHolderComponent>(uid, ref holder);

            if (holder == null || holder.Seed == null)
                return;

            if (component.WaterConsumption > 0 && holder.WaterLevel > 0 && _random.Prob(0.75f))
            {
                holder.WaterLevel -= MathF.Max(0f,
                    holder.Seed.WaterConsumption * PlantHolderSystem.HydroponicsConsumptionMultiplier * PlantHolderSystem.HydroponicsSpeedMultiplier);
                if (holder.DrawWarnings)
                    holder.UpdateSpriteAfterUpdate = true;
            }

            var healthMod = _random.Next(1, 3) * PlantHolderSystem.HydroponicsSpeedMultiplier;
            if (holder.SkipAging < 10)
            {
                // Make sure the plant is not thirsty.
                if (holder.WaterLevel > 10)
                {
                    holder.Health += Convert.ToInt32(_random.Prob(0.35f)) * healthMod;
                }
                else
                {
                    AffectGrowth(-1, holder);
                    holder.Health -= healthMod;
                }
            }
        }
    }
}
