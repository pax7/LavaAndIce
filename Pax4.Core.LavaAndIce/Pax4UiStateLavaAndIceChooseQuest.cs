using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Runtime.Serialization;

namespace Pax4.Core
{
    [DataContract]
    [KnownType(typeof(Pax4UiStateLavaAndIceChooseQuest))]
    public class Pax4UiStateLavaAndIceChooseQuest : Pax4UiState
    {
        public static String _questName = null;

        public Pax4UiStateLavaAndIceChooseQuest(String p_name, Pax4Ui p_ui)
            : base(p_name, p_ui)
        {
            Pax4UiLavaAndIceQuestScore score = new Pax4UiLavaAndIceQuestScore();
            
            float duration = 0.5f;
            float delay = 0.0f;
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

            duration = Pax4Ui._btnDuration;

            Pax4SpritePositionModifier positionModifierEnter = null;
            Pax4SpritePositionModifier positionModifierExit = null;
            Pax4SpriteTextModifier textModifier = null;
            
            Vector2 position0 = Vector2.Zero;
            Vector2 position1 = Vector2.Zero;

            String textureName = null;
            Texture2D texture = null;
            Texture2D textureOver = null;
            //SpriteFont spriteFont = null;
            Pax4Sprite sprite = null;

            String questName = null;
           
            textureName = "Sprite/lavaandiceMainBg";
            texture = Pax4Texture2D._current.Get(textureName);
            sprite = new Pax4SpriteTexture("lavaandiceMainBg", null);
            ((Pax4SpriteTexture)sprite).SetTexture(texture);
            position.X = .5f;
            position.Y = .5f;
            sprite.SetPosition(position);            
            AddChild(sprite);
            duration = 0.5f;
            colorModifierEnter.AddChild(sprite);
            colorModifierExit.AddChild(sprite);
            alphaModifierEnter.AddChild(sprite);
            alphaModifierExit.AddChild(sprite);



            delay = 0.3f;
            position.X = 0.5f;
            position.Y = 0.5f;
            sprite = new Pax4SpriteText("lavaandiceChooseQuestTitle", sprite);
            AddChild(sprite);
            ((Pax4SpriteText)sprite).SetSpriteFont("SpriteFont/Livingstone");
            ((Pax4SpriteText)sprite).SetPosition(position);
            textModifier = new Pax4SpriteTextModifier("", null);
            textModifier.AddChild(sprite);

            textModifier.Ini("Choose Quest", duration, delay);
            AddStateEnterModifier(textModifier);
            duration = 0.5f;
            colorModifierEnter.AddChild(sprite);
            colorModifierExit.AddChild(sprite);
            alphaModifierEnter.AddChild(sprite);
            alphaModifierExit.AddChild(sprite);

            //ENd of Moved Up


            //**************************************************
            //create
            //**************************************************
            textureName = "Sprite/lavaandiceQuestBtn";
            texture = Pax4Texture2D._current.Get(textureName);
            textureName = "Sprite/lavaandiceQuestBtnOver";
            textureOver = Pax4Texture2D._current.Get(textureName);

            position.X = .5f;
            position.Y = .75f;
            Pax4Slider slider = new Pax4Slider("sldQuest", null);
            //slider.SetPosition(position, Pax4Camera._backBufferWidth, texture.Height);
            slider.SetRectangleWidthHeight(0, texture.Height);
            slider.SetViewingThreshold(0,Pax4Game._graphicsDeviceManager.PreferredBackBufferWidth,0,Pax4Game._graphicsDeviceManager.PreferredBackBufferHeight);
            slider.SetPosition(position);//, 100f, texture.Height/Pax4Game._graphicsDeviceManager.PreferredBackBufferHeight);
            slider._verticalScroll = false;
            slider._skipRectangleDraw = true;
            //slider._horizontalSpacing = 30.0f;
            //slider.SetSnapPosition(position);
            //slider._leftTouchThreshold = 0;

            AddChild(slider);

            //duration = 0.5f;
            //delay = 0.0f;
            //position0.X = 1091.0f;
            //position0.Y = position.Y;
            //position0 *= Pax4Camera._current._scale2;
            //position *= Pax4Camera._current._scale2;
            //positionModifierEnter = new Pax4SpritePositionModifier(slider);
            //positionModifierEnter.Ini(position0, position, duration, delay);
            //AddStateEnterModifier(positionModifierEnter);
            //positionModifierExit = new Pax4SpritePositionModifier(slider);
            //positionModifierExit.Ini(position, position0, duration, delay);
            //AddStateExitModifier(positionModifierExit);

            //colorModifier = new Pax4SpriteColorModifier(slider);
            //colorModifier.Ini(Color.Black, Color.White, duration/2.0f);
            //AddStateEnterModifier(colorModifier);
            //colorModifier = new Pax4SpriteColorModifier(slider);
            //colorModifier.Ini(Color.White, Color.Black, duration/2.0f);
            //AddStateExitModifier(colorModifier);

            #region quests
            questName = "Prologue";
            //position.X = (float)Pax4Camera._backBufferWidth / 2.0f - (float)texture.Width / 2.0f;
            //position.Y = 330.0f;    
            sprite = new Pax4ButtonLavaAndIceQuest("lavaandicePrologueBtn", slider, questName);
            position.X = -.17f;// 64.0f;//108.0f delta 44
            position.Y = -.1f;// 200.0f;//255.0f delta 55
            ((Pax4Button)sprite).SetText(questName, position);
            ((Pax4Button)sprite).SetTexture(texture);
            sprite.SetRectangleWidthHeight(texture.Width, texture.Height);
            ((Pax4Button)sprite).SetTextureOver(textureOver);
            ((Pax4Button)sprite).SetOnClick(this.lavaandicePrologueBtn_Click);
            slider.AddChild(sprite);
            colorModifierEnter.AddChild(sprite);
            colorModifierExit.AddChild(sprite);
            alphaModifierEnter.AddChild(sprite);
            alphaModifierExit.AddChild(sprite);

            //alphaModifier = new Pax4SpriteAlphaModifier(sprite);
            //alphaModifier.Ini(0.0f, 1.0f, 3.0f);
            //AddStateEnterModifier(alphaModifier);
            //alphaModifier.SetOscillating();

            questName = "Equilibrium";
            sprite = new Pax4ButtonLavaAndIceQuest("lavaandiceEquilibriumBtn", slider, questName);
            //position.X = 10f;//50.0f;
            //position.Y = 0f;// 200.0f;
            ((Pax4Button)sprite).SetText(questName, position);
            ((Pax4Button)sprite).SetTexture(texture);
            ((Pax4Button)sprite).SetTextureOver(textureOver);
            slider.AddChild(sprite);
            ((Pax4ButtonLavaAndIceQuest)sprite).SetComingSoon();
            if (!((Pax4ButtonLavaAndIceQuest)sprite)._comingSoon)
                ((Pax4Button)sprite).SetOnClick(this.lavaandiceEquilibriumBtn_Click);
            colorModifierEnter.AddChild(sprite);
            colorModifierExit.AddChild(sprite);
            alphaModifierEnter.AddChild(sprite);
            alphaModifierExit.AddChild(sprite);

            questName = "LavaGrail";
            sprite = new Pax4ButtonLavaAndIceQuest("lavaandiceLavaGrailBtn", slider, questName);
            //position.X = 68.0f;
            //position.Y = 200.0f;
            ((Pax4Button)sprite).SetText("Lava Grail", position);
            ((Pax4Button)sprite).SetTexture(texture);
            ((Pax4Button)sprite).SetTextureOver(textureOver);
            slider.AddChild(sprite);
            ((Pax4ButtonLavaAndIceQuest)sprite).SetComingSoon();
            if (!((Pax4ButtonLavaAndIceQuest)sprite)._comingSoon)
                ((Pax4Button)sprite).SetOnClick(this.lavaandiceLavaGrailBtn_Click);
            colorModifierEnter.AddChild(sprite);
            colorModifierExit.AddChild(sprite);
            alphaModifierEnter.AddChild(sprite);
            alphaModifierExit.AddChild(sprite);

            //questName = "IceGrail";
            //sprite = new Pax4ButtonLavaAndIceQuest("lavaandiceIceGrailBtn", slider, questName);
            //position.X = 78.0f;
            //position.Y = 200.0f;
            //((Pax4Button)sprite).SetText("Ice Grail", position);
            //((Pax4Button)sprite).SetTexture(texture);
            //((Pax4Button)sprite).SetTextureOver(textureOver);
            //slider.AddChild(sprite);
            //((Pax4ButtonLavaAndIceQuest)sprite).SetComingSoon();
            //if (!((Pax4ButtonLavaAndIceQuest)sprite)._comingSoon)
            //    ((Pax4Button)sprite).SetOnClick(this.lavaandiceIceGrailBtn_Click);
            //colorModifierEnter.AddChild(sprite);
            //colorModifierExit.AddChild(sprite);
            //alphaModifierEnter.AddChild(sprite);
            //alphaModifierExit.AddChild(sprite);

            //questName = "Dragons";
            //sprite = new Pax4ButtonLavaAndIceQuest("lavaandiceDragonsBtn", slider, questName);
            //position.X = 70.0f;
            //position.Y = 200.0f;
            //((Pax4Button)sprite).SetText("Dragons", position);
            //((Pax4Button)sprite).SetTexture(texture);
            //((Pax4Button)sprite).SetTextureOver(textureOver);
            //slider.AddChild(sprite);
            //((Pax4ButtonLavaAndIceQuest)sprite).SetComingSoon();
            //if (!((Pax4ButtonLavaAndIceQuest)sprite)._comingSoon)
            //    ((Pax4Button)sprite).SetOnClick(this.lavaandiceDragonsBtn_Click);
            //colorModifierEnter.AddChild(sprite);
            //colorModifierExit.AddChild(sprite);
            //alphaModifierEnter.AddChild(sprite);
            //alphaModifierExit.AddChild(sprite);



            slider.AddChild(null);//no mas buttons, wrap shit up



            #endregion //quests

            //Moved Stuff up

            //misc
            //sprite = new Pax4Button("lavaandiceSettingsBtn", null);
            //AddSprite(sprite);
            //textureName = "Sprite/lavaandiceSettingsBtn";
            //texture = Pax4Texture2D._current.Get(textureName);
            //((Pax4Button)sprite).SetTexture(texture);
            //textureName = "Sprite/lavaandiceSettingsBtnOver";
            //texture = Pax4Texture2D._current.Get(textureName);
            //((Pax4Button)sprite).SetTextureOver(texture);
            ////position.X = 14.0f;
            ////position.Y = 20.0f;
            ////((Pax4Button)sprite).SetTextSpriteFont("SpriteFont/Livingstone", position, "Settings", 0.8f);
            //position.X = 28.0f;
            //position.Y = 7.0f;
            //((Pax4Button)sprite).SetTextSpriteFont("SpriteFont/Livingstone", position, "Instru\nctions", 0.7f);
            //((Pax4Button)sprite).SetOnClick(this.lavaandiceSettingsBtn_Click);

            //position.X = 160.0f;
            //position.Y = 820.0f;

            //duration = 0.5f;
            //delay = 0.0f;
            //position0.X = position.X;
            //position0.Y = 1920.0f;
            //sprite.SetPosition(position0);
            //position *= Pax4Camera._current._scale2;
            //position0 *= Pax4Camera._current._scale2;
            //positionModifierEnter = new Pax4SpritePositionModifier(sprite);
            //positionModifierEnter.Ini(position0, position, duration, delay);
            //AddStateEnterModifier(positionModifierEnter);

            //positionModifierExit = new Pax4SpritePositionModifier(sprite);
            //positionModifierExit.Ini(position, position0, duration);
            //AddStateExitModifier(positionModifierExit);

            //colorModifierEnter.AddChild(sprite);
            //colorModifierExit.AddChild(sprite);
            //alphaModifierEnter.AddChild(sprite);
            //alphaModifierExit.AddChild(sprite);
        }

        private void lavaandiceSettingsBtn_Click()
        {
            ((Pax4SoundLavaAndIce)Pax4Sound._current)._lavaandiceButtonAccepted.Play();
            Pax4Ui._current.Enter("instructions");
        }

        private void lavaandiceExitBtn_Click()
        {
            Pax4Game._current.Exit();
        }

        private void lavaandicePrologueBtn_Click()
        {
            //((Pax4SoundLavaAndIce)Pax4Sound._current)._lavaandiceButtonAccepted.Play();
            Pax4WorldLavaAndIce._questType = Pax4WorldLavaAndIce.ELavaAndIceQuestType._LAVA_AND_ICE_PROLOGUE;
            Pax4UiStateLavaAndIceChooseQuest._questName = "Prologue";
            Pax4Ui._current.Enter(Pax4UiStateLavaAndIceChooseQuest._questName);
        }

        private void lavaandiceEquilibriumBtn_Click()
        {
            ((Pax4SoundLavaAndIce)Pax4Sound._current)._lavaandiceButtonDenied.Play();
            Pax4WorldLavaAndIce._questType = Pax4WorldLavaAndIce.ELavaAndIceQuestType._LAVA_AND_ICE_EQUILIBRIUM;
            Pax4UiStateLavaAndIceChooseQuest._questName = "Equilibrium";
            Pax4Ui._current.Enter(Pax4UiStateLavaAndIceChooseQuest._questName);
        }

        private void lavaandiceLavaGrailBtn_Click()
        {
            ((Pax4SoundLavaAndIce)Pax4Sound._current)._lavaandiceButtonDenied.Play();
            Pax4WorldLavaAndIce._questType = Pax4WorldLavaAndIce.ELavaAndIceQuestType._LAVA_AND_ICE_LAVA_GRAIL;
            Pax4UiStateLavaAndIceChooseQuest._questName = "LavaGrail";
            Pax4Ui._current.Enter(Pax4UiStateLavaAndIceChooseQuest._questName);
        }

        private void lavaandiceIceGrailBtn_Click()
        {
            ((Pax4SoundLavaAndIce)Pax4Sound._current)._lavaandiceButtonDenied.Play();
            Pax4WorldLavaAndIce._questType = Pax4WorldLavaAndIce.ELavaAndIceQuestType._LAVA_AND_ICE_ICE_GRAIL;
            Pax4UiStateLavaAndIceChooseQuest._questName = "IceGrail";
            Pax4Ui._current.Enter(Pax4UiStateLavaAndIceChooseQuest._questName);
        }

        private void lavaandiceDragonsBtn_Click()
        {
            ((Pax4SoundLavaAndIce)Pax4Sound._current)._lavaandiceButtonDenied.Play();
            Pax4WorldLavaAndIce._questType = Pax4WorldLavaAndIce.ELavaAndIceQuestType._LAVA_AND_ICE_DRAGONS;
            Pax4UiStateLavaAndIceChooseQuest._questName = "Dragons";
            Pax4Ui._current.Enter(Pax4UiStateLavaAndIceChooseQuest._questName);
        }

        public override void Enter()
        {
            Pax4Sound._current.PlayRandomSong();

            Pax4ButtonLavaAndIceQuest.UpdateScore();

            base.Enter();
        }
    }
}