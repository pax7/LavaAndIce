using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Runtime.Serialization;

namespace Pax4.Core
{
    [DataContract]
    [KnownType(typeof(Pax4ButtonLavaAndIceMission))]
    class Pax4ButtonLavaAndIceMission : Pax4Button
    {
        public static List<Pax4ButtonLavaAndIceMission> _missionButton = new List<Pax4ButtonLavaAndIceMission>();

        //titles
        public const String _pctCompleted = "";
        public const String _lastScore = "";

        public List<Pax4Sprite> _sprite = null;

        public Pax4Sprite _lockedSprite = null;
        public bool _locked = false;

        public String _questName = null;
        public int _missionIndex = 0;
        public String _questNameMissionIndex = null;

        public OnClick _onClick1 = null;

        public Pax4Sprite _pctCompletedValue = null;
        public Pax4Sprite _highScoreValue = null;
        public Pax4Sprite _lastScoreValue = null;

        public Pax4Sprite _nightmareMedal = null;
        public Pax4Sprite _nightmareMedalCount = null;
        public Pax4Sprite _hardMedal = null;
        public Pax4Sprite _hardMedalCount = null;
        public Pax4Sprite _normalMedal = null;
        public Pax4Sprite _normalMedalCount = null;
        public Pax4Sprite _easyMedal = null;
        public Pax4Sprite _easyMedalCount = null;

        public Pax4ButtonLavaAndIceMission(String p_name, Pax4Sprite p_parent, String p_questName, int p_missionIndex)
            : base(p_name, p_parent)
        {
            _questName = p_questName;
            _missionIndex = p_missionIndex;

            _missionButton.Add(this);

            _questNameMissionIndex = p_questName + "_" + p_missionIndex;

            _sprite = new List<Pax4Sprite>();

            Pax4Sprite sprite = null;
            Vector2 position;
            float scale;

            float xpos = 0.0f;
            float xstep = 0.0f;
            float xoff = 0.0f;

            float ypos = 0.0f;
            float ystep = 0.0f;
            float yoff = 0.0f;

            //title
            sprite = new Pax4SpriteText(p_name + "_Text", this);
            //position.X = 154.0f;
            //position.Y = 66.0f;
            scale = 0.9f;
            sprite.SetPosition(Vector2.Zero);
            sprite.SetColor(Color.Gold);
            ((Pax4SpriteText)sprite).SetSpriteFont("SpriteFont/Livingstone");
            ((Pax4SpriteText)sprite).SetScale(scale);
            _textSprite = (Pax4SpriteText)sprite;

            if (p_missionIndex <= 0)
                return;

            #region scores

            //High Score
            xpos = 12.0f;
            ypos = 12.0f;
            ystep = 12.0f;
            sprite = new Pax4SpriteText("highScore", this);
            position.X = xpos;
            position.Y = ypos; ypos += ystep;
            scale = 0.30f;
            sprite.SetPosition(position);
            sprite.SetColor(Color.White);
            ((Pax4SpriteText)sprite).SetSpriteFont("SpriteFont/ArialBold");
            ((Pax4SpriteText)sprite).SetScale(scale);
            ((Pax4SpriteText)sprite).SetText("High Score");
            _sprite.Add(sprite);

            //High Score value
            sprite = new Pax4SpriteText("highScoreValue", this);
            position.X = xpos;
            position.Y = ypos; ypos += ystep;
            scale = 0.35f;
            sprite.SetPosition(position);
            sprite.SetColor(Color.GreenYellow);
            ((Pax4SpriteText)sprite).SetSpriteFont("SpriteFont/ArialBold");
            ((Pax4SpriteText)sprite).SetScale(scale);
            _sprite.Add(sprite);
            _highScoreValue = sprite;

            //xpos = xpos;
            ypos = ypos + 5.0f;
            ystep = 12.0f;
            //Last Score
            sprite = new Pax4SpriteText("lastScore", this);
            position.X = xpos;
            position.Y = ypos; ypos += ystep;
            scale = 0.30f;
            sprite.SetPosition(position);
            sprite.SetColor(Color.White);
            ((Pax4SpriteText)sprite).SetSpriteFont("SpriteFont/ArialBold");
            ((Pax4SpriteText)sprite).SetScale(scale);
            ((Pax4SpriteText)sprite).SetText("Last Score");
            _sprite.Add(sprite);

            //Last Score value
            sprite = new Pax4SpriteText("lastScoreValue", this);
            position.X = xpos;
            position.Y = ypos; ypos += ystep;
            scale = 0.35f;
            sprite.SetPosition(position);
            sprite.SetColor(Color.GreenYellow);
            ((Pax4SpriteText)sprite).SetSpriteFont("SpriteFont/ArialBold");
            ((Pax4SpriteText)sprite).SetScale(scale);
            _sprite.Add(sprite);
            _lastScoreValue = sprite;

            #endregion

            #region medals

            //String textureName = null;
            //Texture2D texture = null;

            xpos = 142.0f;
            xoff = 24.0f;
            xstep = 48.0f;
            ypos = 14.0f;
            yoff = 26.0f;

            //int medalCount = 0;
            //int medal = 0;

            sprite = new Pax4SpriteTexture("nightmare", this);
            sprite.SetPosition(new Vector2(xpos, ypos));
            _sprite.Add(sprite);
            _nightmareMedal = sprite;

            sprite = new Pax4SpriteText("nightmareCount", this);
            position.X = xpos + xoff;
            position.Y = ypos + yoff;
            scale = 0.4f;
            sprite.SetPosition(position);
            sprite.SetColor(Color.White);
            ((Pax4SpriteText)sprite).SetSpriteFont("SpriteFont/ArialBold");
            ((Pax4SpriteText)sprite).SetScale(scale);
            _sprite.Add(sprite);
            _nightmareMedalCount = sprite;

            xpos += xstep;

            sprite = new Pax4SpriteTexture("hard", this);
            sprite.SetPosition(new Vector2(xpos, ypos));
            _sprite.Add(sprite);
            _hardMedal = sprite;

            sprite = new Pax4SpriteText("hardCount", this);
            position.X = xpos + xoff;
            position.Y = ypos + yoff;
            scale = 0.4f;
            sprite.SetPosition(position);
            sprite.SetColor(Color.White);
            ((Pax4SpriteText)sprite).SetSpriteFont("SpriteFont/ArialBold");
            ((Pax4SpriteText)sprite).SetScale(scale);
            _sprite.Add(sprite);
            _hardMedalCount = sprite;

            xpos += xstep;

            sprite = new Pax4SpriteTexture("normal", this);
            sprite.SetPosition(new Vector2(xpos, ypos));
            _sprite.Add(sprite);
            _normalMedal = sprite;

            sprite = new Pax4SpriteText("normalCount", this);
            position.X = xpos + xoff;
            position.Y = ypos + yoff;
            scale = 0.4f;
            sprite.SetPosition(position);
            sprite.SetColor(Color.White);
            ((Pax4SpriteText)sprite).SetSpriteFont("SpriteFont/ArialBold");
            ((Pax4SpriteText)sprite).SetScale(scale);
            _sprite.Add(sprite);
            _normalMedalCount = sprite;

            xpos += xstep;

            sprite = new Pax4SpriteTexture("easy", this);
            sprite.SetPosition(new Vector2(xpos, ypos));
            _sprite.Add(sprite);
            _easyMedal = sprite;

            sprite = new Pax4SpriteText("easyCount", this);
            //position.X = xpos + xoff;
            //position.Y = ypos + yoff;
            scale = 0.4f;
            sprite.SetPosition(position);
            sprite.SetColor(Color.White);
            ((Pax4SpriteText)sprite).SetSpriteFont("SpriteFont/ArialBold");
            ((Pax4SpriteText)sprite).SetScale(scale);
            _sprite.Add(sprite);
            _easyMedalCount = sprite;

            #endregion //medals

            //pct completed value
            sprite = new Pax4SpriteText("pctCompletedValue", this);
            //position.X = 86.0f;
            //position.Y = 50.0f;
            scale = 0.4f;
            sprite.SetPosition(position);
            sprite.SetColor(Color.OrangeRed);
            ((Pax4SpriteText)sprite).SetSpriteFont("SpriteFont/ArialBold");
            ((Pax4SpriteText)sprite).SetScale(scale);
            _sprite.Add(sprite);
            _pctCompletedValue = sprite;

            if (Pax4UiLavaAndIceQuestScore._score[_questNameMissionIndex + "_Locked"] > 0)
                SetLocked();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (_sprite == null)
                return;

            for (int i = 0; i < _sprite.Count; i++)
            {
                _sprite[i].Update(gameTime);
                _sprite[i].SetDisabledInvisible();
            }
            
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);

            if (_sprite == null)
                return;

            for (int i = 0; i < _sprite.Count; i++)
                _sprite[i].Draw(gameTime);
        }

        public static void UpdateScore()
        {
            int medalCount = 0;
            String questNameMissionIndexName = null;
            Pax4ButtonLavaAndIceMission missionButton = null;
            String textureName = null;
            Texture2D texture = null;
            int medal = 0;

            for (int bi = 0; bi < _missionButton.Count; bi++)
            {
                medal = 0;
                missionButton = _missionButton[bi];

                if (missionButton._missionIndex <= 0)
                    continue;

                questNameMissionIndexName = missionButton._questNameMissionIndex;

                ((Pax4SpriteText)missionButton._highScoreValue).SetText(Pax4Tools.NumberCommaFormat(Pax4UiLavaAndIceQuestScore._score[questNameMissionIndexName + "_HighScore"]));
                ((Pax4SpriteText)missionButton._lastScoreValue).SetText(Pax4Tools.NumberCommaFormat(Pax4UiLavaAndIceQuestScore._score[questNameMissionIndexName + "_LastScore"]));

                medalCount = Pax4UiLavaAndIceQuestScore._score[questNameMissionIndexName + "_NightmareMedalCount"];
                if (medalCount > 0)
                {
                    medal++;
                    textureName = "Sprite/lavaandiceNightmareOn";
                    texture = Pax4Texture2D._current.Get(textureName);
                    ((Pax4SpriteTexture)missionButton._nightmareMedal).SetTexture(texture);
                    ((Pax4SpriteText)missionButton._nightmareMedalCount).SetText(Pax4Tools.NumberCommaFormat(medalCount));
                }
                else
                {
                    textureName = "Sprite/lavaandiceNightmareOff";
                    texture = Pax4Texture2D._current.Get(textureName);
                    ((Pax4SpriteTexture)missionButton._nightmareMedal).SetTexture(texture);
                }

                medalCount = Pax4UiLavaAndIceQuestScore._score[questNameMissionIndexName + "_HardMedalCount"];
                if (medalCount > 0)
                {
                    medal++;
                    textureName = "Sprite/lavaandiceHardOn";
                    texture = Pax4Texture2D._current.Get(textureName);
                    ((Pax4SpriteTexture)missionButton._hardMedal).SetTexture(texture);
                    ((Pax4SpriteText)missionButton._hardMedalCount).SetText(Pax4Tools.NumberCommaFormat(medalCount));
                }
                else
                {
                    textureName = "Sprite/lavaandiceHardOff";
                    texture = Pax4Texture2D._current.Get(textureName);
                    ((Pax4SpriteTexture)missionButton._hardMedal).SetTexture(texture);
                }

                medalCount = Pax4UiLavaAndIceQuestScore._score[questNameMissionIndexName + "_NormalMedalCount"];
                if (medalCount > 0)
                {
                    medal++;
                    textureName = "Sprite/lavaandiceNormalOn";
                    texture = Pax4Texture2D._current.Get(textureName);
                    ((Pax4SpriteTexture)missionButton._normalMedal).SetTexture(texture);
                    ((Pax4SpriteText)missionButton._normalMedalCount).SetText(Pax4Tools.NumberCommaFormat(medalCount));
                }
                else
                {
                    textureName = "Sprite/lavaandiceNormalOff";
                    texture = Pax4Texture2D._current.Get(textureName);
                    ((Pax4SpriteTexture)missionButton._normalMedal).SetTexture(texture);
                }

                medalCount = Pax4UiLavaAndIceQuestScore._score[questNameMissionIndexName + "_EasyMedalCount"];
                if (medalCount > 0)
                {
                    medal++;
                    textureName = "Sprite/lavaandiceEasyOn";
                    texture = Pax4Texture2D._current.Get(textureName);
                    ((Pax4SpriteTexture)missionButton._easyMedal).SetTexture(texture);
                    ((Pax4SpriteText)missionButton._easyMedalCount).SetText(Pax4Tools.NumberCommaFormat(medalCount));
                }
                else
                {
                    textureName = "Sprite/lavaandiceEasyOff";
                    texture = Pax4Texture2D._current.Get(textureName);
                    ((Pax4SpriteTexture)missionButton._easyMedal).SetTexture(texture);
                }

                ((Pax4SpriteText)missionButton._pctCompletedValue).SetText(100 * medal / 4 + "%");

                if (medal > 0)
                    Pax4UiLavaAndIceQuestScore._score[missionButton._questName + "_" + (missionButton._missionIndex + 1) + "_Locked"] = 0;

                if(Pax4UiLavaAndIceQuestScore._score[missionButton._questName + "_" + missionButton._missionIndex + "_Locked"] == 0)
                    missionButton.SetLocked(false);
            }
        }

        public void SetLocked(bool p_locked = true)
        {
            //return;//!* undoz this

            if (!p_locked)
            {
                if (_lockedSprite != null)
                    _sprite.Remove(_lockedSprite);
                _locked = false;
                _onClick = _onClick1;
            }
            else if (!_locked)
            {
                String textureName = null;
                Texture2D texture = null;
                Pax4Sprite sprite = null;

                textureName = "Sprite/lavaandiceMissionLockedBtn";
                texture = Pax4Texture2D._current.Get(textureName);
                sprite = new Pax4SpriteTexture("locked", this);
                ((Pax4SpriteTexture)sprite).SetTexture(texture);
                sprite.SetPosition(Vector2.Zero);

                _lockedSprite = sprite;
                _sprite.Add(sprite);
                _locked = true;
            }
        }

        public void SetOnClick1(OnClick p_onClick1 = null)
        {
            _onClick1 = p_onClick1;
        }
    }
}