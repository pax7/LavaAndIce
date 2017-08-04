using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Runtime.Serialization;

namespace Pax4.Core
{
    [DataContract]
    [KnownType(typeof(Pax4UiStateLavaAndIceMenu))]
    public class Pax4UiStateLavaAndIceMenu : Pax4UiState
    {
        public Pax4UiStateLavaAndIceMenu(String p_name, Pax4Ui p_ui)
            : base(p_name, p_ui)
        {
            float duration = 0.5f;
            //float delay = 0.0f;
            Vector2 position;
            Pax4SpriteColorModifier colorModifierEnter = new Pax4SpriteColorModifier("", null);
            colorModifierEnter.Ini(Color.Black, Color.White, duration);
            AddStateEnterModifier(colorModifierEnter);
            Pax4SpriteColorModifier colorModifierExit = new Pax4SpriteColorModifier("", null);
            colorModifierExit.Ini(Color.White, Color.Black, duration);
            AddStateExitModifier(colorModifierExit);

            Pax4SpriteAlphaModifier alphaModifierEnter = new Pax4SpriteAlphaModifier("", null);
            alphaModifierEnter.Ini(0.0f, 1.0f, duration);
            AddStateEnterModifier(alphaModifierEnter);
            Pax4SpriteAlphaModifier alphaModifierExit = new Pax4SpriteAlphaModifier("", null);
            alphaModifierExit.Ini(1.0f, 0.0f, duration);
            AddStateExitModifier(alphaModifierExit);

            //Pax4SpritePositionModifier positionModifierEnter = null;
            //Pax4SpritePositionModifier positionModifierExit = null;
            //Pax4SpriteTextModifier textModifier = null;
            //Pax4UiState state = null;
            //float duration = Pax4Ui._btnDuration;
            //float delay = 0.0f;
            Vector2 position0 = Vector2.Zero;
            Vector2 position1 = Vector2.Zero;

            String textureName = null;
            Texture2D texture = null;
            //SpriteFont spriteFont = null;
            Pax4Sprite sprite = null;

            //**************************************************
            //create
            //**************************************************

            //**************************************************
            //bg
            //**************************************************
            position.X = 95.0f;
            position.Y = 223.0f;
            //misc
            sprite = new Pax4SpriteTexture("lavaandiceMenuBg", null);
            AddChild(sprite);
            textureName = "Sprite/lavaandiceInGameMenuBg";
            texture = Pax4Texture2D._current.Get(textureName);
            ((Pax4SpriteTexture)sprite).SetTexture(texture);
            sprite.SetPosition(position);
            colorModifierEnter.AddChild(sprite);
            colorModifierExit.AddChild(sprite);
            alphaModifierEnter.AddChild(sprite);
            alphaModifierExit.AddChild(sprite);

            position.X = 170.0f;
            position.Y = 246.0f;
            sprite = new Pax4SpriteTexture("lavaandiceMenuTitle", null);
            AddChild(sprite);
            textureName = "Sprite/lavaandiceInGameMenuTextMenu";
            texture = Pax4Texture2D._current.Get(textureName);
            ((Pax4SpriteTexture)sprite).SetTexture(texture);
            sprite.SetPosition(position);
            colorModifierEnter.AddChild(sprite);
            colorModifierExit.AddChild(sprite);
            alphaModifierEnter.AddChild(sprite);
            alphaModifierExit.AddChild(sprite);
            //**************************************************
            //buttons
            //**************************************************

            sprite = new Pax4Button("lavaandiceMenuResume", null);
            AddChild(sprite);
            textureName = "Sprite/lavaandiceResumeBtn";
            texture = Pax4Texture2D._current.Get(textureName);
            ((Pax4Button)sprite).SetTexture(texture);
            textureName = "Sprite/lavaandiceResumeBtnOver";
            texture = Pax4Texture2D._current.Get(textureName);
            ((Pax4Button)sprite).SetTextureOver(texture);
            ((Pax4Button)sprite).SetOnClick(this.lavaandiceResumeBtn_Click);
            position.X = 126.0f;
            position.Y = 318.0f;
            sprite.SetPosition(position);
            colorModifierEnter.AddChild(sprite);
            colorModifierExit.AddChild(sprite);
            alphaModifierEnter.AddChild(sprite);
            alphaModifierExit.AddChild(sprite);

            sprite = new Pax4Button("lavaandiceMenuRetry", null);
            AddChild(sprite);
            textureName = "Sprite/lavaandiceRetryBtn";
            texture = Pax4Texture2D._current.Get(textureName);
            ((Pax4Button)sprite).SetTexture(texture);
            textureName = "Sprite/lavaandiceRetryBtnOver";
            texture = Pax4Texture2D._current.Get(textureName);
            ((Pax4Button)sprite).SetTextureOver(texture);
            ((Pax4Button)sprite).SetOnClick(this.lavaandiceRetryBtn_Click);
            position.X = 126.0f;
            position.Y = 446.0f;
            sprite.SetPosition(position);
            colorModifierEnter.AddChild(sprite);
            colorModifierExit.AddChild(sprite);
            alphaModifierEnter.AddChild(sprite);
            alphaModifierExit.AddChild(sprite);

            sprite = new Pax4Button("lavaandiceMenuExit", null);
            AddChild(sprite);
            textureName = "Sprite/lavaandiceExitBtn";
            texture = Pax4Texture2D._current.Get(textureName);
            ((Pax4Button)sprite).SetTexture(texture);
            textureName = "Sprite/lavaandiceExitBtnOver";
            texture = Pax4Texture2D._current.Get(textureName);
            ((Pax4Button)sprite).SetTextureOver(texture);
            ((Pax4Button)sprite).SetOnClick(this.lavaandiceExitBtn_Click);
            position.X = 126.0f;
            position.Y = 574.0f;
            sprite.SetPosition(position);
            colorModifierEnter.AddChild(sprite);
            colorModifierExit.AddChild(sprite);
            alphaModifierEnter.AddChild(sprite);
            alphaModifierExit.AddChild(sprite);
        }

        private void lavaandiceResumeBtn_Click()
        {
            ((Pax4SoundLavaAndIce)Pax4Sound._current)._lavaandiceButtonAccepted.Play();

            Pax4Ui._current.Enter(Pax4UiStateLavaAndIceMission._currentMissionState);

            Pax4Game._pause = false;
        }        

        private void lavaandiceRetryBtn_Click()
        {
            ((Pax4SoundLavaAndIce)Pax4Sound._current)._lavaandiceButtonAccepted.Play();

            Pax4World._current.Dx();

            Pax4WorldLavaAndIce.CreateAndEnterQuest();

            Pax4Game._pause = false;

            Pax4UiStateLavaAndIceMission._currentMissionState.UpdateMedalSprite();
            Pax4UiStateLavaAndIceVictory.UpdateMedalSprite();
        }

        private void lavaandiceExitBtn_Click()
        {
            ((Pax4SoundLavaAndIce)Pax4Sound._current)._lavaandiceButtonAccepted.Play();

            Pax4World._current.Dx();

            Pax4Ui._current.Enter(Pax4UiStateLavaAndIceChooseMission._currentMissionState);
        }    
    }
}