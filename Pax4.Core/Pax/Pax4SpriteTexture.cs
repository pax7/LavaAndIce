using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Pax.Core;
using System.IO;

namespace Pax4.Core
{
    [DataContract]
    [KnownType(typeof(Pax4SpriteTexture))]
    public class Pax4SpriteTexture : Pax4Sprite
    {
        [IgnoreDataMember]
        public Texture2D _texture = null;
        
        [IgnoreDataMember]  
        private Vector2 _positionOffset = Vector2.One;

        public Pax4SpriteTexture(String p_name, Pax4Sprite p_parent)
            : base(p_name, p_parent)
        {            
        }

        public override void Draw(GameTime gameTime)
        {
            if (_texture == null || _isInvisible)
                return;

            //New To Allow Reposition of Sprites about to leave screen to the left side.
            _positionOffset = _centerPositionDraw;
            _positionOffset.X += _rectangleDraw.X;
            _positionOffset.Y += _rectangleDraw.Y;

            //Pax4Game._spriteBatch.Draw(_texture, _PositionOffset, new Rectangle(_leftThreshold,_topThreshold,_rightThreshold,_bottomThreshold), _color, _rotationZ, _originDraw, _scaleDraw, SpriteEffects.None, 0.0f);
            
            Pax4Game._spriteBatch.Draw(_texture, _positionOffset, _rectangleDraw, _color, _rotationZ, _originDraw, _scaleDraw, SpriteEffects.None, 0.0f);
        }

        public virtual void SetTexture(Texture2D p_texture = null)
        {
            if (p_texture == null)
                return;

            _texture = p_texture;

            SetRectangleWidthHeight(p_texture);

            UpdateThreshold();
        }

        public override void Exe(PaxIntent p_intent)
        {
            base.Exe(p_intent);
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