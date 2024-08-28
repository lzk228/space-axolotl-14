using Content.Server.Botany.Components;
using Robust.Shared.Random;
using Robust.Shared.Timing;

namespace Content.Server.Botany.Systems
{
    public sealed class AutoHarvestGrowthSystem : PlantGrowthSystem
    {
        [Dependency] private readonly PlantHolderSystem _plantHolderSystem = default!;
        public override void Initialize()
        {
            base.Initialize();
        }

        public override void Update(float frameTime)
        {
            if (nextUpdate > _gameTiming.CurTime)
                return;

            var query = EntityQueryEnumerator<AutoHarvestGrowthComponent>();
            while (query.MoveNext(out var uid, out var autoHarvestGrowthComponent))
            {
                Update(uid, autoHarvestGrowthComponent);
            }
            nextUpdate = _gameTiming.CurTime + updateDelay;
        }

        public void Update(EntityUid uid, AutoHarvestGrowthComponent component)
        {
            PlantHolderComponent? holder = null;
            Resolve<PlantHolderComponent>(uid, ref holder);

            if (holder == null || holder.Seed == null || holder.Dead || !holder.Harvest)
                return;
            
            _plantHolderSystem.AutoHarvest(uid, holder);
        }
    }
}
