using Content.Server.Botany.Components;
using Robust.Shared.Random;

namespace Content.Server.Botany.Systems
{
    public sealed class WaterGrowthSystem : PlantGrowthSystem // EntitySystem?
    {
        [Dependency] private readonly IRobustRandom _random = default!;
        public override void Initialize()
        {
            base.Initialize();
            SubscribeLocalEvent<PlantHolderComponent, GrowEvent>(OnGrow);
        }

        public void OnGrow(Entity<PlantHolderComponent> ent, ref GrowEvent args)
        {
            if (args.holder.Seed == null || args.component is not WaterGrowthComponent)
                return;

            var holder = args.holder;
            var component = (WaterGrowthComponent)args.component;

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
