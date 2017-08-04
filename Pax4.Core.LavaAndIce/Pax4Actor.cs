using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Pax4.ProjectMercury;
using Pax4.ProjectMercury.Proxies;
using System.Runtime.Serialization;

namespace Pax4.Core
{
    [DataContract]
    [KnownType(typeof(Pax4Actor))]
    public class Pax4Actor : Pax4ObjectPhysicsPartSphere
    {
        public enum EActorPowerUp
        {
            _NULL,
            _NORMAL,
            _DURABILITY,
            _REMOTE_CONTROL,//remember the bug
            _COUNT
        };

        public enum EActorType
        {
            _NULL,
            _PLAYER,
            _AMMO,
            _ENEMY,
            _MONSTER,
            _LAVA,
            _ICE,
            _NPC,
            _WORLD,
            _COUNT
        };

        public const float _playerMass = 1.0f;
        public const float _enemyMass = 2.0f;
        public const float _monsterMass = 5.0f;
        public const float _worldMass = 10.0f;

        public const float _durabilityHealthStep = 1.5f;
        public const float _normalHealthStep = 1.0f;
        public static float _similarHealthStep = 0.2f;

        public EActorType _actorType = EActorType._NULL;
        public EActorType _actorElementType = EActorType._NULL;
        public EActorType _actorClassType = EActorType._NULL;

        public float _health = 1.0f;

        public Pax4ParticleEffectPart _particleEffectAura = null;
        public Pax4ParticleEffectPart _particleEffectTrail = null;
        public Pax4ParticleEffectPart _particleEffectExplosion = null;

        public EActorPowerUp _actorPowerUp = EActorPowerUp._NORMAL;

        public Pax4Actor(String p_name, Pax4Object p_parent0)
            : base(p_name, p_parent0)
        {            
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if(_particleEffectAura != null)
                _particleEffectAura.Trigger(true);

            if (_particleEffectTrail != null)
                _particleEffectTrail.Trigger(true);

            if (_body.Position.Y < -100.0f)
                Dx();
        }

        public virtual void SetPowerUp(EActorPowerUp p_actorPowerUp)
        {
            _actorPowerUp = p_actorPowerUp;
            
            _health = 1.0f;

            switch (p_actorPowerUp)
            {
                case EActorPowerUp._NORMAL:
                    _health = 1.0f;
                    break;

                case EActorPowerUp._DURABILITY:
                    if (_actorType == EActorType._ENEMY)
                        break;
                        
                     _health = 2.0f;
                     ((Pax4SoundLavaAndIce)Pax4Sound._current)._lavaandiceTrumpet.Play();
                    
                    break;
            }
        }

        public override void Enable()
        {
            base.Enable();

            if (_particleEffectAura != null)
                _particleEffectAura.Enable();
        }

        public override void Disable()
        {
            base.Disable();
        }

        public override void Dx()
        {
            switch(this._actorElementType)
            {
                case EActorType._LAVA:            
                    if (_actorPowerUp == EActorPowerUp._DURABILITY)
                        ((Pax4SoundLavaAndIce)Pax4Sound._current)._lavaandiceLavaExplosion1.Play();
                    else
                        ((Pax4SoundLavaAndIce)Pax4Sound._current)._lavaandiceLavaExplosion.Play();                    
                    break;

                case EActorType._ICE:            
                    if (_actorPowerUp == EActorPowerUp._DURABILITY)
                        ((Pax4SoundLavaAndIce)Pax4Sound._current)._lavaandiceIceExplosion1.Play();
                    else
                        ((Pax4SoundLavaAndIce)Pax4Sound._current)._lavaandiceIceExplosion.Play();
                    break;
            }

            if (_particleEffectExplosion != null)
            {
                _particleEffectExplosion.Trigger(true);

                if (this._actorClassType == EActorType._MONSTER)
                {
                    float offsetMax = 50.0f;
                    _particleEffectExplosion.Trigger(true, offsetMax);
                    _particleEffectExplosion.Trigger(true, offsetMax);
                    _particleEffectExplosion.Trigger(true, offsetMax);
                    _particleEffectExplosion.Trigger(true, offsetMax);

                    //Pax4Camera._current.Shake();
                }
            }

            base.Dx();
        }
    }
}