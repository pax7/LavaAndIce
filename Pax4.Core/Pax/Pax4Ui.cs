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
    [KnownType(typeof(Pax4Ui))]
    public class Pax4Ui : PaxState
    {
        #region Class Members
        [IgnoreDataMember]
        public static Pax4UiState _uiState0 = null;

        [IgnoreDataMember]
        public static List<Pax4UiState> _currentUiState = new List<Pax4UiState>();

        [IgnoreDataMember]
        public static List<Pax4UiState> _previousUiState = new List<Pax4UiState>();

        [IgnoreDataMember]
        public const float _btnDuration = 0.2f;

        [IgnoreDataMember]
        public const float _btnDurationStep = 0.05f;

        [IgnoreDataMember]
        public const bool _stateChange = false;

        [IgnoreDataMember]
        public static Pax4Ui _current = null;

        [IgnoreDataMember]
        public static List<Pax4UiState> _uiRemove = new List<Pax4UiState>();
        #endregion

        public Pax4Ui(String p_name, PaxState p_parent0)
            : base(p_name, p_parent0)
        {
            _current = this;
        }

        public virtual void Update(GameTime gameTime)
        {
            for (int i = 0; i < _currentUiState.Count; i++)
                _currentUiState[i].Update(gameTime);            

            if (_previousUiState.Count > 0)
            {
                for (int i = 0; i < _previousUiState.Count; i++)
                {
                    if (_previousUiState[i]._done)
                        _uiRemove.Add(_previousUiState[i]);
                    else
                        _previousUiState[i].Update(gameTime);
                }

                if (_uiRemove.Count > 0)
                {
                    for (int i = 0; i < _uiRemove.Count; i++)
                        _previousUiState.Remove(_uiRemove[i]);
                    _uiRemove.Clear();
                }
            }
        }

        public virtual void Draw(GameTime gameTime)
        {
            //Pax4Game._graphicsDevice.RasterizerState = Pax4Camera._rasterizerState;
            //Pax4Game._graphicsDevice.DepthStencilState = DepthStencilState.Default;
            //Pax4Game._graphicsDevice.BlendState = Pax4Camera._blendState;
            //.AlphaBlend
            //Pax4Game._spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend);

            for (int i = 0; i < _currentUiState.Count; i++)   
                _currentUiState[i].Draw(gameTime);             

            if (_previousUiState.Count > 0)
            {
                for (int i = 0; i < _previousUiState.Count; i++)
                    _previousUiState[i].Draw(gameTime);
            }            

            //Pax4Game._spriteBatch.End();
        }

        public virtual void Create()
        {
        }

        public void Enter(Pax4UiState p_uiState = null)
        {
            if (p_uiState == null)
                return;

            if (p_uiState._persistent)
            {
                for (int i = 0; i < _currentUiState.Count; i++)
                {
                    _currentUiState[i].Exit();
                    _previousUiState.Add(_currentUiState[i]);
                    _uiRemove.Add(_currentUiState[i]);
                }
            }
            else
            {
                _previousUiState.Add(p_uiState);
                p_uiState.Enter();

                return;
            }

            if (_uiRemove.Count > 0)
            {
                for (int i = 0; i < _uiRemove.Count; i++)
                    _currentUiState.Remove(_uiRemove[i]);
                _uiRemove.Clear();
            }

            _currentUiState.Add(p_uiState);
            p_uiState.Enter();
        }

        public void Enter(String p_uiState = null)
        {
            if (p_uiState == null)
                return;

            PaxState uiState = null;

            if (TryGetChild(p_uiState, out uiState))
                Enter((Pax4UiState)uiState);
        }

        public void AddUiState(Pax4UiState p_uiState)
        {
            AddChild(p_uiState);            
        }

        #region serialize

        public override MemoryStream Serialize(bool p_volatile = false)
        {
            return Serialize(this.GetType(), p_volatile);
        }

        #endregion
    }
}