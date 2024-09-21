using Content.Server.Atmos.EntitySystems;
using Content.Server.Botany.Components;
using Content.Shared.Atmos;

namespace Content.Server.Botany.Systems
{
    public sealed class ExudeGasGrowthSystem : PlantGrowthSystem
    {
        [Dependency] private readonly AtmosphereSystem _atmosphere = default!;
        public override void Initialize()
        {
            base.Initialize();
        }

        public override void Update(float frameTime)
        {
            if (nextUpdate > _gameTiming.CurTime)
                return;

            var query = EntityQueryEnumerator<ExudeGasGrowthComponent>();
            while (query.MoveNext(out var uid, out var exudeGasGrowthComponent))
            {
                Update(uid, exudeGasGrowthComponent);
            }
            nextUpdate = _gameTiming.CurTime + updateDelay;
        }

        public void Update(EntityUid uid, ExudeGasGrowthComponent component)
        {
            PlantHolderComponent? holder = null;
            Resolve<PlantHolderComponent>(uid, ref holder);

            if (holder == null || holder.Seed == null || holder.Dead)
                return;

            var environment = _atmosphere.GetContainingMixture(uid, true, true) ?? GasMixture.SpaceGas;

            // Gas production.
            var exudeCount = component.ExudeGasses.Count;
            if (exudeCount > 0)
            {
                foreach (var (gas, amount) in component.ExudeGasses)
                {
                    environment.AdjustMoles(gas,
                        MathF.Max(1f, MathF.Round(amount * MathF.Round(holder.Seed.Potency) / exudeCount)));
                }
            }
        }
    }
}
