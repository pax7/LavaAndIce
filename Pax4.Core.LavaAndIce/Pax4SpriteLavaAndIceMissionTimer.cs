using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Runtime.Serialization;

namespace Pax4.Core
{
    [DataContract]
    [KnownType(typeof(Pax4UiLavaAndIceMissionTimer))]
    public class Pax4UiLavaAndIceMissionTimer : Pax4Sprite
    {
        public Pax4Sprite _normalSprite = null;
        public Pax4Sprite _alarmSprite = null;
        public Pax4Sprite _alarmSpriteYellow = null;
        public Pax4Sprite _alarmSpriteRed = null;
        public Pax4Sprite _timerValue = null;

        public float _duration = 0.0f;
        public float _timer = 0.0f;
        public bool _alarm = false;
        public bool _done = true;

        public bool _timerDisabled = true;

        public bool _timer1 = true;
        public bool _timer2 = true;

        public Pax4UiLavaAndIceMissionTimer(String p_name, Pax4Sprite p_parent)
            : base(p_name, p_parent)
        {
            String textureName = null;
            Texture2D texture = null;
            Pax4Sprite sprite = null;

            float scale = 1.0f;
            textureName = "Sprite/lavaandiceInGameFgTimer";
            texture = Pax4Texture2D._current.Get(textureName);
            sprite = new Pax4SpriteTexture("normalTimer", this);
            ((Pax4SpriteTexture)sprite).SetTexture(texture);
            _normalSprite = (Pax4SpriteTexture)sprite;

            textureName = "Sprite/lavaandiceInGameFgTimerYellow";
            texture = Pax4Texture2D._current.Get(textureName);
            sprite = new Pax4SpriteTexture("alarmTimer", this);
            ((Pax4SpriteTexture)sprite).SetTexture(texture);
            _alarmSpriteYellow = (Pax4SpriteTexture)sprite;

            textureName = "Sprite/lavaandiceInGameFgTimerRed";
            texture = Pax4Texture2D._current.Get(textureName);
            sprite = new Pax4SpriteTexture("alarmTimer", this);
            ((Pax4SpriteTexture)sprite).SetTexture(texture);
            _alarmSpriteRed = (Pax4SpriteTexture)sprite;

            //timer value
            Vector2 position = new Vector2(11.0f, 9.0f);
            sprite = new Pax4SpriteText("timerValue", this);
            scale = 0.76f;
            sprite.SetPosition(position);
            sprite.SetColor(Color.GreenYellow);
            ((Pax4SpriteText)sprite).SetSpriteFont("SpriteFont/ArialBold");
            ((Pax4SpriteText)sprite).SetScale(scale);
            _timerValue = sprite;
        }

        public override void Update(GameTime gameTime)
        {
            if (_done || Pax4Game._pause || _timerDisabled)
                return;

            if (_timer > 0.0f)
            {
                _timer -= (float)gameTime.ElapsedGameTime.TotalSeconds;

                _alarm = false;
                if (_timer <= 20.0f && ((int)_timer) % 2 == 0)
                {
                    if (_timer1)
                    {
                        ((Pax4SoundLavaAndIce)Pax4Sound._current)._lavaandiceTimer1.Play();
                        _timer1 = false;
                    }

                    if (_timer <= 8.5f && _timer2)
                    {
                        ((Pax4SoundLavaAndIce)Pax4Sound._current)._lavaandiceTimer2.Play();
                        _timer2 = false;
                    }

                    _alarm = true;

                    if (_timer > 10.0f)
                        _alarmSprite = _alarmSpriteYellow;
                    else
                        _alarmSprite = _alarmSpriteRed;
                }

                if (_timer <= 0.0f)
                {
                    _timer = 0.0f;
                    _alarm = true;
                }

                ((Pax4SpriteText)_timerValue).SetText(Pax4Tools.FloatSecondsToMinutesSeconds(_timer));
            }
            else
            {
                _done = true;
            }

            if (_normalSprite != null)
                _normalSprite.Update(gameTime);

            if (_alarm && _alarmSprite != null)
                _alarmSprite.Update(gameTime);

            if (_timerValue != null)
                _timerValue.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            if (_timerDisabled)
                return;

            if (_normalSprite != null)
                _normalSprite.Draw(gameTime);

            if (_alarm && _alarmSprite != null || _done)
                _alarmSprite.Draw(gameTime);

            if (_timerValue != null)
                _timerValue.Draw(gameTime);
        }

        public void Enable(float p_duration)
        {
            _duration = p_duration;
        }

        public void Trigger()
        {
            _timerDisabled = false;
            _timer = _duration;
            _done = false;
            _timer1 = true;
            _timer2 = true;
        }
    }
}