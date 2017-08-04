using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Pax.Core;

namespace Pax4.Core
{
    [DataContract]
    [KnownType(typeof(Pax4Texture2D))]
    public class Pax4Texture2D : PaxState
    {
        public static Pax4Texture2D _current = null;

        public Dictionary<String, Texture2D> _texture2D = new Dictionary<String, Texture2D>();

        //private bool _dx = true;

        public Pax4Texture2D(String p_name,PaxState p_parent0)
            : base(p_name, p_parent0)
        {
            _current = this;
        }

        public void Load(String p_texture2D)
        {
            if (p_texture2D == null)
                return;

            if (_texture2D.ContainsKey(p_texture2D))
                return;

            Texture2D texture2D = Pax4Game._current.Content.Load<Texture2D>(p_texture2D);

            _texture2D.Add(p_texture2D, texture2D);
        }

        public void Load(List<String> p_texture2D)
        {
            if (p_texture2D == null)
                return;

            Texture2D texture2D = null;

            for (int i = 0; i < p_texture2D.Count; i++)
            {
                if (_texture2D.ContainsKey(p_texture2D[i]))
                    continue;

                texture2D = Pax4Game._current.Content.Load<Texture2D>(p_texture2D[i]);

                _texture2D.Add(p_texture2D[i], texture2D);
            }

            //_dx = true;
        }

        public Texture2D Get(String p_texture2D)
        {
            Texture2D result = null;

            if (!_texture2D.ContainsKey(p_texture2D))
                Load(p_texture2D);

            _texture2D.TryGetValue(p_texture2D, out result);

            return result;
        }

        public override void Dx()
        {
            _texture2D.Clear();
            _texture2D = null;
            //_dx = false;

            if (this == _current)
                _current = null;

            base.Dx();
        }

        public void Reset()
        {
            _texture2D.Clear();
        }
    }
}