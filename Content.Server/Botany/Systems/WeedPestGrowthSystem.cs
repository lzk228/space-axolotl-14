using Content.Server.Botany.Components;
using Robust.Shared.Random;

namespace Content.Server.Botany.Systems
{
    public sealed class WeedPestGrowthSystem : PlantGrowthSystem
    {
        public override void Initialize()
        {
            base.Initialize();
        }

        public override void Update(float frameTime)
        {
            if (nextUpdate > _gameTiming.CurTime)
                return;

            var query = EntityQueryEnumerator<WeedPestGrowthComponent>();
            while (query.MoveNext(out var uid, out var weedPestGrowthComponent))
            {
                Update(uid, weedPestGrowthComponent);
            }
            nextUpdate = _gameTiming.CurTime + updateDelay;
        }

        public void Update(EntityUid uid, WeedPestGrowthComponent component)
        {
            PlantHolderComponent? holder = null;
            Resolve<PlantHolderComponent>(uid, ref holder);

            if (holder == null || holder.Seed == null || holder.Dead)
                return;

            // There's a small chance the pest population increases. Only happens with plants present.
            if (_random.Prob(0.01f))
            {
                holder.PestLevel += 0.5f * HydroponicsSpeedMultiplier;
                if (holder.DrawWarnings)
                    holder.UpdateSpriteAfterUpdate = true;
            }

            // Pest levels.
            if (holder.PestLevel > component.PestTolerance)
                holder.Health -= HydroponicsSpeedMultiplier;

            // Weed levels.
            if (holder.WeedLevel >= component.WeedTolerance)
                holder.Health -= HydroponicsSpeedMultiplier;
        }
    }
}
