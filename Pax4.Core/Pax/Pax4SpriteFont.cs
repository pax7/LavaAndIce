using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Pax.Core;

namespace Pax4.Core
{
    [DataContract]
    [KnownType(typeof(Pax4SpriteFont))]
    public class Pax4SpriteFont : PaxState
    {
        public static Pax4SpriteFont _current = null;

        public Dictionary<String, SpriteFont> _spriteFont = new Dictionary<String, SpriteFont>();

        //private bool _dx = true;

        public Pax4SpriteFont(String p_name, PaxState p_parent0) 
            : base(p_name, p_parent0)
        {
            _current = this;
        }

        public void Load(String p_spriteFont)
        {
            if (p_spriteFont == null)
                return;

            if (_spriteFont.ContainsKey(p_spriteFont))
                return;

            SpriteFont spriteFont = Pax4Game._current.Content.Load<SpriteFont>(p_spriteFont);

            _spriteFont.Add(p_spriteFont, spriteFont);

            //_dx = true;
        }

        public void Load(List<String> p_spriteFont)
        {
            if (p_spriteFont == null)
                return;

            SpriteFont spriteFont = null;

            for (int i = 0; i < p_spriteFont.Count; i++)
            {
                if (_spriteFont.ContainsKey(p_spriteFont[i]))
                    continue;

                spriteFont = Pax4Game._current.Content.Load<SpriteFont>(p_spriteFont[i]);

                _spriteFont.Add(p_spriteFont[i], spriteFont);
            }

            //_dx = true;
        }

        public SpriteFont Get(String p_spriteFont)
        {
            SpriteFont result = null;

            _spriteFont.TryGetValue(p_spriteFont, out result);

            return result;
        }

        public void Reset()
        {
            _spriteFont.Clear();
        }

        public override void Dx()
        {
            _spriteFont.Clear();
            _spriteFont = null;

            //_dx = false;

            if (this == _current)
                _current = null;

            base.Dx();
        }

        #region serialize

        public override MemoryStream Serialize(bool p_volatile = false)
        {
            return Serialize(this.GetType(), p_volatile);
        }

        #endregion
    }
}