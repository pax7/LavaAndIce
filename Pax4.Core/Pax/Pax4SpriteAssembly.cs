using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Pax4.Core
{
    [DataContract]
    [KnownType(typeof(Pax4SpriteAssembly))]
    public class Pax4SpriteAssembly : Pax4Sprite
    {
        //titles
        public List<Pax4Sprite> _sprite = null;

        public Pax4SpriteAssembly(String p_name, Pax4Sprite p_parent0)
            : base(p_name, p_parent0)
        {
            _sprite = new List<Pax4Sprite>();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (_sprite == null || _sprite.Count <= 0)
                return;

            for (int i = 0; i < _sprite.Count; i++)
                _sprite[i].Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);

            if (_sprite == null || _sprite.Count <= 0)
                return;

            for (int i = 0; i < _sprite.Count; i++)
                _sprite[i].Draw(gameTime);
        }

        public void AddSprite(Pax4Sprite p_sprite = null)
        {
            if (p_sprite == null)
                return;
            if (_sprite == null)
                _sprite = new List<Pax4Sprite>();

            _sprite.Add(p_sprite);
        }
    }
}