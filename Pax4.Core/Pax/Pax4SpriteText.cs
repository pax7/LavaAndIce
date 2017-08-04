using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Pax.Core;
using System.Runtime.Serialization;
using System.IO;

namespace Pax4.Core
{
    [DataContract]
    [KnownType(typeof(Pax4SpriteText))]
    public class Pax4SpriteText : Pax4Sprite
    {
        #region Class Memebers
        [DataMember]
        public String _text = null;
        
        [IgnoreDataMember]
        public SpriteFont _spriteFont = null;

        [DataMember]
        public Rectangle _rasterizerScissor0 = Rectangle.Empty;

        [DataMember]
        private Vector2 _positionOffset = Vector2.One;

        [IgnoreDataMember]
        private Pax4Sprite _spriteParent = null;
        #endregion

        public Pax4SpriteText(String p_name, Pax4Sprite p_parent)
            : base(p_name, p_parent)
        {
        }

        public override void Draw(GameTime gameTime)
        {
            if (_spriteFont == null || _text == null || (_parent0 != null && ((Pax4Object)_parent0)._isInvisible))
                return;

            if (_parent0 != null && _parent0 is Pax4Sprite)
            {
                _spriteParent = ((Pax4Sprite)_parent0);
                _rectangleDraw = _spriteParent._rectangleDraw;

                
                _positionOffset.X += _rectangleDraw.X;

                _rectangleDraw.X += (int)(_spriteParent._centerPositionDraw.X - _spriteParent._originScaledDraw.X);
                _rectangleDraw.Y += (int)(_spriteParent._centerPositionDraw.Y - _spriteParent._originScaledDraw.Y);

                _positionOffset = _centerPositionDraw;
                
                _rasterizerScissor0 = Pax4Game._spriteBatch.GraphicsDevice.ScissorRectangle;

                Pax4Game._spriteBatch.GraphicsDevice.ScissorRectangle = _rectangleDraw;

                Pax4Game._spriteBatch.DrawString(_spriteFont, _text, _positionOffset, _color, _rotationZ,_originDraw, _scaleDraw, SpriteEffects.None, 0.0f);

                Pax4Game._spriteBatch.GraphicsDevice.ScissorRectangle = _rasterizerScissor0;
            }
        }

        [Intent(typeof(Pax4SpriteText), "SetSpriteFont", typeof(String), "p_spriteFontName")]
        public void SetSpriteFont(String p_spriteFontName = null)
        {
            if (p_spriteFontName == null)
                return;

            _spriteFont = Pax4SpriteFont._current.Get(p_spriteFontName);
        }

        [Intent(typeof(Pax4SpriteText), "SetText", typeof(String), "p_text")]
        public void SetText(String p_text = null)
        {
            _text = p_text;
            //SetRectangleWidthHeight(_spriteFont.MeasureString(_text));
            //you sure must deal with this soon :D
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