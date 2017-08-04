using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Pax4.Core
{
    [DataContract]
    [KnownType(typeof(Pax4Button))]
    public class Pax4Button : Pax4SpriteTexture
    {
        [IgnoreDataMember]
        private Texture2D _textureOver = null;

        [IgnoreDataMember]
        public Pax4SpriteText _textSprite = null;
        
        public delegate void OnClick();
        public OnClick _onClick = null;

        private Vector2 _positionOffset = Vector2.One;

        public Pax4Button(String p_name, Pax4Sprite p_parent)
            : base(p_name, p_parent)
        {
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (_textSprite != null)
                _textSprite.Update(gameTime);

            _oneTouch = false;

            if (_isDisabled)
                return;

            if (Touched())
            {
#if WINDOWS
                if (Pax4Touch._current._currentTouchState._clean && Pax4Touch._current._currentTouchState._oneTouch)
#else
				if (Pax4Touch._current._currentTouchState._clean)
#endif
				{              
					_oneTouch = true;
				}

                if (_parent0 != null && !((Pax4Sprite)_parent0)._oneTap)
                    return;

                _oneTap = Pax4Touch._current._currentTouchState._oneTap;

                if (_oneTap)
                {
                    if(_onClick != null)
                        _onClick();

                    Pax4Touch._current._currentTouchState._oneTap = false;
                }
            }
        }

        public override void Draw(GameTime gameTime)
        {
            if (_oneTouch && _textureOver != null)
            {
                _positionOffset = _centerPositionDraw;
                _positionOffset.X += _rectangleDraw.X;
                _positionOffset.Y += _rectangleDraw.Y;

                Pax4Game._spriteBatch.Draw(_textureOver, _positionOffset, _rectangleDraw, _color, _rotationZ, _originDraw, _scaleDraw, SpriteEffects.None, 0.0f);
            }
            else
                base.Draw(gameTime);

            if (_textSprite != null)
                _textSprite.Draw(gameTime);
        }

        public void SetTextureOver(Texture2D p_textureOver = null)
        {
            _textureOver = p_textureOver;
        }

        public void SetText(String p_text, Vector2 p_position)
        {
            if (_textSprite == null)
                return;

            _textSprite.SetText(p_text);
            _textSprite.SetPosition(p_position);
        }

        public void SetText(String p_text)
        {
            if (_textSprite == null)
                return;

            _textSprite.SetText(p_text);
        }

        public void SetTextSpriteFont(String p_spriteFont, Vector2 p_position, float p_scale = 1.0f)
        {
            if (p_spriteFont == null)
                return;

            if (_textSprite == null)
                _textSprite = new Pax4SpriteText(_name + "_Text", this);

            _textSprite.SetSpriteFont(p_spriteFont);
            _textSprite.SetScale(p_scale);
            _textSprite.SetPosition(p_position);
        }

        public void SetTextSpriteFont(String p_spriteFont, Vector2 p_position, String p_text, float p_scale = 1.0f)
        {
            if (p_spriteFont == null)
                return;

            if (_textSprite == null)
                _textSprite = new Pax4SpriteText(_name + "_Text", this);

            SetTextSpriteFont(p_spriteFont, p_position, p_scale);

            _textSprite.SetPosition(p_position);

            _textSprite._text = p_text;
            _textSprite.SetScale(p_scale);
        }

        public void SetOnClick(OnClick p_onClick = null)
        {
            _onClick = p_onClick;
        }

        #region serialize

        public override MemoryStream Serialize(bool p_volatile = false)
        {
            return Serialize(this.GetType(), p_volatile);
        }

        #endregion
    }
}