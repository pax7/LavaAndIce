using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Runtime.Serialization;

namespace Pax4.Core
{
    [DataContract]
    [KnownType(typeof(Pax4UiStateLavaAndIceVictory))]
    public class Pax4UiStateLavaAndIceVictory : Pax4UiState
    {
        public static Pax4Sprite _scoreSprite = null;
        public static Pax4Sprite _totalScoreSprite = null;
        public static Pax4Sprite _difficultySprite = null;

        public static Pax4Sprite _currentMedalSprite = null;
        public static Dictionary<String, Pax4Sprite> _medalSprite = null;

        public static Pax4SpriteTextNumberModifier _currentScoreModifier = null;
        public static Pax4SpriteTextNumberModifier _totalScoreModifier = null;

        public static Pax4SpriteColorModifier _medalColorModifier = null;
        public static Pax4SpriteAlphaModifier _medalAlphaModifier = null;


        public Pax4UiStateLavaAndIceVictory(String p_name, Pax4Ui p_ui)
            : base(p_name, p_ui)
        {
            Vector2 position;
            float duration = 0.5f;

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
            Pax4SpriteTextNumberModifier textNumberModifier = null;

            //float delay = 0.0f;
            Vector2 position0 = Vector2.Zero;
            Vector2 position1 = Vector2.Zero;

            String textureName = null;
            Texture2D texture = null;
            //SpriteFont spriteFont = null;
            Pax4Sprite sprite = null;

            //**************************************************
            //bg
            //**************************************************
            position.X = 63.0f;
            position.Y = 145.0f;
            //misc
            sprite = new Pax4SpriteTexture("lavaandiceMenuBg", null);
            AddChild(sprite);
            textureName = "Sprite/lavaandiceInGameVictoryBg";
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
            position.Y = 368.0f;
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
            position.Y = 496.0f;
            sprite.SetPosition(position);
            colorModifierEnter.AddChild(sprite);
            colorModifierExit.AddChild(sprite);
            alphaModifierEnter.AddChild(sprite);
            alphaModifierExit.AddChild(sprite);

            sprite = new Pax4Button("lavaandiceMenuContinue", null);
            AddChild(sprite);
            textureName = "Sprite/lavaandiceContinueBtn";
            texture = Pax4Texture2D._current.Get(textureName);
            ((Pax4Button)sprite).SetTexture(texture);
            textureName = "Sprite/lavaandiceContinueBtnOver";
            texture = Pax4Texture2D._current.Get(textureName);
            ((Pax4Button)sprite).SetTextureOver(texture);
            ((Pax4Button)sprite).SetOnClick(this.lavaandiceContinueBtn_Click);
            position.X = 126.0f;
            position.Y = 624.0f;
            sprite.SetPosition(position);
            colorModifierEnter.AddChild(sprite);
            colorModifierExit.AddChild(sprite);
            alphaModifierEnter.AddChild(sprite);
            alphaModifierExit.AddChild(sprite);

            //**************************************************
            //create
            //**************************************************
            float xpos = 0.0f;
            //float xstep = 0.0f;
            //float xoff = 0.0f;

            float ypos = 0.0f;
            float ystep = 0.0f;
            //float yoff = 0.0f;

            float scale;

            //Difficulty
            xpos = 105.0f;
            ypos = 246.0f;
            sprite = new Pax4SpriteText("difficulty", null);
            position.X = xpos;
            position.Y = ypos;
            scale = 0.55f;
            sprite.SetPosition(position);
            sprite.SetColor(Color.White);
            ((Pax4SpriteText)sprite).SetSpriteFont("SpriteFont/ArialBold");
            ((Pax4SpriteText)sprite).SetScale(scale);
            ((Pax4SpriteText)sprite).SetText("Difficulty:");
            AddChild(sprite);
            colorModifierEnter.AddChild(sprite);
            colorModifierExit.AddChild(sprite);
            alphaModifierEnter.AddChild(sprite);
            alphaModifierExit.AddChild(sprite);

            //Difficulty Value
            xpos = 210.0f;
            ypos = 246.0f;
            ystep = 30.0f;
            sprite = new Pax4SpriteText("difficultyValue", null);
            position.X = xpos;
            position.Y = ypos; ypos += ystep;
            scale = 0.60f;
            sprite.SetPosition(position);
            sprite.SetColor(Color.OrangeRed);
            ((Pax4SpriteText)sprite).SetSpriteFont("SpriteFont/ArialBold");
            ((Pax4SpriteText)sprite).SetScale(scale);
            ((Pax4SpriteText)sprite).SetText("Hard");
            AddChild(sprite);
            _difficultySprite = sprite;
            colorModifierEnter.AddChild(sprite);
            colorModifierExit.AddChild(sprite);
            alphaModifierEnter.AddChild(sprite);
            alphaModifierExit.AddChild(sprite);

            //Score
            xpos = 105.0f;
            sprite = new Pax4SpriteText("score", null);
            position.X = xpos;
            position.Y = ypos;
            scale = 0.65f;
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
            xpos = 188.0f;
            sprite = new Pax4SpriteText("scoreValue", null);
            position.X = xpos;
            position.Y = ypos - 1.0f; ypos += ystep;
            scale = 0.75f;
            sprite.SetPosition(position);
            sprite.SetColor(Color.GreenYellow);
            ((Pax4SpriteText)sprite).SetSpriteFont("SpriteFont/ArialBold");
            ((Pax4SpriteText)sprite).SetScale(scale);
            ((Pax4SpriteText)sprite).SetText("100,000,000");
            AddChild(sprite);
            _scoreSprite = sprite;
            colorModifierEnter.AddChild(sprite);
            colorModifierExit.AddChild(sprite);
            alphaModifierEnter.AddChild(sprite);
            alphaModifierExit.AddChild(sprite);

            textNumberModifier = new Pax4SpriteTextNumberModifier("", null);
            textNumberModifier.AddChild(sprite);
            //textNumberModifier.Ini(1000000.0f, 0.0f, true, 10.0f);
            AddStateEnterModifier(textNumberModifier);
            _currentScoreModifier = textNumberModifier;

            //Total Score
            xpos = 105.0f;
            sprite = new Pax4SpriteText("totalScore", null);
            position.X = xpos;
            position.Y = ypos + 5.0f;
            scale = 0.50f;
            sprite.SetPosition(position);
            sprite.SetColor(Color.White);
            ((Pax4SpriteText)sprite).SetSpriteFont("SpriteFont/ArialBold");
            ((Pax4SpriteText)sprite).SetScale(scale);
            ((Pax4SpriteText)sprite).SetText("Total Score:");
            AddChild(sprite);
            colorModifierEnter.AddChild(sprite);
            colorModifierExit.AddChild(sprite);
            alphaModifierEnter.AddChild(sprite);
            alphaModifierExit.AddChild(sprite);

            //Total Score value
            xpos = 222.0f;
            ypos = ypos + 4.0f;
            sprite = new Pax4SpriteText("totalScoreValue", null);
            position.X = xpos;
            position.Y = ypos;
            scale = 0.55f;
            sprite.SetPosition(position);
            sprite.SetColor(Color.GreenYellow);
            ((Pax4SpriteText)sprite).SetSpriteFont("SpriteFont/ArialBold");
            ((Pax4SpriteText)sprite).SetScale(scale);
            ((Pax4SpriteText)sprite).SetText("100,000,000");
            AddChild(sprite);
            _totalScoreSprite = sprite;
            colorModifierEnter.AddChild(sprite);
            colorModifierExit.AddChild(sprite);
            alphaModifierEnter.AddChild(sprite);
            alphaModifierExit.AddChild(sprite);

            textNumberModifier = new Pax4SpriteTextNumberModifier("", null);
            textNumberModifier.AddChild(sprite);
            //textNumberModifier.Ini(0.0f, 10000000.0f, true, 10.0f);
            AddStateEnterModifier(textNumberModifier);
            _totalScoreModifier = textNumberModifier;

            //**************************************************
            //create medal sprites
            //**************************************************
            if (_medalSprite == null)
                _medalSprite = new Dictionary<String, Pax4Sprite>();
            else
                return;

            duration = 0.2f;
            _medalColorModifier = new Pax4SpriteColorModifier("", null);
            _medalColorModifier.Ini(Color.White, Color.Black, duration);
            _medalColorModifier.SetOscillating();
            AddStateEnterModifier(_medalColorModifier);

            _medalAlphaModifier = new Pax4SpriteAlphaModifier("", null);
            _medalAlphaModifier.Ini(1.0f, 0.0f, duration);
            _medalAlphaModifier.SetOscillating();
            AddStateEnterModifier(_medalAlphaModifier);

            Dictionary<String, String> _medal = new Dictionary<String, String>();

            _medal.Add("nightmareOn", "Sprite/lavaandiceNightmareOn");
            _medal.Add("hardOn", "Sprite/lavaandiceHardOn");
            _medal.Add("normalOn", "Sprite/lavaandiceNormalOn");
            _medal.Add("easyOn", "Sprite/lavaandiceEasyOn");

            xpos = 335.0f;
            ypos = 250.0f;
            foreach (KeyValuePair<String, String> kvp in _medal)
            {
                textureName = kvp.Value;
                texture = Pax4Texture2D._current.Get(textureName);
                sprite = new Pax4SpriteTexture(kvp.Key, null);
                ((Pax4SpriteTexture)sprite).SetTexture(texture);
                sprite.SetPosition(new Vector2(xpos, ypos));
                sprite.SetScale(1.6f);
                _medalSprite.Add(sprite._name, sprite);
                
                _medalColorModifier.AddChild(sprite);
                _medalAlphaModifier.AddChild(sprite);

                colorModifierExit.AddChild(sprite);
                alphaModifierExit.AddChild(sprite);
            }

            _currentMedalSprite = sprite;
        }

        private void lavaandiceExitBtn_Click()
        {
            ((Pax4SoundLavaAndIce)Pax4Sound._current)._lavaandiceButtonAccepted.Play();

            Pax4World._current.Dx();

            if (Pax4Ui._current != null)
                Pax4Ui._current.Enter(Pax4UiStateLavaAndIceChooseMission._currentMissionState);
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

        private void lavaandiceContinueBtn_Click()
        {
            ((Pax4SoundLavaAndIce)Pax4Sound._current)._lavaandiceButtonAccepted.Play();

            Pax4WorldLavaAndIce._missionIndex++;
            if (Pax4WorldLavaAndIce._missionIndex > ((Pax4WorldLavaAndIce)Pax4World._current)._missionCount)
            {
                Pax4WorldLavaAndIce._missionIndex = 1;
                Pax4Ui._current.Enter("chooseQuest");
            }
            else
            {
                Pax4Ui._current.Enter("difficulty");
            }

            Pax4World._current.Dx();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (!_currentScoreModifier._done)
                _currentScoreModifier.Update(gameTime);
            if (!_totalScoreModifier._done)
                _totalScoreModifier.Update(gameTime);

            if(_currentMedalSprite != null)
                _currentMedalSprite.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);

            if (_currentMedalSprite != null)                
                _currentMedalSprite.Draw(gameTime);
        }

        public override void Enter()
        {
            _medalColorModifier.SetOscillating();
            _medalAlphaModifier.SetOscillating();

            Pax4Sound._current.PlayStateSong("Sound/lavaandiceWinParade");
            Pax4ParticleEffect._current.Enable();
            UpdateScore();

            base.Enter();
        }

        public override void Exit()
        {
            _medalColorModifier.SetOscillating(false);
            _medalAlphaModifier.SetOscillating(false);

            Pax4Sound._current.PlayRandomSong();

            base.Exit();
        }

        public void UpdateScore()
        {
            if (Pax4WorldLavaAndIce._missionIndex <= 0)
                return;

            String totalScoreName = Pax4UiStateLavaAndIceChooseQuest._questName + "_TotalScore";
            String lastScoreQuestName = Pax4UiStateLavaAndIceChooseQuest._questName + "_LastScore";

            String highScoreName = Pax4UiStateLavaAndIceChooseQuest._questName + "_" + Pax4WorldLavaAndIce._missionIndex + "_HighScore";
            String lastScoreName = Pax4UiStateLavaAndIceChooseQuest._questName + "_" + Pax4WorldLavaAndIce._missionIndex + "_LastScore";

            String lavaKillsName = Pax4UiStateLavaAndIceChooseQuest._questName + "_LavaKills";
            String iceKillsName = Pax4UiStateLavaAndIceChooseQuest._questName + "_IceKills";
            String monsterKillsName = Pax4UiStateLavaAndIceChooseQuest._questName + "_MonsterKills";

            int currentScore = ((Pax4WorldLavaAndIce)Pax4World._current)._score;

            int totalScore = Pax4UiLavaAndIceQuestScore._score[totalScoreName];

            Pax4UiLavaAndIceQuestScore._score[totalScoreName] += currentScore;

            Pax4UiLavaAndIceQuestScore._score[lastScoreQuestName] = currentScore;
            Pax4UiLavaAndIceQuestScore._score[lastScoreName] = currentScore;

            int highScore = Pax4UiLavaAndIceQuestScore._score[highScoreName];

            if (currentScore > highScore)
                Pax4UiLavaAndIceQuestScore._score[highScoreName] = currentScore;

            if (Pax4WorldLavaAndIce._difficulty == Pax4WorldLavaAndIce._difficultyNightmare)
            {
                Pax4UiLavaAndIceQuestScore._score[Pax4UiStateLavaAndIceChooseQuest._questName + "_NightmareMedalCount"]++;
                Pax4UiLavaAndIceQuestScore._score[Pax4UiStateLavaAndIceChooseQuest._questName + "_" + Pax4WorldLavaAndIce._missionIndex + "_NightmareMedalCount"]++;
            }
            else if (Pax4WorldLavaAndIce._difficulty == Pax4WorldLavaAndIce._difficultyHard)
            {
                Pax4UiLavaAndIceQuestScore._score[Pax4UiStateLavaAndIceChooseQuest._questName + "_HardMedalCount"]++;
                Pax4UiLavaAndIceQuestScore._score[Pax4UiStateLavaAndIceChooseQuest._questName + "_" + Pax4WorldLavaAndIce._missionIndex + "_HardMedalCount"]++;
            }
            else if (Pax4WorldLavaAndIce._difficulty == Pax4WorldLavaAndIce._difficultyNormal)
            {
                Pax4UiLavaAndIceQuestScore._score[Pax4UiStateLavaAndIceChooseQuest._questName + "_NormalMedalCount"]++;
                Pax4UiLavaAndIceQuestScore._score[Pax4UiStateLavaAndIceChooseQuest._questName + "_" + Pax4WorldLavaAndIce._missionIndex + "_NormalMedalCount"]++;
            }
            else if (Pax4WorldLavaAndIce._difficulty == Pax4WorldLavaAndIce._difficultyEasy)
            {
                Pax4UiLavaAndIceQuestScore._score[Pax4UiStateLavaAndIceChooseQuest._questName + "_EasyMedalCount"]++;
                Pax4UiLavaAndIceQuestScore._score[Pax4UiStateLavaAndIceChooseQuest._questName + "_" + Pax4WorldLavaAndIce._missionIndex + "_EasyMedalCount"]++;
            }

            Pax4UiLavaAndIceQuestScore._score[lavaKillsName] += ((Pax4WorldLavaAndIce)Pax4World._current)._lavaKills;
            Pax4UiLavaAndIceQuestScore._score[iceKillsName] += ((Pax4WorldLavaAndIce)Pax4World._current)._iceKills;
            Pax4UiLavaAndIceQuestScore._score[monsterKillsName] += ((Pax4WorldLavaAndIce)Pax4World._current)._monsterKills;

            Pax4UiLavaAndIceQuestScore._score[Pax4UiStateLavaAndIceChooseQuest._questName + "_" + (Pax4WorldLavaAndIce._missionIndex + 1).ToString() + "_Locked"] = 0;

            Pax4UiLavaAndIceQuestScore._currentScore.Write();

            _currentScoreModifier.Ini(currentScore, 0.0f, true, 5.0f);
            _currentScoreModifier.Trigger();
            _totalScoreModifier.Ini(totalScore, totalScore + currentScore, true, 5.0f);
            _totalScoreModifier.Trigger();
        }

        public static void UpdateMedalSprite()
        {
            if (Pax4WorldLavaAndIce._difficulty == Pax4WorldLavaAndIce._difficultyNightmare)
            {
                _currentMedalSprite = _medalSprite["nightmareOn"];
                ((Pax4SpriteText)_difficultySprite).SetText("Nightmare");
            }
            else if (Pax4WorldLavaAndIce._difficulty == Pax4WorldLavaAndIce._difficultyHard)
            {
                _currentMedalSprite = _medalSprite["hardOn"];
                ((Pax4SpriteText)_difficultySprite).SetText("Hard");
            }
            else if (Pax4WorldLavaAndIce._difficulty == Pax4WorldLavaAndIce._difficultyNormal)
            {
                _currentMedalSprite = _medalSprite["normalOn"];
                ((Pax4SpriteText)_difficultySprite).SetText("Normal");
            }
            else if (Pax4WorldLavaAndIce._difficulty == Pax4WorldLavaAndIce._difficultyEasy)
            {
                _currentMedalSprite = _medalSprite["easyOn"];
                ((Pax4SpriteText)_difficultySprite).SetText("Easy");
            }
        }
    }
}