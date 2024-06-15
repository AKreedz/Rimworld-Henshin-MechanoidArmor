using HenshinCoreMod.basic;
using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;
using Verse.Sound;

namespace HenshinMechanoidArmor.armor
{
    public class HenshinArmor_Mechanoid : HenshinArmor
    {


        private void takeOffFX()
        {
            if (this.Wearer != null && this.Wearer.Map != null)
            {
                DefDatabase<EffecterDef>.GetNamed("AtomizerResolve").SpawnAttached(this.Wearer, this.Wearer.Map, 2f);
                //DefDatabase<EffecterDef>.GetNamed("HenshinTest_Cancel").SpawnAttached(this.Wearer, this.Wearer.Map, 2f);

                float magnitude = (this.Wearer.Position.ToVector3Shifted() - Find.Camera.transform.position).magnitude;
                Find.CameraDriver.shaker.DoShake(300000f / magnitude);
                this.HideArmor = true;
                this.GraphicNotDirty = false;
                if (IsMaxPain)
                {
                    EffecterDef def = DefDatabase<EffecterDef>.GetNamed("BlastMechBandShockwave");//.Spawn(this.Wearer.Position,this.Wearer.Map);

                    IntVec3 positionHeld = this.Wearer.PositionHeld;
                    float radius = 30;
                    DamageDef explosiveDamageType = DamageDefOf.MechBandShockwave;
                    Thing thing = Wearer;
                    int damageAmountBase = 40;
                    float armorPenetrationBase = 1;
                    SoundDef explosionSound = null;
                    ThingDef weapon = null;
                    ThingDef projectile = null;
                    Thing intendedTarget = null;
                    ThingDef postExplosionSpawnThingDef = null;
                    float postExplosionSpawnChance = 0;
                    int postExplosionSpawnThingCount = 0;
                    GasType? postExplosionGasType = null;
                    bool applyDamageToExplosionCellsNeighbors = false;
                    ThingDef preExplosionSpawnThingDef = null;
                    float preExplosionSpawnChance = 0;
                    int preExplosionSpawnThingCount = 1;
                    float chanceToStartFire = 0;
                    bool damageFalloff = false;
                    float? direction = null;
                    List<Thing> ignoredThings = new List<Thing>();
                    ignoredThings.Add(this.Wearer);
                    FloatRange? affectedAngle = null;
                    bool doVisualEffects = true;
                    bool doSoundEffects = true;
                    GenExplosion.DoExplosion(positionHeld, this.Wearer.Map, radius, explosiveDamageType, thing, damageAmountBase, armorPenetrationBase, explosionSound, weapon, projectile, intendedTarget, 
                        postExplosionSpawnThingDef, postExplosionSpawnChance, postExplosionSpawnThingCount, postExplosionGasType, applyDamageToExplosionCellsNeighbors, preExplosionSpawnThingDef, 
                        preExplosionSpawnChance, preExplosionSpawnThingCount, chanceToStartFire, damageFalloff, direction, ignoredThings, affectedAngle, doVisualEffects, 0.5f, 
                        0f, doSoundEffects, null, 1f, null, null);




                }
            }

        }

        public override void PostTakeOff()
        {
            takeOffFX();

        }

    }
}
