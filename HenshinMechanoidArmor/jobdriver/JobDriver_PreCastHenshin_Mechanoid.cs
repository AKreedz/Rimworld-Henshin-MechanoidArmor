using HenshinCoreMod.basic;
using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;
using Verse.AI;

namespace HenshinMechanoidArmor.jobdriver
{
    public class JobDriver_PreCastHenshin_Mechanoid : Job_Henshin
    {
        public override bool TryMakePreToilReservations(bool errorOnFailed)
        {
            return true;
        }



        // Token: 0x060002E0 RID: 736 RVA: 0x0001C17C File Offset: 0x0001A57C
        public override IEnumerable<Toil> ExtraToils()
        {
            int tick = 50;
            int maxTime = 10;
            while (tick>5) {
                Toil preparef = simpleEffect(tick, DefDatabase<EffecterDef>.GetNamed("HenshinMechanoidArmor_HenshinEffect"));
                yield return preparef;
                //Toil prepare = simpleEffect(tick, EffecterDefOf.ApocrionAoeResolve);
                tick -= 5;
                //yield return prepare;
            }
            while (maxTime>0) {
                Toil preparef = simpleEffect(3, DefDatabase<EffecterDef>.GetNamed("HenshinMechanoidArmor_HenshinEffect"));
                yield return preparef;
                maxTime--;
            }
            Toil prepare2 = simpleEffect(5, DefDatabase<EffecterDef>.GetNamed("MechRepairing"));
            addFinishEvent(prepare2, delegate
            {
                DefDatabase<EffecterDef>.GetNamed("GiantExplosion").SpawnAttached(prepare2.actor, prepare2.actor.Map, 2f);
                DefDatabase<EffecterDef>.GetNamed("AtomizerResolve").SpawnAttached(prepare2.actor, prepare2.actor.Map, 2f);
            });
            
            yield return prepare2;


            yield break;
        }

        public override void MainToil(Toil main)
        {
            //main.WithEffect(DefDatabase<EffecterDef>.GetNamed("AtomizerResolve"),TargetIndex.A);
            /*main.finishActions = new List<Action> {
                delegate{
                    Log.Message("wuhu");
                    DefDatabase<EffecterDef>.GetNamed("AtomizerResolve").SpawnAttached(main.actor,main.actor.Map);
                }
            };*/

        }
    }
}
