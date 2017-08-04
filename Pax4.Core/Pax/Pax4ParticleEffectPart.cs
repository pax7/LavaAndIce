using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Pax4.ProjectMercury;
using Pax4.ProjectMercury.Emitters;
using Pax4.ProjectMercury.Modifiers;
using Pax4.ProjectMercury.Renderers;
using Pax4.ProjectMercury.Controllers;
using Pax4.ProjectMercury.Proxies;
using System.Runtime.Serialization;

namespace Pax4.Core
{
    [DataContract]
    [KnownType(typeof(Pax4ParticleEffectPart))]
    public class Pax4ParticleEffectPart : Pax4Object
    {
        public Pax4ObjectSceneryPart _objectSceneryPart = null;
        public ParticleEffectProxy _particleEffectProxy = null;

        public Matrix _matScale = Matrix.Identity;
        public Vector3 _rotation = Vector3.Zero;
        
        public Vector3 _position0 = Vector3.Zero;
        public Vector3 _position = Vector3.Zero;

        public bool _disabled = false;

        public Pax4ParticleEffectPart(String p_name, Pax4Object p_parent0)
            : base(p_name, p_parent0)
        {
        }       

        public void Ini(ParticleEffect p_particleEffect)
        {
            _particleEffectProxy = new ParticleEffectProxy(p_particleEffect);
            _objectSceneryPart = (Pax4ObjectSceneryPart)_parent0;
        }

        public override void Update(GameTime gameTime)
        {
            if (_disabled || _particleEffectProxy == null)
                return;

            if (_particleEffectProxy.Effect.ActiveParticlesCount <= 0)
            {
                if (_objectSceneryPart == null
                || (_objectSceneryPart != null
                    && _objectSceneryPart._dxRequested))
                {
                    Dx();
                }
            }
            else
            {   
                _particleEffectProxy.Effect.Update((float)gameTime.ElapsedGameTime.TotalSeconds);
            }
        }

        public override void Draw(GameTime gameTime)
        {
        }

        public virtual void Trigger(Vector3 p_position, bool p_trail = false)
        {
            if (_disabled)
                return;

            Trigger(ref p_position, p_trail);
        }

        public virtual void Trigger(ref Vector3 p_position, bool p_trail = false)
        {
            if (_disabled || _particleEffectProxy == null)
                return;

            for (int i = 0; i < _particleEffectProxy.Effect.Emitters.Count; i++)
            {
                if (_particleEffectProxy.Effect.Emitters[i].Controllers.Count == 2)
                {
                    ((TriggerRotationController)_particleEffectProxy.Effect.Emitters[i].Controllers[1]).TriggerRotation = _rotation;
                }

                if (p_trail)
                {
                    if(_objectSceneryPart != null)
                        _position = Vector3.Transform(_position0, _objectSceneryPart.GetWorld());

                    ((TriggerOffsetController)_particleEffectProxy.Effect.Emitters[i].Controllers[0]).TriggerOffset = _position + p_position;
                }
            }

            if (!p_trail && _objectSceneryPart != null)
                _particleEffectProxy.World = _matScale * _objectSceneryPart._matWorld;

            _particleEffectProxy.Trigger();
        }        

        public virtual void TriggerWorldToScreen()
        {
            if (_disabled || _particleEffectProxy == null)
                return;
            
            Vector3 effectPosition = Pax4Tools.WorldToScreen(_objectSceneryPart.GetPosition());
            for (int i = 0; i < _particleEffectProxy.Effect.Emitters.Count; i++)
                ((TriggerOffsetController)_particleEffectProxy.Effect.Emitters[i].Controllers[0]).TriggerOffset = effectPosition;

            _particleEffectProxy.Trigger();
        }

        public virtual void Trigger(bool p_randomOffset, float p_offsetMax = 0.0f)
        {
            if (_disabled || _particleEffectProxy == null || _objectSceneryPart == null)
                return;

            Vector3 effectPosition = Pax4Tools.WorldToScreen(_objectSceneryPart.GetPosition());

            if (p_randomOffset)
                effectPosition += RandomUtil.NextUnitVector3() * p_offsetMax * Pax4Camera._current._scale;

            for (int i = 0; i < _particleEffectProxy.Effect.Emitters.Count; i++)
                ((TriggerOffsetController)_particleEffectProxy.Effect.Emitters[i].Controllers[0]).TriggerOffset = effectPosition;

            _particleEffectProxy.Trigger();
        }

        public override void Enable()
        {
            if (Pax4ParticleEffect._current == null)
                return;

            _disabled = false;
        }

        public override void Disable()
        {
            if (Pax4ParticleEffect._current == null)
                return;

            _disabled = true;
        }

        public override void Dx()
        {
            Disable();

            _objectSceneryPart = null;
            _particleEffectProxy = null;

            base.Dx();
        }

        public virtual void SetParticleEffect(ParticleEffect p_particleEffect)
        {
            _particleEffectProxy = new ParticleEffectProxy(p_particleEffect);
        }

        public virtual void SetScale(Vector3 p_scale)
        {
            _matScale = Matrix.CreateScale(p_scale);
        }

        public virtual void SetRotation(Vector3 p_rotation)
        {
            _rotation = p_rotation;
        }

        public virtual void SetScaleRotation(Vector3 p_scale, Vector3 p_rotation)
        {
            _matScale = Matrix.CreateScale(p_scale);
            _rotation = p_rotation;
        }

        public virtual void SetPosition(Vector3 p_position0)
        {
            _position0 = p_position0;
        }

        public virtual void SetScaleRotationPosition(Vector3 p_scale, Vector3 p_rotation, Vector3 p_position)
        {
            _matScale = Matrix.CreateScale(p_scale);
            _rotation = p_rotation;
            _position0 = p_position;
        }
    }
}