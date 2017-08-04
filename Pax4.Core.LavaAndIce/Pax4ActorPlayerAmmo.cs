using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Pax4.JigLibX.Physics;
using Pax4.JigLibX.Collision;
using Pax4.JigLibX.Geometry;
using Pax4.JigLibX.Math;
using System.Runtime.Serialization;

namespace Pax4.Core
{
    [DataContract]
    [KnownType(typeof(Pax4ActorPlayerAmmo))]
    public class Pax4ActorPlayerAmmo : Pax4Actor
    {
        public const float _playerAmmoMaxImpulseLength = 40.0f;
        public const float _playerAmmoMinImpulseLength = 15.0f;
        public const float _playerAmmoSpawnVelocity = 5.5f;

        public const float _destroyLowerThresholdFactor = -2.5f;
        public static float _tapLaunchThreshold = 50.0f;   

        public bool _spawning = true;
        public bool _prelaunch = false;
        public bool _launched = false;

        public Pax4ActorPlayerAmmo(String p_name, Pax4Object p_parent0)
            : base(p_name, p_parent0)
        {
            _actorType = EActorType._PLAYER;
            _actorClassType = EActorType._AMMO;
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

            if (((Pax4Actor)other)._actorType == EActorType._WORLD)
                return true;

            Vector3 effectPosition = Pax4Tools.WorldToScreen(other._body.Position);

            if (((Pax4Actor)other)._actorType != EActorType._PLAYER)
            {
                if (this._actorElementType != ((Pax4Actor)other)._actorElementType)
                {
                    this._health -= _normalHealthStep;
                    if (this._health <= 0.0f)
                        this.RequestDx();

                    if (this._actorPowerUp == EActorPowerUp._DURABILITY)
                    {
                        if (this._actorElementType == EActorType._LAVA)
                            ((Pax4SoundLavaAndIce)Pax4Sound._current)._lavaandiceBurning1.Play();
                        else if (this._actorElementType == EActorType._ICE)
                            ((Pax4SoundLavaAndIce)Pax4Sound._current)._lavaandiceFreezing1.Play();

                        return HandleCollisionDetectionOther(effectPosition, (Pax4Actor)other, _durabilityHealthStep);
                    }
                    else if (this._actorPowerUp == EActorPowerUp._NORMAL)
                    {
                        if (((Pax4Actor)other)._actorPowerUp != EActorPowerUp._NULL && ((Pax4Actor)other)._actorClassType == EActorType._AMMO)
                        {
                            if (this == Pax4ActorPlayerAmmoLava._current)
                                Pax4ActorPlayerAmmoLava._powerUp = ((Pax4Actor)other)._actorPowerUp;
                            else if (this == Pax4ActorPlayerAmmoIce._current)
                                Pax4ActorPlayerAmmoIce._powerUp = ((Pax4Actor)other)._actorPowerUp;
                        }

                        return HandleCollisionDetectionOther(effectPosition, (Pax4Actor)other, _normalHealthStep);
                    }
                }
                else// (this._actorElementType == other._actorElementType)
                {
                    if (this._actorElementType == EActorType._LAVA)
                        ((Pax4SoundLavaAndIce)Pax4Sound._current)._lavaandiceBurning.Play();
                    else if (this._actorElementType == EActorType._ICE)
                        ((Pax4SoundLavaAndIce)Pax4Sound._current)._lavaandiceFreezing.Play();

                    this._health -= _similarHealthStep;
                    if (this._health <= 0.0f)
                        this.RequestDx();

                    if (_similarHealthStep >= 1.0f)
                        return HandleCollisionDetectionOther(effectPosition, (Pax4Actor)other, _durabilityHealthStep);
                    else if (this._actorPowerUp == EActorPowerUp._DURABILITY)
                        return false;
                }
            }
            else if (((Pax4Actor)other)._actorType == EActorType._PLAYER
                     && Pax4ActorPlayerAmmoLava._current._launched
                     && Pax4ActorPlayerAmmoIce._current._launched)
            {

                ((Pax4ParticleEffectLavaAndIce)Pax4ParticleEffect._current)._particleEffectScore300.Trigger(ref effectPosition);
                ((Pax4ParticleEffectLavaAndIce)Pax4ParticleEffect._current)._particleEffectScoreHelpingHand.Trigger(ref effectPosition);

                Pax4UiStateLavaAndIceMission._currentMissionState._scoreSpriteModifier.Ini(((Pax4WorldLavaAndIce)Pax4World._current)._score, ((Pax4WorldLavaAndIce)Pax4World._current)._score + 300, true, 1.0f);
                Pax4UiStateLavaAndIceMission._currentMissionState._scoreSpriteModifier.Trigger();
                ((Pax4WorldLavaAndIce)Pax4World._current)._score += 300;
            }

            return true;
        }

        private bool HandleCollisionDetectionOther(Vector3 p_effectPosition, Pax4Actor p_other = null, float p_healthStep = _normalHealthStep)
        {
            bool result = true;

            if (p_other == Pax4ActorEnemyMonsterLava._current)
            {
                if (Pax4ActorEnemyMonsterLava._current._shieldDown)
                {
                    ((Pax4Actor)p_other)._health -= p_healthStep;
                    ((Pax4WorldLavaAndIce)Pax4World._current).AutoBalance();

                    ((Pax4ParticleEffectLavaAndIce)Pax4ParticleEffect._current)._particleEffectScore500.Trigger(ref p_effectPosition);
                    Pax4UiStateLavaAndIceMission._currentMissionState._scoreSpriteModifier.Ini(((Pax4WorldLavaAndIce)Pax4World._current)._score, ((Pax4WorldLavaAndIce)Pax4World._current)._score + 500, true, 1.0f);
                    Pax4UiStateLavaAndIceMission._currentMissionState._scoreSpriteModifier.Trigger();
                    ((Pax4WorldLavaAndIce)Pax4World._current)._score += 500;
                }
                else
                {
                    ((Pax4ParticleEffectLavaAndIce)Pax4ParticleEffect._current)._particleEffectScoreShieldUp.Trigger(ref p_effectPosition);
                }
            }
            else if (p_other == Pax4ActorEnemyMonsterIce._current)
            {
                if (Pax4ActorEnemyMonsterIce._current._shieldDown)
                {
                    ((Pax4Actor)p_other)._health -= p_healthStep;
                    ((Pax4WorldLavaAndIce)Pax4World._current).AutoBalance();

                    ((Pax4ParticleEffectLavaAndIce)Pax4ParticleEffect._current)._particleEffectScore500.Trigger(ref p_effectPosition);
                    Pax4UiStateLavaAndIceMission._currentMissionState._scoreSpriteModifier.Ini(((Pax4WorldLavaAndIce)Pax4World._current)._score, ((Pax4WorldLavaAndIce)Pax4World._current)._score + 500, true, 1.0f);
                    Pax4UiStateLavaAndIceMission._currentMissionState._scoreSpriteModifier.Trigger();
                    ((Pax4WorldLavaAndIce)Pax4World._current)._score += 500;
                }
                else
                {
                    ((Pax4ParticleEffectLavaAndIce)Pax4ParticleEffect._current)._particleEffectScoreShieldUp.Trigger(ref p_effectPosition);
                }
            }
            else
            {
                ((Pax4Actor)p_other)._health -= p_healthStep;

                ((Pax4ParticleEffectLavaAndIce)Pax4ParticleEffect._current)._particleEffectScore100.Trigger(ref p_effectPosition);
                Pax4UiStateLavaAndIceMission._currentMissionState._scoreSpriteModifier.Ini(((Pax4WorldLavaAndIce)Pax4World._current)._score, ((Pax4WorldLavaAndIce)Pax4World._current)._score + 100, true, 1.0f);
                Pax4UiStateLavaAndIceMission._currentMissionState._scoreSpriteModifier.Trigger();
                ((Pax4WorldLavaAndIce)Pax4World._current)._score += 100;
            }

            if (p_other._actorClassType != EActorType._MONSTER && p_other._actorElementType == ((Pax4WorldLavaAndIce)Pax4World._current)._elementType1)
            {
                ((Pax4ParticleEffectLavaAndIce)Pax4ParticleEffect._current)._particleEffectScoreGreat.Trigger(ref p_effectPosition);
                ((Pax4ParticleEffectLavaAndIce)Pax4ParticleEffect._current)._particleEffectScore300.Trigger(ref p_effectPosition);
                int score = 300;
                ((Pax4WorldLavaAndIce)Pax4World._current)._score += score;

                if (p_other._actorElementType == ((Pax4WorldLavaAndIce)Pax4World._current)._elementType0)
                {
                    ((Pax4ParticleEffectLavaAndIce)Pax4ParticleEffect._current)._particleEffectScorePerfect.Trigger(ref p_effectPosition);
                    ((Pax4ParticleEffectLavaAndIce)Pax4ParticleEffect._current)._particleEffectScore500.Trigger(ref p_effectPosition);
                    score += 500;                    
                    ((Pax4WorldLavaAndIce)Pax4World._current)._score += 500;
                }

                Pax4UiStateLavaAndIceMission._currentMissionState._scoreSpriteModifier.Ini(((Pax4WorldLavaAndIce)Pax4World._current)._score, ((Pax4WorldLavaAndIce)Pax4World._current)._score + score, true, 1.0f);
                Pax4UiStateLavaAndIceMission._currentMissionState._scoreSpriteModifier.Trigger();
            }

            if (((Pax4Actor)p_other)._health <= 0.0f)
            {
                if (p_other._actorClassType == EActorType._MONSTER)
                    ((Pax4WorldLavaAndIce)Pax4World._current)._monsterKills++;
                else if (p_other._actorClassType == EActorType._AMMO && p_other._actorElementType == EActorType._LAVA)
                    ((Pax4WorldLavaAndIce)Pax4World._current)._lavaKills++;
                else if (p_other._actorClassType == EActorType._AMMO && p_other._actorElementType == EActorType._ICE)
                    ((Pax4WorldLavaAndIce)Pax4World._current)._iceKills++;

                ((Pax4Actor)p_other).RequestDx();
                result = false;

                ((Pax4WorldLavaAndIce)Pax4World._current)._elementType0 = ((Pax4WorldLavaAndIce)Pax4World._current)._elementType1;
                ((Pax4WorldLavaAndIce)Pax4World._current)._elementType1 = p_other._actorElementType;

                if (this._actorPowerUp == EActorPowerUp._NORMAL)
                {
                    if (this == Pax4ActorPlayerAmmoLava._current)
                        Pax4ActorPlayerAmmoLava._powerUp = ((Pax4Actor)p_other)._actorPowerUp;
                    else if (this == Pax4ActorPlayerAmmoIce._current)
                        Pax4ActorPlayerAmmoIce._powerUp = ((Pax4Actor)p_other)._actorPowerUp;
                }
            }

            return result;
        }              
    }
}