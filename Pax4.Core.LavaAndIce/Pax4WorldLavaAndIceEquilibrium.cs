//using System;
//using System.Collections.Generic;

//using Microsoft.Xna.Framework;
//using Microsoft.Xna.Framework.Graphics;

//using Pax4.JigLibX.Physics;
//using Pax4.JigLibX.Collision;
//using Pax4.JigLibX.Geometry;
//using Pax4.JigLibX.Math;

//namespace Pax4.Core
//{
//    public class Pax4WorldLavaAndIceEquilibrium : Pax4WorldLavaAndIce
//    {
//        public Pax4WorldLavaAndIceEquilibrium(Vector3 p_position, Vector3 p_rotation, Vector3 p_scale)
//            : base(p_position, p_rotation, p_scale, p_enablePhysics)
//        {
//            _missionCount = 32;
//        }

//        public override void Ini()
//        {
//            base.Ini();

//            switch (_missionIndex)
//            {
//                case 1:
//                    IniM1(); _updateDelegate = UpdateM1;
//                    break;
//                case 2:
//                    IniM2(); _updateDelegate = UpdateM2;
//                    break;
//                case 3:
//                    IniM3(); _updateDelegate = UpdateM3;
//                    break;
//                case 4:
//                    IniM4(); _updateDelegate = UpdateM4;
//                    break;
//                case 5:
//                    IniM5(); _updateDelegate = UpdateM5;
//                    break;
//                case 6:
//                    IniM6(); _updateDelegate = UpdateM6;
//                    break;
//                case 7:
//                    IniM7(); _updateDelegate = UpdateM7;
//                    break;
//                case 8:
//                    IniM8(); _updateDelegate = UpdateM8;
//                    break;
//                case 9:
//                    IniM9(); _updateDelegate = UpdateM9;
//                    break;
//                case 10:
//                    //IniM10(); _updateDelegate = UpdateM10;
//                    break;
//                case 11:
//                    //IniM11(); _updateDelegate = UpdateM11;
//                    break;
//                case 12:
//                    //IniM12(); _updateDelegate = UpdateM12;
//                    break;
//                case 13:
//                    //IniM13(); _updateDelegate = UpdateM13;
//                    break;
//                case 14:
//                    //IniM14(); _updateDelegate = UpdateM14;
//                    break;
//                case 15:
//                    //IniM15(); _updateDelegate = UpdateM15;
//                    break;
//                case 16:
//                    //IniM16(); _updateDelegate = UpdateM16;
//                    break;
//                case 17:
//                    //IniM17(); _updateDelegate = UpdateM17;
//                    break;
//                case 18:
//                    //IniM18(); _updateDelegate = UpdateM18;
//                    break;
//                case 19:
//                    //IniM19(); _updateDelegate = UpdateM19;
//                    break;
//                case 20:
//                    //IniM20(); _updateDelegate = UpdateM20;
//                    break;
//                case 21:
//                    //IniM21(); _updateDelegate = UpdateM21;
//                    break;
//                case 22:
//                    //IniM22(); _updateDelegate = UpdateM22;
//                    break;
//                case 23:
//                    //IniM23(); _updateDelegate = UpdateM23;
//                case 24:
//                    //IniM24(); _updateDelegate = UpdateM24;
//                    break;
//                case 25:
//                    //IniM25(); _updateDelegate = UpdateM25;
//                    break;
//                case 26:
//                    //IniM26(); _updateDelegate = UpdateM26;
//                    break;
//                case 27:
//                    //IniM27(); _updateDelegate = UpdateM27;
//                    break;
//                case 28:
//                    //IniM28(); _updateDelegate = UpdateM28;
//                    break;
//                case 29:
//                    //IniM29(); _updateDelegate = UpdateM29;
//                    break;
//                case 30:
//                    //IniM30(); _updateDelegate = UpdateM30;
//                    break;
//                case 31:
//                    //IniM31(); _updateDelegate = UpdateM31;
//                    break;
//                case 32:
//                    //IniM32(); _updateDelegate = UpdateM32;
//                    break;
//            }
//        }

//        private void IniWayPointM1()
//        {
//        }

//        //maxxrad = 8.0f maxyrad = 13.0f
//        private void IniM1()
//        {
//            Pax4Ui._current.Enter("fgLava");
//            Pax4WorldLavaAndIce._missionType = ELavaAndIceMissionType._LAVA;

//            _lavaIndex = 1;
//            _iceIndex = 1;

//            Pax4World._current.LoadModel("Model/lavaandiceq1m1");
//            IniSingle();

//            //************************************************************
//            //monster***
//            _monsterIcePrelaunchPosition = new Vector3(0.0f, 6.0f, 0.0f);
//            _monsterIcePrelaunchWayPoint = new Pax4WayPointPath();
//            _monsterIcePrelaunchWayPoint.GenerateWayPoint(_monsterIcePrelaunchPosition);

//            Pax4WayPointPath waypointPath = null;

//            float radX = 0.0f;
//            float radY = 0.0f;
//            int maxEnemyPairCount = 10;
//            Vector3 center = Vector3.Zero;
//            center.Y = 6.0f;
//            int steps = maxEnemyPairCount;
//            float thetaStep = 360.0f / maxEnemyPairCount;

//            for (int i = 0; i < 2; i++)
//            {
//                waypointPath = new Pax4WayPointPath();
//                _wayPointPath.Add(waypointPath);

//                if (i % 2 == 0)
//                {
//                    radX = 8.0f;
//                    radY = 8.0f;

//                    waypointPath.GenerateCircleWayPoints(center, radX, radY, steps);
//                }
//                else
//                {
//                    radX = 5.0f;
//                    radY = 5.0f;

//                    waypointPath.GenerateCircleWayPoints(center, radX, radY, steps, 0.0f, false);
//                }
//            }

//            //************************************************************
//            //************************************************************
//            //monster            
//            _monsterIce = new Pax4Actor(Pax4Object.EActorType._ENEMY, Pax4Object.EActorType._ICE, Pax4Object.String._MONSTER, Pax4Actor.EActorPowerUp._DURABILITY, _monsterIcePrelaunchPosition, Vector3.Zero, Vector3.One * _monsterIceScaleFactor, true);
//            _monsterIce.LoadModel("Model/Actor/enemyIce" + _iceIndex);
//            _monsterIce._controller = new Pax4WayPointController();
//            ((Pax4WayPointController)_monsterIce._controller).Ini(_monsterIce, 1.0f, _monsterIcePrelaunchWayPoint);
//            _monsterIce.EnableComponent();
            
//            //************************************************************
//            //************************************************************
//            //ammo
//            Pax4Actor actor = null;
//            float velocity0 = 2.0f;
//            int wayPointPathIndex = 0;
//            for (int i = 0; i < maxEnemyPairCount; i++)
//            {
//                if (i % 2 == 0)
//                    actor = new Pax4Actor(Pax4Object.EActorType._ENEMY, Pax4Object.EActorType._LAVA, Pax4Object.EActorType._AMMO, Pax4Actor.EActorPowerUp._DURABILITY, _wayPointPath[wayPointPathIndex]._wayPoint[i], Vector3.Zero, Vector3.One * 1.5f, true);
//                else
//                    actor = new Pax4Actor(Pax4Object.EActorType._ENEMY, Pax4Object.EActorType._LAVA, Pax4Object.EActorType._AMMO, Pax4Actor.EActorPowerUp._NORMAL, _wayPointPath[wayPointPathIndex]._wayPoint[i], Vector3.Zero, Vector3.One * 1.5f, true);

//                actor.LoadModel("Model/Actor/enemyLava" + _lavaIndex);
//                actor._controller = new Pax4WayPointController();

//                ((Pax4WayPointController)actor._controller).Ini(actor, Pax4WorldLavaAndIce._difficulty * velocity0, _wayPointPath[wayPointPathIndex], i);

//                _enemyAmmo.Add(actor);

//                if (i % 2 == 0)
//                    actor = new Pax4Actor(Pax4Object.EActorType._ENEMY, Pax4Object.EActorType._ICE, Pax4Object.EActorType._AMMO, Pax4Actor.EActorPowerUp._DURABILITY, _wayPointPath[wayPointPathIndex + 1]._wayPoint[i], Vector3.Zero, Vector3.One * 1.5f, true);
//                else
//                    actor = new Pax4Actor(Pax4Object.EActorType._ENEMY, Pax4Object.EActorType._ICE, Pax4Object.EActorType._AMMO, Pax4Actor.EActorPowerUp._NORMAL, _wayPointPath[wayPointPathIndex + 1]._wayPoint[i], Vector3.Zero, Vector3.One * 1.5f, true);

//                actor.LoadModel("Model/Actor/enemyIce" + _iceIndex);
//                actor._controller = new Pax4WayPointController();

//                ((Pax4WayPointController)actor._controller).Ini(actor, Pax4WorldLavaAndIce._difficulty * velocity0, _wayPointPath[wayPointPathIndex + 1], i);

//                _enemyAmmo.Add(actor);
//            }

//            for (int i = 0; i < _enemyAmmo.Count; i++)
//            {
//                actor = (Pax4Actor)_enemyAmmo[i];
//                actor.EnableComponent();
//            }
//        }

//        private void UpdateM1(GameTime gameTime)
//        {
//        }

//        private void IniM2()
//        {
//            Pax4Ui._current.Enter("fgLava");
//            Pax4WorldLavaAndIce._missionType = ELavaAndIceMissionType._LAVA;

//            _lavaIndex = 2;
//            _iceIndex = 2;

//            Pax4World._current.LoadModel("Model/lavaandiceq1m2");
//            IniSingle();

//            Pax4WayPointPath waypointPath = null;
//            Pax4ModifierWayPointPath wayPointPathModifier = null;

//            float duration = 30.0f / _difficulty;

//            //************************************************************
//            //monster***
//            _monsterIcePrelaunchPosition = new Vector3(0.0f, 4.0f, 0.0f);
//            _monsterIcePrelaunchWayPoint = new Pax4WayPointPath();
//            _monsterIcePrelaunchWayPoint.GenerateWayPoint(_monsterIcePrelaunchPosition);
//            _monsterIcePrelaunchWayPoint.SetPosition(new Vector3(0.0f, 8.0f, 0.0f));

//            wayPointPathModifier = new Pax4ModifierWayPointPathRotationZ();
//            _wayPointPathModifier.Add(wayPointPathModifier);
//            ((Pax4ModifierWayPointPathRotationZ)wayPointPathModifier).Ini(MathHelper.TwoPi, 0.0f, duration/4.0f);
//            wayPointPathModifier.AddPath(_monsterIcePrelaunchWayPoint);
//            wayPointPathModifier.SetContinuous();
//            wayPointPathModifier.Trigger();

//            int wayPointPathCount = 11;
//            Vector3 position = Vector3.Zero;
//            wayPointPathModifier = new Pax4ModifierWayPointPathRotationZ();
//            _wayPointPathModifier.Add(wayPointPathModifier);
//            ((Pax4ModifierWayPointPathRotationZ)wayPointPathModifier).Ini(0.0f, MathHelper.TwoPi, duration);
//            wayPointPathModifier.SetContinuous();

//            Vector3 position0 = new Vector3(-8.0f, 8.0f, 0.0f);
//            float wayPointStepX = Math.Abs(position0.X * 2.0f / (wayPointPathCount - 1));
//            float wayPointStepY = Math.Abs(position0.Y * 2.0f / (wayPointPathCount - 1));
            
//            for (int i = 0; i < wayPointPathCount; i++)
//            {
//                position.X = position0.X + i * wayPointStepX;
//                position.Y = 0;
//                waypointPath = new Pax4WayPointPath();
//                _wayPointPath.Add(waypointPath);                
//                waypointPath.GenerateWayPoint(position);
//                wayPointPathModifier.AddPath(waypointPath);

//                position.X = 0;
//                position.Y = position0.Y - i * wayPointStepY;
//                waypointPath = new Pax4WayPointPath();
//                _wayPointPath.Add(waypointPath);
//                waypointPath.GenerateWayPoint(position);
//                wayPointPathModifier.AddPath(waypointPath);
//            }

//            wayPointPathModifier.Trigger();

//            wayPointPathCount *= 2;

//            float velocity0 = 5.0f;
//            //************************************************************
//            //************************************************************
//            //monster     

//            _monsterIce = new Pax4Actor(Pax4Object.EActorType._ENEMY, Pax4Object.EActorType._ICE, Pax4Object.String._MONSTER, Pax4Actor.EActorPowerUp._DURABILITY, _monsterIcePrelaunchPosition, Vector3.Zero, Vector3.One * _monsterIceScaleFactor, true);
//            _monsterIce.LoadModel("Model/Actor/enemyIce" + _iceIndex);
//            _monsterIce._controller = new Pax4WayPointController();
//            ((Pax4WayPointController)_monsterIce._controller).Ini(_monsterIce, velocity0, _monsterIcePrelaunchWayPoint);
//            _monsterIce.EnableComponent();

//            //************************************************************
//            //************************************************************
//            //ammo
//            Pax4Actor actor = null;
//            for (int i = 0; i < wayPointPathCount; i++)
//            {
//                if (i != (wayPointPathCount - 1) / 2)
//                {
//                    if (i % 4 == 0)
//                        actor = new Pax4Actor(Pax4Object.EActorType._ENEMY, Pax4Object.EActorType._LAVA, Pax4Object.EActorType._AMMO, Pax4Actor.EActorPowerUp._DURABILITY, _wayPointPath[i]._wayPoint[0], Vector3.Zero, Vector3.One * 1.5f, true);
//                    else
//                        actor = new Pax4Actor(Pax4Object.EActorType._ENEMY, Pax4Object.EActorType._LAVA, Pax4Object.EActorType._AMMO, Pax4Actor.EActorPowerUp._NORMAL, _wayPointPath[i]._wayPoint[0], Vector3.Zero, Vector3.One * 1.5f, true);

//                    actor.LoadModel("Model/Actor/enemyLava" + _lavaIndex);
//                    actor._controller = new Pax4WayPointController();

//                    ((Pax4WayPointController)actor._controller).Ini(actor, velocity0, _wayPointPath[i], 0);

//                    _enemyAmmo.Add(actor);
//                }

//                i++;

//                if (i % 5 == 0)
//                    actor = new Pax4Actor(Pax4Object.EActorType._ENEMY, Pax4Object.EActorType._ICE, Pax4Object.EActorType._AMMO, Pax4Actor.EActorPowerUp._DURABILITY, _wayPointPath[i]._wayPoint[0], Vector3.Zero, Vector3.One * 1.5f, true);
//                else
//                    actor = new Pax4Actor(Pax4Object.EActorType._ENEMY, Pax4Object.EActorType._ICE, Pax4Object.EActorType._AMMO, Pax4Actor.EActorPowerUp._NORMAL, _wayPointPath[i]._wayPoint[0], Vector3.Zero, Vector3.One * 1.5f, true);

//                actor.LoadModel("Model/Actor/enemyIce" + _iceIndex);
//                actor._controller = new Pax4WayPointController();

//                ((Pax4WayPointController)actor._controller).Ini(actor, velocity0, _wayPointPath[i], 0);

//                _enemyAmmo.Add(actor);
//            }

//            _wayPointPath.Add(_monsterIcePrelaunchWayPoint);

//            for (int i = 0; i < _enemyAmmo.Count; i++)
//            {
//                actor = (Pax4Actor)_enemyAmmo[i];
//                actor.EnableComponent();
//            }
//        }

//        private void UpdateM2(GameTime gameTime)
//        {
//        }

//        private void IniM3()
//        {
//            Pax4Ui._current.Enter("fgLava");
//            Pax4WorldLavaAndIce._missionType = ELavaAndIceMissionType._LAVA;

//            _lavaIndex = 3;
//            _iceIndex = 3;

//            Pax4World._current.LoadModel("Model/lavaandiceq1m3");
//            IniSingle();

//            Pax4WayPointPath waypointPath = null;
//            Pax4ModifierWayPointPath wayPointPathModifier = null;

//            float duration = 30.0f / _difficulty;

//            //************************************************************
//            //monster***
//            _monsterIcePrelaunchPosition = new Vector3(0.0f, 10.5f, 0.0f);
//            _monsterIcePrelaunchWayPoint = new Pax4WayPointPath();
//            _monsterIcePrelaunchWayPoint.GenerateCircleWayPoints(_monsterIcePrelaunchPosition, 6.0f, 0.2f, 4);

//            Vector3 position = Vector3.Zero;
//            wayPointPathModifier = new Pax4ModifierWayPointPathRotationZ();
//            _wayPointPathModifier.Add(wayPointPathModifier);
//            ((Pax4ModifierWayPointPathRotationZ)wayPointPathModifier).Ini(0.0f, MathHelper.TwoPi, duration);
//            wayPointPathModifier.SetContinuous();

//            int wayPointPathCount = 5;
//            Vector3 position0 = new Vector3(-5.0f, 5.0f, 0.0f);
//            float wayPointStepX = Math.Abs(position0.X * 2.0f / (wayPointPathCount - 1));
//            float wayPointStepY = Math.Abs(position0.Y * 2.0f / (wayPointPathCount - 1));

//            Vector3 position1 = new Vector3(0.0f, -2.0f, 0.0f);
//            for (int i = 0; i < wayPointPathCount; i++)
//            {
//                position.X = position0.X + i * wayPointStepX;                    

//                for (int j = 0; j < wayPointPathCount; j++)
//                {
//                    position.Y = position0.Y - j * wayPointStepY;   

//                    waypointPath = new Pax4WayPointPath();
//                    _wayPointPath.Add(waypointPath);
//                    waypointPath.GenerateWayPoint(position);
//                    waypointPath.SetPosition(position1);
//                    wayPointPathModifier.AddPath(waypointPath);
//                }
//            }

//            wayPointPathModifier.Trigger();

//            wayPointPathCount *= wayPointPathCount;

//            float velocity0 = 5.0f;
//            //************************************************************
//            //************************************************************
//            //monster     

//            _monsterIce = new Pax4Actor(Pax4Object.EActorType._ENEMY, Pax4Object.EActorType._ICE, Pax4Object.String._MONSTER, Pax4Actor.EActorPowerUp._DURABILITY, _monsterIcePrelaunchPosition, Vector3.Zero, Vector3.One * _monsterIceScaleFactor, true);
//            _monsterIce.LoadModel("Model/Actor/enemyIce" + _iceIndex);
//            _monsterIce._controller = new Pax4WayPointController();
//            ((Pax4WayPointController)_monsterIce._controller).Ini(_monsterIce, _difficulty, _monsterIcePrelaunchWayPoint);
//            _monsterIce.EnableComponent();

//            //************************************************************
//            //************************************************************
//            //ammo
//            Pax4Actor actor = null;
//            for (int i = 0; i < wayPointPathCount; i++)
//            {
//                if (i != (wayPointPathCount - 1) / 2)
//                {
//                    if (i % 4 == 0)
//                        actor = new Pax4Actor(Pax4Object.EActorType._ENEMY, Pax4Object.EActorType._LAVA, Pax4Object.EActorType._AMMO, Pax4Actor.EActorPowerUp._DURABILITY, _wayPointPath[i]._wayPoint[0], Vector3.Zero, Vector3.One * 1.5f, true);
//                    else
//                        actor = new Pax4Actor(Pax4Object.EActorType._ENEMY, Pax4Object.EActorType._LAVA, Pax4Object.EActorType._AMMO, Pax4Actor.EActorPowerUp._NORMAL, _wayPointPath[i]._wayPoint[0], Vector3.Zero, Vector3.One * 1.5f, true);

//                    actor.LoadModel("Model/Actor/enemyLava" + _lavaIndex);
//                    actor._controller = new Pax4WayPointController();

//                    ((Pax4WayPointController)actor._controller).Ini(actor, velocity0, _wayPointPath[i], 0);

//                    _enemyAmmo.Add(actor);
//                }

//                i++;

//                if (i >= wayPointPathCount)
//                    break;

//                if (i % 5 == 0)
//                    actor = new Pax4Actor(Pax4Object.EActorType._ENEMY, Pax4Object.EActorType._ICE, Pax4Object.EActorType._AMMO, Pax4Actor.EActorPowerUp._DURABILITY, _wayPointPath[i]._wayPoint[0], Vector3.Zero, Vector3.One * 1.5f, true);
//                else
//                    actor = new Pax4Actor(Pax4Object.EActorType._ENEMY, Pax4Object.EActorType._ICE, Pax4Object.EActorType._AMMO, Pax4Actor.EActorPowerUp._NORMAL, _wayPointPath[i]._wayPoint[0], Vector3.Zero, Vector3.One * 1.5f, true);

//                actor.LoadModel("Model/Actor/enemyIce" + _iceIndex);
//                actor._controller = new Pax4WayPointController();

//                ((Pax4WayPointController)actor._controller).Ini(actor, velocity0, _wayPointPath[i], 0);

//                _enemyAmmo.Add(actor);
//            }

//            _wayPointPath.Add(_monsterIcePrelaunchWayPoint);

//            for (int i = 0; i < _enemyAmmo.Count; i++)
//            {
//                actor = (Pax4Actor)_enemyAmmo[i];
//                actor.EnableComponent();
//            }
//        }

//        private void UpdateM3(GameTime gameTime)
//        {
//        }

//        private void IniM4()
//        {
//            Pax4Ui._current.Enter("fgIce");
//            Pax4WorldLavaAndIce._missionType = ELavaAndIceMissionType._ICE;

//            _lavaIndex = 1;
//            _iceIndex = 1;

//            Pax4World._current.LoadModel("Model/lavaandiceq1m4");
//            IniSingle();

//            //************************************************************
//            //monster***
//            _monsterLavaPrelaunchPosition = new Vector3(0.0f, 6.0f, 0.0f);
//            _monsterLavaPrelaunchWayPoint = new Pax4WayPointPath();
//            _monsterLavaPrelaunchWayPoint.GenerateWayPoint(_monsterLavaPrelaunchPosition);

//            Pax4WayPointPath waypointPath = null;

//            float radX = 0.0f;
//            float radY = 0.0f;
//            int maxEnemyPairCount = 10;
//            Vector3 center = Vector3.Zero;
//            center.Y = 6.0f;
//            int steps = maxEnemyPairCount;
//            float thetaStep = 360.0f / maxEnemyPairCount;

//            for (int i = 0; i < 2; i++)
//            {
//                waypointPath = new Pax4WayPointPath();
//                _wayPointPath.Add(waypointPath);

//                if (i % 2 == 0)
//                {
//                    radX = 8.0f;
//                    radY = 8.0f;

//                    waypointPath.GenerateCircleWayPoints(center, radX, radY, steps);
//                }
//                else
//                {
//                    radX = 5.0f;
//                    radY = 5.0f;

//                    waypointPath.GenerateCircleWayPoints(center, radX, radY, steps, 0.0f, false);
//                }
//            }

//            //************************************************************
//            //************************************************************
//            //monster            
//            _monsterLava = new Pax4Actor(Pax4Object.EActorType._ENEMY, Pax4Object.EActorType._LAVA, Pax4Object.String._MONSTER, Pax4Actor.EActorPowerUp._DURABILITY, _monsterLavaPrelaunchPosition, Vector3.Zero, Vector3.One * _monsterLavaScaleFactor, true);
//            _monsterLava.LoadModel("Model/Actor/enemyLava" + _lavaIndex);
//            _monsterLava._controller = new Pax4WayPointController();
//            ((Pax4WayPointController)_monsterLava._controller).Ini(_monsterLava, 1.0f, _monsterLavaPrelaunchWayPoint);
//            _monsterLava.EnableComponent();

//            //************************************************************
//            //************************************************************
//            //ammo
//            Pax4Actor actor = null;
//            float velocity0 = 2.0f;
//            int wayPointPathIndex = 0;
//            for (int i = 0; i < maxEnemyPairCount; i++)
//            {
//                if (i % 2 == 0)
//                    actor = new Pax4Actor(Pax4Object.EActorType._ENEMY, Pax4Object.EActorType._ICE, Pax4Object.EActorType._AMMO, Pax4Actor.EActorPowerUp._DURABILITY, _wayPointPath[wayPointPathIndex]._wayPoint0[i], Vector3.Zero, Vector3.One * 1.5f, true);
//                else
//                    actor = new Pax4Actor(Pax4Object.EActorType._ENEMY, Pax4Object.EActorType._ICE, Pax4Object.EActorType._AMMO, Pax4Actor.EActorPowerUp._NORMAL, _wayPointPath[wayPointPathIndex]._wayPoint0[i], Vector3.Zero, Vector3.One * 1.5f, true);

//                actor.LoadModel("Model/Actor/enemyIce" + _iceIndex);
//                actor._controller = new Pax4WayPointController();

//                ((Pax4WayPointController)actor._controller).Ini(actor, Pax4WorldLavaAndIce._difficulty * velocity0, _wayPointPath[wayPointPathIndex], i);

//                _enemyAmmo.Add(actor);

//                if (i % 2 == 0)
//                    actor = new Pax4Actor(Pax4Object.EActorType._ENEMY, Pax4Object.EActorType._LAVA, Pax4Object.EActorType._AMMO, Pax4Actor.EActorPowerUp._DURABILITY, _wayPointPath[wayPointPathIndex + 1]._wayPoint0[i], Vector3.Zero, Vector3.One * 1.5f, true);
//                else
//                    actor = new Pax4Actor(Pax4Object.EActorType._ENEMY, Pax4Object.EActorType._LAVA, Pax4Object.EActorType._AMMO, Pax4Actor.EActorPowerUp._NORMAL, _wayPointPath[wayPointPathIndex + 1]._wayPoint0[i], Vector3.Zero, Vector3.One * 1.5f, true);

//                actor.LoadModel("Model/Actor/enemyLava" + _lavaIndex);
//                actor._controller = new Pax4WayPointController();

//                ((Pax4WayPointController)actor._controller).Ini(actor, Pax4WorldLavaAndIce._difficulty * velocity0, _wayPointPath[wayPointPathIndex + 1], i);

//                _enemyAmmo.Add(actor);
//            }

//            for (int i = 0; i < _enemyAmmo.Count; i++)
//            {
//                actor = (Pax4Actor)_enemyAmmo[i];
//                actor.EnableComponent();
//            }
//        }

//        private void UpdateM4(GameTime gameTime)
//        {
//        }

//        private void IniM5()
//        {
//            Pax4Ui._current.Enter("fgIce");
//            Pax4WorldLavaAndIce._missionType = ELavaAndIceMissionType._ICE;

//            _lavaIndex = 2;
//            _iceIndex = 2;

//            Pax4World._current.LoadModel("Model/lavaandiceq1m5");
//            IniSingle();

//            Pax4WayPointPath waypointPath = null;
//            Pax4ModifierWayPointPath wayPointPathModifier = null;

//            float duration = 30.0f / _difficulty;

//            //************************************************************
//            //monster***
//            _monsterLavaPrelaunchPosition = new Vector3(0.0f, 4.0f, 0.0f);
//            _monsterLavaPrelaunchWayPoint = new Pax4WayPointPath();
//            _monsterLavaPrelaunchWayPoint.GenerateWayPoint(_monsterLavaPrelaunchPosition);
//            _monsterLavaPrelaunchWayPoint.SetPosition(new Vector3(0.0f, 8.0f, 0.0f));

//            wayPointPathModifier = new Pax4ModifierWayPointPathRotationZ();
//            _wayPointPathModifier.Add(wayPointPathModifier);
//            ((Pax4ModifierWayPointPathRotationZ)wayPointPathModifier).Ini(MathHelper.TwoPi, 0.0f, duration / 4.0f);
//            wayPointPathModifier.AddPath(_monsterLavaPrelaunchWayPoint);
//            wayPointPathModifier.SetContinuous();
//            wayPointPathModifier.Trigger();

//            int wayPointPathCount = 11;
//            Vector3 position = Vector3.Zero;
//            wayPointPathModifier = new Pax4ModifierWayPointPathRotationZ();
//            _wayPointPathModifier.Add(wayPointPathModifier);
//            ((Pax4ModifierWayPointPathRotationZ)wayPointPathModifier).Ini(0.0f, MathHelper.TwoPi, duration);
//            wayPointPathModifier.SetContinuous();

//            Vector3 position0 = new Vector3(-8.0f, 8.0f, 0.0f);
//            float wayPointStepX = Math.Abs(position0.X * 2.0f / (wayPointPathCount - 1));
//            float wayPointStepY = Math.Abs(position0.Y * 2.0f / (wayPointPathCount - 1));

//            for (int i = 0; i < wayPointPathCount; i++)
//            {
//                position.X = position0.X + i * wayPointStepX;
//                position.Y = 0;
//                waypointPath = new Pax4WayPointPath();
//                _wayPointPath.Add(waypointPath);
//                waypointPath.GenerateWayPoint(position);
//                wayPointPathModifier.AddPath(waypointPath);

//                position.X = 0;
//                position.Y = position0.Y - i * wayPointStepY;
//                waypointPath = new Pax4WayPointPath();
//                _wayPointPath.Add(waypointPath);
//                waypointPath.GenerateWayPoint(position);
//                wayPointPathModifier.AddPath(waypointPath);
//            }

//            wayPointPathModifier.Trigger();

//            wayPointPathCount *= 2;

//            float velocity0 = 5.0f;
//            //************************************************************
//            //************************************************************
//            //monster     

//            _monsterLava = new Pax4Actor(Pax4Object.EActorType._ENEMY, Pax4Object.EActorType._LAVA, Pax4Object.String._MONSTER, Pax4Actor.EActorPowerUp._DURABILITY, _monsterLavaPrelaunchPosition, Vector3.Zero, Vector3.One * _monsterLavaScaleFactor, true);
//            _monsterLava.LoadModel("Model/Actor/enemyLava" + _lavaIndex);
//            _monsterLava._controller = new Pax4WayPointController();
//            ((Pax4WayPointController)_monsterLava._controller).Ini(_monsterLava, velocity0, _monsterLavaPrelaunchWayPoint);
//            _monsterLava.EnableComponent();
            
//            //************************************************************
//            //************************************************************
//            //ammo
//            Pax4Actor actor = null;
//            for (int i = 0; i < wayPointPathCount; i++)
//            {
//                if (i != (wayPointPathCount - 1) / 2)
//                {
//                    if (i % 4 == 0)
//                        actor = new Pax4Actor(Pax4Object.EActorType._ENEMY, Pax4Object.EActorType._ICE, Pax4Object.EActorType._AMMO, Pax4Actor.EActorPowerUp._DURABILITY, _wayPointPath[i]._wayPoint[0], Vector3.Zero, Vector3.One * 1.5f, true);
//                    else
//                        actor = new Pax4Actor(Pax4Object.EActorType._ENEMY, Pax4Object.EActorType._ICE, Pax4Object.EActorType._AMMO, Pax4Actor.EActorPowerUp._NORMAL, _wayPointPath[i]._wayPoint[0], Vector3.Zero, Vector3.One * 1.5f, true);

//                    actor.LoadModel("Model/Actor/enemyIce" + _iceIndex);
//                    actor._controller = new Pax4WayPointController();

//                    ((Pax4WayPointController)actor._controller).Ini(actor, velocity0, _wayPointPath[i], 0);

//                    _enemyAmmo.Add(actor);
//                }

//                i++;

//                if (i % 5 == 0)
//                    actor = new Pax4Actor(Pax4Object.EActorType._ENEMY, Pax4Object.EActorType._LAVA, Pax4Object.EActorType._AMMO, Pax4Actor.EActorPowerUp._DURABILITY, _wayPointPath[i]._wayPoint[0], Vector3.Zero, Vector3.One * 1.5f, true);
//                else
//                    actor = new Pax4Actor(Pax4Object.EActorType._ENEMY, Pax4Object.EActorType._LAVA, Pax4Object.EActorType._AMMO, Pax4Actor.EActorPowerUp._NORMAL, _wayPointPath[i]._wayPoint[0], Vector3.Zero, Vector3.One * 1.5f, true);

//                actor.LoadModel("Model/Actor/enemyLava" + _lavaIndex);
//                actor._controller = new Pax4WayPointController();                

//                ((Pax4WayPointController)actor._controller).Ini(actor, velocity0, _wayPointPath[i], 0);

//                _enemyAmmo.Add(actor);
//            }

//            _wayPointPath.Add(_monsterLavaPrelaunchWayPoint);

//            for (int i = 0; i < _enemyAmmo.Count; i++)
//            {
//                actor = (Pax4Actor)_enemyAmmo[i];
//                actor.EnableComponent();
//            }
//        }

//        private void UpdateM5(GameTime gameTime)
//        {
//        }

//        private void IniM6()
//        {
//            Pax4Ui._current.Enter("fgIce");
//            Pax4WorldLavaAndIce._missionType = ELavaAndIceMissionType._ICE;

//            _lavaIndex = 3;
//            _iceIndex = 3;

//            Pax4World._current.LoadModel("Model/lavaandiceq1m9");
//            IniSingle();

//            Pax4WayPointPath waypointPath = null;
//            Pax4ModifierWayPointPath wayPointPathModifier = null;

//            float duration = 30.0f / _difficulty;

//            //************************************************************
//            //monster***
//            _monsterLavaPrelaunchPosition = new Vector3(0.0f, 10.5f, 0.0f);
//            _monsterLavaPrelaunchWayPoint = new Pax4WayPointPath();
//            _monsterLavaPrelaunchWayPoint.GenerateCircleWayPoints(_monsterLavaPrelaunchPosition, 6.0f, 0.2f, 4);

//            Vector3 position = Vector3.Zero;
//            wayPointPathModifier = new Pax4ModifierWayPointPathRotationZ();
//            _wayPointPathModifier.Add(wayPointPathModifier);
//            ((Pax4ModifierWayPointPathRotationZ)wayPointPathModifier).Ini(0.0f, MathHelper.TwoPi, duration);
//            wayPointPathModifier.SetContinuous();

//            int wayPointPathCount = 5;
//            Vector3 position0 = new Vector3(-5.0f, 5.0f, 0.0f);
//            float wayPointStepX = Math.Abs(position0.X * 2.0f / (wayPointPathCount - 1));
//            float wayPointStepY = Math.Abs(position0.Y * 2.0f / (wayPointPathCount - 1));

//            Vector3 position1 = new Vector3(0.0f, -2.0f, 0.0f);
//            for (int i = 0; i < wayPointPathCount; i++)
//            {
//                position.X = position0.X + i * wayPointStepX;

//                for (int j = 0; j < wayPointPathCount; j++)
//                {
//                    position.Y = position0.Y - j * wayPointStepY;

//                    waypointPath = new Pax4WayPointPath();
//                    _wayPointPath.Add(waypointPath);
//                    waypointPath.GenerateWayPoint(position);
//                    waypointPath.SetPosition(position1);
//                    wayPointPathModifier.AddPath(waypointPath);
//                }
//            }

//            wayPointPathModifier.Trigger();

//            wayPointPathCount *= wayPointPathCount;

//            float velocity0 = 5.0f;
//            //************************************************************
//            //************************************************************
//            //monster     

//            _monsterLava = new Pax4Actor(Pax4Object.EActorType._ENEMY, Pax4Object.EActorType._LAVA, Pax4Object.String._MONSTER, Pax4Actor.EActorPowerUp._DURABILITY, _monsterLavaPrelaunchPosition, Vector3.Zero, Vector3.One * _monsterLavaScaleFactor, true);
//            _monsterLava.LoadModel("Model/Actor/enemyLava" + _lavaIndex);
//            _monsterLava._controller = new Pax4WayPointController();
//            ((Pax4WayPointController)_monsterLava._controller).Ini(_monsterLava, _difficulty, _monsterLavaPrelaunchWayPoint);
//            _monsterLava.EnableComponent();

//            //************************************************************
//            //************************************************************
//            //ammo
//            Pax4Actor actor = null;
//            for (int i = 0; i < wayPointPathCount; i++)
//            {
//                if (i != (wayPointPathCount - 1) / 2)
//                {
//                    if (i % 4 == 0)
//                        actor = new Pax4Actor(Pax4Object.EActorType._ENEMY, Pax4Object.EActorType._ICE, Pax4Object.EActorType._AMMO, Pax4Actor.EActorPowerUp._DURABILITY, _wayPointPath[i]._wayPoint[0], Vector3.Zero, Vector3.One * 1.5f, true);
//                    else
//                        actor = new Pax4Actor(Pax4Object.EActorType._ENEMY, Pax4Object.EActorType._ICE, Pax4Object.EActorType._AMMO, Pax4Actor.EActorPowerUp._NORMAL, _wayPointPath[i]._wayPoint[0], Vector3.Zero, Vector3.One * 1.5f, true);

//                    actor.LoadModel("Model/Actor/enemyIce" + _iceIndex);
//                    actor._controller = new Pax4WayPointController();


//                    ((Pax4WayPointController)actor._controller).Ini(actor, velocity0, _wayPointPath[i], 0);

//                    _enemyAmmo.Add(actor);
//                }

//                i++;

//                if (i >= wayPointPathCount)
//                    break;

//                if (i % 5 == 0)
//                    actor = new Pax4Actor(Pax4Object.EActorType._ENEMY, Pax4Object.EActorType._LAVA, Pax4Object.EActorType._AMMO, Pax4Actor.EActorPowerUp._DURABILITY, _wayPointPath[i]._wayPoint[0], Vector3.Zero, Vector3.One * 1.5f, true);
//                else
//                    actor = new Pax4Actor(Pax4Object.EActorType._ENEMY, Pax4Object.EActorType._LAVA, Pax4Object.EActorType._AMMO, Pax4Actor.EActorPowerUp._NORMAL, _wayPointPath[i]._wayPoint[0], Vector3.Zero, Vector3.One * 1.5f, true);

//                actor.LoadModel("Model/Actor/enemyLava" + _lavaIndex);
//                actor._controller = new Pax4WayPointController();

//                ((Pax4WayPointController)actor._controller).Ini(actor, velocity0, _wayPointPath[i], 0);

//                _enemyAmmo.Add(actor);
//            }

//            _wayPointPath.Add(_monsterLavaPrelaunchWayPoint);

//            for (int i = 0; i < _enemyAmmo.Count; i++)
//            {
//                actor = (Pax4Actor)_enemyAmmo[i];
//                actor.EnableComponent();
//            }
//        }

//        private void UpdateM6(GameTime gameTime)
//        {
//        }

//        private void IniM7()
//        {
//            Pax4Ui._current.Enter("fgLavaAndIce");
//            Pax4WorldLavaAndIce._missionType = ELavaAndIceMissionType._LAVA_AND_ICE;

//            _lavaIndex = 7;
//            _iceIndex = 7;

//            Pax4World._current.LoadModel("Model/lavaandiceq1m6");//the model is the skybox
//            IniDual();

//            Pax4WayPointPath waypointPath = null;

//            float duration = 20.0f / _difficulty;

//            //************************************************************
//            //monster***
//            _monsterLavaPrelaunchPosition = new Vector3(0.0f, 6.0f, 0.0f);
//            _monsterLavaPrelaunchWayPoint = new Pax4WayPointPath();
//            _monsterLavaPrelaunchWayPoint.GenerateCircleWayPoints(_monsterLavaPrelaunchPosition, 3.5f, 4.0f, 10, 0.0f);

//            _monsterIcePrelaunchPosition = new Vector3(0.0f, 6.0f, 0.0f);
//            _monsterIcePrelaunchWayPoint = new Pax4WayPointPath();
//            _monsterIcePrelaunchWayPoint.GenerateCircleWayPoints(_monsterIcePrelaunchPosition, 3.5f, 4.0f, 10, (float)Math.PI);

//            Vector3 position = Vector3.Zero;
//            int wayPointPathCount = 5;
//            Vector3 position0 = new Vector3(-7.0f, 0.0f, 0.0f);
//            float wayPointStepX = Math.Abs(position0.X * 2.0f / (wayPointPathCount - 1));

//            Pax4ModifierWayPointPath wayPointPathModifier = null;
//            //1************************************************************************************************
//            wayPointPathModifier = new Pax4ModifierWayPointPathPosition();
//            _wayPointPathModifier.Add(wayPointPathModifier);
//            for (int i = 0; i < wayPointPathCount; i++)
//            {
//                position.X = position0.X + i * wayPointStepX;

//                waypointPath = new Pax4WayPointPath();
//                _wayPointPath.Add(waypointPath);
//                waypointPath.GenerateWayPoint(position);
//                wayPointPathModifier.AddPath(waypointPath);
//            }
//            ((Pax4ModifierWayPointPathPosition)wayPointPathModifier).Ini(new Vector3(0.0f, 11.0f, 0.0f), new Vector3(0.0f, -9.0f, 0.0f), duration);
//            ((Pax4ModifierWayPointPathPosition)wayPointPathModifier).SetOscillating();
//            wayPointPathModifier.Trigger();
//            //2************************************************************************************************
//            wayPointPathModifier = new Pax4ModifierWayPointPathPosition();
//            _wayPointPathModifier.Add(wayPointPathModifier);
//            for (int i = 0; i < wayPointPathCount; i++)
//            {
//                position.X = position0.X + i * wayPointStepX;

//                waypointPath = new Pax4WayPointPath();
//                _wayPointPath.Add(waypointPath);
//                waypointPath.GenerateWayPoint(position);
//                wayPointPathModifier.AddPath(waypointPath);
//            }
//            ((Pax4ModifierWayPointPathPosition)wayPointPathModifier).Ini(new Vector3(0.0f, -9.0f, 0.0f), new Vector3(0.0f, 11.0f, 0.0f), duration);
//            ((Pax4ModifierWayPointPathPosition)wayPointPathModifier).SetOscillating();
//            wayPointPathModifier.Trigger();
//            //3************************************************************************************************
//            position0 = new Vector3(0.0f, 7.0f, 0.0f);
//            wayPointPathModifier = new Pax4ModifierWayPointPathPosition();
//            _wayPointPathModifier.Add(wayPointPathModifier);
//            position.X = 0.0f;
//            for (int i = 0; i < wayPointPathCount; i++)
//            {
//                position.Y = position0.Y - i * wayPointStepX;

//                waypointPath = new Pax4WayPointPath();
//                _wayPointPath.Add(waypointPath);
//                waypointPath.GenerateWayPoint(position);
//                wayPointPathModifier.AddPath(waypointPath);
//            }
//            ((Pax4ModifierWayPointPathPosition)wayPointPathModifier).Ini(new Vector3(-8.0f, 0.0f, 0.0f), new Vector3(8.0f, 0.0f, 0.0f), duration);
//            ((Pax4ModifierWayPointPathPosition)wayPointPathModifier).SetOscillating();
//            wayPointPathModifier.Trigger();
//            //4************************************************************************************************
//            position0 = new Vector3(0.0f, 7.0f, 0.0f);
//            wayPointPathModifier = new Pax4ModifierWayPointPathPosition();
//            _wayPointPathModifier.Add(wayPointPathModifier);
//            position.X = 0.0f;
//            for (int i = 0; i < wayPointPathCount; i++)
//            {
//                position.Y = position0.Y - i * wayPointStepX;

//                waypointPath = new Pax4WayPointPath();
//                _wayPointPath.Add(waypointPath);
//                waypointPath.GenerateWayPoint(position);
//                wayPointPathModifier.AddPath(waypointPath);
//            }
//            ((Pax4ModifierWayPointPathPosition)wayPointPathModifier).Ini(new Vector3(8.0f, 0.0f, 0.0f), new Vector3(-8.0f, 0.0f, 0.0f), duration);
//            ((Pax4ModifierWayPointPathPosition)wayPointPathModifier).SetOscillating();
//            wayPointPathModifier.Trigger();


//            wayPointPathCount *= 4;
//            float velocity0 = 5.0f;
//            //************************************************************
//            //************************************************************
//            //monster     

//            _monsterLava = new Pax4Actor(Pax4Object.EActorType._ENEMY, Pax4Object.EActorType._LAVA, Pax4Object.String._MONSTER, Pax4Actor.EActorPowerUp._DURABILITY, new Vector3(3.0f, 6.0f, 0.0f), Vector3.Zero, Vector3.One * _monsterLavaScaleFactor, true);
//            _monsterLava.LoadModel("Model/Actor/enemyLava" + _lavaIndex);
//            _monsterLava._controller = new Pax4WayPointController();
//            ((Pax4WayPointController)_monsterLava._controller).Ini(_monsterLava, _difficulty, _monsterLavaPrelaunchWayPoint);
//            _monsterLava.EnableComponent();

//            _monsterIce = new Pax4Actor(Pax4Object.EActorType._ENEMY, Pax4Object.EActorType._ICE, Pax4Object.String._MONSTER, Pax4Actor.EActorPowerUp._DURABILITY, new Vector3(-3.0f, 6.0f, 0.0f), Vector3.Zero, Vector3.One * _monsterIceScaleFactor, true);
//            _monsterIce.LoadModel("Model/Actor/enemyIce" + _iceIndex);
//            _monsterIce._controller = new Pax4WayPointController();
//            ((Pax4WayPointController)_monsterIce._controller).Ini(_monsterIce, _difficulty, _monsterIcePrelaunchWayPoint);
//            _monsterIce.EnableComponent();

//            //************************************************************
//            //************************************************************
//            //ammo
//            Pax4Actor actor = null;
//            for (int i = 0; i < wayPointPathCount; i++)
//            {
//                if (i % 4 == 0)
//                    actor = new Pax4Actor(Pax4Object.EActorType._ENEMY, Pax4Object.EActorType._ICE, Pax4Object.EActorType._AMMO, Pax4Actor.EActorPowerUp._DURABILITY, _wayPointPath[i]._wayPoint[0], Vector3.Zero, Vector3.One * 1.8f, true);
//                else
//                    actor = new Pax4Actor(Pax4Object.EActorType._ENEMY, Pax4Object.EActorType._ICE, Pax4Object.EActorType._AMMO, Pax4Actor.EActorPowerUp._NORMAL, _wayPointPath[i]._wayPoint[0], Vector3.Zero, Vector3.One * 1.8f, true);

//                actor.LoadModel("Model/Actor/enemyIce" + _iceIndex);
//                actor._controller = new Pax4WayPointController();


//                ((Pax4WayPointController)actor._controller).Ini(actor, velocity0, _wayPointPath[i], 0);

//                _enemyAmmo.Add(actor);

//                i++;

//                if (i >= wayPointPathCount)
//                    break;

//                if (i % 5 == 0)
//                    actor = new Pax4Actor(Pax4Object.EActorType._ENEMY, Pax4Object.EActorType._LAVA, Pax4Object.EActorType._AMMO, Pax4Actor.EActorPowerUp._DURABILITY, _wayPointPath[i]._wayPoint[0], Vector3.Zero, Vector3.One * 1.8f, true);
//                else
//                    actor = new Pax4Actor(Pax4Object.EActorType._ENEMY, Pax4Object.EActorType._LAVA, Pax4Object.EActorType._AMMO, Pax4Actor.EActorPowerUp._NORMAL, _wayPointPath[i]._wayPoint[0], Vector3.Zero, Vector3.One * 1.8f, true);

//                actor.LoadModel("Model/Actor/enemyLava" + _lavaIndex);
//                actor._controller = new Pax4WayPointController();

//                ((Pax4WayPointController)actor._controller).Ini(actor, velocity0, _wayPointPath[i], 0);

//                _enemyAmmo.Add(actor);
//            }

//            _wayPointPath.Add(_monsterLavaPrelaunchWayPoint);
//            _wayPointPath.Add(_monsterIcePrelaunchWayPoint);

//            for (int i = 0; i < _enemyAmmo.Count; i++)
//            {
//                actor = (Pax4Actor)_enemyAmmo[i];
//                actor.EnableComponent();
//            }
//        }

//        private void UpdateM7(GameTime gameTime)
//        {
//        }

//        private void IniM8()
//        {
//            Pax4Ui._current.Enter("fgLavaAndIce");
//            Pax4WorldLavaAndIce._missionType = ELavaAndIceMissionType._LAVA_AND_ICE;

//            _lavaIndex = 8;
//            _iceIndex = 8;

//            Pax4World._current.LoadModel("Model/lavaandiceq1m7");//the model is the skybox
//            IniDual();

//            Pax4WayPointPath waypointPath = null;

//            float duration = 30.0f / _difficulty;

//            //************************************************************
//            //monster***
//            _monsterLavaPrelaunchPosition = new Vector3(-4.3f, 11.5f, 0.0f);
//            _monsterLavaPrelaunchWayPoint = new Pax4WayPointPath();
//            _monsterLavaPrelaunchWayPoint.GenerateWayPoint(_monsterLavaPrelaunchPosition);

//            _monsterIcePrelaunchPosition = new Vector3(4.3f, 11.5f, 0.0f);
//            _monsterIcePrelaunchWayPoint = new Pax4WayPointPath();
//            _monsterIcePrelaunchWayPoint.GenerateWayPoint(_monsterIcePrelaunchPosition);

//            Vector3 position = Vector3.Zero;
//            int wayPointPathRowCount = 6;
//            int wayPointPathColumnCount = 5;
//            int wayPointPathCount = wayPointPathRowCount * wayPointPathColumnCount;
//            Vector3 position0 = new Vector3(-7.0f, 6.0f, 0.0f);
//            float wayPointStepX = Math.Abs(position0.X * 2.0f / (wayPointPathColumnCount - 1));
//            float wayPointStepY = Math.Abs(position0.Y * 2.0f / (wayPointPathRowCount - 1));

//            for (int i = 0; i < wayPointPathRowCount; i++)
//            {
//                position.Y = position0.Y - i * wayPointStepY;

//                for (int j = 0; j < wayPointPathColumnCount; j++)
//                {
//                    position.X = position0.X + j * wayPointStepX;

//                    waypointPath = new Pax4WayPointPath();
//                    _wayPointPath.Add(waypointPath);
//                    waypointPath.GenerateWayPoint(position);
//                }
//            }

//            float velocity0 = 5.0f;
//            //************************************************************
//            //************************************************************
//            //monster     

//            _monsterLava = new Pax4Actor(Pax4Object.EActorType._ENEMY, Pax4Object.EActorType._LAVA, Pax4Object.String._MONSTER, Pax4Actor.EActorPowerUp._DURABILITY, _monsterLavaPrelaunchPosition, Vector3.Zero, Vector3.One * _monsterLavaScaleFactor, true);
//            _monsterLava.LoadModel("Model/Actor/enemyLava" + _lavaIndex);
//            _monsterLava._controller = new Pax4WayPointController();
//            ((Pax4WayPointController)_monsterLava._controller).Ini(_monsterLava, _difficulty, _monsterLavaPrelaunchWayPoint);
//            _monsterLava.EnableComponent();

//            _monsterIce = new Pax4Actor(Pax4Object.EActorType._ENEMY, Pax4Object.EActorType._ICE, Pax4Object.String._MONSTER, Pax4Actor.EActorPowerUp._DURABILITY, _monsterIcePrelaunchPosition, Vector3.Zero, Vector3.One * _monsterIceScaleFactor, true);
//            _monsterIce.LoadModel("Model/Actor/enemyIce" + _iceIndex);
//            _monsterIce._controller = new Pax4WayPointController();
//            ((Pax4WayPointController)_monsterIce._controller).Ini(_monsterIce, _difficulty, _monsterIcePrelaunchWayPoint);
//            _monsterIce.EnableComponent();

//            //************************************************************
//            //************************************************************
//            //ammo
//            Pax4Actor actor = null;
//            for (int i = 0; i < wayPointPathCount; i++)
//            {
//                if (i % 4 == 0)
//                    actor = new Pax4Actor(Pax4Object.EActorType._ENEMY, Pax4Object.EActorType._ICE, Pax4Object.EActorType._AMMO, Pax4Actor.EActorPowerUp._DURABILITY, _wayPointPath[i]._wayPoint[0], Vector3.Zero, Vector3.One * 1.5f, true);
//                else
//                    actor = new Pax4Actor(Pax4Object.EActorType._ENEMY, Pax4Object.EActorType._ICE, Pax4Object.EActorType._AMMO, Pax4Actor.EActorPowerUp._NORMAL, _wayPointPath[i]._wayPoint[0], Vector3.Zero, Vector3.One * 1.5f, true);

//                actor.LoadModel("Model/Actor/enemyIce" + _iceIndex);
//                actor._controller = new Pax4WayPointController();


//                ((Pax4WayPointController)actor._controller).Ini(actor, velocity0, _wayPointPath[i], 0);

//                _enemyAmmo.Add(actor);

//                i++;

//                //if (i >= wayPointPathCount)
//                //    break;

//                if (i % 5 == 0)
//                    actor = new Pax4Actor(Pax4Object.EActorType._ENEMY, Pax4Object.EActorType._LAVA, Pax4Object.EActorType._AMMO, Pax4Actor.EActorPowerUp._DURABILITY, _wayPointPath[i]._wayPoint[0], Vector3.Zero, Vector3.One * 1.5f, true);
//                else
//                    actor = new Pax4Actor(Pax4Object.EActorType._ENEMY, Pax4Object.EActorType._LAVA, Pax4Object.EActorType._AMMO, Pax4Actor.EActorPowerUp._NORMAL, _wayPointPath[i]._wayPoint[0], Vector3.Zero, Vector3.One * 1.5f, true);

//                actor.LoadModel("Model/Actor/enemyLava" + _lavaIndex);
//                actor._controller = new Pax4WayPointController();

//                ((Pax4WayPointController)actor._controller).Ini(actor, velocity0, _wayPointPath[i], 0);

//                _enemyAmmo.Add(actor);
//            }

//            _wayPointPath.Add(_monsterLavaPrelaunchWayPoint);
//            _wayPointPath.Add(_monsterIcePrelaunchWayPoint);

//            for (int i = 0; i < _enemyAmmo.Count; i++)
//            {
//                actor = (Pax4Actor)_enemyAmmo[i];
//                actor.EnableComponent();
//            }
//        }

//        private void UpdateM8(GameTime gameTime)
//        {
//        }

//        private void IniM9()
//        {
//            Pax4Ui._current.Enter("fgLavaAndIce");
//            Pax4WorldLavaAndIce._missionType = ELavaAndIceMissionType._LAVA_AND_ICE;

//            _lavaIndex = 9;
//            _iceIndex = 9;

//            Pax4World._current.LoadModel("Model/lavaandiceq1m8");//the model is the skybox
//            IniDual();

//            Pax4WayPointPath waypointPath = null;

//            float duration = 30.0f / _difficulty;

//            //************************************************************
//            //monster***
//            _monsterLavaPrelaunchPosition = new Vector3(0.0f, 6.0f, 0.0f);
//            _monsterLavaPrelaunchWayPoint = new Pax4WayPointPath();
//            _monsterLavaPrelaunchWayPoint.GenerateCircleWayPoints(_monsterLavaPrelaunchPosition, 3.5f, 4.0f, 10, 0.0f);

//            _monsterIcePrelaunchPosition = new Vector3(0.0f, 6.0f, 0.0f);
//            _monsterIcePrelaunchWayPoint = new Pax4WayPointPath();
//            _monsterIcePrelaunchWayPoint.GenerateCircleWayPoints(_monsterIcePrelaunchPosition, 3.5f, 4.0f, 10, (float)Math.PI);

//            Vector3 position = Vector3.Zero;
//            int wayPointPathRowCount = 3;
//            int wayPointPathColumnCount = 5;
//            int wayPointPathCount = wayPointPathRowCount * wayPointPathColumnCount;
//            Vector3 position0 = new Vector3(-7.0f, 2.0f, 0.0f);
//            float wayPointStepX = Math.Abs(position0.X * 2.0f / (wayPointPathColumnCount - 1));
//            float wayPointStepY = wayPointStepX * 0.75f;// Math.Abs(position0.Y * 2.0f / (wayPointPathRowCount - 1));

//            Vector3 position1 = new Vector3(0.0f, -4.5f, 0.0f);
//            for (int i = 0; i < wayPointPathRowCount; i++)
//            {
//                position.Y = position0.Y - i * wayPointStepY;

//                for (int j = 0; j < wayPointPathColumnCount; j++)
//                {
//                    position.X = position0.X + j * wayPointStepX;

//                    waypointPath = new Pax4WayPointPath();
//                    _wayPointPath.Add(waypointPath);
//                    waypointPath.GenerateWayPoint(position);
//                    waypointPath.SetPosition(position1);
//                }
//            }

//            Pax4ModifierWayPointPath wayPointPathModifier = null;
//            wayPointPathModifier = new Pax4ModifierWayPointPathRotationZ();
//            _wayPointPathModifier.Add(wayPointPathModifier);
//            ((Pax4ModifierWayPointPathRotationZ)wayPointPathModifier).Ini(0.0f, MathHelper.TwoPi, duration);
//            wayPointPathModifier.SetContinuous();

//            int wayPointPathRowCount1 = 4;
//            wayPointPathCount += wayPointPathRowCount1 * 2;
//            position1 = new Vector3(0.0f, 2.5f, 0.0f);
//            Vector3 position2 = new Vector3(0.0f, 6.0f, 0.0f);
//            for (int i = 0; i < wayPointPathRowCount1; i++)
//            {
//                position.Y = position1.Y - i * wayPointStepY;

//                position.X = -7.0f;
//                waypointPath = new Pax4WayPointPath();
//                _wayPointPath.Add(waypointPath);
//                waypointPath.GenerateWayPoint(position);
//                waypointPath.SetPosition(position2);
//                wayPointPathModifier.AddPath(waypointPath);

//                position.X = 7.0f;
//                waypointPath = new Pax4WayPointPath();
//                _wayPointPath.Add(waypointPath);
//                waypointPath.GenerateWayPoint(position);
//                waypointPath.SetPosition(position2);
//                wayPointPathModifier.AddPath(waypointPath);
//            }

//            wayPointPathModifier.Trigger();

//            float velocity0 = 5.0f;
//            //************************************************************
//            //************************************************************
//            //monster     

//            _monsterLava = new Pax4Actor(Pax4Object.EActorType._ENEMY, Pax4Object.EActorType._LAVA, Pax4Object.String._MONSTER, Pax4Actor.EActorPowerUp._DURABILITY, new Vector3(3.0f, 6.0f, 0.0f), Vector3.Zero, Vector3.One * _monsterLavaScaleFactor, true);
//            _monsterLava.LoadModel("Model/Actor/enemyLava" + _lavaIndex);
//            _monsterLava._controller = new Pax4WayPointController();
//            ((Pax4WayPointController)_monsterLava._controller).Ini(_monsterLava, _difficulty, _monsterLavaPrelaunchWayPoint);
//            _monsterLava.EnableComponent();

//            _monsterIce = new Pax4Actor(Pax4Object.EActorType._ENEMY, Pax4Object.EActorType._ICE, Pax4Object.String._MONSTER, Pax4Actor.EActorPowerUp._DURABILITY, new Vector3(-3.0f, 6.0f, 0.0f), Vector3.Zero, Vector3.One * _monsterIceScaleFactor, true);
//            _monsterIce.LoadModel("Model/Actor/enemyIce" + _iceIndex);
//            _monsterIce._controller = new Pax4WayPointController();
//            ((Pax4WayPointController)_monsterIce._controller).Ini(_monsterIce, _difficulty, _monsterIcePrelaunchWayPoint);
//            _monsterIce.EnableComponent();

//            //************************************************************
//            //************************************************************
//            //ammo
//            Pax4Actor actor = null;
//            for (int i = 0; i < wayPointPathCount; i++)
//            {
//                if (i % 4 == 0)
//                    actor = new Pax4Actor(Pax4Object.EActorType._ENEMY, Pax4Object.EActorType._ICE, Pax4Object.EActorType._AMMO, Pax4Actor.EActorPowerUp._DURABILITY, _wayPointPath[i]._wayPoint[0], Vector3.Zero, Vector3.One * 1.8f, true);
//                else
//                    actor = new Pax4Actor(Pax4Object.EActorType._ENEMY, Pax4Object.EActorType._ICE, Pax4Object.EActorType._AMMO, Pax4Actor.EActorPowerUp._NORMAL, _wayPointPath[i]._wayPoint[0], Vector3.Zero, Vector3.One * 1.8f, true);

//                actor.LoadModel("Model/Actor/enemyIce" + _iceIndex);
//                actor._controller = new Pax4WayPointController();


//                ((Pax4WayPointController)actor._controller).Ini(actor, velocity0, _wayPointPath[i], 0);

//                _enemyAmmo.Add(actor);

//                i++;

//                if (i >= wayPointPathCount)
//                    break;

//                if (i % 5 == 0)
//                    actor = new Pax4Actor(Pax4Object.EActorType._ENEMY, Pax4Object.EActorType._LAVA, Pax4Object.EActorType._AMMO, Pax4Actor.EActorPowerUp._DURABILITY, _wayPointPath[i]._wayPoint[0], Vector3.Zero, Vector3.One * 1.8f, true);
//                else
//                    actor = new Pax4Actor(Pax4Object.EActorType._ENEMY, Pax4Object.EActorType._LAVA, Pax4Object.EActorType._AMMO, Pax4Actor.EActorPowerUp._NORMAL, _wayPointPath[i]._wayPoint[0], Vector3.Zero, Vector3.One * 1.8f, true);

//                actor.LoadModel("Model/Actor/enemyLava" + _lavaIndex);
//                actor._controller = new Pax4WayPointController();

//                ((Pax4WayPointController)actor._controller).Ini(actor, velocity0, _wayPointPath[i], 0);

//                _enemyAmmo.Add(actor);
//            }

//            _wayPointPath.Add(_monsterLavaPrelaunchWayPoint);
//            _wayPointPath.Add(_monsterIcePrelaunchWayPoint);

//            for (int i = 0; i < _enemyAmmo.Count; i++)
//            {
//                actor = (Pax4Actor)_enemyAmmo[i];
//                actor.EnableComponent();
//            }
//        }

//        private void UpdateM9(GameTime gameTime)
//        {
//        }
//    }
//}