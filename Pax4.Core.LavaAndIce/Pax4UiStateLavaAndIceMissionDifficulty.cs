using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Runtime.Serialization;

namespace Pax4.Core
{
    [DataContract]
    [KnownType(typeof(Pax4UiStateLavaAndIceMissionDifficulty))]
    public class Pax4UiStateLavaAndIceMissionDifficulty : Pax4UiState
    {
        public Pax4UiStateLavaAndIceMissionDifficulty(String p_name, Pax4Ui p_ui)
            : base(p_name, p_ui)
        {
            Vector2 position;
            float duration = 0.5f;
            //float delay = 0.0f;

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
            position.X = 0.5f;
            position.Y = 0.5f;
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

            position.X = 0.5f;
            position.Y = 0.2f;
            sprite = new Pax4SpriteTexture("lavaandiceDifficultyTitle", null);
            AddChild(sprite);
            textureName = "Sprite/lavaandiceInGameMenuTextDifficulty";
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

            sprite = new Pax4Button("lavaandiceDifficultyNightmare", null);
            AddChild(sprite);
            textureName = "Sprite/lavaandiceNightmareBtn";
            texture = Pax4Texture2D._current.Get(textureName);
            ((Pax4Button)sprite).SetTexture(texture);
            textureName = "Sprite/lavaandiceNightmareBtnOver";
            texture = Pax4Texture2D._current.Get(textureName);
            ((Pax4Button)sprite).SetTextureOver(texture);
            ((Pax4Button)sprite).SetOnClick(this.lavaandiceNightmareBtn_Click);
            position.X = 0.5f;
            position.Y = 0.3f;
            sprite.SetPosition(position);
            colorModifierEnter.AddChild(sprite);
            colorModifierExit.AddChild(sprite);
            alphaModifierEnter.AddChild(sprite);
            alphaModifierExit.AddChild(sprite);

            sprite = new Pax4Button("lavaandiceDifficultyHard", null);
            AddChild(sprite);
            textureName = "Sprite/lavaandiceHardBtn";
            texture = Pax4Texture2D._current.Get(textureName);
            ((Pax4Button)sprite).SetTexture(texture);
            textureName = "Sprite/lavaandiceHardBtnOver";
            texture = Pax4Texture2D._current.Get(textureName);
            ((Pax4Button)sprite).SetTextureOver(texture);
            ((Pax4Button)sprite).SetOnClick(this.lavaandiceHardBtn_Click);
            position.X = 0.5f;
            position.Y = 0.5f;
            sprite.SetPosition(position);
            colorModifierEnter.AddChild(sprite);
            colorModifierExit.AddChild(sprite);
            alphaModifierEnter.AddChild(sprite);
            alphaModifierExit.AddChild(sprite);

            sprite = new Pax4Button("lavaandiceDifficultyNormal", null);
            AddChild(sprite);
            textureName = "Sprite/lavaandiceNormalBtn";
            texture = Pax4Texture2D._current.Get(textureName);
            ((Pax4Button)sprite).SetTexture(texture);
            textureName = "Sprite/lavaandiceNormalBtnOver";
            texture = Pax4Texture2D._current.Get(textureName);
            ((Pax4Button)sprite).SetTextureOver(texture);
            ((Pax4Button)sprite).SetOnClick(this.lavaandiceNormalBtn_Click);
            position.X = 0.5f;
            position.Y = 0.7f;
            sprite.SetPosition(position);
            colorModifierEnter.AddChild(sprite);
            colorModifierExit.AddChild(sprite);
            alphaModifierEnter.AddChild(sprite);
            alphaModifierExit.AddChild(sprite);

            sprite = new Pax4Button("lavaandiceDifficultyEasy", null);
            AddChild(sprite);
            textureName = "Sprite/lavaandiceEasyBtn";
            texture = Pax4Texture2D._current.Get(textureName);
            ((Pax4Button)sprite).SetTexture(texture);
            textureName = "Sprite/lavaandiceEasyBtnOver";
            texture = Pax4Texture2D._current.Get(textureName);
            ((Pax4Button)sprite).SetTextureOver(texture);
            ((Pax4Button)sprite).SetOnClick(this.lavaandiceEasyBtn_Click);
            position.X = 0.5f;
            position.Y = 0.9f;
            sprite.SetPosition(position);
            colorModifierEnter.AddChild(sprite);
            colorModifierExit.AddChild(sprite);
            alphaModifierEnter.AddChild(sprite);
            alphaModifierExit.AddChild(sprite);

            position.X = 0.1f;
            position.Y = 0.1f;
            //misc
            sprite = new Pax4Button("lavaandiceBackBtn", null);
            AddChild(sprite);
            textureName = "Sprite/lavaandiceBackBtn";
            texture = Pax4Texture2D._current.Get(textureName);
            ((Pax4Button)sprite).SetTexture(texture);
            textureName = "Sprite/lavaandiceBackBtnOver";
            texture = Pax4Texture2D._current.Get(textureName);
            ((Pax4Button)sprite).SetTextureOver(texture);
            ((Pax4Button)sprite).SetTextSpriteFont("SpriteFont/Livingstone", position, "Back");
            ((Pax4Button)sprite).SetOnClick(this.lavaandiceBackBtn_Click);
            //position.X = 160.0f;
            //position.Y = 820.0f;
            sprite.SetPosition(position);
            colorModifierEnter.AddChild(sprite);
            colorModifierExit.AddChild(sprite);
            alphaModifierEnter.AddChild(sprite);
            alphaModifierExit.AddChild(sprite);
        }

        private void lavaandiceBackBtn_Click()
        {
            ((Pax4SoundLavaAndIce)Pax4Sound._current)._lavaandiceButtonAccepted.Play();

            Pax4Ui._current.Enter(Pax4UiStateLavaAndIceChooseMission._currentMissionState);
        }

        private void lavaandiceNightmareBtn_Click()
        {
            ((Pax4SoundLavaAndIce)Pax4Sound._current)._lavaandiceButtonAccepted.Play();

            Pax4WorldLavaAndIce._difficulty = Pax4WorldLavaAndIce._difficultyNightmare;

            Pax4WorldLavaAndIce.CreateAndEnterQuest();

            Pax4Game._pause = false;

            Pax4UiStateLavaAndIceMission._currentMissionState.UpdateMedalSprite();
            Pax4UiStateLavaAndIceVictory.UpdateMedalSprite();
        }

        private void lavaandiceHardBtn_Click()
        {
            ((Pax4SoundLavaAndIce)Pax4Sound._current)._lavaandiceButtonAccepted.Play();

            Pax4WorldLavaAndIce._difficulty = Pax4WorldLavaAndIce._difficultyHard;

            Pax4WorldLavaAndIce.CreateAndEnterQuest();

            Pax4Game._pause = false;

            Pax4UiStateLavaAndIceMission._currentMissionState.UpdateMedalSprite();
            Pax4UiStateLavaAndIceVictory.UpdateMedalSprite();
        }

        private void lavaandiceNormalBtn_Click()
        {
            ((Pax4SoundLavaAndIce)Pax4Sound._current)._lavaandiceButtonAccepted.Play();

            Pax4WorldLavaAndIce._difficulty = Pax4WorldLavaAndIce._difficultyNormal;

            Pax4WorldLavaAndIce.CreateAndEnterQuest();

            Pax4Game._pause = false;

            Pax4UiStateLavaAndIceMission._currentMissionState.UpdateMedalSprite();
            Pax4UiStateLavaAndIceVictory.UpdateMedalSprite();
        }

        private void lavaandiceEasyBtn_Click()
        {
            ((Pax4SoundLavaAndIce)Pax4Sound._current)._lavaandiceButtonAccepted.Play();

            Pax4WorldLavaAndIce._difficulty = Pax4WorldLavaAndIce._difficultyEasy;

            Pax4WorldLavaAndIce.CreateAndEnterQuest();

            Pax4Game._pause = false;

            Pax4UiStateLavaAndIceMission._currentMissionState.UpdateMedalSprite();
            Pax4UiStateLavaAndIceVictory.UpdateMedalSprite();
        }
    }
}