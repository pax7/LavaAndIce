using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Runtime.Serialization;

namespace Pax4.Core
{
    [DataContract]
    [KnownType(typeof(Pax4ButtonLavaAndIceQuest))]
    class Pax4ButtonLavaAndIceQuest : Pax4Button
    {
        public static List<Pax4ButtonLavaAndIceQuest> _questButton = new List<Pax4ButtonLavaAndIceQuest>();

        //titles
        public List<Pax4Sprite> _sprite = null;

        public bool _locked = false;
        public Pax4Sprite _lockedSprite = null;

        public bool _comingSoon = false;
        public Pax4Sprite _comingSoonSprite = null;

        public String _questName = null;

        public Pax4Sprite _pctCompletedValue = null;
        public Pax4Sprite _totalScoreValue = null;
        public Pax4Sprite _lastScoreValue = null;
        public Pax4Sprite _lavaKillsValue = null;
        public Pax4Sprite _iceKillsValue = null;
        public Pax4Sprite _monsterKillsValue = null;

        public Pax4Sprite _nightmareMedal = null;
        public Pax4Sprite _nightmareMedalCount = null;
        public Pax4Sprite _hardMedal = null;
        public Pax4Sprite _hardMedalCount = null;
        public Pax4Sprite _normalMedal = null;
        public Pax4Sprite _normalMedalCount = null;
        public Pax4Sprite _easyMedal = null;
        public Pax4Sprite _easyMedalCount = null;

        public Pax4ButtonLavaAndIceQuest(String p_name, Pax4Sprite p_parent, String p_questName)
            : base(p_name, p_parent)
        {
            _questButton.Add(this);

            _questName = p_questName;

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

            #region scores

            //title
            sprite = new Pax4SpriteText(p_name + "_Text", this);
            position.X = 0f;//120.0f;
            position.Y = 0f;//250.0f;
            scale = 1.0f;
            sprite.SetPosition(position);
            sprite.SetColor(Color.White);
            ((Pax4SpriteText)sprite).SetSpriteFont("SpriteFont/Livingstone");
            ((Pax4SpriteText)sprite).SetScale(scale);
            _textSprite = (Pax4SpriteText)sprite;

            //pct completed
            sprite = new Pax4SpriteText("pctCompleted", this);
            //position.X = 140.0f;//50
            //position.Y = 82.0f;//20
            scale = 0.45f;
            sprite.SetPosition(position);
            sprite.SetColor(Color.White);
            ((Pax4SpriteText)sprite).SetSpriteFont("SpriteFont/ArialBold");
            ((Pax4SpriteText)sprite).SetScale(scale);
            ((Pax4SpriteText)sprite).SetText("% completed");
            _sprite.Add(sprite);

            //pct completed value
            sprite = new Pax4SpriteText("pctCompletedValue", this);
            //position.X = 176.0f;
            //position.Y = 40.0f;
            scale = 1.0f;
            sprite.SetPosition(position);
            sprite.SetColor(Color.GreenYellow);
            ((Pax4SpriteText)sprite).SetSpriteFont("SpriteFont/ArialBold");
            ((Pax4SpriteText)sprite).SetScale(scale);
            _sprite.Add(sprite);
            _pctCompletedValue = sprite;

            //Total Score
            xpos = 30.0f;
            ypos = 260.0f;
            ystep = 20.0f;
            sprite = new Pax4SpriteText("totalScore", this);
            //position.X = xpos;
            //position.Y = ypos; ypos += ystep;
            scale = 0.41f;
            sprite.SetPosition(position);
            sprite.SetColor(Color.White);
            ((Pax4SpriteText)sprite).SetSpriteFont("SpriteFont/ArialBold");
            ((Pax4SpriteText)sprite).SetScale(scale);
            ((Pax4SpriteText)sprite).SetText("Total Score");
            _sprite.Add(sprite);

            //Total Score value
            sprite = new Pax4SpriteText("totalScoreValue", this);
            //position.X = xpos;
            //position.Y = ypos; ypos += ystep;
            scale = 0.41f;
            sprite.SetPosition(position);
            sprite.SetColor(Color.GreenYellow);
            ((Pax4SpriteText)sprite).SetSpriteFont("SpriteFont/ArialBold");
            ((Pax4SpriteText)sprite).SetScale(scale);
            _sprite.Add(sprite);
            _totalScoreValue = sprite;

            //Last Score
            sprite = new Pax4SpriteText("lastScore", this);
            //position.X = xpos;
            //position.Y = ypos; ypos += ystep;
            scale = 0.41f;
            sprite.SetPosition(position);
            sprite.SetColor(Color.White);
            ((Pax4SpriteText)sprite).SetSpriteFont("SpriteFont/ArialBold");
            ((Pax4SpriteText)sprite).SetScale(scale);
            ((Pax4SpriteText)sprite).SetText("Last Score");
            _sprite.Add(sprite);

            //Last Score value
            sprite = new Pax4SpriteText("lastScoreValue", this);
            //position.X = xpos;
            //position.Y = ypos; ypos += ystep;
            scale = 0.41f;
            sprite.SetPosition(position);
            sprite.SetColor(Color.GreenYellow);
            ((Pax4SpriteText)sprite).SetSpriteFont("SpriteFont/ArialBold");
            ((Pax4SpriteText)sprite).SetScale(scale);
            _sprite.Add(sprite);
            _lastScoreValue = sprite;

            ////Kills
            //sprite = new Pax4SpriteText("kills", this);
            //position.X = 160.0f;
            //position.Y = 370.0f;
            //scale = 0.3f;
            //sprite.SetPosition(position);
            //sprite.SetColor(Color.White);
            //((Pax4SpriteText)sprite).SetSpriteFont("SpriteFont/ArialBold");
            //((Pax4SpriteText)sprite).SetScale(scale);
            //((Pax4SpriteText)sprite).SetText(_kills);
            //_sprite.Add(sprite);

            xpos = 160.0f;
            ypos = 247.0f;
            ystep = 18;
            //lava
            sprite = new Pax4SpriteText("lavaKills", this);
            //position.X = xpos;
            //position.Y = ypos; ypos += ystep;
            scale = 0.39f;
            sprite.SetPosition(position);
            sprite.SetColor(Color.Red);
            ((Pax4SpriteText)sprite).SetSpriteFont("SpriteFont/ArialBold");
            ((Pax4SpriteText)sprite).SetScale(scale);
            ((Pax4SpriteText)sprite).SetText("lava kills");
            _sprite.Add(sprite);

            //lava value
            sprite = new Pax4SpriteText("lavaKillsValue", this);
            //position.X = xpos + 20.0f;
            //position.Y = ypos; ypos += ystep;
            scale = 0.39f;
            sprite.SetPosition(position);
            sprite.SetColor(Color.GreenYellow);
            ((Pax4SpriteText)sprite).SetSpriteFont("SpriteFont/ArialBold");
            ((Pax4SpriteText)sprite).SetScale(scale);
            _sprite.Add(sprite);
            _lavaKillsValue = sprite;

            //ice
            sprite = new Pax4SpriteText("iceKills", this);
            //position.X = xpos;
            //position.Y = ypos; ypos += ystep;
            scale = 0.39f;
            sprite.SetPosition(position);
            sprite.SetColor(Color.Cyan);
            ((Pax4SpriteText)sprite).SetSpriteFont("SpriteFont/ArialBold");
            ((Pax4SpriteText)sprite).SetScale(scale);
            ((Pax4SpriteText)sprite).SetText("ice kills");
            _sprite.Add(sprite);

            //ice value
            sprite = new Pax4SpriteText("iceKillsValue", this);
            //position.X = xpos + 20.0f;
            //position.Y = ypos; ypos += ystep;
            scale = 0.39f;
            sprite.SetPosition(position);
            sprite.SetColor(Color.GreenYellow);
            ((Pax4SpriteText)sprite).SetSpriteFont("SpriteFont/ArialBold");
            ((Pax4SpriteText)sprite).SetScale(scale);
            _sprite.Add(sprite);
            _iceKillsValue = sprite;

            //monsters
            sprite = new Pax4SpriteText("monsterKills", this);
            //position.X = xpos;
            //position.Y = ypos; ypos += ystep;
            scale = 0.39f;
            sprite.SetPosition(position);
            sprite.SetColor(Color.OrangeRed);
            ((Pax4SpriteText)sprite).SetSpriteFont("SpriteFont/ArialBold");
            ((Pax4SpriteText)sprite).SetScale(scale);
            ((Pax4SpriteText)sprite).SetText("monster kills");
            _sprite.Add(sprite);

            //monsters value
            sprite = new Pax4SpriteText("monsterKillsValue", this);
            //position.X = xpos + 20.0f;
            //position.Y = ypos; ypos += ystep;
            scale = 0.39f;
            sprite.SetPosition(position);
            sprite.SetColor(Color.GreenYellow);
            ((Pax4SpriteText)sprite).SetSpriteFont("SpriteFont/ArialBold");
            ((Pax4SpriteText)sprite).SetScale(scale);
            _sprite.Add(sprite);
            _monsterKillsValue = sprite;

            #endregion //scores

            #region medals

            //String textureName = null;
            //Texture2D texture = null;

            xpos = 58.0f;
            xoff = 24.0f;
            xstep = 48.0f;
            ypos = 152.0f;
            yoff = 26.0f;

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
            position.X = xpos + xoff;
            position.Y = ypos + yoff;
            scale = 0.4f;
            sprite.SetPosition(position);
            sprite.SetColor(Color.White);
            ((Pax4SpriteText)sprite).SetSpriteFont("SpriteFont/ArialBold");
            ((Pax4SpriteText)sprite).SetScale(scale);
            _sprite.Add(sprite);
            _easyMedalCount = sprite;

            #endregion //medals
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (_sprite == null)
                return;

            for (int i = 0; i < _sprite.Count; i++)
                _sprite[i].Update(gameTime);
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
            int maxMedalCount = 0;
            int medalCount = 0;
            String questName = null;
            String questNameMissionIndexName = null;
            Pax4ButtonLavaAndIceQuest questButton = null;
            String textureName = null;
            Texture2D texture = null;

            for (int bi = 0; bi < _questButton.Count; bi++)
            {
                questButton = _questButton[bi];
                questName = questButton._questName;

                if (questName.Equals("Prologue"))
                    maxMedalCount = 19 * 4;//!*update this to the count of missions
                else
                    maxMedalCount = Pax4WorldLavaAndIce._maxMissions * 4;

                for (int i = 0; i < Pax4WorldLavaAndIce._maxMissions; i++)
                {
                    questNameMissionIndexName = questName + "_" + (i + 1).ToString();

                    if (!Pax4UiLavaAndIceQuestScore._score.ContainsKey(questNameMissionIndexName + "_NightmareMedalCount"))
                        Pax4UiLavaAndIceQuestScore._score.Add(questNameMissionIndexName + "_NightmareMedalCount", 0);
                    if (Pax4UiLavaAndIceQuestScore._score[questNameMissionIndexName + "_NightmareMedalCount"] > 0)
                        medalCount++;

                    if (!Pax4UiLavaAndIceQuestScore._score.ContainsKey(questNameMissionIndexName + "_HardMedalCount"))
                        Pax4UiLavaAndIceQuestScore._score.Add(questNameMissionIndexName + "_HardMedalCount", 0);                    
                    if (Pax4UiLavaAndIceQuestScore._score[questNameMissionIndexName + "_HardMedalCount"] > 0)
                        medalCount++;

                    if (!Pax4UiLavaAndIceQuestScore._score.ContainsKey(questNameMissionIndexName + "_NormalMedalCount"))
                        Pax4UiLavaAndIceQuestScore._score.Add(questNameMissionIndexName + "_NormalMedalCount", 0);      
                    if (Pax4UiLavaAndIceQuestScore._score[questNameMissionIndexName + "_NormalMedalCount"] > 0)
                        medalCount++;

                    if (!Pax4UiLavaAndIceQuestScore._score.ContainsKey(questNameMissionIndexName + "_EasyMedalCount"))
                        Pax4UiLavaAndIceQuestScore._score.Add(questNameMissionIndexName + "_EasyMedalCount", 0);    
                    if (Pax4UiLavaAndIceQuestScore._score[questNameMissionIndexName + "_EasyMedalCount"] > 0)
                        medalCount++;
                }

                ((Pax4SpriteText)questButton._pctCompletedValue).SetText((100 * medalCount / maxMedalCount).ToString());

                ((Pax4SpriteText)questButton._totalScoreValue).SetText(Pax4Tools.NumberCommaFormat(Pax4UiLavaAndIceQuestScore._score[questName + "_TotalScore"]));
                ((Pax4SpriteText)questButton._lastScoreValue).SetText(Pax4Tools.NumberCommaFormat(Pax4UiLavaAndIceQuestScore._score[questName + "_LastScore"]));
                ((Pax4SpriteText)questButton._lavaKillsValue).SetText(Pax4Tools.NumberCommaFormat(Pax4UiLavaAndIceQuestScore._score[questName + "_LavaKills"]));
                ((Pax4SpriteText)questButton._iceKillsValue).SetText(Pax4Tools.NumberCommaFormat(Pax4UiLavaAndIceQuestScore._score[questName + "_IceKills"]));
                ((Pax4SpriteText)questButton._monsterKillsValue).SetText(Pax4Tools.NumberCommaFormat(Pax4UiLavaAndIceQuestScore._score[questName + "_MonsterKills"]));

                medalCount = Pax4UiLavaAndIceQuestScore._score[questName + "_NightmareMedalCount"];
                if (medalCount > 0)
                {
                    textureName = "Sprite/lavaandiceNightmareOn";
                    texture = Pax4Texture2D._current.Get(textureName);
                    ((Pax4SpriteTexture)questButton._nightmareMedal).SetTexture(texture);
                    ((Pax4SpriteText)questButton._nightmareMedalCount).SetText(Pax4Tools.NumberCommaFormat(medalCount));
                }
                else
                {
                    textureName = "Sprite/lavaandiceNightmareOff";
                    texture = Pax4Texture2D._current.Get(textureName);
                    ((Pax4SpriteTexture)questButton._nightmareMedal).SetTexture(texture);
                }

                medalCount = Pax4UiLavaAndIceQuestScore._score[questName + "_HardMedalCount"];
                if (medalCount > 0)
                {
                    textureName = "Sprite/lavaandiceHardOn";
                    texture = Pax4Texture2D._current.Get(textureName);
                    ((Pax4SpriteTexture)questButton._hardMedal).SetTexture(texture);
                    ((Pax4SpriteText)questButton._hardMedalCount).SetText(Pax4Tools.NumberCommaFormat(medalCount));
                }
                else
                {
                    textureName = "Sprite/lavaandiceHardOff";
                    texture = Pax4Texture2D._current.Get(textureName);
                    ((Pax4SpriteTexture)questButton._hardMedal).SetTexture(texture);
                }

                medalCount = Pax4UiLavaAndIceQuestScore._score[questName + "_NormalMedalCount"];
                if (medalCount > 0)
                {
                    textureName = "Sprite/lavaandiceNormalOn";
                    texture = Pax4Texture2D._current.Get(textureName);
                    ((Pax4SpriteTexture)questButton._normalMedal).SetTexture(texture);
                    ((Pax4SpriteText)questButton._normalMedalCount).SetText(Pax4Tools.NumberCommaFormat(medalCount));
                }
                else
                {
                    textureName = "Sprite/lavaandiceNormalOff";
                    texture = Pax4Texture2D._current.Get(textureName);
                    ((Pax4SpriteTexture)questButton._normalMedal).SetTexture(texture);
                }

                medalCount = Pax4UiLavaAndIceQuestScore._score[questName + "_EasyMedalCount"];
                if (medalCount > 0)
                {
                    textureName = "Sprite/lavaandiceEasyOn";
                    texture = Pax4Texture2D._current.Get(textureName);
                    ((Pax4SpriteTexture)questButton._easyMedal).SetTexture(texture);
                    ((Pax4SpriteText)questButton._easyMedalCount).SetText(Pax4Tools.NumberCommaFormat(medalCount));
                }
                else
                {
                    textureName = "Sprite/lavaandiceEasyOff";
                    texture = Pax4Texture2D._current.Get(textureName);
                    ((Pax4SpriteTexture)questButton._easyMedal).SetTexture(texture);
                }
            }
        }

        public void SetLocked(bool p_locked = true)
        {
            if (!p_locked)
            {
                if (_lockedSprite != null)
                    _sprite.Remove(_lockedSprite);
                _locked = false;
            }
            else if (!_locked)
            {
                String textureName = null;
                Texture2D texture = null;
                Pax4Sprite sprite = null;

                textureName = "Sprite/lavaandiceQuestLockedBtn";
                texture = Pax4Texture2D._current.Get(textureName);
                sprite = new Pax4SpriteTexture("locked", this);
                ((Pax4SpriteTexture)sprite).SetTexture(texture);
                sprite.SetPosition(Vector2.Zero);

                _lockedSprite = sprite;
                _sprite.Add(sprite);
                _locked = true;
            }
        }

        public void SetComingSoon(bool p_comingSoon = true)
        {
            if (!p_comingSoon)
            {
                if (_comingSoonSprite != null)
                    _sprite.Remove(_comingSoonSprite);
                _comingSoon = false;
            }
            else if (!_comingSoon)
            {
                String textureName = null;
                Texture2D texture = null;
                Pax4Sprite sprite = null;

                textureName = "Sprite/lavaandiceQuestComingSoonBtn";
                texture = Pax4Texture2D._current.Get(textureName);
                sprite = new Pax4SpriteTexture("comingSoon", this);
                ((Pax4SpriteTexture)sprite).SetTexture(texture);
                sprite.SetPosition(Vector2.Zero);

                _comingSoonSprite = sprite;
                _sprite.Add(sprite);
                _comingSoon = true;
            }
        }
    }
}