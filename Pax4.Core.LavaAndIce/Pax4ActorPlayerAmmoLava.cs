using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Runtime.Serialization;

namespace Pax4.Core
{
    [DataContract]
    [KnownType(typeof(Pax4ActorPlayerAmmoLava))]
    public class Pax4ActorPlayerAmmoLava : Pax4ActorPlayerAmmo
    {
        public static Pax4ActorPlayerAmmoLava _current = null;

        public static float _scaleFactor = 2.6f;
        public static Vector3 _prelaunchPosition = new Vector3(-2.5f, -13.5f, 0.0f);
        public static Pax4WayPointPath _prelaunchWayPoint = null;
        public static Pax4WayPointControllerActor _wayPointController = null;
        public static EActorPowerUp _powerUp = EActorPowerUp._NORMAL;

        public Pax4ActorPlayerAmmoLava(String p_name, Pax4Object p_parent0, int p_modelIndex = -1)
            : base(p_name, p_parent0)
        {
            SetScale(_scaleFactor * Vector3.One);

            _actorElementType = EActorType._LAVA;            

            if (p_modelIndex < 0)
                SetModel("Model/lavaandiceAmmoLava");

            _current = this;
            _wayPointController.SetPhysicsPart(this);

            SetPowerUp(_powerUp);
            _powerUp = EActorPowerUp._NORMAL;

            MoveTo(Vector3.Zero, _prelaunchPosition + Vector3.Down * _scaleFactor * 2.0f);

            Enable();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            Launch();
        }

        public override void Dx()
        {
            base.Dx();

            if (this == _current)
                _current = null;
        }

        public override void SetPowerUp(EActorPowerUp p_actorPowerUp)
        {
            base.SetPowerUp(p_actorPowerUp);

            switch (p_actorPowerUp)
            {
                case EActorPowerUp._NORMAL:
                    _particleEffectTrail = new Pax4ParticleEffectPart("_particleEffectTrail", this);
                    _particleEffectTrail.Ini(((Pax4ParticleEffectLavaAndIce)Pax4ParticleEffect._current)._particleEffectLavaTrail);

                    _particleEffectExplosion = new Pax4ParticleEffectPart("_particleEffectExplosion", this);
                    _particleEffectExplosion.Ini(((Pax4ParticleEffectLavaAndIce)Pax4ParticleEffect._current)._particleEffectLavaExplosion);
                    break;

                case EActorPowerUp._DURABILITY:
                    _particleEffectTrail = new Pax4ParticleEffectPart("_particleEffectTrail", this);
                    _particleEffectTrail.Ini(((Pax4ParticleEffectLavaAndIce)Pax4ParticleEffect._current)._particleEffectLavaTrail1);

                    _particleEffectExplosion = new Pax4ParticleEffectPart("_particleEffectExplosion", this);
                    _particleEffectExplosion.Ini(((Pax4ParticleEffectLavaAndIce)Pax4ParticleEffect._current)._particleEffectLavaExplosion);
                    break;
            }
        }

        public void Launch()
        {
            if (!_spawning)
            {
                if (_body.Position.Y < Pax4ActorWorld._fudgeFactor / _destroyLowerThresholdFactor)
                {
                    Dx();
                }
                else if ((Pax4WorldLavaAndIce._missionType == Pax4WorldLavaAndIce.ELavaAndIceMissionType._LAVA
                          || Pax4WorldLavaAndIce._missionType == Pax4WorldLavaAndIce.ELavaAndIceMissionType._LAVA_AND_ICE)
                        && Pax4UiStateLavaAndIceMission._currentMissionState._fg
                        && Pax4Touch._current._currentTouchState._oneTouch == true
                        && !_launched
                        && Pax4UiStateLavaAndIceMission._currentMissionState._lavaLauncher._toggle
                        && Pax4Touch._current._currentTouchState._xy.Y > _tapLaunchThreshold
                        && !Pax4UiStateLavaAndIceMission._currentMissionState._lavaLauncher.Touched())
                {
                    if (Pax4UiStateLavaAndIceMission._currentMissionState._iceLauncher == null
                        || (Pax4UiStateLavaAndIceMission._currentMissionState._iceLauncher != null
                            && !Pax4UiStateLavaAndIceMission._currentMissionState._iceLauncher.Touched())
                        )
                    {
                        Vector3 _playerAmmoImpulse = Pax4Tools.WorldToScreen(_body.Position) - Pax4Touch._current._currentTouchState._xy;
                        _playerAmmoImpulse.X = -_playerAmmoImpulse.X;

                        DisableConstraint();

                        _playerAmmoImpulse.Normalize();
                        _playerAmmoImpulse *= _playerAmmoMaxImpulseLength;

                        _body.ApplyBodyWorldImpulse(_playerAmmoImpulse, Vector3.Zero);

                        if (_actorPowerUp == EActorPowerUp._DURABILITY)
                            ((Pax4SoundLavaAndIce)Pax4Sound._current)._lavaandiceLavaLaunch1.Play();
                        else
                            ((Pax4SoundLavaAndIce)Pax4Sound._current)._lavaandiceLavaLaunch.Play();

                        _launched = true;//false this for remote control
                    }
                }
            }
            else
            {
                if (_body.Position.Y >= Pax4ActorWorld._fudgeFactor / _destroyLowerThresholdFactor)
                    _spawning = false;
            }
        }  
    }
}