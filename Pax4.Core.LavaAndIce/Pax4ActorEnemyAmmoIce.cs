using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Runtime.Serialization;

namespace Pax4.Core
{
    [DataContract]
    [KnownType(typeof(Pax4ActorEnemyAmmoIce))]
    public class Pax4ActorEnemyAmmoIce : Pax4ActorEnemyAmmo
    {
        public static List<Pax4Object> _current = new List<Pax4Object>();

        public Pax4ActorEnemyAmmoIce(String p_name, Pax4Object p_parent0, int p_modelIndex)
            : base(p_name, p_parent0)
        {
            _actorElementType = EActorType._ICE;

            SetModel("Model/lavaandiceEnemyIce" + p_modelIndex);

            _particleEffectExplosion = new Pax4ParticleEffectPart("_particleEffectExplosion", this);
            _particleEffectExplosion.Ini(((Pax4ParticleEffectLavaAndIce)Pax4ParticleEffect._current)._particleEffectIceExplosion);
        }

        public override void Enable()
        {
            base.Enable();
            _current.Add(this);
        }

        public override void Disable()
        {
            base.Disable();
            _current.Remove(this);
        }

        public override void SetPowerUp(EActorPowerUp p_actorPowerUp)
        {
            base.SetPowerUp(p_actorPowerUp);

            switch (p_actorPowerUp)
            {
                case EActorPowerUp._NORMAL:
                    _particleEffectTrail = null;
                    break;

                case EActorPowerUp._DURABILITY:
                    _particleEffectTrail = new Pax4ParticleEffectPart("_particleEffectTrail", this);
                    _particleEffectTrail.Ini(((Pax4ParticleEffectLavaAndIce)Pax4ParticleEffect._current)._particleEffectIceTrailEnemy);
                    break;
            }
        }
    }
}