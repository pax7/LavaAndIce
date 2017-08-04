using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Pax4.JigLibX.Collision;
using System.Runtime.Serialization;

namespace Pax4.Core
{
    [DataContract]
    [KnownType(typeof(Pax4ActorEnemyMonster))]
    public class Pax4ActorEnemyMonster : Pax4Actor
    {
        public static float _defaultHealth = 3.0f;

        public bool _shieldDown = false;

        public Pax4ParticleEffectPart _particleEffectAura1 = null;
        public Pax4ParticleEffectPart _particleEffectAura2 = null;

        public Pax4ActorEnemyMonster(String p_name, Pax4Object p_parent0)
            : base(p_name, p_parent0)
        {
            _actorType = EActorType._ENEMY;
            _actorClassType = EActorType._MONSTER;

            _health = _defaultHealth;

            SetMass(_monsterMass);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (_particleEffectAura1 != null)
                _particleEffectAura1.Trigger(false);
            if (_particleEffectAura2 != null)
                _particleEffectAura2.Trigger(false);
        }

        public override void Enable()
        {
            base.Enable();

            _collisionSkin.callbackFn += new CollisionCallbackFn(HandleCollisionDetection);

            if (_particleEffectAura1 != null)
                _particleEffectAura1.Enable();
            if (_particleEffectAura2 != null)
                _particleEffectAura2.Enable();
        }

        public virtual bool HandleCollisionDetection(CollisionSkin p_this, CollisionSkin p_other)
        {
            if (_dxRequested)
                return false;

            Pax4ObjectPhysicsPart other = (Pax4ObjectPhysicsPart)p_other._pax4Object;

            if (((Pax4Actor)other)._actorType == EActorType._ENEMY
                || ((Pax4Actor)other)._actorType == EActorType._WORLD)
            {
                return false;
            }

            return true;
        }

        public virtual void SetShieldDown(bool p_shieldDown = true)
        {
            _shieldDown = p_shieldDown;

            if (_shieldDown)
            {
                _particleEffectAura.Disable();
                _particleEffectAura1.Disable();
                _particleEffectAura2.Disable();                
            }
            else
            {
                _particleEffectAura.Enable();
                _particleEffectAura1.Enable();
                _particleEffectAura2.Enable();
            }
        }

        public override void Dx()
        {
            base.Dx();

            _similarHealthStep = 1.0f;//reset such that either ammo can kill enemy ammo
        }
    }
}