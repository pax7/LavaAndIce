using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Runtime.Serialization;
using Pax.Core;
using System.IO;

namespace Pax4.Core
{
    [DataContract]
    public class Pax4ToggleButton : Pax4Button
    {
        [DataMember]
        public bool _toggle = false;

        [DataMember ]
        public bool _toggleEnabled = true;

        public Pax4ToggleButton(String p_name, Pax4Sprite p_parent)
            : base(p_name, p_parent)
        {
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (_oneTap && _toggleEnabled)
                Toggle();
        }

        public override void Draw(GameTime gameTime)
        {
            if(_toggle)
                _oneTouch = true;

            base.Draw(gameTime);

            if (_toggle)
                _oneTouch = false;
        }

        [Intent(typeof(Pax4ToggleButton), "Toggle")]
        public void Toggle()
        {
            if (_toggle)
                _toggle = false;
            else
                _toggle = true;
        }

        [Intent(typeof(Pax4ToggleButton), "ToggleEnabled", typeof(bool), "p_toggleEnabled")]
        public void ToggleEnabled(bool p_toggleEnabled = true)
        {
            _toggleEnabled = p_toggleEnabled;
        }

        public override void Exe(PaxIntent p_intent)
        {
            switch (p_intent._intent)
            {
                default:
                    base.Exe(p_intent);
                    break;
            }

            _skipUpdate = false;
        }

        #region serialize

        public override MemoryStream Serialize(bool p_volatile = false)
        {
            return Serialize(this.GetType(), p_volatile);
        }

        #endregion
    }
}