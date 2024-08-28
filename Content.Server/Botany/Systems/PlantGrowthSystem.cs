using Content.Server.Botany.Components;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using Robust.Shared.Random;
using Robust.Shared.Serialization.Manager;

namespace Content.Server.Botany.Systems
{
    public abstract class PlantGrowthSystem : EntitySystem
    {
        [Dependency] private readonly IRobustRandom _random = default!;
        [Dependency] private readonly ISerializationManager _serializationManager = default!;


        public TimeSpan nextUpdate = TimeSpan.Zero;
        public TimeSpan updateDelay = TimeSpan.FromSeconds(15); //PlantHolder has a 15 second delay on cycles, but checks every 3 for sprite updates.

        public const float HydroponicsSpeedMultiplier = 1f;
        public const float HydroponicsConsumptionMultiplier = 2f;

        public override void Initialize()
        {
            base.Initialize();
        }

        public void Cross(ref PlantGrowthComponent main, PlantGrowthComponent other)
        {
            if (_random.Prob(0.5f))
                _serializationManager.CopyTo(other, ref main, notNullableOverride: true);
        }

        public void AffectGrowth(int amount, PlantHolderComponent? component = null)
        {
            if (component == null || component.Seed == null)
                return;

            if (amount > 0)
            {
                if (component.Age < component.Seed.Maturation)
                    component.Age += amount;
                else if (!component.Harvest && component.Seed.Yield <= 0f)
                    component.LastProduce -= amount;
            }
            else
            {
                if (component.Age < component.Seed.Maturation)
                    component.SkipAging++;
                else if (!component.Harvest && component.Seed.Yield <= 0f)
                    component.LastProduce += amount;
            }
        }
    }
}
