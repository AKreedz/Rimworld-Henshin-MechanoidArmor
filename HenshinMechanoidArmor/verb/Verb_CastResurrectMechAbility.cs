using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;

namespace HenshinMechanoidArmor.verb
{
    public class Verb_CastResurrectMechAbility : Verb_CastAbility
    {

		protected override bool TryCastShot()
		{
			return base.TryCastShot() &&
				 
				this.DoResurrect(this.CasterPawn, this.currentTarget, this.verbProps  );
		}

		// Token: 0x06006F04 RID: 28420 RVA: 0x002578EA File Offset: 0x00255AEA
		public override void OnGUI(LocalTargetInfo target)
		{
			if (this.CanHitTarget(target) && isMechCorpse(this.CasterPawn, target,this.verbProps ))
			{
				base.OnGUI(target);
				return;
			}
			GenUI.DrawMouseAttachment(TexCommand.CannotShoot);
		}


		private bool isMechCorpse(Pawn caster, LocalTargetInfo target, VerbProperties verbProps) {
			bool canSaveEnemy = verbProps.flyWithCarriedThing;//用来当做是否可以复活敌人

			return caster.Map.thingGrid.ThingsAt(target.Cell).Any(x => {
				Corpse corpse = x as Corpse; 
				return judgeResurrect(canSaveEnemy,corpse,caster);
			});
			 
		}

		private bool judgeResurrect(bool canSaveEnemy,Corpse corpse,Pawn caster) {
			if (corpse == null)
			{
				return false;
			}
			return corpse.InnerPawn.RaceProps.IsMechanoid && corpse.InnerPawn.RaceProps.mechWeightClass < MechWeightClass.UltraHeavy && (canSaveEnemy || corpse.InnerPawn.Faction == caster.Faction)
					&& (corpse.InnerPawn.kindDef.abilities == null || !corpse.InnerPawn.kindDef.abilities.Contains(AbilityDefOf.ResurrectionMech));// && corpse.timeOfDeath >= Find.TickManager.TicksGame - this.Props.maxCorpseAgeTicks ;

		}


		private bool DoResurrect(Pawn caster,LocalTargetInfo target,VerbProperties verbProps ) {
			bool canSaveEnemy = verbProps.flyWithCarriedThing;
			IEnumerable<Thing> ts = caster.Map.thingGrid.ThingsAt(target.Cell).Where(x => {
				Corpse corpse = x as Corpse; 
				return judgeResurrect(canSaveEnemy, corpse, caster);
			});
			foreach (Thing t in ts)
			{
				Corpse corpse = (Corpse)t;
				if (corpse != null)
				{
					Pawn innerPawn = corpse.InnerPawn;

					ResurrectionUtility.TryResurrect(innerPawn, null);
					 
						Effecter effecter = //DefDatabase<EffecterDef>.GetNamed("MechResurrected").SpawnAttached(innerPawn, innerPawn.MapHeld, 1f);  //
											verbProps.beamEndEffecterDef.SpawnAttached(innerPawn, innerPawn.MapHeld, 1f);
						effecter.Trigger(innerPawn, innerPawn, -1);
						effecter.Cleanup();
					 
					innerPawn.stances.stagger.StaggerFor(60, 0.17f);
				}
			}
			return true;

		}

	}
}
