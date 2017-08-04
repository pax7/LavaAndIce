using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Pax4.JigLibX.Physics;
using Pax4.JigLibX.Collision;
using Pax4.JigLibX.Geometry;
using Pax4.JigLibX.Math;
using Pax.Core;
using System.Runtime.Serialization;

namespace Pax4.Core
{
    [DataContract]
    [KnownType(typeof(Pax4WorldLavaAndIce))]
    public class Pax4WorldLavaAndIce : Pax4World
    {
        public enum ELavaAndIceQuestType
        {
            _NULL,
            _LAVA_AND_ICE_PROLOGUE,
            _LAVA_AND_ICE_EQUILIBRIUM,
            _LAVA_AND_ICE_LAVA_GRAIL,
            _LAVA_AND_ICE_ICE_GRAIL,
            _LAVA_AND_ICE_DRAGONS,
            _COUNT
        };

        public enum ELavaAndIceMissionType
        {
            _NULL,
            _LAVA,
            _ICE,
            _LAVA_AND_ICE,
            _COUNT
        };

        public enum EPopulateWithActorsType
        {
            _NULL,
            _HOMOGENEOUS,       //all are the same
            _HETEROGENEOUS,     //alternating
            _LAYERED,           //half lava, half ice            
            _COUNT
        };

        public List<Pax4ObjectPhysicsPart> _physicsPartList = new List<Pax4ObjectPhysicsPart>();//helper thingie

        public static ELavaAndIceQuestType _questType = ELavaAndIceQuestType._NULL;
        public static ELavaAndIceMissionType _missionType = ELavaAndIceMissionType._NULL;

        public int _lavaIndex = 1;
        public int _iceIndex = 1;
        public static int _missionIndex = 0;

        public bool _playerAmmoLavaEnabled = false;
        public bool _playerAmmoIceEnabled = false;

        //public delegate void UpdateDelegate(GameTime gameTime);
        //public UpdateDelegate _updateDelegate = null;

        public bool _autoBalance = false;

        public float _temperature = 0.5f;
        public float _defaultTemperature = 0.5f;
        public float _temperatureStep = 0.0f;
        public float _midTemperatureThreshold = 0.5f;

        public static float _difficultyTimer = 0.0f;
        public static float _difficulty = _difficultyNormal;

        public int _score = 0;
        public int _highScore = 0;
        public int _lavaKills = 0;
        public int _iceKills = 0;
        public int _monsterKills = 0;

        public const float _difficultyEasy = 0.75f;
        public const float _difficultyNormal = 1.00f;
        public const float _difficultyHard = 1.50f;//1.5f
        public const float _difficultyNightmare = 2.00f;//2.0f

        public int _missionCount = 0;
        public const int _maxMissions = 32;

        public bool _winConditionPending = true;
        public float _winConditionTimer = 3.5f;
        public bool _winConditionTimerRunning = true;
        public String _winConditionStateName = null;

        public Pax4Actor.EActorType _elementType0 = Pax4Actor.EActorType._LAVA;
        public Pax4Actor.EActorType _elementType1 = Pax4Actor.EActorType._ICE;

        public Vector3 _playerAmmoImpulse = Vector3.Zero;

        public int _powerUpRatio = 4;

        public Pax4WorldLavaAndIce(String p_name, PaxState p_parent0)
            : base(p_name, p_parent0)
        {
            if (Pax4ActorPlayerAmmoLava._current != null)
                Pax4ActorPlayerAmmoLava._current.Dx();
            Pax4ActorPlayerAmmoLava._current = null;

            if (Pax4ActorPlayerAmmoIce._current != null)
                Pax4ActorPlayerAmmoIce._current.Dx();
            Pax4ActorPlayerAmmoIce._current = null;

            if (Pax4ActorEnemyMonsterLava._current != null)
                Pax4ActorEnemyMonsterLava._current.Dx();
            Pax4ActorEnemyMonsterLava._current = null;

            if (Pax4ActorEnemyMonsterIce._current != null)
                Pax4ActorEnemyMonsterIce._current.Dx();
            Pax4ActorEnemyMonsterIce._current = null;

            Pax4ActorEnemyAmmoLava._current.Clear();
            Pax4ActorEnemyAmmoIce._current.Clear();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            UpdateScoreEffect(gameTime);

            SpawnPlayerAmmo(gameTime);

            if (_updateDelegate != null)
                _updateDelegate(gameTime);

            UpdateWinCondition(gameTime);

            if (_winConditionPending)
                UpdateTemperature(gameTime);
        }

        public void SpawnPlayerAmmo(GameTime gameTime)
        {
            if (_playerAmmoLavaEnabled && Pax4ActorPlayerAmmoLava._current == null)
                new Pax4ActorPlayerAmmoLava("ammoLava", null, -1);

            if (_playerAmmoIceEnabled && Pax4ActorPlayerAmmoIce._current == null)
                new Pax4ActorPlayerAmmoIce("ammoIce", null, -1);
        }

        public void UpdateScoreEffect(GameTime gameTime)
        {
            if (!Pax4UiStateLavaAndIceMission._currentMissionState._scoreSpriteModifier._done)
                Pax4UiStateLavaAndIceMission._currentMissionState._scoreSpriteModifier.Update(gameTime);
        }

        public void UpdateTemperature(GameTime gameTime)
        {
            _temperature = _defaultTemperature;

            _temperatureStep = 1.0f / (Pax4ActorEnemyAmmoLava._current.Count + Pax4ActorEnemyAmmoIce._current.Count);

            _temperature = _defaultTemperature + _temperatureStep * (float)(Pax4ActorEnemyAmmoLava._current.Count - Pax4ActorEnemyAmmoIce._current.Count);

            if (Pax4ActorEnemyMonsterLava._current != null)
            {
                if (_temperature <= 0.25f)
                {
                    //_monsterLava.DisableBoneAuraParticleEffect();
                    Pax4ActorEnemyMonsterLava._current.SetShieldDown();
                }
                else if (_temperature > 0.25f && Pax4ActorEnemyMonsterLava._current._shieldDown)
                {
                    //_monsterLava.EnableBoneAuraParticleEffect();
                    Pax4ActorEnemyMonsterLava._current.SetShieldDown(false);
                }

                Pax4UiLavaAndIceMissionHealth.SetLavaHealth(Pax4ActorEnemyMonsterLava._current._health);
            }
            else
            {
                Pax4UiLavaAndIceMissionHealth.SetLavaHealth(0.0f);
            }

            if (Pax4ActorEnemyMonsterIce._current != null)
            {
                if (_temperature >= 0.75f)
                {
                    //_monsterIce.DisableBoneAuraParticleEffect();
                    Pax4ActorEnemyMonsterIce._current.SetShieldDown();
                }
                else if (_temperature < 0.75f && Pax4ActorEnemyMonsterIce._current._shieldDown)
                {
                    //_monsterIce.EnableBoneAuraParticleEffect();
                    Pax4ActorEnemyMonsterIce._current.SetShieldDown(false);
                }

                Pax4UiLavaAndIceMissionHealth.SetIceHealth(Pax4ActorEnemyMonsterIce._current._health);
            }
            else
            {
                Pax4UiLavaAndIceMissionHealth.SetIceHealth(0.0f);
            }

            Pax4UiLavaAndIceMissionThermometer.SetTemperature(_temperature);

            if (_autoBalance)
            {
                Pax4Actor actor = null;
                int nextAvailableWayPointPathIndex = GetNextAvailableWayPointPathIndex();

                if (_temperature < _midTemperatureThreshold)
                {
                    //add random power-ups to 20% of created enemy ammo
                    if (Pax4ActorEnemyMonsterLava._current != null && nextAvailableWayPointPathIndex >= 0)
                    {
                        actor = new Pax4ActorEnemyAmmoLava(_lavaIndex.ToString(), null, _lavaIndex);
                        actor.MoveTo(Vector3.Zero, Pax4ActorEnemyMonsterLava._current._body.Position);
                    }
                }
                else if (_temperature > _midTemperatureThreshold)
                {
                    //add random power-ups to 20% of created enemy ammo
                    if (Pax4ActorEnemyMonsterIce._current != null && nextAvailableWayPointPathIndex >= 0)
                    {
                        actor = new Pax4ActorEnemyAmmoIce(_iceIndex.ToString(), null, _iceIndex);
                        actor.MoveTo(Vector3.Zero, Pax4ActorEnemyMonsterIce._current._body.Position);
                    }
                }

                if (actor != null)
                {
                    new Pax4WayPointControllerActor(actor, 2.0f, _wayPointPath[nextAvailableWayPointPathIndex], _wayPointPath[nextAvailableWayPointPathIndex].GetRandomWayPointIndex());
                    actor.Enable();
                }

                if (_temperature >= 0.48f && _temperature <= 0.52f)
                    _autoBalance = false;
            }
        }

        public void UpdateWinCondition(GameTime gameTime)
        {
            //_winConditionPending = true;//!*disable this
            if (_winConditionPending)
            {
                //Pax4Ui._current.Enter("victory");
                //_winConditionPending = false;

                if (!Pax4UiStateLavaAndIceMission._currentMissionState._missionTimer._timerDisabled
                    && Pax4UiStateLavaAndIceMission._currentMissionState._missionTimer._done)
                {
                    _winConditionStateName = "defeat";
                    _winConditionPending = false;
                }

                if (Pax4ActorEnemyMonsterLava._current == null && Pax4ActorEnemyMonsterIce._current == null)
                {
                    _winConditionStateName = "victory";
                    _winConditionPending = false;
                }

                switch (_missionType) //update these conditions with power-ups that will be added later
                {
                    case ELavaAndIceMissionType._LAVA:
                        if (Pax4ActorEnemyAmmoLava._current.Count <= 0 && Pax4ActorEnemyMonsterIce._current != null)
                        {
                            Pax4Ui._current.Enter("defeat");
                            _winConditionPending = false;
                        }

                        break;

                    case ELavaAndIceMissionType._ICE:
                        if (Pax4ActorEnemyAmmoIce._current.Count <= 0 && Pax4ActorEnemyMonsterLava._current != null)
                        {
                            _winConditionStateName = "defeat";
                            _winConditionPending = false;
                        }

                        break;

                    case ELavaAndIceMissionType._LAVA_AND_ICE:
                        if (Pax4ActorEnemyMonsterLava._current != null && Pax4ActorEnemyMonsterIce._current == null && Pax4ActorEnemyAmmoIce._current.Count <= 0)
                        {
                            _winConditionStateName = "defeat";
                            _winConditionPending = false;
                        }
                        if (Pax4ActorEnemyMonsterIce._current != null && Pax4ActorEnemyMonsterLava._current == null && Pax4ActorEnemyAmmoLava._current.Count <= 0)
                        {
                            _winConditionStateName = "defeat";
                            _winConditionPending = false;
                        }
                        if (Pax4ActorEnemyMonsterLava._current != null && Pax4ActorEnemyMonsterIce._current != null && (Pax4ActorEnemyAmmoLava._current.Count + Pax4ActorEnemyAmmoIce._current.Count) <= 0)
                        {
                            _winConditionStateName = "defeat";
                            _winConditionPending = false;
                        }

                        break;
                }
            }
            else// if (!_winConditionPending)
            {
                _winConditionTimer -= (float)gameTime.ElapsedGameTime.TotalSeconds;
                if (_winConditionTimer <= 0.0f && _winConditionTimerRunning)
                {
                    _winConditionTimerRunning = false;
                    Pax4Ui._current.Enter(_winConditionStateName);
                }
            }
        }

        public void Enter(int p_missionIndex = -1)
        {
            Pax4World._current = this;

            Pax4Sound._current.PlayRandomSong();

            if (p_missionIndex >= 0)
                _missionIndex = p_missionIndex;

            Enable();

            ((Pax4WorldLavaAndIce)Pax4World._current)._temperature = _defaultTemperature;
        }

        public override void Enable()
        {
            base.Enable();

            Pax4Game._pause = true;
        }

        public virtual void AutoBalance()
        {
            _autoBalance = true;
        }

        public void IniSingle()
        {
            //waypoint
            //player ammo**
            Pax4ActorPlayerAmmoLava._prelaunchPosition = new Vector3(0.0f, -13.5f, 0.0f);
            Pax4ActorPlayerAmmoIce._prelaunchPosition = new Vector3(0.0f, -13.5f, 0.0f);

            int x = Pax4Camera._backBufferWidth / 4 + Pax4Camera._backBufferWidth / 8;
            int y = Pax4Camera._backBufferHeight - Pax4Camera._backBufferHeight / 5;
            int width = Pax4Camera._backBufferWidth / 4;
            int height = Pax4Camera._backBufferHeight / 4;

            Pax4ActorPlayerAmmoLava._prelaunchWayPoint = new Pax4WayPointPath();
            Pax4ActorPlayerAmmoLava._prelaunchWayPoint.GenerateWayPoint(Pax4ActorPlayerAmmoLava._prelaunchPosition);

            Pax4ActorPlayerAmmoIce._prelaunchWayPoint = new Pax4WayPointPath();
            Pax4ActorPlayerAmmoIce._prelaunchWayPoint.GenerateWayPoint(Pax4ActorPlayerAmmoIce._prelaunchPosition);

            Pax4ActorPlayerAmmoLava._wayPointController = new Pax4WayPointControllerActor(null, Pax4ActorPlayerAmmoLava._playerAmmoSpawnVelocity, Pax4ActorPlayerAmmoLava._prelaunchWayPoint);
            Pax4ActorPlayerAmmoIce._wayPointController = Pax4ActorPlayerAmmoLava._wayPointController;

            if (Pax4WorldLavaAndIce._missionType == ELavaAndIceMissionType._LAVA)
            {
                _playerAmmoLavaEnabled = true;
                _playerAmmoIceEnabled = false;

                Pax4UiStateLavaAndIceMission._currentMissionState._lavaLauncher._toggle = true;
            }
            else if (Pax4WorldLavaAndIce._missionType == ELavaAndIceMissionType._ICE)
            {
                _playerAmmoLavaEnabled = false;
                _playerAmmoIceEnabled = true;

                Pax4UiStateLavaAndIceMission._currentMissionState._iceLauncher._toggle = true;
            }

            Pax4Actor._similarHealthStep = 1.0f;
        }

        public void IniDual()
        {
            //waypoint
            //player ammo**
            Pax4ActorPlayerAmmoLava._prelaunchPosition = new Vector3(-2.5f, -13.5f, 0.0f);
            Pax4ActorPlayerAmmoIce._prelaunchPosition = new Vector3(2.5f, -13.5f, 0.0f);

            int x = Pax4Camera._backBufferWidth / 4;
            int y = Pax4Camera._backBufferHeight - Pax4Camera._backBufferHeight / 5;
            int width = Pax4Camera._backBufferWidth / 4;
            int height = Pax4Camera._backBufferHeight / 4;

            Pax4ActorPlayerAmmoLava._prelaunchWayPoint = new Pax4WayPointPath();
            Pax4ActorPlayerAmmoLava._prelaunchWayPoint.GenerateWayPoint(Pax4ActorPlayerAmmoLava._prelaunchPosition);
            Pax4ActorPlayerAmmoLava._wayPointController = new Pax4WayPointControllerActor(null, Pax4ActorPlayerAmmoLava._playerAmmoSpawnVelocity, Pax4ActorPlayerAmmoLava._prelaunchWayPoint);

            Pax4ActorPlayerAmmoIce._prelaunchWayPoint = new Pax4WayPointPath();
            Pax4ActorPlayerAmmoIce._prelaunchWayPoint.GenerateWayPoint(Pax4ActorPlayerAmmoIce._prelaunchPosition);
            Pax4ActorPlayerAmmoIce._wayPointController = new Pax4WayPointControllerActor(null, Pax4ActorPlayerAmmoIce._playerAmmoSpawnVelocity, Pax4ActorPlayerAmmoIce._prelaunchWayPoint);

            Pax4Actor._similarHealthStep = 0.1f;

            _playerAmmoLavaEnabled = true;
            _playerAmmoIceEnabled = true;

            Pax4UiStateLavaAndIceMission._currentMissionState._lavaLauncher._toggle = true;
            Pax4UiStateLavaAndIceMission._currentMissionState._iceLauncher._toggle = false;
        }

        public static Pax4WorldLavaAndIce CreateAndEnterQuest(int p_missioIndex = -1)
        {   
            Pax4Model._current.DxChild();

            Pax4WorldLavaAndIce quest = null;
            
            switch (Pax4WorldLavaAndIce._questType)
            {
                case ELavaAndIceQuestType._LAVA_AND_ICE_PROLOGUE:
                    quest = new Pax4WorldLavaAndIcePrologue("",null);
                    break;

                case ELavaAndIceQuestType._LAVA_AND_ICE_EQUILIBRIUM:
                    //quest = new Pax4WorldLavaAndIcePrologue(Vector3.Backward * Pax4WorldLavaAndIce._questPosition, Vector3.Zero, Vector3.One * Pax4WorldLavaAndIce._questScaleFactor, true);            
                    break;

                case ELavaAndIceQuestType._LAVA_AND_ICE_LAVA_GRAIL:
                    //quest = new Pax4WorldLavaAndIcePrologue(Vector3.Backward * Pax4WorldLavaAndIce._questPosition, Vector3.Zero, Vector3.One * Pax4WorldLavaAndIce._questScaleFactor, true);            
                    break;

                case ELavaAndIceQuestType._LAVA_AND_ICE_ICE_GRAIL:
                    //quest = new Pax4WorldLavaAndIcePrologue(Vector3.Backward * Pax4WorldLavaAndIce._questPosition, Vector3.Zero, Vector3.One * Pax4WorldLavaAndIce._questScaleFactor, true);            
                    break;

                case ELavaAndIceQuestType._LAVA_AND_ICE_DRAGONS:
                    //quest = new Pax4WorldLavaAndIcePrologue(Vector3.Backward * Pax4WorldLavaAndIce._questPosition, Vector3.Zero, Vector3.One * Pax4WorldLavaAndIce._questScaleFactor, true);            
                    break;
            }

            quest.Enter();

            return quest;
        }

        public void SetDifficultyTimer(int p_nightmare, int p_hard, int p_normal, int p_easy, bool p_timerDisabled = false)
        {
            if (Pax4WorldLavaAndIce._difficulty == Pax4WorldLavaAndIce._difficultyNightmare)
                _difficultyTimer = p_nightmare;
            else if (Pax4WorldLavaAndIce._difficulty == Pax4WorldLavaAndIce._difficultyHard)
                _difficultyTimer = p_hard;
            else if (Pax4WorldLavaAndIce._difficulty == Pax4WorldLavaAndIce._difficultyNormal)
                _difficultyTimer = p_normal;
            else if (Pax4WorldLavaAndIce._difficulty == Pax4WorldLavaAndIce._difficultyEasy)
                _difficultyTimer = p_easy;

            Pax4UiStateLavaAndIceMission._currentMissionState._missionTimer.Enable(_difficultyTimer);
            Pax4UiStateLavaAndIceMission._currentMissionState._missionTimer.Trigger();
            Pax4UiStateLavaAndIceMission._currentMissionState._missionTimer._timerDisabled = p_timerDisabled;
        }

        public void PopulateWithActors(List<Pax4ObjectPhysicsPart> p_result = null,
                                       EPopulateWithActorsType p_populateWithActorsType = EPopulateWithActorsType._HETEROGENEOUS, 
                                       bool p_lava = true, 
                                       Pax4WayPointPathList p_wayPointPaths = null, 
                                       float p_velocityFactor = 1.0f)
        {
            List<Pax4WayPointPath> wayPointPathList = null;

            if (p_wayPointPaths != null)
                wayPointPathList = p_wayPointPaths._wayPointPath;
            else
                wayPointPathList = _wayPointPath;

            if (wayPointPathList.Count <= 0)
                return;

            if (p_result != null)
                p_result.Clear();

            Pax4WayPointPath wayPointPath = null;
            Pax4Actor actor = null;

            int half = wayPointPathList.Count / 2;
            int halfLength = half;

            for (int wi = 0; wi < wayPointPathList.Count; wi++)
            {
                wayPointPath = wayPointPathList[wi];
                
                if (p_wayPointPaths == null)
                    halfLength = wayPointPath._wayPoint.Length / 2;

                if (wayPointPath._residentCount != 0) //this allows for multipe calls to PopulateWithActors for the glorious benefit of waves after wave after wave
                    continue;

                if (wayPointPath._locked)
                {
                    if (   Pax4WorldLavaAndIce._missionType == ELavaAndIceMissionType._LAVA
                        || Pax4WorldLavaAndIce._missionType == ELavaAndIceMissionType._LAVA_AND_ICE
                        && Pax4ActorEnemyMonsterIce._current == null)
                    {
                        Pax4ActorEnemyMonsterIce monsterIce = new Pax4ActorEnemyMonsterIce(_iceIndex.ToString(), null, _iceIndex);
                        monsterIce.MoveTo(Vector3.Zero, wayPointPath._wayPoint[0]);
                        new Pax4WayPointControllerActor(monsterIce, _difficulty, wayPointPath);
                        monsterIce.Enable();
                        continue;
                    }
                    else if (   Pax4WorldLavaAndIce._missionType == ELavaAndIceMissionType._ICE
                             || Pax4WorldLavaAndIce._missionType == ELavaAndIceMissionType._LAVA_AND_ICE
                             && Pax4ActorEnemyMonsterLava._current == null)
                    {
                        Pax4ActorEnemyMonsterLava monsterLava = new Pax4ActorEnemyMonsterLava(_lavaIndex.ToString(), null, _lavaIndex);
                        monsterLava.MoveTo(Vector3.Zero, wayPointPath._wayPoint[0]);
                        new Pax4WayPointControllerActor(monsterLava, _difficulty, wayPointPath);
                        monsterLava.Enable();
                        continue;
                    }
                }
                else
                {
                    switch (p_populateWithActorsType)
                    {
                        case EPopulateWithActorsType._HOMOGENEOUS:

                            if (wayPointPath._wayPoint.Length == 1)
                            {
                                if (p_lava)
                                {
                                    actor = new Pax4ActorEnemyAmmoLava(_lavaIndex.ToString(), null, _lavaIndex);
                                    actor.SetScale(Vector3.One * 1.8f);
                                }
                                else
                                {
                                    actor = new Pax4ActorEnemyAmmoIce(_iceIndex.ToString(), null, _iceIndex);
                                    actor.SetScale(Vector3.One * 1.8f);
                                }

                                if (wi % _powerUpRatio == 0)
                                    actor.SetPowerUp(Pax4Actor.EActorPowerUp._DURABILITY);

                                actor.MoveTo(Vector3.Zero, wayPointPath._wayPoint[0]);

                                new Pax4WayPointControllerActor(actor, Pax4WorldLavaAndIce._difficulty * 3.0f * p_velocityFactor, wayPointPath, 0);
                                actor.Enable();
                                if(p_result != null)
                                    p_result.Add(actor);
                            }
                            else
                            {
                                for (int i = 0; i < wayPointPath._wayPoint.Length; i++)
                                {
                                    if (i % 3 != 0)
                                        continue;

                                    if (p_lava)
                                    {
                                        actor = new Pax4ActorEnemyAmmoLava(_lavaIndex.ToString(), null, _lavaIndex);
                                        actor.SetScale(Vector3.One * 1.8f);
                                    }
                                    else
                                    {
                                        actor = new Pax4ActorEnemyAmmoIce(_iceIndex.ToString(), null, _iceIndex);
                                        actor.SetScale(Vector3.One * 1.8f);
                                    }

                                    actor.MoveTo(Vector3.Zero, wayPointPath._wayPoint[i]);

                                    if (i % _powerUpRatio == 0)
                                        actor.SetPowerUp(Pax4Actor.EActorPowerUp._DURABILITY);

                                    new Pax4WayPointControllerActor(actor, Pax4WorldLavaAndIce._difficulty * 3.0f * p_velocityFactor, wayPointPath, i);
                                    actor.Enable();
                                    if (p_result != null)
                                        p_result.Add(actor);
                                }
                            }

                            break;

                        case EPopulateWithActorsType._HETEROGENEOUS:

                            if (wayPointPath._wayPoint.Length == 1)
                            {
                                if (wi % 2 == 0)
                                {
                                    actor = new Pax4ActorEnemyAmmoLava(_lavaIndex.ToString(), null, _lavaIndex);
                                    actor.SetScale(Vector3.One * 1.8f);
                                }
                                else
                                {
                                    actor = new Pax4ActorEnemyAmmoIce(_iceIndex.ToString(), null, _iceIndex);
                                    actor.SetScale(Vector3.One * 1.8f);
                                }

                                if (wi % _powerUpRatio == 0)
                                    actor.SetPowerUp(Pax4Actor.EActorPowerUp._DURABILITY);

                                actor.MoveTo(Vector3.Zero, wayPointPath._wayPoint[0]);

                                new Pax4WayPointControllerActor(actor, Pax4WorldLavaAndIce._difficulty * 3.0f * p_velocityFactor, wayPointPath, 0);
                                actor.Enable();
                                if (p_result != null)
                                    p_result.Add(actor);
                            }
                            else
                            {
                                for (int i = 0; i < wayPointPath._wayPoint.Length; i++)
                                {
                                    if (i % 3 != 0)
                                        continue;

                                    if (i % 2 == 0)
                                    {
                                        actor = new Pax4ActorEnemyAmmoLava(_lavaIndex.ToString(), null, _lavaIndex);
                                        actor.SetScale(Vector3.One * 1.8f);
                                    }
                                    else
                                    {
                                        actor = new Pax4ActorEnemyAmmoIce(_iceIndex.ToString(), null, _iceIndex);
                                        actor.SetScale(Vector3.One * 1.8f);
                                    }

                                    actor.MoveTo(Vector3.Zero, wayPointPath._wayPoint[i]);

                                    if (i % _powerUpRatio == 0)
                                        actor.SetPowerUp(Pax4Actor.EActorPowerUp._DURABILITY);

                                    new Pax4WayPointControllerActor(actor, Pax4WorldLavaAndIce._difficulty * 3.0f * p_velocityFactor, wayPointPath, i);
                                    actor.Enable();
                                    if (p_result != null)
                                        p_result.Add(actor);
                                }
                            }

                            break;

                        case EPopulateWithActorsType._LAYERED:

                            if (wayPointPath._wayPoint.Length == 1)
                            {
                                if (wi < half)
                                {
                                    if (p_lava)
                                    {
                                        actor = new Pax4ActorEnemyAmmoLava(_lavaIndex.ToString(), null, _lavaIndex);
                                        actor.SetScale(Vector3.One * 1.8f);
                                    }
                                    else
                                    {
                                        actor = new Pax4ActorEnemyAmmoIce(_iceIndex.ToString(), null, _iceIndex);
                                        actor.SetScale(Vector3.One * 1.8f);
                                    }
                                }
                                else
                                {
                                    if (p_lava)
                                    {
                                        actor = new Pax4ActorEnemyAmmoLava(_lavaIndex.ToString(), null, _lavaIndex);
                                        actor.SetScale(Vector3.One * 1.8f);
                                    }
                                    else
                                    {
                                        actor = new Pax4ActorEnemyAmmoIce(_iceIndex.ToString(), null, _iceIndex);
                                        actor.SetScale(Vector3.One * 1.8f);
                                    }
                                }

                                if (wi % _powerUpRatio == 0)
                                    actor.SetPowerUp(Pax4Actor.EActorPowerUp._DURABILITY);

                                actor.MoveTo(Vector3.Zero, wayPointPath._wayPoint[0]);

                                new Pax4WayPointControllerActor(actor, Pax4WorldLavaAndIce._difficulty * 3.0f * p_velocityFactor, wayPointPath, 0);
                                actor.Enable();
                                if (p_result != null)
                                    p_result.Add(actor);
                            }
                            else
                            {
                                for (int i = 0; i < wayPointPath._wayPoint.Length; i++)
                                {
                                    if (i % 3 != 0)
                                        continue;

                                    if (i < halfLength)
                                    {
                                        if (p_lava)
                                        {
                                            actor = new Pax4ActorEnemyAmmoLava(_lavaIndex.ToString(), null, _lavaIndex);
                                            actor.SetScale(Vector3.One * 1.8f);
                                        }
                                        else
                                        {
                                            actor = new Pax4ActorEnemyAmmoIce(_iceIndex.ToString(), null, _iceIndex);
                                            actor.SetScale(Vector3.One * 1.8f);
                                        }
                                    }
                                    else
                                    {
                                        if (p_lava)
                                        {
                                            actor = new Pax4ActorEnemyAmmoLava(_lavaIndex.ToString(), null, _lavaIndex);
                                            actor.SetScale(Vector3.One * 1.8f);
                                        }
                                        else
                                        {
                                            actor = new Pax4ActorEnemyAmmoIce(_iceIndex.ToString(), null, _iceIndex);
                                            actor.SetScale(Vector3.One * 1.8f);
                                        }
                                    }

                                    actor.MoveTo(Vector3.Zero, wayPointPath._wayPoint[i]);

                                    if (i % _powerUpRatio == 0)
                                        actor.SetPowerUp(Pax4Actor.EActorPowerUp._DURABILITY);

                                    new Pax4WayPointControllerActor(actor, Pax4WorldLavaAndIce._difficulty * 3.0f * p_velocityFactor, wayPointPath, i);
                                    actor.Enable();
                                    if (p_result != null)
                                        p_result.Add(actor);
                                }
                            }

                            break;
                    }
                }
            }
        }
    }
}