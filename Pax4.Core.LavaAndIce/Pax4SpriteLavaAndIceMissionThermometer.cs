using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Runtime.Serialization;

namespace Pax4.Core
{
    [DataContract]
    [KnownType(typeof(Pax4UiLavaAndIceMissionThermometer))]
    public class Pax4UiLavaAndIceMissionThermometer : Pax4Sprite
    {
        public static Pax4UiLavaAndIceMissionThermometer _currentThermometer = null;
        
        public static Dictionary<String, Pax4Sprite> _sprite = null;

        public static List<Pax4SpriteTexture> _currentTemperaturePathSprite = new List<Pax4SpriteTexture>();

        public static Pax4SpriteTexture _currentTemperatureSprite = null;

        private static Pax4SpriteTexture _defaultTemperature = null;

        public static float _temperature0 = 0.0f;

        public static float _duration = 0.5f;
        public static float _timer = 0.0f;

        public Pax4UiLavaAndIceMissionThermometer(String p_name, Pax4Sprite p_parent)
            : base(p_name, p_parent)
        {
            _currentThermometer = this;

            if (_sprite != null)
                return;

            String textureName = null;
            Texture2D texture = null;
            Pax4Sprite sprite = null;

            _sprite = new Dictionary<String, Pax4Sprite>();

            textureName = "Sprite/lavaandiceTemperature0.10";
            texture = Pax4Texture2D._current.Get(textureName);
            sprite = new Pax4SpriteTexture("0.10", this);
            ((Pax4SpriteTexture)sprite).SetTexture(texture);
            _sprite.Add(sprite._name, sprite);

            textureName = "Sprite/lavaandiceTemperature0.20";
            texture = Pax4Texture2D._current.Get(textureName);
            sprite = new Pax4SpriteTexture("0.20", this);
            ((Pax4SpriteTexture)sprite).SetTexture(texture);
            _sprite.Add(sprite._name, sprite);

            textureName = "Sprite/lavaandiceTemperature0.30";
            texture = Pax4Texture2D._current.Get(textureName);
            sprite = new Pax4SpriteTexture("0.30", this);
            ((Pax4SpriteTexture)sprite).SetTexture(texture);
            _sprite.Add(sprite._name, sprite);

            textureName = "Sprite/lavaandiceTemperature0.40";
            texture = Pax4Texture2D._current.Get(textureName);
            sprite = new Pax4SpriteTexture("0.40", this);
            ((Pax4SpriteTexture)sprite).SetTexture(texture);
            _sprite.Add(sprite._name, sprite);

            textureName = "Sprite/lavaandiceTemperature0.45";
            texture = Pax4Texture2D._current.Get(textureName);
            sprite = new Pax4SpriteTexture("0.45", this);
            ((Pax4SpriteTexture)sprite).SetTexture(texture);
            _sprite.Add(sprite._name, sprite);

            textureName = "Sprite/lavaandiceTemperature0.50";
            texture = Pax4Texture2D._current.Get(textureName);
            sprite = new Pax4SpriteTexture("0.50", this);
            ((Pax4SpriteTexture)sprite).SetTexture(texture);
            _sprite.Add(sprite._name, sprite);

            _currentTemperatureSprite = (Pax4SpriteTexture)sprite;//"0.50"
            _defaultTemperature = (Pax4SpriteTexture)sprite;

            textureName = "Sprite/lavaandiceTemperature0.55";
            texture = Pax4Texture2D._current.Get(textureName);
            sprite = new Pax4SpriteTexture("0.55", this);
            ((Pax4SpriteTexture)sprite).SetTexture(texture);
            _sprite.Add(sprite._name, sprite);

            textureName = "Sprite/lavaandiceTemperature0.60";
            texture = Pax4Texture2D._current.Get(textureName);
            sprite = new Pax4SpriteTexture("0.60", this);
            ((Pax4SpriteTexture)sprite).SetTexture(texture);
            _sprite.Add(sprite._name, sprite);

            textureName = "Sprite/lavaandiceTemperature0.70";
            texture = Pax4Texture2D._current.Get(textureName);
            sprite = new Pax4SpriteTexture("0.70", this);
            ((Pax4SpriteTexture)sprite).SetTexture(texture);
            _sprite.Add(sprite._name, sprite);

            textureName = "Sprite/lavaandiceTemperature0.80";
            texture = Pax4Texture2D._current.Get(textureName);
            sprite = new Pax4SpriteTexture("0.80", this);
            ((Pax4SpriteTexture)sprite).SetTexture(texture);
            _sprite.Add(sprite._name, sprite);

            textureName = "Sprite/lavaandiceTemperature0.90";
            texture = Pax4Texture2D._current.Get(textureName);
            sprite = new Pax4SpriteTexture("0.90", this);
            ((Pax4SpriteTexture)sprite).SetTexture(texture);
            _sprite.Add(sprite._name, sprite);
        }

        public override void Update(GameTime gameTime)
        {
            if (_currentTemperatureSprite == null)
                return;

            _timer -= (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (_currentTemperaturePathSprite.Count > 0 && _timer <= 0.0f)
            {
                _currentTemperatureSprite = _currentTemperaturePathSprite[0];
                _currentTemperaturePathSprite.Remove(_currentTemperatureSprite);

                _timer = _duration;
            }

            _currentTemperatureSprite.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            if (_currentTemperatureSprite == null)
                return;

            _currentTemperatureSprite.Draw(gameTime);
        }

        public override void SetPosition(Vector2 p_position0)
        {
            base.SetPosition(p_position0);

            foreach (Pax4Sprite sprite in _sprite.Values)
                sprite._centerPosition = this._centerPosition;
        }

        public static void SetTemperature(float p_temperature = 0.50f)
        {
            if (   (p_temperature == 0.0f && _temperature0 == 0.0f)
                || (p_temperature != 0.0f && _temperature0 == p_temperature))
                return;
            
            _currentTemperaturePathSprite.Clear();

            float td = 0.05f;

            if (p_temperature < _temperature0)
            {
                td = -td;
                for (float t0 = _temperature0; t0 >= p_temperature; t0 += td)
                    _currentTemperaturePathSprite.Add(_currentThermometer.GetTemperatureSprite(t0));
            }
            else
            {
                for (float t0 = _temperature0; t0 <= p_temperature; t0 += td)
                    _currentTemperaturePathSprite.Add(_currentThermometer.GetTemperatureSprite(t0));
            }

            if (_currentTemperaturePathSprite.Count > 0)
                _duration = 0.5f / _currentTemperaturePathSprite.Count;

            _currentTemperaturePathSprite.Add(_currentThermometer.GetTemperatureSprite(p_temperature));

            _temperature0 = p_temperature;
        }

        private Pax4SpriteTexture GetTemperatureSprite(float p_temperature = 0.50f)
        {
            Pax4SpriteTexture result = _defaultTemperature;

            if (p_temperature <= 0.10f)
                result = (Pax4SpriteTexture)_sprite["0.10"];

            else if (p_temperature > 0.10f && p_temperature <= 0.20f)
                result = (Pax4SpriteTexture)_sprite["0.20"];

            else if (p_temperature > 0.20f && p_temperature <= 0.30f)
                result = (Pax4SpriteTexture)_sprite["0.30"];

            else if (p_temperature > 0.30f && p_temperature <= 0.40f)
                result = (Pax4SpriteTexture)_sprite["0.40"];

            else if (p_temperature > 0.40f && p_temperature < 0.50f)
                result = (Pax4SpriteTexture)_sprite["0.45"];

            //else if (   p_temperature == 0.50f)
            //    result = (Pax4SpriteTexture)_sprite["0.50"];

            else if (p_temperature > 0.50f && p_temperature < 0.60f)
                result = (Pax4SpriteTexture)_sprite["0.55"];

            else if (p_temperature >= 0.60f && p_temperature < 0.70f)
                result = (Pax4SpriteTexture)_sprite["0.60"];

            else if (p_temperature >= 0.70f && p_temperature < 0.80f)
                result = (Pax4SpriteTexture)_sprite["0.70"];

            else if (p_temperature >= 0.80f && p_temperature < 0.90f)
                result = (Pax4SpriteTexture)_sprite["0.80"];

            else if (p_temperature >= 0.90f)
                result = (Pax4SpriteTexture)_sprite["0.90"];

            return result;
        }

        public override void Enable()
        {
            base.Enable();

            foreach (Pax4Sprite s in _sprite.Values)
                s.Enable();
        }

        public override void Disable()
        {
            base.Disable();

            foreach (Pax4Sprite s in _sprite.Values)
                s.Disable();
        }
    }
}