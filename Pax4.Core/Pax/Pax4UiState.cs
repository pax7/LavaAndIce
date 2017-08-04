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
    [KnownType(typeof(Pax4UiState))]
    public class Pax4UiState : PaxState
    {
        #region Class Members
        [IgnoreDataMember]
        public const String _EnterState = "Enter";
        [IgnoreDataMember]
        public const String _ExitState = "Exit";

        [DataMember]
        public bool _done = true;
                
        [DataMember]
        public float _duration = 0.0f; //if this is set, then this state automatically moves to another state when timer == 0.0f;
        
        [DataMember]
        public float _timer = 0.0f;

        [DataMember]
        public String _data = null;

        //when going from this to the next String Pax4UiState, trigger all the Pax4UiModifier of all Pax4Sprite
        [IgnoreDataMember]
        public Dictionary<String, List<Pax4ModifierSprite>> _spriteModifier = new Dictionary<String, List<Pax4ModifierSprite>>();
        
        [IgnoreDataMember]        
        public List<Pax4Sprite> _sprite = new List<Pax4Sprite>();//this stays because the order is important

        [IgnoreDataMember]   
        public Pax4UiState _previousState = null;

        [IgnoreDataMember]
        public Pax4UiState _nextState = null;

        public bool _persistent = true;

        //[ScriptIgnore]
        //public BlendState _blendState = BlendState.AlphaBlend;

        public bool _fg = false;
        #endregion

        public Pax4UiState(String p_name, PaxState p_parent0)
            : base(p_name, p_parent0)
        {
            if(p_parent0 == null)
                SetParent0(Pax4Ui._current);
        }

        public virtual void Update(GameTime gameTime)
        {
            for (int i = 0; i < _sprite.Count; i++)
                _sprite[i].Update(gameTime);

            if (_done)
                return;

            _done = true;
            foreach (List<Pax4ModifierSprite> spriteModifier in _spriteModifier.Values)
            {
                for (int i = 0; i < spriteModifier.Count; i++)
                {
                    if (!spriteModifier[i]._done)
                    {
                        spriteModifier[i].Update(gameTime);
                        if(_done)
                            _done = spriteModifier[i]._done;
                    }
                }
            }

            //timer until moving on to the next state
            if (_duration > 0.0f)
            {
                _done = false;

                _timer -= (float)gameTime.ElapsedGameTime.TotalSeconds;

                if (_timer <= 0.0f)
                {
                    if (_nextState != null)
                        _nextState.Enter();

                    _done = true;
                }
            }
        }

        public virtual void Draw(GameTime gameTime)
        {           
            //Pax4Game._spriteBatch.Begin(SpriteSortMode.Deferred, _blendState);

            for (int i = 0; i < _sprite.Count; i++)
                _sprite[i].Draw(gameTime);

            //Pax4Game._spriteBatch.End();
        }

        public virtual void Enter()
        {
            _fg = true;

            if (_sprite == null)
                return;

            for (int i = 0; i < _sprite.Count; i++)
                _sprite[i].Enable();

            _done = false;

            _timer = _duration;

            if (_spriteModifier == null)
            {
                _done = true;
                return;
            }

            List<Pax4ModifierSprite> spriteModifier = null;
            if (!_spriteModifier.TryGetValue(_EnterState, out spriteModifier))
            {
                _done = true;
                return;
            }

            for (int i = 0; i < spriteModifier.Count; i++)
                spriteModifier[i].Trigger();
        }

        public virtual void Exit()
        {
            _fg = false;

            if (_sprite == null)
                return;

            for (int i = 0; i < _sprite.Count; i++)
                _sprite[i].Disable();           

            _done = false;

            if (_spriteModifier == null)
            {
                _done = true;
                return;
            }
            
            List<Pax4ModifierSprite> spriteModifier = null;
            if (!_spriteModifier.TryGetValue(_ExitState, out spriteModifier))
            {
                _done = true;
                return;
            }
            
            for (int i = 0; i < spriteModifier.Count; i++)
                spriteModifier[i].Trigger();
        }

        public void AddStateEnterModifier(Pax4ModifierSprite p_spriteModifier = null)
        {
            AddStateModifier(_EnterState, p_spriteModifier);
        }

        public void AddStateExitModifier(Pax4ModifierSprite p_spriteModifier = null)
        {
            AddStateModifier(_ExitState, p_spriteModifier);
        }

        public void AddStateModifier(String p_state, Pax4ModifierSprite p_spriteModifier = null)
        {
            if (p_spriteModifier == null)
                return;

            if (_spriteModifier == null)
                _spriteModifier = new Dictionary<String, List<Pax4ModifierSprite>>();

            List<Pax4ModifierSprite> spriteModifier = null;

            if (!_spriteModifier.TryGetValue(p_state, out spriteModifier))
            {
                spriteModifier = new List<Pax4ModifierSprite>();
                _spriteModifier.Add(p_state, spriteModifier);
            }

            spriteModifier.Add(p_spriteModifier);
        }

        public virtual void SetDuration(float p_duration = 1.0f)
        {
            if (p_duration <= 0.0f)
                return;

            _duration = p_duration;
        }

        public override void AddChild(PaxState p_state,bool p_recursive = true)
        {
            if (p_state is Pax4Sprite)
            {
                Pax4Sprite sprite = (Pax4Sprite)p_state;

                if (sprite == null)
                    return;
                if (_sprite == null)
                    _sprite = new List<Pax4Sprite>();

                _sprite.Add(sprite);

            }
            base.AddChild(p_state, p_recursive);
        }

        public void Reset()
        {
            if (_spriteModifier != null)
            {
                foreach (List<Pax4ModifierSprite> list in _spriteModifier.Values)
                    list.Clear();

                _spriteModifier.Clear();
            }

            if (_sprite != null)
                _sprite.Clear();
        }

        public override MemoryStream Serialize(bool p_volatile = false)
        {
            return Serialize(this.GetType(), p_volatile);
        }
    
    }
}