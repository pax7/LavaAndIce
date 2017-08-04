using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Runtime.Serialization;

namespace Pax4.Core
{
    [DataContract]
    [KnownType(typeof(Pax4UiStateLavaAndIceChooseMission))]
    public class Pax4UiStateLavaAndIceChooseMission : Pax4UiState
    {
        public static Pax4UiStateLavaAndIceChooseMission _currentMissionState = null;

        public Pax4UiStateLavaAndIceChooseMission(String p_name, Pax4Ui p_ui)
            : base(p_name, p_ui)
        {
            _currentMissionState = this;

            //float duration = 0.5f;
            //float delay = 0.0f;

            //Pax4SpriteColorModifier colorModifierEnter = new Pax4SpriteColorModifier();
            //colorModifierEnter.Ini(Color.Black, Color.White, duration);
            //AddStateEnterModifier(colorModifierEnter);
            //Pax4SpriteColorModifier colorModifierExit = new Pax4SpriteColorModifier();
            //colorModifierExit.Ini(Color.White, Color.Black, duration);
            //AddStateExitModifier(colorModifierExit);

            //Pax4SpriteAlphaModifier alphaModifierEnter = new Pax4SpriteAlphaModifier();
            //alphaModifierEnter.Ini(0.0f, 1.0f, duration);
            //AddStateEnterModifier(alphaModifierEnter);
            //Pax4SpriteAlphaModifier alphaModifierExit = new Pax4SpriteAlphaModifier();
            //alphaModifierExit.Ini(1.0f, 0.0f, duration);
            //AddStateExitModifier(alphaModifierExit);

            //colorModifierEnter.AddChild(sprite);
            //colorModifierExit.AddChild(sprite);
            //alphaModifierEnter.AddChild(sprite);
            //alphaModifierExit.AddChild(sprite);

            //Vector2 position;
            //Pax4SpriteColorModifier colorModifier = null;
            //Pax4SpriteAlphaModifier alphaModifier = null;
            //Pax4SpritePositionModifier positionModifierEnter = null;
            //Pax4SpritePositionModifier positionModifierExit = null;
            //Pax4SpriteTextModifier textModifier = null;
            //Pax4UiState state = null;
            //float duration = Pax4Ui._btnDuration;
            //float delay = 0.0f;
            //Vector2 position0 = Vector2.Zero;
            //Vector2 position1 = Vector2.Zero;

            //String textureName = null;
            //Texture2D texture = null;
            //SpriteFont spriteFont = null;
            //Pax4Sprite sprite = null;

            ////**************************************************
            ////create
            ////**************************************************

            //position.X = 80.0f;
            //position.Y = 16.0f;
            ////misc
            //sprite = new Pax4Button("lavaandiceBackBtn", null);
            //AddSprite(sprite);
            //textureName = "Sprite/lavaandiceBackBtn";
            //texture = Pax4Texture2D._current.Get(textureName);
            //((Pax4Button)sprite).SetTexture(texture);
            //textureName = "Sprite/lavaandiceBackBtnOver";
            //texture = Pax4Texture2D._current.Get(textureName);
            //((Pax4Button)sprite).SetTextureOver(texture);
            //((Pax4Button)sprite).SetTextSpriteFont("SpriteFont/Livingstone", position, "Back");
            //((Pax4Button)sprite).SetOnClick(this.lavaandiceBackBtn_Click);
            //position.X = 160.0f;
            //position.Y = 820.0f;
            //sprite.SetPosition(position);

            //sprite.Enable();

            //duration = 0.5f;
            //delay = 0.0f;
            //position0.X = position.X;
            //position0.Y = 1920.0f;
            //position *= Pax4Camera._current._scale2;
            //position0 *= Pax4Camera._current._scale2;
            //positionModifierEnter = new Pax4SpritePositionModifier(sprite);
            //positionModifierEnter.Ini(position0, position, duration, delay);
            //AddStateEnterModifier(positionModifierEnter);

            //positionModifierExit = new Pax4SpritePositionModifier(sprite);
            //positionModifierExit.Ini(position, position0, duration, delay);

            ////colorModifier = new Pax4SpriteColorModifier(sprite);
            ////colorModifier.Ini(Color.Black, Color.White, duration/2.0f);
            ////AddStateEnterModifier(colorModifier);

            ////colorModifier = new Pax4SpriteColorModifier(sprite);
            ////colorModifier.Ini(Color.White, Color.Black, duration / 2.0f);
            ////AddStateExitModifier(colorModifier);
        }

        public void lavaandiceBackBtn_Click()
        {
            ((Pax4SoundLavaAndIce)Pax4Sound._current)._lavaandiceButtonAccepted.Play();
            Pax4Ui._current.Enter("chooseQuest");
        }

        public override void Enter()
        {
            Pax4ButtonLavaAndIceMission.UpdateScore();

            base.Enter();
        }
    }
}