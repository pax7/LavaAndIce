using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Runtime.Serialization;

namespace Pax4.Core
{
    [DataContract]
    [KnownType(typeof(Pax4UiLavaAndIceMissionHealth))]
    public class Pax4UiLavaAndIceMissionHealth : Pax4Sprite
    {
        public static Pax4UiLavaAndIceMissionHealth _currentHealth = null;
        
        public static Dictionary<String, Pax4Sprite> _sprite = null;

        public static Pax4SpriteTexture _currentLavaHealthSprite = null;
        public static Pax4SpriteTexture _currentIceHealthSprite = null;

        public static Vector2 _lavaHealthSpritePosition = new Vector2(367.0f, 0.0f);
        public static Vector2 _iceHealthSpritePosition = new Vector2(64.0f, 0.0f);

        public static float _lavaHealth0 = 0.0f;
        public static float _iceHealth0 = 0.0f;

        public Pax4UiLavaAndIceMissionHealth(String p_name, Pax4Sprite p_parent)
            : base(p_name, p_parent)
        {
            _currentHealth = this;

            _sprite = new Dictionary<String, Pax4Sprite>();

            String textureName = null;
            Texture2D texture = null;
            Pax4Sprite sprite = null;

            #region lavaHealth

            textureName = "Sprite/lavaandiceLavaHealth0";
            texture = Pax4Texture2D._current.Get(textureName);
            sprite = new Pax4SpriteTexture("lava0", this);
            ((Pax4SpriteTexture)sprite).SetTexture(texture);
            sprite.SetPosition(_lavaHealthSpritePosition);
            _sprite.Add(sprite._name, sprite);

            _currentLavaHealthSprite = (Pax4SpriteTexture)sprite;

            textureName = "Sprite/lavaandiceLavaHealth1";
            texture = Pax4Texture2D._current.Get(textureName);
            sprite = new Pax4SpriteTexture("lava1", this);
            ((Pax4SpriteTexture)sprite).SetTexture(texture);
            sprite.SetPosition(_lavaHealthSpritePosition);
            _sprite.Add(sprite._name, sprite);

            textureName = "Sprite/lavaandiceLavaHealth2";
            texture = Pax4Texture2D._current.Get(textureName);
            sprite = new Pax4SpriteTexture("lava2", this);
            ((Pax4SpriteTexture)sprite).SetTexture(texture);
            sprite.SetPosition(_lavaHealthSpritePosition);
            _sprite.Add(sprite._name, sprite);

            textureName = "Sprite/lavaandiceLavaHealth3";
            texture = Pax4Texture2D._current.Get(textureName);
            sprite = new Pax4SpriteTexture("lava3", this);
            ((Pax4SpriteTexture)sprite).SetTexture(texture);
            sprite.SetPosition(_lavaHealthSpritePosition);
            _sprite.Add(sprite._name, sprite);

            #endregion

            #region iceHealth

            textureName = "Sprite/lavaandiceIceHealth0";
            texture = Pax4Texture2D._current.Get(textureName);
            sprite = new Pax4SpriteTexture("ice0", this);
            ((Pax4SpriteTexture)sprite).SetTexture(texture);
            sprite.SetPosition(_iceHealthSpritePosition);
            _sprite.Add(sprite._name, sprite);

            _currentIceHealthSprite = (Pax4SpriteTexture)sprite;

            textureName = "Sprite/lavaandiceIceHealth1";
            texture = Pax4Texture2D._current.Get(textureName);
            sprite = new Pax4SpriteTexture("ice1", this);
            ((Pax4SpriteTexture)sprite).SetTexture(texture);
            sprite.SetPosition(_iceHealthSpritePosition);
            _sprite.Add(sprite._name, sprite);

            textureName = "Sprite/lavaandiceIceHealth2";
            texture = Pax4Texture2D._current.Get(textureName);
            sprite = new Pax4SpriteTexture("ice2", this);
            ((Pax4SpriteTexture)sprite).SetTexture(texture);
            sprite.SetPosition(_iceHealthSpritePosition);
            _sprite.Add(sprite._name, sprite);

            textureName = "Sprite/lavaandiceIceHealth3";
            texture = Pax4Texture2D._current.Get(textureName);
            sprite = new Pax4SpriteTexture("ice3", this);
            ((Pax4SpriteTexture)sprite).SetTexture(texture);
            sprite.SetPosition(_iceHealthSpritePosition);
            _sprite.Add(sprite._name, sprite);

            #endregion
        }

        public override void Update(GameTime gameTime)
        {
            if (_currentLavaHealthSprite != null)
                _currentLavaHealthSprite.Update(gameTime);

            if (_currentIceHealthSprite != null)
                _currentIceHealthSprite.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            if (_currentLavaHealthSprite != null)
                _currentLavaHealthSprite.Draw(gameTime);

            if (_currentIceHealthSprite != null)
                _currentIceHealthSprite.Draw(gameTime);
        }

        public static void SetLavaHealth(float p_lavaHealth = 0.0f)
        {
            if (( p_lavaHealth == 0.0f && _lavaHealth0 == 0.0f)
                || (p_lavaHealth != 0.0f && _lavaHealth0 / p_lavaHealth == 1.0f))
                return;            

            if (p_lavaHealth > 2.0f && p_lavaHealth <= 3.0f)
                _currentLavaHealthSprite = (Pax4SpriteTexture)_sprite["lava3"];
            else if (p_lavaHealth > 1.0f && p_lavaHealth <= 2.0f)
                _currentLavaHealthSprite = (Pax4SpriteTexture)_sprite["lava2"];
            else if (p_lavaHealth > 0.0f && p_lavaHealth <= 1.0f)
                _currentLavaHealthSprite = (Pax4SpriteTexture)_sprite["lava1"];
            else if (p_lavaHealth <= 0.0f)
                _currentLavaHealthSprite = (Pax4SpriteTexture)_sprite["lava0"];

            _lavaHealth0 = p_lavaHealth;
        }

        public static void SetIceHealth(float p_iceHealth = 0.0f)
        {
            if ((p_iceHealth == 0.0f && _iceHealth0 == 0.0f)
                || (p_iceHealth != 0.0f && _iceHealth0 / p_iceHealth == 1.0f))
                return;

            if (p_iceHealth > 2.0f && p_iceHealth <= 3.0f)
                _currentIceHealthSprite = (Pax4SpriteTexture)_sprite["ice3"];
            else if (p_iceHealth > 1.0f && p_iceHealth <= 2.0f)
                _currentIceHealthSprite = (Pax4SpriteTexture)_sprite["ice2"];
            else if (p_iceHealth > 0.0f && p_iceHealth <= 1.0f)
                _currentIceHealthSprite = (Pax4SpriteTexture)_sprite["ice1"];
            else if (p_iceHealth <= 0.0f)
                _currentIceHealthSprite = (Pax4SpriteTexture)_sprite["ice0"];

            _iceHealth0 = p_iceHealth;
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