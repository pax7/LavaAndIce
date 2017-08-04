using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Pax4.JigLibX.Collision;
using System.Runtime.Serialization;

namespace Pax4.Core
{
    [DataContract]
    [KnownType(typeof(Pax4ActorEnemyAmmo))]
    public class Pax4ActorEnemyAmmo : Pax4Actor
    {
        public static float _scaleFactor = 1.5f;

        public Pax4ActorEnemyAmmo(String p_name, Pax4Object p_parent0)
            : base(p_name, p_parent0)
        {
            _actorType = EActorType._ENEMY;
            _actorClassType = EActorType._AMMO;

            SetScale(_scaleFactor * Vector3.One);

            SetMass(_enemyMass);
        }        

        public override void Enable()
        {
            base.Enable();

            _collisionSkin.callbackFn += new CollisionCallbackFn(HandleCollisionDetection);
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
    }
}