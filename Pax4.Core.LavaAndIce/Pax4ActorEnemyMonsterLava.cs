using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Runtime.Serialization;

namespace Pax4.Core
{
    [DataContract]
    [KnownType(typeof(Pax4ActorEnemyMonsterLava))]
    public class Pax4ActorEnemyMonsterLava: Pax4ActorEnemyMonster
    {
        public static Pax4ActorEnemyMonsterLava _current = null;

        public static float _scaleFactor = 5.5f;

        public Pax4ActorEnemyMonsterLava(String p_name, Pax4Object p_parent0, int p_modelIndex)
            : base(p_name, p_parent0)
        {
            SetScale(_scaleFactor * Vector3.One);

            _actorElementType = EActorType._LAVA;

            SetModel("Model/lavaandiceEnemyLava" + p_modelIndex);

            float scaleFactor = 0.2f;

            _particleEffectAura = new Pax4ParticleEffectPart("_particleEffectAura", this);
            _particleEffectAura.Ini(((Pax4ParticleEffectLavaAndIce)Pax4ParticleEffect._current)._particleEffectLavaMonsterShield);
            _particleEffectAura.SetScale(Vector3.One * scaleFactor);

            _particleEffectAura1 = new Pax4ParticleEffectPart("_particleEffectAura1", this);
            _particleEffectAura1.Ini(((Pax4ParticleEffectLavaAndIce)Pax4ParticleEffect._current)._particleEffectLavaMonsterShield);
            _particleEffectAura1.SetScaleRotation(Vector3.One * scaleFactor, new Vector3(0.0f, (float)Math.PI / 2.0f, 0.0f));

            _particleEffectAura2 = new Pax4ParticleEffectPart("_particleEffectAura2", this);
            _particleEffectAura2.Ini(((Pax4ParticleEffectLavaAndIce)Pax4ParticleEffect._current)._particleEffectLavaMonsterShield);
            _particleEffectAura2.SetScaleRotation(Vector3.One * scaleFactor, new Vector3((float)Math.PI / 2.0f, (float)Math.PI / 2.0f, 0.0f));

            _particleEffectExplosion = new Pax4ParticleEffectPart("_particleEffectExplosion", this);
            _particleEffectExplosion.Ini(((Pax4ParticleEffectLavaAndIce)Pax4ParticleEffect._current)._particleEffectLavaExplosion);

            _current = this;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public override void Dx()
        {
            base.Dx();

            if (this == _current)
                _current = null;
        }
    }
}