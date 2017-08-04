using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Pax.Core;
using System.Runtime.Serialization;

namespace Pax4.Core
{
    [DataContract]
    [KnownType(typeof(Pax4UiLavaAndIce))]
    public class Pax4UiLavaAndIce : Pax4Ui
    {
        public Pax4UiLavaAndIce(String p_name, PaxState p_parent0)
            : base(p_name, p_parent0)
        {
        }

        public override void Create()
        {
            Pax4UiState state = null;

            state = new Pax4UiStateLavaAndIceChooseQuest("chooseQuest", this);
            AddUiState(state);

            Enter(state);

            state = new Pax4UiStateLavaAndIceChooseMissionPrologue("Prologue", this);
            AddUiState(state);

            //state = new Pax4UiStateLavaAndIceChooseMissionEquilibrium("Equilibrium", this);
            //AddState(state);

            //state = new Pax4UiStateLavaAndIceChooseMissionEquilibrium("LavaGrail", this);
            //AddState(state);

            //state = new Pax4UiStateLavaAndIceChooseMissionEquilibrium("IceGrail", this);
            //AddState(state);

            //state = new Pax4UiStateLavaAndIceChooseMissionEquilibrium("Dragons", this);
            //AddState(state);

            state = new Pax4UiStateLavaAndIceInstructions("instructions", null);
            AddUiState(state);

            state = new Pax4UiStateLavaAndIceMissionDifficulty("difficulty", this);
            AddUiState(state);

            state = new Pax4UiStateLavaAndIceMenu("menu", this);
            AddUiState(state);

            state = new Pax4UiStateLavaAndIceVictory("victory", this);
            AddUiState(state);

            state = new Pax4UiStateLavaAndIceDefeat("defeat", this);
            AddUiState(state);

            state = new Pax4UiStateLavaAndIceMission("fgLava", this, Pax4WorldLavaAndIce.ELavaAndIceMissionType._LAVA);
            AddUiState(state);

            state = new Pax4UiStateLavaAndIceMission("fgIce", this, Pax4WorldLavaAndIce.ELavaAndIceMissionType._ICE);
            AddUiState(state);

            state = new Pax4UiStateLavaAndIceMission("fgLavaAndIce", this, Pax4WorldLavaAndIce.ELavaAndIceMissionType._LAVA_AND_ICE);
            AddUiState(state);
        }
    }
}