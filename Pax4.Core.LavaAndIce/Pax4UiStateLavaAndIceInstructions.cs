using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Runtime.Serialization;

namespace Pax4.Core
{
    [DataContract]
    [KnownType(typeof(Pax4UiStateLavaAndIceInstructions))]
    class Pax4UiStateLavaAndIceInstructions : Pax4UiState
    {
        public Pax4UiStateLavaAndIceInstructions(String p_name, Pax4Ui p_ui)
            : base(p_name, p_ui)
        {
            String textureName = null;
            Texture2D texture = null;
            Pax4Sprite sprite = null;

            sprite = new Pax4Button("instructions", null);

            textureName = "Sprite/lavaandiceInstructions";
            texture = Pax4Texture2D._current.Get(textureName);
            ((Pax4Button)sprite).SetTexture(texture);
            ((Pax4Button)sprite).SetOnClick(this.lavaandiceInstructionsButton_Click);
            AddChild(sprite);
        }

        private void lavaandiceInstructionsButton_Click()
        {
            ((Pax4SoundLavaAndIce)Pax4Sound._current)._lavaandiceButtonAccepted.Play();
            Pax4Ui._current.Enter("chooseQuest");
        }
    }
}