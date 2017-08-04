using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Runtime.Serialization;

namespace Pax4.Core
{
    [DataContract]
    [KnownType(typeof(Pax4UiStateLavaAndIceMission))]
    public class Pax4UiStateLavaAndIceMission : Pax4UiState
    {
        public static Pax4UiStateLavaAndIceMission _currentMissionState = null;

        public Pax4Sprite _highScoreSprite = null;
        public Pax4Sprite _scoreSprite = null;
        public Pax4SpriteTextNumberModifier _scoreSpriteModifier = null;
        public Pax4Sprite _currentMedalSprite = null;

        public Dictionary<String, Pax4Sprite> _medalSprite = null;

        public Pax4ToggleButton _lavaLauncher = null;
        public Pax4ToggleButton _iceLauncher = null;

        public Pax4UiLavaAndIceMissionTimer _missionTimer = null;

        public Pax4UiStateLavaAndIceMission(String p_name, Pax4Ui p_ui, Pax4WorldLavaAndIce.ELavaAndIceMissionType p_missionType = Pax4WorldLavaAndIce.ELavaAndIceMissionType._LAVA_AND_ICE)
            : base(p_name, p_ui)
        {
            //_blendState = BlendState.AlphaBlend;

            String textureName = null;
            Texture2D texture = null;
            Pax4Sprite sprite = null;
            Pax4Sprite scoreSprite = null;
            Vector2 position;

            float duration = 0.3f;
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

            //Pax4SpriteColorModifier colorModifier = null;
            //Pax4SpriteAlphaModifier alphaModifier = null;
            Pax4SpritePositionModifier positionModifier = null;
            //Pax4SpriteTextModifier textModifier = null;
            Pax4SpriteTextNumberModifier textNumberModifier = null;

            position.X = 0.1f;
            position.Y = 0.1f;
            textureName = "Sprite/lavaandiceInGameFgScore";
            texture = Pax4Texture2D._current.Get(textureName);
            sprite = new Pax4SpriteTexture("top", null);
            ((Pax4SpriteTexture)sprite).SetTexture(texture);
            sprite.SetPosition(position);
            AddChild(sprite);
            scoreSprite = sprite;
            colorModifierEnter.AddChild(sprite);
            colorModifierExit.AddChild(sprite);
            alphaModifierEnter.AddChild(sprite);
            alphaModifierExit.AddChild(sprite);

            textureName = "Sprite/lavaandiceInGameFgBottom";
            texture = Pax4Texture2D._current.Get(textureName);
            sprite = new Pax4SpriteTexture("bottom", null);
            ((Pax4SpriteTexture)sprite).SetTexture(texture);
            position.X = 0.1f;
            position.Y = 0.2f;
            sprite.SetPosition(position);
            AddChild(sprite);
            colorModifierEnter.AddChild(sprite);
            colorModifierExit.AddChild(sprite);
            alphaModifierEnter.AddChild(sprite);
            alphaModifierExit.AddChild(sprite);

            position.Y = 0.3f;
            position.X = 0.4f;

            if (p_missionType == Pax4WorldLavaAndIce.ELavaAndIceMissionType._LAVA || p_missionType == Pax4WorldLavaAndIce.ELavaAndIceMissionType._LAVA_AND_ICE)
            {
                textureName = "Sprite/lavaandiceInGameFgLavaLauncher";
                texture = Pax4Texture2D._current.Get(textureName);
                sprite = new Pax4ToggleButton("lavaLauncher", null);
                ((Pax4SpriteTexture)sprite).SetTexture(texture);

                textureName = "Sprite/lavaandiceInGameFgLavaLauncherOver";
                texture = Pax4Texture2D._current.Get(textureName);
                ((Pax4ToggleButton)sprite).SetTextureOver(texture);

                ((Pax4ToggleButton)sprite).SetOnClick(lavaandiceLavaLauncher_Click);
                ((Pax4ToggleButton)sprite).ToggleEnabled(false);

                _lavaLauncher = (Pax4ToggleButton)sprite;
                
                //if (Pax4Camera._backBufferWidth > 480)
                //{
                //    if (p_missionType == Pax4WorldLavaAndIce.ELavaAndIceMissionType._LAVA_AND_ICE)
                //        position.X = 127.0f;
                //}
                //else if (Pax4Camera._backBufferWidth == 480)
                //{
                //    if (p_missionType == Pax4WorldLavaAndIce.ELavaAndIceMissionType._LAVA_AND_ICE)
                //        position.X = 130.0f;
                //}
                //else if (Pax4Camera._backBufferWidth <= 320)
                //{
                //    if (p_missionType == Pax4WorldLavaAndIce.ELavaAndIceMissionType._LAVA_AND_ICE)
                //        position.X = 137.0f;
                //}
                sprite.SetPosition(position);
                AddChild(sprite);
                colorModifierEnter.AddChild(sprite);
                colorModifierExit.AddChild(sprite);
                alphaModifierEnter.AddChild(sprite);
                alphaModifierExit.AddChild(sprite);
            }

            if (p_missionType == Pax4WorldLavaAndIce.ELavaAndIceMissionType._ICE || p_missionType == Pax4WorldLavaAndIce.ELavaAndIceMissionType._LAVA_AND_ICE)
            {
                textureName = "Sprite/lavaandiceInGameFgIceLauncher";
                texture = Pax4Texture2D._current.Get(textureName);
                sprite = new Pax4ToggleButton("iceLauncher", null);
                ((Pax4SpriteTexture)sprite).SetTexture(texture);

                textureName = "Sprite/lavaandiceInGameFgIceLauncherOver";
                texture = Pax4Texture2D._current.Get(textureName);
                ((Pax4ToggleButton)sprite).SetTextureOver(texture);

                ((Pax4ToggleButton)sprite).SetOnClick(lavaandiceIceLauncher_Click);
                ((Pax4ToggleButton)sprite).ToggleEnabled(false);

                _iceLauncher = (Pax4ToggleButton)sprite;

                //position.X = 0.4;

                //if (Pax4Camera._backBufferWidth > 480)
                //{
                //    if (p_missionType == Pax4WorldLavaAndIce.ELavaAndIceMissionType._LAVA_AND_ICE)
                //        position.X = 263.0f;
                //}
                //else if (Pax4Camera._backBufferWidth == 480)
                //{
                //    if (p_missionType == Pax4WorldLavaAndIce.ELavaAndIceMissionType._LAVA_AND_ICE)
                //        position.X = 260.0f;
                //}
                //else if (Pax4Camera._backBufferWidth <= 320)
                //{
                //    if (p_missionType == Pax4WorldLavaAndIce.ELavaAndIceMissionType._LAVA_AND_ICE)
                //        position.X = 253.0f;
                //}
                sprite.SetPosition(position);
                AddChild(sprite);
                colorModifierEnter.AddChild(sprite);
                colorModifierExit.AddChild(sprite);
                alphaModifierEnter.AddChild(sprite);
                alphaModifierExit.AddChild(sprite);
            }

            textureName = "Sprite/lavaandiceInGameMenuBtn";
            texture = Pax4Texture2D._current.Get(textureName);
            sprite = new Pax4Button("menu", null);
            ((Pax4Button)sprite).SetTexture(texture);
            position.X = 0.7f;
            position.Y = 0.1f;
            sprite.SetPosition(position);
            textureName = "Sprite/lavaandiceInGameMenuBtnOver";
            texture = Pax4Texture2D._current.Get(textureName);
            ((Pax4Button)sprite).SetTextureOver(texture);
            ((Pax4Button)sprite).SetOnClick(this.lavaandiceMenuBtn_Click);
            AddChild(sprite);
            colorModifierEnter.AddChild(sprite);
            colorModifierExit.AddChild(sprite);
            alphaModifierEnter.AddChild(sprite);
            alphaModifierExit.AddChild(sprite);

            textureName = "Sprite/lavaandiceInGamePauseBtn";
            texture = Pax4Texture2D._current.Get(textureName);
            sprite = new Pax4ToggleButton("pause", null);
            ((Pax4Button)sprite).SetTexture(texture);
            position.X = 0.3f;
            position.Y = 0.3f;
            sprite.SetPosition(position);
            textureName = "Sprite/lavaandiceInGamePauseBtnOver";
            texture = Pax4Texture2D._current.Get(textureName);
            ((Pax4Button)sprite).SetTextureOver(texture);
            ((Pax4Button)sprite).SetOnClick(this.lavaandicePauseBtn_Click);
            AddChild(sprite);
            colorModifierEnter.AddChild(sprite);
            colorModifierExit.AddChild(sprite);
            alphaModifierEnter.AddChild(sprite);
            alphaModifierExit.AddChild(sprite);

            //position.X = -20.0f;
            //position.Y = 45.0f;
            position.X = 64.0f;
            position.Y = 47.0f;
            sprite = new Pax4UiLavaAndIceMissionThermometer("thermometer", null);
            sprite.SetPosition(position);
            AddChild(sprite);

            position.X = 0.0f;
            position.Y = 0.0f;
            sprite = new Pax4UiLavaAndIceMissionHealth("health", null);
            sprite.SetPosition(position);
            AddChild(sprite);

            position.X = 8.0f;
            position.Y = 52.0f;
            sprite = new Pax4UiLavaAndIceMissionTimer("timer", null);
            sprite.SetPosition(position);
            AddChild(sprite);
            _missionTimer = (Pax4UiLavaAndIceMissionTimer)sprite;

            Vector2 position1 = new Vector2(-150, 52);
            position1 *= Pax4Camera._current._scale2;

            position *= Pax4Camera._current._scale2;
            positionModifier = new Pax4SpritePositionModifier("", null);
            positionModifier.Ini(position1, position, 0.2f);
            positionModifier.AddChild(sprite);
            AddStateEnterModifier(positionModifier);

            position *= Pax4Camera._current._scale2;
            positionModifier = new Pax4SpritePositionModifier("", null);
            positionModifier.AddChild(sprite);
            positionModifier.Ini(position, position1, 0.2f);
            AddStateExitModifier(positionModifier);

            //**************************************************
            //create text sprites
            //**************************************************
            float xpos = 0.0f;
            //float xstep = 0.0f;
            //float xoff = 0.0f;

            float ypos = 0.0f;
            float ystep = 0.0f;
            //float yoff = 0.0f;

            float scale;

            //Score
            xpos = 91.0f;
            ypos = 4.0f;
            ystep = 22.0f;
            sprite = new Pax4SpriteText("score", scoreSprite);
            position.X = xpos;
            position.Y = ypos; ypos += ystep;
            scale = 0.40f;
            sprite.SetPosition(position);
            sprite.SetColor(Color.White);            
            ((Pax4SpriteText)sprite).SetSpriteFont("SpriteFont/ArialBold");
            ((Pax4SpriteText)sprite).SetScale(scale);
            ((Pax4SpriteText)sprite).SetText("Score:");
            AddChild(sprite);
            colorModifierEnter.AddChild(sprite);
            colorModifierExit.AddChild(sprite);
            alphaModifierEnter.AddChild(sprite);
            alphaModifierExit.AddChild(sprite);

            //Score value
            xpos = 145.0f;
            ypos = 4.0f;
            sprite = new Pax4SpriteText("scoreValue", scoreSprite);
            position.X = xpos;
            position.Y = ypos; ypos += ystep;
            scale = 0.45f;
            sprite.SetPosition(position);
            sprite.SetColor(Color.GreenYellow);
            ((Pax4SpriteText)sprite).SetSpriteFont("SpriteFont/ArialBold");
            ((Pax4SpriteText)sprite).SetScale(scale);
            ((Pax4SpriteText)sprite).SetText("0");
            AddChild(sprite);
            _scoreSprite = sprite;
            colorModifierEnter.AddChild(sprite);
            colorModifierExit.AddChild(sprite);
            alphaModifierEnter.AddChild(sprite);
            alphaModifierExit.AddChild(sprite);

            textNumberModifier = new Pax4SpriteTextNumberModifier("", null);
            textNumberModifier.AddChild(sprite);
            AddStateEnterModifier(textNumberModifier);
            _scoreSpriteModifier = textNumberModifier;

            //High Score
            xpos = 81.0f;
            sprite = new Pax4SpriteText("highScore", scoreSprite);
            position.X = xpos;
            position.Y = ypos;
            scale = 0.35f;
            sprite.SetPosition(position);
            sprite.SetColor(Color.White);
            ((Pax4SpriteText)sprite).SetSpriteFont("SpriteFont/ArialBold");
            ((Pax4SpriteText)sprite).SetScale(scale);
            ((Pax4SpriteText)sprite).SetText("High Score:");
            AddChild(sprite);
            colorModifierEnter.AddChild(sprite);
            colorModifierExit.AddChild(sprite);
            alphaModifierEnter.AddChild(sprite);
            alphaModifierExit.AddChild(sprite);

            //High Score value
            xpos = 161.0f;
            ypos = ypos - 1.0f;
            sprite = new Pax4SpriteText("highScoreValue", scoreSprite);
            position.X = xpos;
            position.Y = ypos;
            scale = 0.40f;
            sprite.SetPosition(position);
            sprite.SetColor(Color.GreenYellow);
            ((Pax4SpriteText)sprite).SetSpriteFont("SpriteFont/ArialBold");
            ((Pax4SpriteText)sprite).SetScale(scale);
            ((Pax4SpriteText)sprite).SetText("0");
            AddChild(sprite);
            _highScoreSprite = sprite;
            colorModifierEnter.AddChild(sprite);
            colorModifierExit.AddChild(sprite);
            alphaModifierEnter.AddChild(sprite);
            alphaModifierExit.AddChild(sprite);

            //**************************************************
            //create medal sprites
            //**************************************************
            if (_medalSprite == null)
                _medalSprite = new Dictionary<String, Pax4Sprite>();
            else
                return;

            Dictionary<String, String> _medal = new Dictionary<String, String>();

            _medal.Add("nightmareOn", "Sprite/lavaandiceNightmareOn");
            //_medal.Add("nightmareOff", "Sprite/lavaandiceNightmareOff");
            _medal.Add("hardOn", "Sprite/lavaandiceHardOn");
            //_medal.Add("hardOff", "Sprite/lavaandiceHardOff");
            _medal.Add("normalOn", "Sprite/lavaandiceNormalOn");
            //_medal.Add("normalOff", "Sprite/lavaandiceNormalOff");
            _medal.Add("easyOn", "Sprite/lavaandiceEasyOn");
            //_medal.Add("easyOff", "Sprite/lavaandiceEasyOff");

            xpos = 251.0f;
            ypos = 2.0f;
            foreach (KeyValuePair<String, String> kvp in _medal)
            {
                textureName = kvp.Value;
                texture = Pax4Texture2D._current.Get(textureName);
                sprite = new Pax4SpriteTexture(kvp.Key, scoreSprite);
                ((Pax4SpriteTexture)sprite).SetTexture(texture);
                sprite.SetPosition(new Vector2(xpos, ypos));
                _medalSprite.Add(sprite._name, sprite);
                colorModifierEnter.AddChild(sprite);
                colorModifierExit.AddChild(sprite);
                alphaModifierEnter.AddChild(sprite);
                alphaModifierExit.AddChild(sprite);
            }

            _currentMedalSprite = sprite;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            _currentMedalSprite.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);

            _currentMedalSprite.Draw(gameTime);
        }

        public override void Enter()
        {
            Pax4ParticleEffect._current.Enable();

            _currentMissionState = this;

            _scoreSpriteModifier.Ini(0.0f, 0.0f, true, 0.0f);

            if (Pax4WorldLavaAndIce._missionIndex > 0)
            {
                String highScoreName = Pax4UiStateLavaAndIceChooseQuest._questName + "_" + Pax4WorldLavaAndIce._missionIndex + "_HighScore";
                ((Pax4SpriteText)_highScoreSprite).SetText(Pax4Tools.NumberCommaFormat(Pax4UiLavaAndIceQuestScore._score[highScoreName]));
            }

            base.Enter();
        }

        public override void Exit()
        {
            base.Exit();

            Pax4ParticleEffect._current.Disable();
        }

        private void lavaandicePauseBtn_Click()
        {
            ((Pax4SoundLavaAndIce)Pax4Sound._current)._lavaandiceButtonAccepted.Play();
            if (Pax4Game._pause)
                Pax4Game._pause = false;
            else
                Pax4Game._pause = true;
        }

        private void lavaandiceMenuBtn_Click()
        {
            Pax4Game._pause = true;

            ((Pax4SoundLavaAndIce)Pax4Sound._current)._lavaandiceButtonAccepted.Play();

            Pax4Ui._current.Enter("menu");
        }

        public void UpdateMedalSprite(bool p_on = true)
        {
            if (Pax4WorldLavaAndIce._difficulty == Pax4WorldLavaAndIce._difficultyNightmare)
            {
                if (p_on)
                    _currentMedalSprite = _medalSprite["nightmareOn"];
                else
                    _currentMedalSprite = _medalSprite["nightmareOff"];
            }
            else if (Pax4WorldLavaAndIce._difficulty == Pax4WorldLavaAndIce._difficultyHard)
            {
                if (p_on)
                    _currentMedalSprite = _medalSprite["hardOn"];
                else
                    _currentMedalSprite = _medalSprite["hardOff"];
            }
            else if (Pax4WorldLavaAndIce._difficulty == Pax4WorldLavaAndIce._difficultyNormal)
            {
                if (p_on)
                    _currentMedalSprite = _medalSprite["normalOn"];
                else
                    _currentMedalSprite = _medalSprite["normalOff"];
            }
            else if (Pax4WorldLavaAndIce._difficulty == Pax4WorldLavaAndIce._difficultyEasy)
            {
                if (p_on)
                    _currentMedalSprite = _medalSprite["easyOn"];
                else
                    _currentMedalSprite = _medalSprite["easyOff"];
            }
        }

        private void lavaandiceLavaLauncher_Click()
        {
            ((Pax4SoundLavaAndIce)Pax4Sound._current)._lavaandiceButtonAccepted.Play();
            if (_iceLauncher != null)
                _iceLauncher._toggle = false;

            _lavaLauncher._toggle = true;
        }

        private void lavaandiceIceLauncher_Click()
        {
            ((Pax4SoundLavaAndIce)Pax4Sound._current)._lavaandiceButtonAccepted.Play();
            if (_lavaLauncher != null)
                _lavaLauncher._toggle = false;

            _iceLauncher._toggle = true;
        }
    }
}