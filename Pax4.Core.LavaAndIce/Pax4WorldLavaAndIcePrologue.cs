using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Pax4.JigLibX.Physics;
using Pax4.JigLibX.Collision;
using Pax4.JigLibX.Geometry;
using Pax4.JigLibX.Math;
using Pax4.ProjectMercury.Proxies;
using Pax4.ProjectMercury;
using Pax4.ProjectMercury.Controllers;
using Pax.Core;
using System.Runtime.Serialization;

namespace Pax4.Core
{
    [DataContract]
    [KnownType(typeof(Pax4WorldLavaAndIcePrologue))]
    public class Pax4WorldLavaAndIcePrologue : Pax4WorldLavaAndIce
    {
        public Pax4WorldLavaAndIcePrologue(String p_name, PaxState p_parent0)
            : base(p_name, p_parent0)
        {
            _missionCount = 19;
        }

        public override void Enable()
        {
            base.Enable();

            switch (_missionIndex)
            {
                case 0:
                    IniM0(); _updateDelegate = UpdateM0;
                    break;
                case 1:
                    IniM1(); _updateDelegate = UpdateM1;
                    break;
                case 2:
                    IniM2(); _updateDelegate = UpdateM2;
                    break;
                case 3:
                    IniM3(); _updateDelegate = UpdateM3;
                    break;
                case 4:
                    IniM4(); _updateDelegate = UpdateM4;
                    break;
                case 5:
                    IniM5(); _updateDelegate = UpdateM5;
                    break;
                case 6:
                    IniM6(); _updateDelegate = UpdateM6;
                    break;
                case 7:
                    IniM7(); _updateDelegate = UpdateM7;
                    break;
                case 8:
                    IniM8(); _updateDelegate = UpdateM8;
                    break;
                case 9:
                    IniM9(); _updateDelegate = UpdateM9;
                    break;
                case 10:
                    IniM10(); _updateDelegate = UpdateM10;
                    break;
                case 11:
                    IniM11(); _updateDelegate = UpdateM11;
                    break;
                case 12:
                    IniM12(); _updateDelegate = UpdateM12;
                    break;
                case 13:
                    IniM13(); _updateDelegate = UpdateM13;
                    break;
                case 14:
                    IniM14(); _updateDelegate = UpdateM14;
                    break;
                case 15:
                    IniM15(); _updateDelegate = UpdateM15;
                    break;
                case 16:
                    IniM16(); _updateDelegate = UpdateM16;
                    break;
                case 17:
                    IniM17(); _updateDelegate = UpdateM17;
                    break;
                case 18:
                    IniM18(); _updateDelegate = UpdateM18;
                    break;
                case 19:
                    IniM19(); _updateDelegate = UpdateM19;
                    break;
                case 20:
                    IniM20(); _updateDelegate = UpdateM20;
                    break;
                case 21:
                    IniM21(); _updateDelegate = UpdateM21;
                    break;
                case 22:
                    IniM22(); _updateDelegate = UpdateM22;
                    break;
                case 23:
                    IniM23(); _updateDelegate = UpdateM23;
                    break;
                case 24:
                    IniM24(); _updateDelegate = UpdateM24;
                    break;
                case 25:
                    IniM25(); _updateDelegate = UpdateM25;
                    break;
                case 26:
                    IniM26(); _updateDelegate = UpdateM26;
                    break;
                case 27:
                    IniM27(); _updateDelegate = UpdateM27;
                    break;
                case 28:
                    IniM28(); _updateDelegate = UpdateM28;
                    break;
                case 29:
                    IniM29(); _updateDelegate = UpdateM29;
                    break;
                case 30:
                    IniM30(); _updateDelegate = UpdateM30;
                    break;
                case 31:
                    IniM31(); _updateDelegate = UpdateM31;
                    break;
                case 32:
                    IniM32(); _updateDelegate = UpdateM32;
                    break;
            }
        }

        private void IniM0_Animated()
        {
            _lavaIndex = 1;
            _iceIndex = 1;

            Pax4Model._current.Load("Model/lavaandiceAmmoLava");
            Pax4Model._current.Load("Model/lavaandiceq1m1");

            Pax4Model._current.Load("Model/lavaandiceEnemyLava" + _lavaIndex);
            Pax4Model._current.Load("Model/lavaandiceEnemyIce" + _iceIndex);

            //Pax4Model._current.Load("Model/test/dude_gpu");
            //Pax4Model._current.Load("Model/test/Mic_But002");

            //Pax4Model._current.Load("Model/test/FoldIceGate");

            //Pax4ObjectSceneryPartModelSkinned modelSkinned = new Pax4ObjectSceneryPartModelSkinned();
            //modelSkinned.SetScaleRotationPosition(Vector3.One * 5.25f, Vector3.Up * (float)Math.PI, new Vector3(0.0f, 0.0f, 0.0f));
            //modelSkinned.SetScaleRotationPosition(Vector3.One * 2.15f, Vector3.Up * (float)Math.PI, new Vector3(0.0f, -10.0f, 0.0f));

            //modelSkinned.SetModel("Model/test/dude_gpu");            
            //modelSkinned.SetModel("Model/test/Mic_But002");
            //modelSkinned.SetModel("Model/test/FoldIceGate");   

            //modelSkinned.Enable();
            //Pax4World._current.AddUpdateObject(modelSkinned);
            //Pax4World._current.AddDrawObject(modelSkinned);

            //modelSkinned.SetAnimationtVelocityFactor(0.5f);
            //modelSkinned.SetAnimationtVelocityFactor(-0.5f);


            Pax4Model._current.SetDefaultParameters();

            Pax4Ui._current.Enter("fgLava");
            Pax4WorldLavaAndIce._missionType = ELavaAndIceMissionType._LAVA;

            IniSingle();

            Pax4ActorWorld world = new Pax4ActorWorld("lavaandiceq1m1", null);
            world.SetModel("Model/lavaandiceq1m1");
            world.Enable();

            SetDifficultyTimer(40, 55, 70, 85, true);

            //************************************************************
            //***monster***
            Vector3 monsterPosition = new Vector3(0.0f, 6.0f, 0.0f);
            Pax4WayPointPath monsterWayPointPath = new Pax4WayPointPath(true);
            monsterWayPointPath.GenerateWayPoint(monsterPosition);
            monsterWayPointPath.Enable();

            //***ammo***
            Pax4WayPointPath wayPointPath = null;

            wayPointPath = new Pax4WayPointPath();
            wayPointPath.GenerateCircleWayPoints(0.0f, monsterPosition, 8.0f, 8.0f, 3);
            wayPointPath.Enable();
            PopulateWithActors(_physicsPartList, EPopulateWithActorsType._HOMOGENEOUS);

            wayPointPath = new Pax4WayPointPath();
            wayPointPath.GenerateCircleWayPoints(-0.785398f, monsterPosition, 5.0f, 5.0f, 3);//2.35619f
            wayPointPath.Enable();
            PopulateWithActors(_physicsPartList, EPopulateWithActorsType._HOMOGENEOUS, false);
        }

        private void IniM0_()
        {
            _lavaIndex = 1;
            _iceIndex = 1;

            Pax4Model._current.Load("Model/lavaandiceAmmoLava");
            Pax4Model._current.Load("Model/lavaandiceq1m1");

            Pax4Model._current.Load("Model/lavaandiceEnemyLava" + _lavaIndex);
            Pax4Model._current.Load("Model/lavaandiceEnemyIce" + _iceIndex);

            Pax4Model._current.SetDefaultParameters();

            Pax4Ui._current.Enter("fgLava");
            Pax4WorldLavaAndIce._missionType = ELavaAndIceMissionType._LAVA;

            IniSingle();

            Pax4ActorWorld world = new Pax4ActorWorld("prologue", null);
            world.SetModel("Model/lavaandiceq1m1");
            world.Enable();

            SetDifficultyTimer(40, 55, 70, 85, true);

            //monster***
            Vector3 monsterPosition = new Vector3(0.0f, 13.0f, 0.0f);
            Pax4WayPointPath monsterWayPointPath = new Pax4WayPointPath(true);
            monsterWayPointPath.GenerateWayPoint(monsterPosition);

            //monster 
            Pax4ActorEnemyMonsterIce monster = new Pax4ActorEnemyMonsterIce(_iceIndex.ToString(), null, _iceIndex);
            monster.MoveTo(Vector3.Zero, monsterPosition);
            new Pax4WayPointControllerActor(monster, 1.0f, monsterWayPointPath);
            monster.Enable();

            //************************************************************
            //sandbox

            Pax4WayPointPath wayPointPath = null;
            Pax4WayPointPathList wayPointPaths = null;

            wayPointPath = new Pax4WayPointPath();
            wayPointPath.GenerateLineWayPoints(-(float)Math.PI / 4.0f, new Vector3(0.0f, 5.0f, 0.0f), 16.0f, 8);
            wayPointPaths = wayPointPath.ToWayPointPaths();
            wayPointPaths.Enable();
            PopulateWithActors(_physicsPartList, EPopulateWithActorsType._HETEROGENEOUS);
            
            Pax4WayPointPath.ChainUp(_physicsPartList);
        }

        private void UpdateM0_(GameTime gameTime)
        {
            //(Pax4ParticleEffect._current._particleEffectIceStarTrail3.Emitters[0].Controllers[0] as TriggerOffsetController).TriggerOffset = new Vector3
            //{
            //    X = 500.0f,
            //    Y = 500.0f,
            //    Z = (float)Math.Cos(gameTime.TotalGameTime.TotalSeconds * 0.75) * 290f,
            //};

            //(Pax4ParticleEffect._current._particleEffectIceStarTrail3.Emitters[1].Controllers[0] as TriggerOffsetController).TriggerOffset = new Vector3
            //{
            //    X = (float)Math.Cos(gameTime.TotalGameTime.TotalSeconds * 1f) * -275f,
            //    Y = (float)Math.Sin(gameTime.TotalGameTime.TotalSeconds * 1.15f) * -250f,
            //    Z = (float)Math.Sin(gameTime.TotalGameTime.TotalSeconds * 0.75) * -290f,
            //};

            //(Pax4ParticleEffect._current._particleEffectIceStarTrail3.Emitters[2].Controllers[0] as TriggerOffsetController).TriggerOffset = new Vector3
            //{
            //    X = (float)Math.Sin(gameTime.TotalGameTime.TotalSeconds * 0.75f) * 275f,
            //    Y = (float)Math.Cos(gameTime.TotalGameTime.TotalSeconds * 1f) * -250f,
            //    Z = (float)Math.Sin(gameTime.TotalGameTime.TotalSeconds * 1.15f) * -290f,
            //};

            //Vector3 triggerPosition = Vector3.Zero;

            //var frustum = new BoundingFrustum(Pax4Camera._current._matView * Pax4Camera._current._matProjection);
            //Pax4ParticleEffect._current._particleEffectIceStarTrail3.Trigger(ref triggerPosition, ref frustum);
            //_proxy1.Trigger(ref frustum);
            //_proxy2.Trigger(ref frustum);
            //Pax4ParticleEffect._current._particleEffectIceStarTrail3.Update((float)gameTime.ElapsedGameTime.TotalSeconds);

            //Matrix rotateInstances1;
            //Matrix.CreateRotationY(.01f, out rotateInstances1);
            //Matrix.Multiply(ref _proxy2.World, ref rotateInstances1, out _proxy2.World);

            //Matrix rotateInstance2;
            //Matrix.CreateRotationX(.1f, out rotateInstance2);
            //Matrix.Multiply(ref _proxy1.World, ref rotateInstance2, out _proxy1.World);
        }

        private void IniM0()
        {
            _lavaIndex = 1;
            _iceIndex = 1;

            Pax4Model._current.Load("Model/lavaandiceAmmoLava");
            Pax4Model._current.Load("Model/lavaandiceq1m1");

            Pax4Model._current.Load("Model/lavaandiceEnemyLava" + _lavaIndex);
            Pax4Model._current.Load("Model/lavaandiceEnemyIce" + _iceIndex);

            Pax4Model._current.SetDefaultParameters();

            Pax4Ui._current.Enter("fgLava");
            Pax4WorldLavaAndIce._missionType = ELavaAndIceMissionType._LAVA;

            IniSingle();

            Pax4ActorWorld world = new Pax4ActorWorld("lavaandiceq1m1", null);
            world.SetModel("Model/lavaandiceq1m1");
            world.Enable();

            SetDifficultyTimer(40, 55, 70, 85, true);

            //************************************************************
            //monster***
            Vector3 monsterPosition = new Vector3(0.0f, 6.0f, 0.0f);
            Pax4WayPointPath monsterWayPointPath = new Pax4WayPointPath(true);
            monsterWayPointPath.GenerateWayPoint(monsterPosition);

            Pax4WayPointPath waypointPath0 = new Pax4WayPointPath();
            Vector3 ammoPostion = new Vector3(3.5f, 0.0f, 0.0f);
            waypointPath0.GenerateWayPoint(ammoPostion);
            _wayPointPath.Add(waypointPath0);

            Pax4WayPointPath waypointPath1 = new Pax4WayPointPath();
            ammoPostion = new Vector3(0.0f, 11.0f, 0.0f);
            waypointPath1.GenerateWayPoint(ammoPostion);
            _wayPointPath.Add(waypointPath1);
            //************************************************************
            //************************************************************
            //monster 
            Pax4ActorEnemyMonsterIce monster = new Pax4ActorEnemyMonsterIce(_iceIndex.ToString(), null, _iceIndex);
            monster._health = 2.0f;
            monster.MoveTo(Vector3.Zero, monsterPosition);
            new Pax4WayPointControllerActor(monster, 1.0f, monsterWayPointPath);
            monster.Enable();

            //************************************************************
            //************************************************************
            //ammo

            float velocity0 = 2.0f;
            Pax4Actor actor = null;

            actor = new Pax4ActorEnemyAmmoLava("Pax4ActorEnemyAmmoLava", null, _lavaIndex);            
            actor.MoveTo(Vector3.Zero, monsterPosition);
            new Pax4WayPointControllerActor(actor, Pax4WorldLavaAndIce._difficulty * velocity0, _wayPointPath[1]);
            actor.Enable();

            actor = new Pax4ActorEnemyAmmoIce("Pax4ActorEnemyAmmoIce", null, _iceIndex);
            actor.MoveTo(Vector3.Zero, monsterPosition);
            new Pax4WayPointControllerActor(actor, Pax4WorldLavaAndIce._difficulty * velocity0, _wayPointPath[0]);
            actor.Enable();           
        }

        private void UpdateM0(GameTime gameTime)
        {
        }

        //maxxrad = 8.0f maxyrad = 13.0f
        private void IniM1()
        {
            _lavaIndex = 1;
            _iceIndex = 1;

            Pax4Model._current.Load("Model/lavaandiceAmmoLava");
            Pax4Model._current.Load("Model/lavaandiceq1m1");

            Pax4Model._current.Load("Model/lavaandiceEnemyLava" + _lavaIndex);
            Pax4Model._current.Load("Model/lavaandiceEnemyIce" + _iceIndex);

            Pax4Model._current.SetDefaultParameters();

            Pax4Ui._current.Enter("fgLava");
            Pax4WorldLavaAndIce._missionType = ELavaAndIceMissionType._LAVA;

            IniSingle();

            Pax4ActorWorld world = new Pax4ActorWorld("lavaandiceq1m1", null);
            world.SetModel("Model/lavaandiceq1m1");
            world.Enable();

            SetDifficultyTimer(40, 55, 70, 85, true);

            //************************************************************

            //***monster***
            Vector3 monsterPosition = new Vector3(0.0f, 6.0f, 0.0f);
            Pax4WayPointPath monsterWayPointPath = new Pax4WayPointPath(true);
            monsterWayPointPath.GenerateWayPoint(monsterPosition);
            monsterWayPointPath.Enable();

            //***ammo***
            Pax4WayPointPath wayPointPath = null;

            wayPointPath = new Pax4WayPointPath();
            wayPointPath.GenerateCircleWayPoints(0.0f, monsterPosition, 8.0f, 8.0f, 3);
            wayPointPath.Enable();
            PopulateWithActors(_physicsPartList, EPopulateWithActorsType._HOMOGENEOUS);

            wayPointPath = new Pax4WayPointPath();
            wayPointPath.GenerateCircleWayPoints(0.0f, monsterPosition, 5.0f, 5.0f, 3);//2.35619f
            wayPointPath.Enable();
            PopulateWithActors(_physicsPartList, EPopulateWithActorsType._HOMOGENEOUS, false);
        }

        private void UpdateM1(GameTime gameTime)
        {
        }

        private void IniM2()
        {
            _lavaIndex = 2;
            _iceIndex = 2;

            Pax4Model._current.Load("Model/lavaandiceAmmoLava");
            Pax4Model._current.Load("Model/lavaandiceq1m2");

            Pax4Model._current.Load("Model/lavaandiceEnemyLava" + _lavaIndex);
            Pax4Model._current.Load("Model/lavaandiceEnemyIce" + _iceIndex);

            Pax4Model._current.SetDefaultParameters();

            Pax4Ui._current.Enter("fgLava");
            Pax4WorldLavaAndIce._missionType = ELavaAndIceMissionType._LAVA;
            
            IniSingle();

            Pax4ActorWorld world = new Pax4ActorWorld("lavaandiceq1m2", null);
            world.SetModel("Model/lavaandiceq1m2");
            world.Enable();

            SetDifficultyTimer(40, 55, 70, 85, true);

            Pax4WayPointPath waypointPath = null;
            Pax4ModifierWayPointPath wayPointPathModifier = null;

            float duration = 30.0f / _difficulty;

            //************************************************************
            //monster***
            Vector3 monsterPosition = new Vector3(0.0f, 4.0f, 0.0f);
            Pax4WayPointPath monsterWayPointPath = new Pax4WayPointPath(true);
            monsterWayPointPath.GenerateWayPoint(monsterPosition);
            monsterWayPointPath.SetPosition(new Vector3(0.0f, 8.0f, 0.0f));

            wayPointPathModifier = new Pax4ModifierWayPointPathRotationZ("", null);
            _wayPointPathModifier.Add(wayPointPathModifier);
            ((Pax4ModifierWayPointPathRotationZ)wayPointPathModifier).Ini(MathHelper.TwoPi, 0.0f, duration / 4.0f);
            wayPointPathModifier.AddPath(monsterWayPointPath);
            wayPointPathModifier.SetContinuous();
            wayPointPathModifier.Trigger();

            int wayPointPathCount = 5;
            Vector3 position = Vector3.Zero;
            wayPointPathModifier = new Pax4ModifierWayPointPathRotationZ("", null);
            _wayPointPathModifier.Add(wayPointPathModifier);
            ((Pax4ModifierWayPointPathRotationZ)wayPointPathModifier).Ini(0.0f, MathHelper.TwoPi, duration);
            wayPointPathModifier.SetContinuous();

            Vector3 position0 = new Vector3(-5.0f, 5.0f, 0.0f);
            float wayPointStepX = Math.Abs(position0.X * 2.0f / (wayPointPathCount - 1));
            float wayPointStepY = Math.Abs(position0.Y * 2.0f / (wayPointPathCount - 1));

            for (int i = 0; i < wayPointPathCount; i++)
            {
                position.X = position0.X + i * wayPointStepX;
                position.Y = 0;
                waypointPath = new Pax4WayPointPath();
                _wayPointPath.Add(waypointPath);
                waypointPath.GenerateWayPoint(position);
                wayPointPathModifier.AddPath(waypointPath);

                position.X = 0;
                position.Y = position0.Y - i * wayPointStepY;
                waypointPath = new Pax4WayPointPath();
                _wayPointPath.Add(waypointPath);
                waypointPath.GenerateWayPoint(position);
                wayPointPathModifier.AddPath(waypointPath);
            }

            wayPointPathModifier.Trigger();

            wayPointPathCount *= 2;

            float velocity0 = 4.0f;
            //************************************************************
            //************************************************************
            //monster     
            Pax4ActorEnemyMonsterIce monster = new Pax4ActorEnemyMonsterIce(_iceIndex.ToString(), null, _iceIndex);
            monster.MoveTo(Vector3.Zero, monsterPosition);
            new Pax4WayPointControllerActor(monster, velocity0, monsterWayPointPath);
            monster.Enable();            

            //************************************************************
            //************************************************************
            //ammo
            Pax4Actor actor = null;
            for (int i = 0; i < wayPointPathCount; i++)
            {
                if (i != (wayPointPathCount - 1) / 2)
                {
                    actor = new Pax4ActorEnemyAmmoLava(_lavaIndex.ToString(), null, _lavaIndex);
                    actor.MoveTo(Vector3.Zero, _wayPointPath[i]._wayPoint[0]);
                    if (i % 4 == 0)
                        actor.SetPowerUp(Pax4Actor.EActorPowerUp._DURABILITY);
                    new Pax4WayPointControllerActor(actor, velocity0, _wayPointPath[i], 0);
                    actor.Enable();
                }

                i++;

                if (i >= wayPointPathCount)
                    break;

                actor = new Pax4ActorEnemyAmmoIce(_iceIndex.ToString(), null, _iceIndex);
                actor.MoveTo(Vector3.Zero, _wayPointPath[i]._wayPoint[0]);
                if (i % 5 == 0)
                    actor.SetPowerUp(Pax4Actor.EActorPowerUp._DURABILITY);
                new Pax4WayPointControllerActor(actor, velocity0, _wayPointPath[i], 0);
                actor.Enable();
            }

            _wayPointPath.Add(monsterWayPointPath);
        }

        private void UpdateM2(GameTime gameTime)
        {
        }

        private void IniM3()
        {
            _lavaIndex = 3;
            _iceIndex = 3;

            Pax4Model._current.Load("Model/lavaandiceAmmoLava");
            Pax4Model._current.Load("Model/lavaandiceq1m3");

            Pax4Model._current.Load("Model/lavaandiceEnemyLava" + _lavaIndex);
            Pax4Model._current.Load("Model/lavaandiceEnemyIce" + _iceIndex);

            Pax4Model._current.SetDefaultParameters();

            Pax4Ui._current.Enter("fgLava");
            Pax4WorldLavaAndIce._missionType = ELavaAndIceMissionType._LAVA;
            
            IniSingle();

            Pax4ActorWorld world = new Pax4ActorWorld("lavaandiceq1m3", null);
            world.SetModel("Model/lavaandiceq1m3");
            world.Enable();

            SetDifficultyTimer(40, 55, 70, 85, false);

            Pax4WayPointPath waypointPath = null;
            Pax4ModifierWayPointPath wayPointPathModifier = null;

            float duration = 30.0f / _difficulty;

            //************************************************************
            //monster***
            Vector3 monsterPosition = new Vector3(0.0f, 10.5f, 0.0f);
            Pax4WayPointPath monsterWayPointPath = new Pax4WayPointPath(true);
            monsterWayPointPath.GenerateCircleWayPoints(0.0f, monsterPosition, 6.0f, 0.2f, 4);

            Vector3 position = Vector3.Zero;
            wayPointPathModifier = new Pax4ModifierWayPointPathRotationZ("", null);
            _wayPointPathModifier.Add(wayPointPathModifier);
            ((Pax4ModifierWayPointPathRotationZ)wayPointPathModifier).Ini(0.0f, MathHelper.TwoPi, duration);
            wayPointPathModifier.SetContinuous();

            int wayPointPathCount = 3;
            Vector3 position0 = new Vector3(-5.0f, 5.0f, 0.0f);
            float wayPointStepX = Math.Abs(position0.X * 1.5f / (wayPointPathCount - 1));
            float wayPointStepY = Math.Abs(position0.Y * 1.5f / (wayPointPathCount - 1));

            Vector3 position1 = new Vector3(0.0f, -2.0f, 0.0f);
            for (int i = 0; i < wayPointPathCount; i++)
            {
                position.X = position0.X + i * wayPointStepX;

                for (int j = 0; j < wayPointPathCount; j++)
                {
                    position.Y = position0.Y - j * wayPointStepY;

                    waypointPath = new Pax4WayPointPath();
                    _wayPointPath.Add(waypointPath);
                    waypointPath.GenerateWayPoint(position);
                    waypointPath.SetPosition(position1);
                    wayPointPathModifier.AddPath(waypointPath);
                }
            }

            wayPointPathModifier.Trigger();

            wayPointPathCount *= wayPointPathCount;

            float velocity0 = 4.0f;
            //************************************************************
            //************************************************************
            //monster     
            Pax4ActorEnemyMonsterIce monster = new Pax4ActorEnemyMonsterIce(_iceIndex.ToString(), null, _iceIndex);
            monster.MoveTo(Vector3.Zero, monsterPosition);
            new Pax4WayPointControllerActor(monster, _difficulty, monsterWayPointPath);
            monster.Enable(); 

            //************************************************************
            //************************************************************
            //ammo
            Pax4Actor actor = null;
            for (int i = 0; i < wayPointPathCount; i++)
            {
                if (i != (wayPointPathCount - 1) / 2)
                {
                    actor = new Pax4ActorEnemyAmmoLava(_lavaIndex.ToString(), null, _lavaIndex);
                    actor.MoveTo(Vector3.Zero, _wayPointPath[i]._wayPoint[0]);
                    if (i % 4 == 0)
                        actor.SetPowerUp(Pax4Actor.EActorPowerUp._DURABILITY);
                    new Pax4WayPointControllerActor(actor, velocity0, _wayPointPath[i], 0);
                    actor.Enable();
                }

                i++;

                if (i >= wayPointPathCount)
                    break;

                actor = new Pax4ActorEnemyAmmoIce(_iceIndex.ToString(), null, _iceIndex);
                actor.MoveTo(Vector3.Zero, _wayPointPath[i]._wayPoint[0]);
                if (i % 5 == 0)
                    actor.SetPowerUp(Pax4Actor.EActorPowerUp._DURABILITY);
                new Pax4WayPointControllerActor(actor, velocity0, _wayPointPath[i], 0);
                actor.Enable();
            }

            _wayPointPath.Add(monsterWayPointPath);            
        }

        private void UpdateM3(GameTime gameTime)
        {
        }

        private void IniM4()
        {
            _lavaIndex = 1;
            _iceIndex = 1;

            Pax4Model._current.Load("Model/lavaandiceAmmoIce");
            Pax4Model._current.Load("Model/lavaandiceq1m4");

            Pax4Model._current.Load("Model/lavaandiceEnemyLava" + _lavaIndex);
            Pax4Model._current.Load("Model/lavaandiceEnemyIce" + _iceIndex);

            Pax4Model._current.SetDefaultParameters();

            Pax4Ui._current.Enter("fgIce");
            Pax4WorldLavaAndIce._missionType = ELavaAndIceMissionType._ICE;
            
            IniSingle();

            Pax4ActorWorld world = new Pax4ActorWorld("lavaandiceq1m3", null);
            world.SetModel("Model/lavaandiceq1m4");
            world.Enable();

            SetDifficultyTimer(55, 70, 85, 100, true);

            //************************************************************
            //monster***
            Vector3 monsterPosition = new Vector3(0.0f, 6.0f, 0.0f);
            Pax4WayPointPath monsterWayPointPath = new Pax4WayPointPath(true);
            monsterWayPointPath.GenerateWayPoint(monsterPosition);
            monsterWayPointPath.Enable();

            //***ammo***
            Pax4WayPointPath wayPointPath = null;

            wayPointPath = new Pax4WayPointPath();
            wayPointPath.GenerateCircleWayPoints(0.0f, monsterPosition, 8.0f, 8.0f, 8);
            wayPointPath.Enable();
            PopulateWithActors(_physicsPartList, EPopulateWithActorsType._HOMOGENEOUS, false);

            wayPointPath = new Pax4WayPointPath();
            wayPointPath.GenerateCircleWayPoints(-0.785398f, monsterPosition, 5.0f, 5.0f, 8);//2.35619f
            wayPointPath.Enable();
            PopulateWithActors(_physicsPartList, EPopulateWithActorsType._HOMOGENEOUS);       
        }

        private void UpdateM4(GameTime gameTime)
        {
        }

        private void IniM5()
        {
            _lavaIndex = 2;
            _iceIndex = 2;

            Pax4Model._current.Load("Model/lavaandiceAmmoIce");
            Pax4Model._current.Load("Model/lavaandiceq1m5");

            Pax4Model._current.Load("Model/lavaandiceEnemyLava" + _lavaIndex);
            Pax4Model._current.Load("Model/lavaandiceEnemyIce" + _iceIndex);

            Pax4Model._current.SetDefaultParameters();

            Pax4Ui._current.Enter("fgIce");
            Pax4WorldLavaAndIce._missionType = ELavaAndIceMissionType._ICE;

            IniSingle();

            Pax4ActorWorld world = new Pax4ActorWorld("lavaandiceq1m5", null);
            world.SetModel("Model/lavaandiceq1m5");
            world.Enable();

            SetDifficultyTimer(55, 70, 85, 100, true);

            Pax4WayPointPath waypointPath = null;
            Pax4ModifierWayPointPath wayPointPathModifier = null;

            float duration = 30.0f / _difficulty;

            //************************************************************
            //monster***
            Vector3 monsterPosition = new Vector3(0.0f, 4.0f, 0.0f);
            Pax4WayPointPath monsterWayPointPath = new Pax4WayPointPath(true);
            monsterWayPointPath.GenerateWayPoint(monsterPosition);
            monsterWayPointPath.SetPosition(new Vector3(0.0f, 8.0f, 0.0f));

            wayPointPathModifier = new Pax4ModifierWayPointPathRotationZ("", null);
            _wayPointPathModifier.Add(wayPointPathModifier);
            ((Pax4ModifierWayPointPathRotationZ)wayPointPathModifier).Ini(MathHelper.TwoPi, 0.0f, duration / 4.0f);
            wayPointPathModifier.AddPath(monsterWayPointPath);
            wayPointPathModifier.SetContinuous();
            wayPointPathModifier.Trigger();

            int wayPointPathCount = 5;
            Vector3 position = Vector3.Zero;
            wayPointPathModifier = new Pax4ModifierWayPointPathRotationZ("", null);
            _wayPointPathModifier.Add(wayPointPathModifier);
            ((Pax4ModifierWayPointPathRotationZ)wayPointPathModifier).Ini(0.0f, MathHelper.TwoPi, duration);
            wayPointPathModifier.SetContinuous();

            Vector3 position0 = new Vector3(-8.0f, 8.0f, 0.0f);
            float wayPointStepX = Math.Abs(position0.X * 2.0f / (wayPointPathCount - 1));
            float wayPointStepY = Math.Abs(position0.Y * 2.0f / (wayPointPathCount - 1));

            for (int i = 0; i < wayPointPathCount; i++)
            {
                position.X = position0.X + i * wayPointStepX;
                position.Y = 0;
                waypointPath = new Pax4WayPointPath();
                _wayPointPath.Add(waypointPath);
                waypointPath.GenerateWayPoint(position);
                wayPointPathModifier.AddPath(waypointPath);

                position.X = 0;
                position.Y = position0.Y - i * wayPointStepY;
                waypointPath = new Pax4WayPointPath();
                _wayPointPath.Add(waypointPath);
                waypointPath.GenerateWayPoint(position);
                wayPointPathModifier.AddPath(waypointPath);
            }

            wayPointPathModifier.Trigger();

            wayPointPathCount *= 2;

            float velocity0 = 4.0f;
            //************************************************************
            //************************************************************
            //monster     
            Pax4ActorEnemyMonsterLava monster = new Pax4ActorEnemyMonsterLava(_lavaIndex.ToString(), null, _lavaIndex);
            monster.MoveTo(Vector3.Zero, monsterPosition);
            new Pax4WayPointControllerActor(monster, velocity0, monsterWayPointPath);
            monster.Enable();             

            //************************************************************
            //************************************************************
            //ammo
            Pax4Actor actor = null;
            for (int i = 0; i < wayPointPathCount; i++)
            {
                if (i != (wayPointPathCount - 1) / 2)
                {
                    actor = new Pax4ActorEnemyAmmoIce(_iceIndex.ToString(), null, _iceIndex);
                    actor.MoveTo(Vector3.Zero, _wayPointPath[i]._wayPoint[0]);
                    if (i % 4 == 0)
                        actor.SetPowerUp(Pax4Actor.EActorPowerUp._DURABILITY);
                    new Pax4WayPointControllerActor(actor, velocity0, _wayPointPath[i], 0);
                    actor.Enable();
                }

                i++;

                if (i >= wayPointPathCount)
                    break;

                actor = new Pax4ActorEnemyAmmoLava(_lavaIndex.ToString(), null, _lavaIndex);
                actor.MoveTo(Vector3.Zero, _wayPointPath[i]._wayPoint[0]);
                if (i % 5 == 0)
                    actor.SetPowerUp(Pax4Actor.EActorPowerUp._DURABILITY);
                new Pax4WayPointControllerActor(actor, velocity0, _wayPointPath[i], 0);
                actor.Enable();
            }

            _wayPointPath.Add(monsterWayPointPath);
        }

        private void UpdateM5(GameTime gameTime)
        {
        }

        private void IniM6()
        {
            _lavaIndex = 3;
            _iceIndex = 3;

            Pax4Model._current.Load("Model/lavaandiceAmmoIce");
            Pax4Model._current.Load("Model/lavaandiceq1m6");

            Pax4Model._current.Load("Model/lavaandiceEnemyLava" + _lavaIndex);
            Pax4Model._current.Load("Model/lavaandiceEnemyIce" + _iceIndex);

            Pax4Model._current.SetDefaultParameters();

            Pax4Ui._current.Enter("fgIce");
            Pax4WorldLavaAndIce._missionType = ELavaAndIceMissionType._ICE;

            IniSingle();

            Pax4ActorWorld world = new Pax4ActorWorld("lavaandiceq1m6", null);
            world.SetModel("Model/lavaandiceq1m6");
            world.Enable();

            SetDifficultyTimer(55, 70, 85, 100, false);

            Pax4WayPointPath waypointPath = null;
            Pax4ModifierWayPointPath wayPointPathModifier = null;

            float duration = 30.0f / _difficulty;

            //************************************************************
            //monster***
            Vector3 monsterPosition = new Vector3(0.0f, 10.5f, 0.0f);
            Pax4WayPointPath monsterWayPointPath = new Pax4WayPointPath(true);
            monsterWayPointPath.GenerateCircleWayPoints(0.0f, monsterPosition, 6.0f, 0.2f, 4);

            Vector3 position = Vector3.Zero;
            wayPointPathModifier = new Pax4ModifierWayPointPathRotationZ("", null);
            _wayPointPathModifier.Add(wayPointPathModifier);
            ((Pax4ModifierWayPointPathRotationZ)wayPointPathModifier).Ini(0.0f, MathHelper.TwoPi, duration);
            wayPointPathModifier.SetContinuous();

            int wayPointPathCount = 4;
            Vector3 position0 = new Vector3(-5.0f, 5.0f, 0.0f);
            float wayPointStepX = Math.Abs(position0.X * 2.0f / (wayPointPathCount - 1));
            float wayPointStepY = Math.Abs(position0.Y * 2.0f / (wayPointPathCount - 1));

            Vector3 position1 = new Vector3(0.0f, -2.0f, 0.0f);
            for (int i = 0; i < wayPointPathCount; i++)
            {
                position.X = position0.X + i * wayPointStepX;

                for (int j = 0; j < wayPointPathCount; j++)
                {
                    position.Y = position0.Y - j * wayPointStepY;

                    waypointPath = new Pax4WayPointPath();
                    _wayPointPath.Add(waypointPath);
                    waypointPath.GenerateWayPoint(position);
                    waypointPath.SetPosition(position1);
                    wayPointPathModifier.AddPath(waypointPath);
                }
            }

            wayPointPathModifier.Trigger();

            wayPointPathCount *= wayPointPathCount;

            float velocity0 = 5.0f;
            //************************************************************
            //************************************************************
            //monster     
            Pax4ActorEnemyMonsterLava monster = new Pax4ActorEnemyMonsterLava(_lavaIndex.ToString(), null, _lavaIndex);
            monster.MoveTo(Vector3.Zero, monsterPosition);
            new Pax4WayPointControllerActor(monster, _difficulty, monsterWayPointPath);
            monster.Enable();            

            //************************************************************
            //************************************************************
            //ammo
            Pax4Actor actor = null;
            for (int i = 0; i < wayPointPathCount; i++)
            {
                if (i != (wayPointPathCount - 1) / 2)
                {
                    actor = new Pax4ActorEnemyAmmoIce(_iceIndex.ToString(), null, _iceIndex);
                    actor.MoveTo(Vector3.Zero, _wayPointPath[i]._wayPoint[0]);
                    if (i % 6 == 0)
                        actor.SetPowerUp(Pax4Actor.EActorPowerUp._DURABILITY);
                    new Pax4WayPointControllerActor(actor, velocity0, _wayPointPath[i], 0);
                    actor.Enable();
                }

                i++;

                if (i >= wayPointPathCount)
                    break;

                actor = new Pax4ActorEnemyAmmoLava(_lavaIndex.ToString(), null, _lavaIndex);
                actor.MoveTo(Vector3.Zero, _wayPointPath[i]._wayPoint[0]);
                if (i % 7 == 0)
                    actor.SetPowerUp(Pax4Actor.EActorPowerUp._DURABILITY);
                new Pax4WayPointControllerActor(actor, velocity0, _wayPointPath[i], 0);
                actor.Enable();
            }

            _wayPointPath.Add(monsterWayPointPath); 
        }

        private void UpdateM6(GameTime gameTime)
        {
        }

        private void IniM7()
        {
            _lavaIndex = 4;
            _iceIndex = 4;

            Pax4Model._current.Load("Model/lavaandiceAmmoLava");
            Pax4Model._current.Load("Model/lavaandiceAmmoIce");
            Pax4Model._current.Load("Model/lavaandiceq1m7");

            Pax4Model._current.Load("Model/lavaandiceEnemyLava" + _lavaIndex);
            Pax4Model._current.Load("Model/lavaandiceEnemyIce" + _iceIndex);

            Pax4Model._current.SetDefaultParameters();

            Pax4Ui._current.Enter("fgLavaAndIce");
            Pax4WorldLavaAndIce._missionType = ELavaAndIceMissionType._LAVA_AND_ICE;

                        
            IniDual();

            Pax4ActorWorld world = new Pax4ActorWorld("lavaandiceq1m7", null);
            world.SetModel("Model/lavaandiceq1m7");
            world.Enable();

            SetDifficultyTimer(100, 115, 130, 145, false);

            Pax4WayPointPath waypointPath = null;

            float duration = 15.0f / _difficulty;

            //************************************************************
            //monster***
            Vector3 monsterLavaPosition = new Vector3(0.0f, 6.0f, 0.0f);
            Pax4WayPointPath monsterLavaWayPointPath = new Pax4WayPointPath(true);
            monsterLavaWayPointPath.GenerateCircleWayPoints(0.0f, monsterLavaPosition, 3.5f, 4.0f, 10, 0.0f);

            Vector3 monsterIcePosition = new Vector3(0.0f, 6.0f, 0.0f);
            Pax4WayPointPath monsterIceWayPointPath = new Pax4WayPointPath(true);
            monsterIceWayPointPath.GenerateCircleWayPoints(0.0f, monsterIcePosition, 3.5f, 4.0f, 10, (float)Math.PI);            

            Vector3 position = Vector3.Zero;
            int wayPointPathCount = 3;
            Vector3 position0 = new Vector3(-6.0f, 0.0f, 0.0f);
            float wayPointStepX = Math.Abs(position0.X * 2.0f / (wayPointPathCount - 1));

            Pax4ModifierWayPointPath wayPointPathModifier = null;
            //1************************************************************************************************
            wayPointPathModifier = new Pax4ModifierWayPointPathPosition("",null);
            _wayPointPathModifier.Add(wayPointPathModifier);
            for (int i = 0; i < wayPointPathCount; i++)
            {
                position.X = position0.X + i * wayPointStepX;

                waypointPath = new Pax4WayPointPath();
                _wayPointPath.Add(waypointPath);
                waypointPath.GenerateWayPoint(position);
                wayPointPathModifier.AddPath(waypointPath);
            }
            ((Pax4ModifierWayPointPathPosition)wayPointPathModifier).Ini(new Vector3(0.0f, 11.0f, 0.0f), new Vector3(0.0f, -9.0f, 0.0f), duration);
            ((Pax4ModifierWayPointPathPosition)wayPointPathModifier).SetOscillating();
            wayPointPathModifier.Trigger();
            //2************************************************************************************************
            wayPointPathModifier = new Pax4ModifierWayPointPathPosition("", null);
            _wayPointPathModifier.Add(wayPointPathModifier);
            for (int i = 0; i < wayPointPathCount; i++)
            {
                position.X = position0.X + i * wayPointStepX;

                waypointPath = new Pax4WayPointPath();
                _wayPointPath.Add(waypointPath);
                waypointPath.GenerateWayPoint(position);
                wayPointPathModifier.AddPath(waypointPath);
            }
            ((Pax4ModifierWayPointPathPosition)wayPointPathModifier).Ini(new Vector3(0.0f, -9.0f, 0.0f), new Vector3(0.0f, 11.0f, 0.0f), duration);
            ((Pax4ModifierWayPointPathPosition)wayPointPathModifier).SetOscillating();
            wayPointPathModifier.Trigger();
            //3************************************************************************************************
            position0 = new Vector3(0.0f, 6.0f, 0.0f);
            wayPointPathModifier = new Pax4ModifierWayPointPathPosition("", null);
            _wayPointPathModifier.Add(wayPointPathModifier);
            position.X = 0.0f;
            for (int i = 0; i < wayPointPathCount; i++)
            {
                position.Y = position0.Y - i * wayPointStepX;

                waypointPath = new Pax4WayPointPath();
                _wayPointPath.Add(waypointPath);
                waypointPath.GenerateWayPoint(position);
                wayPointPathModifier.AddPath(waypointPath);
            }
            ((Pax4ModifierWayPointPathPosition)wayPointPathModifier).Ini(new Vector3(-8.0f, 0.0f, 0.0f), new Vector3(8.0f, 0.0f, 0.0f), duration);
            ((Pax4ModifierWayPointPathPosition)wayPointPathModifier).SetOscillating();
            wayPointPathModifier.Trigger();
            //4************************************************************************************************

            wayPointPathModifier = new Pax4ModifierWayPointPathPosition("", null);
            _wayPointPathModifier.Add(wayPointPathModifier);
            position.X = 0.0f;
            for (int i = 0; i < wayPointPathCount; i++)
            {
                position.Y = position0.Y - i * wayPointStepX;

                waypointPath = new Pax4WayPointPath();
                _wayPointPath.Add(waypointPath);
                waypointPath.GenerateWayPoint(position);
                wayPointPathModifier.AddPath(waypointPath);
            }
            ((Pax4ModifierWayPointPathPosition)wayPointPathModifier).Ini(new Vector3(8.0f, 0.0f, 0.0f), new Vector3(-8.0f, 0.0f, 0.0f), duration);
            ((Pax4ModifierWayPointPathPosition)wayPointPathModifier).SetOscillating();
            wayPointPathModifier.Trigger();


            wayPointPathCount *= 4;
            float velocity0 = 4.0f;
            //************************************************************
            //************************************************************
            //monster     
            Pax4ActorEnemyMonsterLava monsterLava = new Pax4ActorEnemyMonsterLava(_lavaIndex.ToString(), null, _lavaIndex);
            monsterLava.MoveTo(Vector3.Zero, new Vector3(3.0f, 6.0f, 0.0f));
            new Pax4WayPointControllerActor(monsterLava, _difficulty, monsterLavaWayPointPath);
            monsterLava.Enable();

            Pax4ActorEnemyMonsterIce monsterIce = new Pax4ActorEnemyMonsterIce(_iceIndex.ToString(), null, _iceIndex);
            monsterIce.MoveTo(Vector3.Zero, new Vector3(-3.0f, 6.0f, 0.0f));
            new Pax4WayPointControllerActor(monsterIce, _difficulty, monsterIceWayPointPath);
            monsterIce.Enable();           

            //************************************************************
            //************************************************************
            //ammo
            Pax4Actor actor = null;
            for (int i = 0; i < wayPointPathCount; i++)
            {
                actor = new Pax4ActorEnemyAmmoLava(_lavaIndex.ToString(), null, _lavaIndex);
                actor.SetScale(Vector3.One * 1.8f);
                actor.MoveTo(Vector3.Zero, _wayPointPath[i]._wayPoint[0]);
                if (i % 6 == 0)
                    actor.SetPowerUp(Pax4Actor.EActorPowerUp._DURABILITY);
                new Pax4WayPointControllerActor(actor, velocity0, _wayPointPath[i], 0);
                actor.Enable();

                i++;

                if (i >= wayPointPathCount)
                    break;

                actor = new Pax4ActorEnemyAmmoIce(_iceIndex.ToString(), null, _iceIndex);
                actor.SetScale(Vector3.One * 1.8f);
                actor.MoveTo(Vector3.Zero, _wayPointPath[i]._wayPoint[0]);
                if (i % 7 == 0)
                    actor.SetPowerUp(Pax4Actor.EActorPowerUp._DURABILITY);
                new Pax4WayPointControllerActor(actor, velocity0, _wayPointPath[i], 0);
                actor.Enable();
            }

            _wayPointPath.Add(monsterLavaWayPointPath);
            _wayPointPath.Add(monsterIceWayPointPath);
        }

        private void UpdateM7(GameTime gameTime)
        {
        }

        private void IniM8()
        {
            _lavaIndex = 5;
            _iceIndex = 5;

            Pax4Model._current.Load("Model/lavaandiceAmmoLava");
            Pax4Model._current.Load("Model/lavaandiceAmmoIce");
            Pax4Model._current.Load("Model/lavaandiceq1m8");

            Pax4Model._current.Load("Model/lavaandiceEnemyLava" + _lavaIndex);
            Pax4Model._current.Load("Model/lavaandiceEnemyIce" + _iceIndex);

            Pax4Model._current.SetDefaultParameters();

            Pax4Ui._current.Enter("fgLavaAndIce");
            Pax4WorldLavaAndIce._missionType = ELavaAndIceMissionType._LAVA_AND_ICE;


            IniDual();

            Pax4ActorWorld world = new Pax4ActorWorld("lavaandiceq1m8", null);
            world.SetModel("Model/lavaandiceq1m8");
            world.Enable();

            SetDifficultyTimer(100, 115, 130, 145, false);

            Pax4WayPointPath waypointPath = null;

            float duration = 30.0f / _difficulty;

            //************************************************************
            //monster***
            Vector3 monsterLavaPosition = new Vector3(-4.3f, 11.5f, 0.0f);
            Pax4WayPointPath monsterLavaWayPointPath = new Pax4WayPointPath(true);
            monsterLavaWayPointPath.GenerateWayPoint(monsterLavaPosition);

            Vector3 monsterIcePosition = new Vector3(4.3f, 11.5f, 0.0f);
            Pax4WayPointPath monsterIceWayPointPath = new Pax4WayPointPath(true);
            monsterIceWayPointPath.GenerateWayPoint(monsterIcePosition);            

            Vector3 position = Vector3.Zero;
            int wayPointPathRowCount = 5;
            int wayPointPathColumnCount = 4;
            int wayPointPathCount = wayPointPathRowCount * wayPointPathColumnCount;
            Vector3 position0 = new Vector3(-7.0f, 6.0f, 0.0f);
            float wayPointStepX = Math.Abs(position0.X * 2.0f / (wayPointPathColumnCount - 1));
            float wayPointStepY = Math.Abs(position0.Y * 2.0f / (wayPointPathRowCount - 1));

            for (int i = 0; i < wayPointPathRowCount; i++)
            {
                position.Y = position0.Y - i * wayPointStepY;

                for (int j = 0; j < wayPointPathColumnCount; j++)
                {
                    position.X = position0.X + j * wayPointStepX;

                    waypointPath = new Pax4WayPointPath();
                    _wayPointPath.Add(waypointPath);
                    waypointPath.GenerateWayPoint(position);
                }
            }

            float velocity0 = 5.0f;
            //************************************************************
            //************************************************************
            //monster     
            Pax4ActorEnemyMonsterLava monsterLava = new Pax4ActorEnemyMonsterLava(_lavaIndex.ToString(), null, _lavaIndex);
            monsterLava.MoveTo(Vector3.Zero, monsterLavaPosition);
            new Pax4WayPointControllerActor(monsterLava, _difficulty, monsterLavaWayPointPath);
            monsterLava.Enable();

            Pax4ActorEnemyMonsterIce monsterIce = new Pax4ActorEnemyMonsterIce(_iceIndex.ToString(), null, _iceIndex);
            monsterIce.MoveTo(Vector3.Zero, monsterIcePosition);
            new Pax4WayPointControllerActor(monsterIce, _difficulty, monsterIceWayPointPath);
            monsterIce.Enable();

            //************************************************************
            //************************************************************
            //ammo
            Pax4Actor actor = null;
            for (int i = 0; i < wayPointPathCount; i++)
            {
                actor = new Pax4ActorEnemyAmmoLava(_lavaIndex.ToString(), null, _lavaIndex);
                actor.MoveTo(Vector3.Zero, _wayPointPath[i]._wayPoint[0]);
                if (i % 6 == 0)
                    actor.SetPowerUp(Pax4Actor.EActorPowerUp._DURABILITY);
                new Pax4WayPointControllerActor(actor, velocity0, _wayPointPath[i], 0);
                actor.Enable();

                i++;

                if (i >= wayPointPathCount)
                    break;

                actor = new Pax4ActorEnemyAmmoIce(_iceIndex.ToString(), null, _iceIndex);
                actor.MoveTo(Vector3.Zero, _wayPointPath[i]._wayPoint[0]);
                if (i % 7 == 0)
                    actor.SetPowerUp(Pax4Actor.EActorPowerUp._DURABILITY);
                new Pax4WayPointControllerActor(actor, velocity0, _wayPointPath[i], 0);
                actor.Enable();
            }

            _wayPointPath.Add(monsterLavaWayPointPath);
            _wayPointPath.Add(monsterIceWayPointPath);
        }

        private void UpdateM8(GameTime gameTime)
        {
        }

        private void IniM9()
        {
            _lavaIndex = 9;
            _iceIndex = 9;

            Pax4Model._current.Load("Model/lavaandiceAmmoLava");
            Pax4Model._current.Load("Model/lavaandiceAmmoIce");
            Pax4Model._current.Load("Model/lavaandiceq1m9");

            Pax4Model._current.Load("Model/lavaandiceEnemyLava" + _lavaIndex);
            Pax4Model._current.Load("Model/lavaandiceEnemyIce" + _iceIndex);

            Pax4Model._current.SetDefaultParameters();

            Pax4Ui._current.Enter("fgLavaAndIce");
            Pax4WorldLavaAndIce._missionType = ELavaAndIceMissionType._LAVA_AND_ICE;

            IniDual();

            Pax4ActorWorld world = new Pax4ActorWorld("lavaandiceq1m9", null);
            world.SetModel("Model/lavaandiceq1m9");
            world.Enable();

            SetDifficultyTimer(100, 115, 130, 145, false);

            Pax4WayPointPath waypointPath = null;

            float duration = 30.0f / _difficulty;

            //************************************************************
            //monster***
            Vector3 monsterLavaPosition = new Vector3(0.0f, 6.0f, 0.0f);
            Pax4WayPointPath monsterLavaWayPointPath = new Pax4WayPointPath(true);
            monsterLavaWayPointPath.GenerateCircleWayPoints(0.0f, monsterLavaPosition, 3.5f, 4.0f, 10, 0.0f);

            Vector3 monsterIcePosition = new Vector3(0.0f, 6.0f, 0.0f);
            Pax4WayPointPath monsterIceWayPointPath = new Pax4WayPointPath(true);
            monsterIceWayPointPath.GenerateCircleWayPoints(0.0f, monsterIcePosition, 3.5f, 4.0f, 10, (float)Math.PI);

            Vector3 position = Vector3.Zero;
            int wayPointPathRowCount = 3;
            int wayPointPathColumnCount = 5;
            int wayPointPathCount = wayPointPathRowCount * wayPointPathColumnCount;
            Vector3 position0 = new Vector3(-7.0f, 2.0f, 0.0f);
            float wayPointStepX = Math.Abs(position0.X * 2.0f / (wayPointPathColumnCount - 1));
            float wayPointStepY = wayPointStepX * 0.75f;// Math.Abs(position0.Y * 2.0f / (wayPointPathRowCount - 1));

            Vector3 position1 = new Vector3(0.0f, -4.5f, 0.0f);
            for (int i = 0; i < wayPointPathRowCount; i++)
            {
                position.Y = position0.Y - i * wayPointStepY;

                for (int j = 0; j < wayPointPathColumnCount; j++)
                {
                    position.X = position0.X + j * wayPointStepX;

                    waypointPath = new Pax4WayPointPath();
                    _wayPointPath.Add(waypointPath);
                    waypointPath.GenerateWayPoint(position);
                    waypointPath.SetPosition(position1);
                }
            }

            Pax4ModifierWayPointPath wayPointPathModifier = null;
            wayPointPathModifier = new Pax4ModifierWayPointPathRotationZ("", null);
            _wayPointPathModifier.Add(wayPointPathModifier);
            ((Pax4ModifierWayPointPathRotationZ)wayPointPathModifier).Ini(0.0f, MathHelper.TwoPi, duration);
            wayPointPathModifier.SetContinuous();

            int wayPointPathRowCount1 = 4;
            wayPointPathCount += wayPointPathRowCount1 * 2;
            position1 = new Vector3(0.0f, 2.5f, 0.0f);
            Vector3 position2 = new Vector3(0.0f, 6.0f, 0.0f);
            for (int i = 0; i < wayPointPathRowCount1; i++)
            {
                position.Y = position1.Y - i * wayPointStepY;

                position.X = -7.0f;
                waypointPath = new Pax4WayPointPath();
                _wayPointPath.Add(waypointPath);
                waypointPath.GenerateWayPoint(position);
                waypointPath.SetPosition(position2);
                wayPointPathModifier.AddPath(waypointPath);

                position.X = 7.0f;
                waypointPath = new Pax4WayPointPath();
                _wayPointPath.Add(waypointPath);
                waypointPath.GenerateWayPoint(position);
                waypointPath.SetPosition(position2);
                wayPointPathModifier.AddPath(waypointPath);
            }

            wayPointPathModifier.Trigger();

            float velocity0 = 5.0f;
            //************************************************************
            //************************************************************
            //monster     
            Pax4ActorEnemyMonsterLava monsterLava = new Pax4ActorEnemyMonsterLava(_lavaIndex.ToString(), null, _lavaIndex);
            monsterLava.MoveTo(Vector3.Zero, new Vector3(3.0f, 6.0f, 0.0f));
            new Pax4WayPointControllerActor(monsterLava, _difficulty, monsterLavaWayPointPath);
            monsterLava.Enable();

            Pax4ActorEnemyMonsterIce monsterIce = new Pax4ActorEnemyMonsterIce(_iceIndex.ToString(), null, _iceIndex);
            monsterIce.MoveTo(Vector3.Zero, new Vector3(-3.0f, 6.0f, 0.0f));
            new Pax4WayPointControllerActor(monsterIce, _difficulty, monsterIceWayPointPath);
            monsterIce.Enable();

            //************************************************************
            //************************************************************
            //ammo
            Pax4Actor actor = null;
            for (int i = 0; i < wayPointPathCount; i++)
            {
                actor = new Pax4ActorEnemyAmmoLava(_lavaIndex.ToString(), null, _lavaIndex);
                actor.SetScale(Vector3.One * 1.8f);
                actor.MoveTo(Vector3.Zero, _wayPointPath[i]._wayPoint[0]);
                if (i % 6 == 0)
                    actor.SetPowerUp(Pax4Actor.EActorPowerUp._DURABILITY);
                new Pax4WayPointControllerActor(actor, velocity0, _wayPointPath[i], 0);
                actor.Enable();

                i++;

                if (i >= wayPointPathCount)
                    break;

                actor = new Pax4ActorEnemyAmmoIce(_iceIndex.ToString(), null, _iceIndex);
                actor.SetScale(Vector3.One * 1.8f);
                actor.MoveTo(Vector3.Zero, _wayPointPath[i]._wayPoint[0]);
                if (i % 7 == 0)
                    actor.SetPowerUp(Pax4Actor.EActorPowerUp._DURABILITY);
                new Pax4WayPointControllerActor(actor, velocity0, _wayPointPath[i], 0);
                actor.Enable();
            }

            _wayPointPath.Add(monsterLavaWayPointPath);
            _wayPointPath.Add(monsterIceWayPointPath);
        }

        private void UpdateM9(GameTime gameTime)
        {
        }

        private void IniM10()
        {
            //**********************
            //*** load resources ***
            //**********************
            _lavaIndex = 10;
            _iceIndex = 10;

            Pax4Model._current.Load("Model/lavaandiceAmmoLava");
            Pax4Model._current.Load("Model/lavaandiceAmmoIce");
            Pax4Model._current.Load("Model/lavaandiceq1m1");

            Pax4Model._current.Load("Model/lavaandiceEnemyLava" + _lavaIndex);
            Pax4Model._current.Load("Model/lavaandiceEnemyIce" + _iceIndex);

            Pax4Model._current.SetDefaultParameters();

            Pax4Ui._current.Enter("fgLavaAndIce");
            Pax4WorldLavaAndIce._missionType = ELavaAndIceMissionType._LAVA_AND_ICE;

            IniDual();

            //********************
            //*** create world ***
            //********************

            Pax4ActorWorld world = new Pax4ActorWorld("lavaandiceq1m1", null);
            world.SetModel("Model/lavaandiceq1m1");
            world.Enable();

            SetDifficultyTimer(100, 115, 130, 145, false);

            //*************************************************
            //*** create waypoint paths and their modifiers ***
            //*************************************************

            //***monster***
            Pax4WayPointPath monsterLavaWayPointPath = new Pax4WayPointPath(true);            
            monsterLavaWayPointPath.GenerateCircleWayPoints(0.0f, new Vector3(4.0f, 10.0f, 0.0f), 4.0f, 0.0f, 2, 0.0f);
            monsterLavaWayPointPath.Enable();

            Pax4WayPointPath monsterIceWayPointPath = new Pax4WayPointPath(true);
            monsterIceWayPointPath.GenerateCircleWayPoints(0.0f, new Vector3(-4.0f, 10.0f, 0.0f), 4.0f, 0.0f, 2, (float)Math.PI);
            monsterIceWayPointPath.Enable();

            PopulateWithActors(_physicsPartList, EPopulateWithActorsType._HETEROGENEOUS);

            //***ammo***
            Pax4WayPointPath wayPointPath = null;

            wayPointPath = new Pax4WayPointPath();
            wayPointPath.GenerateCircleWayPoints(0.785398f, new Vector3(-2.5f, 2.0f, 0.0f), 3.0f, 8.0f, 10);            
            wayPointPath.Enable();
            PopulateWithActors(_physicsPartList, EPopulateWithActorsType._HOMOGENEOUS);

            wayPointPath = new Pax4WayPointPath();
            wayPointPath.GenerateCircleWayPoints(-0.785398f, new Vector3(2.5f, 2.0f, 0.0f), 3.0f, 8.0f, 10);//2.35619f
            wayPointPath.Enable();
            PopulateWithActors(_physicsPartList, EPopulateWithActorsType._HOMOGENEOUS, false);

            wayPointPath = new Pax4WayPointPath();
            wayPointPath.GenerateCircleWayPoints(0.0f, new Vector3(0.0f, 8.0f, 0.0f), 3.0f, 4.0f, 8);
            wayPointPath.Enable();
            PopulateWithActors(_physicsPartList, EPopulateWithActorsType._HETEROGENEOUS, false);
        }
        private void UpdateM10(GameTime gameTime)
        {
        }

        private void IniM11()
        {
            //**********************
            //*** load resources ***
            //**********************
            _lavaIndex = 1;
            _iceIndex = 1;

            Pax4Model._current.Load("Model/lavaandiceAmmoLava");
            Pax4Model._current.Load("Model/lavaandiceAmmoIce");
            Pax4Model._current.Load("Model/lavaandiceq1m2");

            Pax4Model._current.Load("Model/lavaandiceEnemyLava" + _lavaIndex);
            Pax4Model._current.Load("Model/lavaandiceEnemyIce" + _iceIndex);

            Pax4Model._current.SetDefaultParameters();

            Pax4Ui._current.Enter("fgLavaAndIce");
            Pax4WorldLavaAndIce._missionType = ELavaAndIceMissionType._LAVA_AND_ICE;

            IniDual();

            //********************
            //*** create world ***
            //********************

            Pax4ActorWorld world = new Pax4ActorWorld("lavaandiceq1m2", null);
            world.SetModel("Model/lavaandiceq1m2");
            world.Enable();

            SetDifficultyTimer(100, 115, 130, 145, false);

            //*************************************************
            //*** create waypoint paths and their modifiers ***
            //*************************************************

            //***monster***
            Pax4WayPointPath monsterLavaWayPointPath = new Pax4WayPointPath(true);
            monsterLavaWayPointPath.GenerateCircleWayPoints(0.0f, new Vector3(4.0f, 11.0f, 0.0f), 4.0f, 1.0f, 10, 0.0f);
            monsterLavaWayPointPath.Enable();

            Pax4WayPointPath monsterIceWayPointPath = new Pax4WayPointPath(true);
            monsterIceWayPointPath.GenerateCircleWayPoints(0.0f, new Vector3(-4.0f, 11.0f, 0.0f), 4.0f, 1.0f, 10, (float)Math.PI);
            monsterIceWayPointPath.Enable();

            PopulateWithActors(_physicsPartList, EPopulateWithActorsType._HETEROGENEOUS);

            //***ammo***
            Pax4WayPointPath wayPointPath = null;

            wayPointPath = new Pax4WayPointPath();
            wayPointPath.GenerateCircleWayPoints(0.785398f, new Vector3(-5.5f, 4.0f, 0.0f), 3.0f, 4.5f, 8);
            wayPointPath.Enable();
            PopulateWithActors(_physicsPartList, EPopulateWithActorsType._HOMOGENEOUS);

            wayPointPath = new Pax4WayPointPath();
            wayPointPath.GenerateCircleWayPoints(-0.785398f, new Vector3(5.5f, 4.0f, 0.0f), 3.0f, 4.5f, 8, 0.0f, false);//2.35619f
            wayPointPath.Enable();
            PopulateWithActors(_physicsPartList, EPopulateWithActorsType._HOMOGENEOUS, false);

            wayPointPath = new Pax4WayPointPath();
            wayPointPath.GenerateCircleWayPoints(0.0f, new Vector3(0.0f, 3.5f, 0.0f), 3.0f, 4.0f, 6);
            wayPointPath.Enable();
            PopulateWithActors(_physicsPartList, EPopulateWithActorsType._HETEROGENEOUS, false);

            Pax4WayPointPathList wayPointPaths = null;
            wayPointPath = new Pax4WayPointPath();
            wayPointPath.GenerateLineWayPoints(0.0f, new Vector3(0.0f, -5.0f, 0.0f), 16.0f, 8);
            wayPointPaths = wayPointPath.ToWayPointPaths();
            wayPointPaths.Enable();
            PopulateWithActors(_physicsPartList, EPopulateWithActorsType._HETEROGENEOUS);
        }
        private void UpdateM11(GameTime gameTime)
        {
        }

        private void IniM12()
        {
            //**********************
            //*** load resources ***
            //**********************
            _lavaIndex = 2;
            _iceIndex = 1;

            Pax4Model._current.Load("Model/lavaandiceAmmoLava");
            Pax4Model._current.Load("Model/lavaandiceAmmoIce");
            Pax4Model._current.Load("Model/lavaandiceq1m3");

            Pax4Model._current.Load("Model/lavaandiceEnemyLava" + _lavaIndex);
            Pax4Model._current.Load("Model/lavaandiceEnemyIce" + _iceIndex);

            Pax4Model._current.SetDefaultParameters();

            Pax4Ui._current.Enter("fgLavaAndIce");
            Pax4WorldLavaAndIce._missionType = ELavaAndIceMissionType._LAVA_AND_ICE;

            IniDual();

            //********************
            //*** create world ***
            //********************

            Pax4ActorWorld world = new Pax4ActorWorld("lavaandiceq1m3", null);
            world.SetModel("Model/lavaandiceq1m3");
            world.Enable();

            SetDifficultyTimer(100, 115, 130, 145, false);

            //*************************************************
            //*** create waypoint paths and their modifiers ***
            //*************************************************

            //***monster***
            Pax4WayPointPath monsterLavaWayPointPath = new Pax4WayPointPath(true);
            monsterLavaWayPointPath.GenerateCircleWayPoints(1.0f, new Vector3(4.0f, 10.0f, 0.0f), 4.0f, 1.0f, 10, 0.0f);
            monsterLavaWayPointPath.Enable();

            Pax4WayPointPath monsterIceWayPointPath = new Pax4WayPointPath(true);
            monsterIceWayPointPath.GenerateCircleWayPoints(-1.0f, new Vector3(-4.0f, 10.0f, 0.0f), 4.0f, 1.0f, 10, (float)Math.PI);
            monsterIceWayPointPath.Enable();

            PopulateWithActors(_physicsPartList, EPopulateWithActorsType._HETEROGENEOUS);

            //***ammo***
            Pax4WayPointPath wayPointPath = null;

            wayPointPath = new Pax4WayPointPath();
            wayPointPath.GenerateCircleWayPoints(0.785398f, new Vector3(5.0f, -3.5f, 0.0f), 3.0f, 4.0f, 6, 0.0f, false);
            wayPointPath.Enable();
            PopulateWithActors(_physicsPartList, EPopulateWithActorsType._HOMOGENEOUS, false);

            wayPointPath = new Pax4WayPointPath();
            wayPointPath.GenerateCircleWayPoints(2.35619f, new Vector3(5.0f, 3.5f, 0.0f), 3.0f, 4.0f, 6, 0.0f);
            wayPointPath.Enable();
            PopulateWithActors(_physicsPartList, EPopulateWithActorsType._HOMOGENEOUS);

            wayPointPath = new Pax4WayPointPath();
            wayPointPath.GenerateCircleWayPoints(2.35619f, new Vector3(-5.0f, -3.5f, 0.0f), 3.0f, 4.0f, 6, 0.0f);
            wayPointPath.Enable();
            PopulateWithActors(_physicsPartList, EPopulateWithActorsType._HOMOGENEOUS, false);

            wayPointPath = new Pax4WayPointPath();
            wayPointPath.GenerateCircleWayPoints(3.92699f, new Vector3(-5.0f, 3.5f, 0.0f), 3.0f, 4.0f, 6, 0.0f, false);
            wayPointPath.Enable();
            PopulateWithActors(_physicsPartList, EPopulateWithActorsType._HOMOGENEOUS);

            wayPointPath = new Pax4WayPointPath();
            wayPointPath.GenerateCircleWayPoints(0.785398f, new Vector3(-1.5f, 7.5f, 0.0f), 3.0f, 3.5f, 6, 0.0f, false);
            wayPointPath.Enable();
            PopulateWithActors(_physicsPartList, EPopulateWithActorsType._LAYERED);

            wayPointPath = new Pax4WayPointPath();
            wayPointPath.GenerateCircleWayPoints(2.35619f, new Vector3(1.5f, 7.5f, 0.0f), 3.0f, 3.5f, 6);
            wayPointPath.Enable();
            PopulateWithActors(_physicsPartList, EPopulateWithActorsType._LAYERED);           
        }
        private void UpdateM12(GameTime gameTime)
        {
        }

        private void IniM13()
        {
            //**********************
            //*** load resources ***
            //**********************
            _lavaIndex = 1;
            _iceIndex = 3;

            Pax4Model._current.Load("Model/lavaandiceAmmoLava");
            Pax4Model._current.Load("Model/lavaandiceAmmoIce");
            Pax4Model._current.Load("Model/lavaandiceq1m4");

            Pax4Model._current.Load("Model/lavaandiceEnemyLava" + _lavaIndex);
            Pax4Model._current.Load("Model/lavaandiceEnemyIce" + _iceIndex);

            Pax4Model._current.SetDefaultParameters();

            Pax4Ui._current.Enter("fgLavaAndIce");
            Pax4WorldLavaAndIce._missionType = ELavaAndIceMissionType._LAVA_AND_ICE;

            IniDual();

            //********************
            //*** create world ***
            //********************

            Pax4ActorWorld world = new Pax4ActorWorld("lavaandiceq1m4", null);
            world.SetModel("Model/lavaandiceq1m4");
            world.Enable();

            SetDifficultyTimer(100, 115, 130, 145, false);

            //*************************************************
            //*** create waypoint paths and their modifiers ***
            //*************************************************

            //***monster***
            Pax4WayPointPath monsterLavaWayPointPath = new Pax4WayPointPath(true);
            monsterLavaWayPointPath.GenerateCircleWayPoints(-1.0f, new Vector3(4.0f, 9.0f, 0.0f), 4.0f, 0.0f, 10, 0.0f);
            monsterLavaWayPointPath.Enable();

            Pax4WayPointPath monsterIceWayPointPath = new Pax4WayPointPath(true);
            monsterIceWayPointPath.GenerateCircleWayPoints(1.0f, new Vector3(-4.0f, 9.0f, 0.0f), 4.0f, 0.0f, 10, (float)Math.PI);
            monsterIceWayPointPath.Enable();

            PopulateWithActors(_physicsPartList, EPopulateWithActorsType._HETEROGENEOUS);

            //***ammo***
            Pax4WayPointPath wayPointPath = null;

            wayPointPath = new Pax4WayPointPath();
            wayPointPath.GenerateCircleWayPoints(0.785398f, new Vector3(4.5f, 5.0f, 0.0f), 3.0f, 7.5f, 6, 0.0f, false);
            wayPointPath.Enable();
            PopulateWithActors(_physicsPartList, EPopulateWithActorsType._HOMOGENEOUS, false);

            wayPointPath = new Pax4WayPointPath();
            wayPointPath.GenerateCircleWayPoints(2.35619f, new Vector3(-4.5f, 5.0f, 0.0f), 3.0f, 7.5f, 6, 0.0f);
            wayPointPath.Enable();
            PopulateWithActors(_physicsPartList, EPopulateWithActorsType._HOMOGENEOUS, false);

            wayPointPath = new Pax4WayPointPath();
            wayPointPath.GenerateCircleWayPoints(0.785398f, new Vector3(5.0f, 1.0f, 0.0f), 3.0f, 4.0f, 6, 0.0f);
            wayPointPath.Enable();
            PopulateWithActors(_physicsPartList, EPopulateWithActorsType._HOMOGENEOUS);

            wayPointPath = new Pax4WayPointPath();
            wayPointPath.GenerateCircleWayPoints(2.35619f, new Vector3(-5.0f, 1.0f, 0.0f), 3.0f, 4.0f, 6, 0.0f, false);
            wayPointPath.Enable();
            PopulateWithActors(_physicsPartList, EPopulateWithActorsType._HOMOGENEOUS);

            Pax4WayPointPathList wayPointPaths = null;
            wayPointPath = new Pax4WayPointPath();
            wayPointPath.GenerateLineWayPoints(0.0f, new Vector3(0.0f, -5.0f, 0.0f), 12.0f, 6);
            wayPointPaths = wayPointPath.ToWayPointPaths(1, true);
            wayPointPaths.Enable();
            PopulateWithActors(_physicsPartList, EPopulateWithActorsType._LAYERED, true, wayPointPaths, 2.0f);

            Pax4ModifierWayPointPath wayPointPathModifier = new Pax4ModifierWayPointPathRotationZ("", null);
            wayPointPathModifier.Enable();            
            ((Pax4ModifierWayPointPathRotationZ)wayPointPathModifier).Ini(0.0f, MathHelper.TwoPi, 20.0f);
            wayPointPathModifier.AddPath(wayPointPaths);
            wayPointPathModifier.SetContinuous();
            wayPointPathModifier.Trigger();
        }
        private void UpdateM13(GameTime gameTime)
        {
        }

        private void IniM14()
        {
            //**********************
            //*** load resources ***
            //**********************
            _lavaIndex = 4;
            _iceIndex = 1;

            Pax4Model._current.Load("Model/lavaandiceAmmoLava");
            Pax4Model._current.Load("Model/lavaandiceAmmoIce");
            Pax4Model._current.Load("Model/lavaandiceq1m5");

            Pax4Model._current.Load("Model/lavaandiceEnemyLava" + _lavaIndex);
            Pax4Model._current.Load("Model/lavaandiceEnemyIce" + _iceIndex);

            Pax4Model._current.SetDefaultParameters();

            Pax4Ui._current.Enter("fgLavaAndIce");
            Pax4WorldLavaAndIce._missionType = ELavaAndIceMissionType._LAVA_AND_ICE;

            IniDual();

            //********************
            //*** create world ***
            //********************

            Pax4ActorWorld world = new Pax4ActorWorld("lavaandiceq1m5", null);
            world.SetModel("Model/lavaandiceq1m5");
            world.Enable();

            SetDifficultyTimer(100, 115, 130, 145, false);

            //*************************************************
            //*** create waypoint paths and their modifiers ***
            //*************************************************

            //***monster***
            Pax4WayPointPath monsterLavaWayPointPath = new Pax4WayPointPath(true);
            monsterLavaWayPointPath.GenerateCircleWayPoints(1.5f, new Vector3(4.0f, -1.0f, 0.0f), 4.0f, 1.0f, 10, 0.0f);
            monsterLavaWayPointPath.Enable();

            Pax4WayPointPath monsterIceWayPointPath = new Pax4WayPointPath(true);
            monsterIceWayPointPath.GenerateCircleWayPoints(-1.5f, new Vector3(-4.0f, -1.0f, 0.0f), 4.0f, 1.0f, 10, (float)Math.PI, false);
            monsterIceWayPointPath.Enable();

            PopulateWithActors(_physicsPartList, EPopulateWithActorsType._HETEROGENEOUS);

            //***ammo***
            Pax4WayPointPath wayPointPath = null;

            wayPointPath = new Pax4WayPointPath();
            wayPointPath.GenerateCircleWayPoints(0.785398f, new Vector3(4.5f, 9.5f, 0.0f), 3.0f, 3.5f, 8);
            wayPointPath.Enable();
            PopulateWithActors(_physicsPartList, EPopulateWithActorsType._HOMOGENEOUS);

            wayPointPath = new Pax4WayPointPath();
            wayPointPath.GenerateCircleWayPoints(-0.785398f, new Vector3(-4.5f, 9.5f, 0.0f), 3.0f, 3.5f, 8, 0.0f, false);//2.35619f
            wayPointPath.Enable();
            PopulateWithActors(_physicsPartList, EPopulateWithActorsType._HOMOGENEOUS);

            wayPointPath = new Pax4WayPointPath();
            wayPointPath.GenerateCircleWayPoints(0.0f, new Vector3(0.0f, 3.5f, 0.0f), 3.0f, 4.0f, 6);
            wayPointPath.Enable();
            PopulateWithActors(_physicsPartList, EPopulateWithActorsType._HETEROGENEOUS, false);

            wayPointPath = new Pax4WayPointPath();
            wayPointPath.GenerateRectangleWayPoints(0.0f, new Vector3(0.0f, 0.0f, 0.0f), 7.0f, 7.0f, 4, 4);
            wayPointPath.ToWayPointPaths().Enable();
        }
        private void UpdateM14(GameTime gameTime)
        {
        }

        private void IniM15()
        {
            //**********************
            //*** load resources ***
            //**********************
            _lavaIndex = 1;
            _iceIndex = 5;

            Pax4Model._current.Load("Model/lavaandiceAmmoLava");
            Pax4Model._current.Load("Model/lavaandiceAmmoIce");
            Pax4Model._current.Load("Model/lavaandiceq1m6");

            Pax4Model._current.Load("Model/lavaandiceEnemyLava" + _lavaIndex);
            Pax4Model._current.Load("Model/lavaandiceEnemyIce" + _iceIndex);

            Pax4Model._current.SetDefaultParameters();

            Pax4Ui._current.Enter("fgLavaAndIce");
            Pax4WorldLavaAndIce._missionType = ELavaAndIceMissionType._LAVA_AND_ICE;

            IniDual();

            //********************
            //*** create world ***
            //********************

            Pax4ActorWorld world = new Pax4ActorWorld("lavaandiceq1m6", null);
            world.SetModel("Model/lavaandiceq1m6");
            world.Enable();

            SetDifficultyTimer(100, 115, 130, 145, false);

            //*************************************************
            //*** create waypoint paths and their modifiers ***
            //*************************************************

            //***monster***
            Pax4WayPointPath monsterLavaWayPointPath = new Pax4WayPointPath(true);
            monsterLavaWayPointPath.GenerateWayPoint(new Vector3(7.0f, 6.0f, 0.0f));
            monsterLavaWayPointPath.Enable();

            Pax4WayPointPath monsterIceWayPointPath = new Pax4WayPointPath(true);
            monsterIceWayPointPath.GenerateWayPoint(new Vector3(-7.0f, 6.0f, 0.0f));
            monsterIceWayPointPath.Enable();

            PopulateWithActors(_physicsPartList, EPopulateWithActorsType._HETEROGENEOUS);

            //***ammo***
            Pax4WayPointPath wayPointPath = null;

            wayPointPath = new Pax4WayPointPath();
            wayPointPath.GenerateCircleWayPoints(0.785398f, new Vector3(-1.0f, 7.0f, 0.0f), 3.0f, 6.0f, 7);
            wayPointPath.Enable();
            PopulateWithActors(_physicsPartList, EPopulateWithActorsType._HOMOGENEOUS);

            wayPointPath = new Pax4WayPointPath();
            wayPointPath.GenerateCircleWayPoints(2.35619f, new Vector3(1.0f, 7.0f, 0.0f), 3.0f, 6.0f, 7, 0.0f, false);//2.35619f
            wayPointPath.Enable();
            PopulateWithActors(_physicsPartList, EPopulateWithActorsType._HOMOGENEOUS);

            wayPointPath = new Pax4WayPointPath();
            wayPointPath.GenerateCircleWayPoints(0.785398f, new Vector3(-2.5f, -1.0f, 0.0f), 4.0f, 6.0f, 7);
            wayPointPath.Enable();
            PopulateWithActors(_physicsPartList, EPopulateWithActorsType._HETEROGENEOUS);

            wayPointPath = new Pax4WayPointPath();
            wayPointPath.GenerateCircleWayPoints(2.35619f, new Vector3(2.5f, -1.0f, 0.0f), 4.0f, 6.0f, 7, 0.0f, false);//2.35619f
            wayPointPath.Enable();
            PopulateWithActors(_physicsPartList, EPopulateWithActorsType._HETEROGENEOUS);

            wayPointPath = new Pax4WayPointPath();
            wayPointPath.GenerateRectangleWayPoints((float)Math.PI/4, new Vector3(0.0f, 0.0f, 0.0f), 7.0f, 7.0f, 4, 4);
            wayPointPath.ToWayPointPaths().Enable();
        }
        private void UpdateM15(GameTime gameTime)
        {
        }

        private void IniM16()
        {
            //**********************
            //*** load resources ***
            //**********************
            _lavaIndex = 6;
            _iceIndex = 1;

            Pax4Model._current.Load("Model/lavaandiceAmmoLava");
            Pax4Model._current.Load("Model/lavaandiceAmmoIce");
            Pax4Model._current.Load("Model/lavaandiceq1m7");

            Pax4Model._current.Load("Model/lavaandiceEnemyLava" + _lavaIndex);
            Pax4Model._current.Load("Model/lavaandiceEnemyIce" + _iceIndex);

            Pax4Model._current.SetDefaultParameters();

            Pax4Ui._current.Enter("fgLavaAndIce");
            Pax4WorldLavaAndIce._missionType = ELavaAndIceMissionType._LAVA_AND_ICE;

            IniDual();

            //********************
            //*** create world ***
            //********************

            Pax4ActorWorld world = new Pax4ActorWorld("lavaandiceq1m7", null);
            world.SetModel("Model/lavaandiceq1m7");
            world.Enable();

            SetDifficultyTimer(100, 115, 130, 145, false);

            //*************************************************
            //*** create waypoint paths and their modifiers ***
            //*************************************************

            //***monster***
            Pax4WayPointPath monsterLavaWayPointPath = new Pax4WayPointPath(true);
            monsterLavaWayPointPath.GenerateWayPoint(new Vector3(4.0f, 8.5f, 0.0f));
            monsterLavaWayPointPath.Enable();

            Pax4WayPointPath monsterIceWayPointPath = new Pax4WayPointPath(true);
            monsterIceWayPointPath.GenerateWayPoint(new Vector3(-4.0f, 8.5f, 0.0f));
            monsterIceWayPointPath.Enable();

            PopulateWithActors(_physicsPartList, EPopulateWithActorsType._HETEROGENEOUS);

            //***ammo***
            Pax4WayPointPath wayPointPath = null;

            wayPointPath = new Pax4WayPointPath();
            wayPointPath.GenerateCircleWayPoints(0.785398f, new Vector3(4.0f, 8.5f, 0.0f), 3.0f, 6.5f, 6);
            wayPointPath.Enable();
            PopulateWithActors(_physicsPartList, EPopulateWithActorsType._HOMOGENEOUS, false);

            wayPointPath = new Pax4WayPointPath();
            wayPointPath.GenerateCircleWayPoints(2.35619f, new Vector3(-4.0f, 8.5f, 0.0f), 3.0f, 6.5f, 6, 0.0f, false);
            wayPointPath.Enable();
            PopulateWithActors(_physicsPartList, EPopulateWithActorsType._HOMOGENEOUS);

            wayPointPath = new Pax4WayPointPath();
            wayPointPath.GenerateCircleWayPoints(0.785398f, new Vector3(2.5f, -2.5f, 0.0f), 4.0f, 4.5f, 8);
            wayPointPath.Enable();
            PopulateWithActors(_physicsPartList, EPopulateWithActorsType._HETEROGENEOUS);

            wayPointPath = new Pax4WayPointPath();
            wayPointPath.GenerateCircleWayPoints(2.35619f, new Vector3(-2.5f, -2.5f, 0.0f), 4.0f, 4.5f, 8, 0.0f, false);
            wayPointPath.Enable();
            PopulateWithActors(_physicsPartList, EPopulateWithActorsType._HETEROGENEOUS);

            wayPointPath = new Pax4WayPointPath();
            wayPointPath.GenerateRectangleWayPoints((float)Math.PI / 4, new Vector3(0.0f, 0.0f, 0.0f), 7.0f, 7.0f, 4, 4);
            wayPointPath.ToWayPointPaths().Enable();
        }
        private void UpdateM16(GameTime gameTime)
        {
        }

        private void IniM17()
        {
            //**********************
            //*** load resources ***
            //**********************
            _lavaIndex = 1;
            _iceIndex = 7;

            Pax4Model._current.Load("Model/lavaandiceAmmoLava");
            Pax4Model._current.Load("Model/lavaandiceAmmoIce");
            Pax4Model._current.Load("Model/lavaandiceq1m8");

            Pax4Model._current.Load("Model/lavaandiceEnemyLava" + _lavaIndex);
            Pax4Model._current.Load("Model/lavaandiceEnemyIce" + _iceIndex);

            Pax4Model._current.SetDefaultParameters();

            Pax4Ui._current.Enter("fgLavaAndIce");
            Pax4WorldLavaAndIce._missionType = ELavaAndIceMissionType._LAVA_AND_ICE;

            IniDual();

            //********************
            //*** create world ***
            //********************

            Pax4ActorWorld world = new Pax4ActorWorld("lavaandiceq1m8", null);
            world.SetModel("Model/lavaandiceq1m8");
            world.Enable();

            SetDifficultyTimer(100, 115, 130, 145, false);

            //*************************************************
            //*** create waypoint paths and their modifiers ***
            //*************************************************

            //***monster***
            Pax4WayPointPath monsterLavaWayPointPath = new Pax4WayPointPath(true);
            monsterLavaWayPointPath.GenerateWayPoint(new Vector3(4.0f, 10.0f, 0.0f));
            monsterLavaWayPointPath.Enable();

            Pax4WayPointPath monsterIceWayPointPath = new Pax4WayPointPath(true);
            monsterIceWayPointPath.GenerateWayPoint(new Vector3(-4.0f, 10.0f, 0.0f));
            monsterIceWayPointPath.Enable();

            PopulateWithActors(_physicsPartList, EPopulateWithActorsType._HETEROGENEOUS);

            //***ammo***
            Pax4WayPointPath wayPointPath = null;

            wayPointPath = new Pax4WayPointPath();
            wayPointPath.GenerateCircleWayPoints(0.785398f, new Vector3(-4.5f, 4.0f, 0.0f), 3.0f, 8.0f, 6);
            wayPointPath.Enable();
            PopulateWithActors(_physicsPartList, EPopulateWithActorsType._LAYERED);

            wayPointPath = new Pax4WayPointPath();
            wayPointPath.GenerateCircleWayPoints(2.35619f, new Vector3(4.5f, 4.0f, 0.0f), 3.0f, 8.0f, 6, 0.0f, false);
            wayPointPath.Enable();
            PopulateWithActors(_physicsPartList, EPopulateWithActorsType._LAYERED);

            Pax4WayPointPathList wayPointPaths = null;

            wayPointPath = new Pax4WayPointPath();
            wayPointPath.GenerateLineWayPoints(0.0f, new Vector3(0.0f, -1.0f, 0.0f), 16.0f, 8);
            wayPointPaths = wayPointPath.ToWayPointPaths();
            PopulateWithActors(_physicsPartList, EPopulateWithActorsType._LAYERED, true, wayPointPaths);
            Pax4WayPointPath.ChainUp(_physicsPartList, true);

            wayPointPath = new Pax4WayPointPath();
            wayPointPath.GenerateRectangleWayPoints(0.0f, new Vector3(0.0f, 0.0f, 0.0f), 6.0f, 6.0f, 4, 4);
            wayPointPaths = wayPointPath.ToWayPointPaths();
            wayPointPaths.Enable();

            Pax4ModifierWayPointPath wayPointPathModifier = new Pax4ModifierWayPointPathRotationZ("", null);
            wayPointPathModifier.Enable();
            ((Pax4ModifierWayPointPathRotationZ)wayPointPathModifier).Ini(0.0f, MathHelper.TwoPi, 20.0f);
            wayPointPathModifier.AddPath(wayPointPaths);
            wayPointPathModifier.SetContinuous();
            wayPointPathModifier.Trigger();
        }
        private void UpdateM17(GameTime gameTime)
        {
        }

        private void IniM18()
        {
            //**********************
            //*** load resources ***
            //**********************
            _lavaIndex = 8;
            _iceIndex = 1;

            Pax4Model._current.Load("Model/lavaandiceAmmoLava");
            Pax4Model._current.Load("Model/lavaandiceAmmoIce");
            Pax4Model._current.Load("Model/lavaandiceq1m9");

            Pax4Model._current.Load("Model/lavaandiceEnemyLava" + _lavaIndex);
            Pax4Model._current.Load("Model/lavaandiceEnemyIce" + _iceIndex);

            Pax4Model._current.SetDefaultParameters();

            Pax4Ui._current.Enter("fgLavaAndIce");
            Pax4WorldLavaAndIce._missionType = ELavaAndIceMissionType._LAVA_AND_ICE;

            IniDual();

            //********************
            //*** create world ***
            //********************

            Pax4ActorWorld world = new Pax4ActorWorld("lavaandiceq1m9", null);
            world.SetModel("Model/lavaandiceq1m9");
            world.Enable();

            SetDifficultyTimer(100, 115, 130, 145, false);

            //*************************************************
            //*** create waypoint paths and their modifiers ***
            //*************************************************

            //***monster***
            Pax4WayPointPath monsterLavaWayPointPath = new Pax4WayPointPath(true);
            monsterLavaWayPointPath.GenerateCurve37WayPoints(0.0f, new Vector3(0.0f, 7.0f, 0.0f), 8.0f, 3.0f, 1, 10, 0.0f);
            monsterLavaWayPointPath.Enable();

            Pax4WayPointPath monsterIceWayPointPath = new Pax4WayPointPath(true);
            monsterIceWayPointPath.GenerateCurve37WayPoints(0.0f, new Vector3(0.0f, 7.0f, 0.0f), 8.0f, 3.0f, 1, 10, (float)Math.PI);
            monsterIceWayPointPath.Enable();

            PopulateWithActors(_physicsPartList, EPopulateWithActorsType._HETEROGENEOUS);

            //***ammo***
            Pax4WayPointPath wayPointPath = null;            

            Pax4WayPointPathList wayPointPaths = null;

            wayPointPath = new Pax4WayPointPath();
            wayPointPath.GenerateLineWayPoints(0.0f, new Vector3(0.0f, 4.0f, 0.0f), 6.0f, 4);
            wayPointPaths = wayPointPath.ToWayPointPaths();
            PopulateWithActors(_physicsPartList, EPopulateWithActorsType._LAYERED, true, wayPointPaths);
            Pax4WayPointPath.ChainUp(_physicsPartList, true);

            wayPointPath = new Pax4WayPointPath();
            wayPointPath.GenerateLineWayPoints(0.0f, new Vector3(0.0f, 1.0f, 0.0f), 10, 6);
            wayPointPaths = wayPointPath.ToWayPointPaths();
            PopulateWithActors(_physicsPartList, EPopulateWithActorsType._HOMOGENEOUS, true, wayPointPaths);
            Pax4WayPointPath.ChainUp(_physicsPartList, true);

            wayPointPath = new Pax4WayPointPath();
            wayPointPath.GenerateLineWayPoints(0.0f, new Vector3(0.0f, -2.0f, 0.0f), 14.0f, 8);
            wayPointPaths = wayPointPath.ToWayPointPaths();
            PopulateWithActors(_physicsPartList, EPopulateWithActorsType._HOMOGENEOUS, false, wayPointPaths);
            Pax4WayPointPath.ChainUp(_physicsPartList, true);

            wayPointPath = new Pax4WayPointPath();
            wayPointPath.GenerateLineWayPoints(0.0f, new Vector3(0.0f, -5.0f, 0.0f), 16.0f, 10);
            wayPointPaths = wayPointPath.ToWayPointPaths();
            PopulateWithActors(_physicsPartList, EPopulateWithActorsType._HOMOGENEOUS, true, wayPointPaths);
            Pax4WayPointPath.ChainUp(_physicsPartList, true);

            wayPointPath = new Pax4WayPointPath();
            wayPointPath.GenerateTriangleWayPoints(0.0f, new Vector3(0.0f, 0.0f, 0.0f), 7.0f, 6);
            wayPointPaths = wayPointPath.ToWayPointPaths();
            wayPointPaths.Enable();
        }
        private void UpdateM18(GameTime gameTime)
        {
        }

        private void IniM19()
        {
            //**********************
            //*** load resources ***
            //**********************
            _lavaIndex = 1;
            _iceIndex = 9;

            Pax4Model._current.Load("Model/lavaandiceAmmoLava");
            Pax4Model._current.Load("Model/lavaandiceAmmoIce");
            Pax4Model._current.Load("Model/lavaandiceq1m1");

            Pax4Model._current.Load("Model/lavaandiceEnemyLava" + _lavaIndex);
            Pax4Model._current.Load("Model/lavaandiceEnemyIce" + _iceIndex);

            Pax4Model._current.SetDefaultParameters();

            Pax4Ui._current.Enter("fgLavaAndIce");
            Pax4WorldLavaAndIce._missionType = ELavaAndIceMissionType._LAVA_AND_ICE;

            IniDual();

            //********************
            //*** create world ***
            //********************

            Pax4ActorWorld world = new Pax4ActorWorld("lavaandiceq1m1", null);
            world.SetModel("Model/lavaandiceq1m1");
            world.Enable();

            SetDifficultyTimer(100, 115, 130, 145, false);

            //*************************************************
            //*** create waypoint paths and their modifiers ***
            //*************************************************

            //***monster***
            Pax4WayPointPath monsterLavaWayPointPath = new Pax4WayPointPath(true);
            monsterLavaWayPointPath.GenerateCircleWayPoints((float)Math.PI/6.0f, new Vector3(4.0f, 10.0f, 0.0f), 0.5f, 3.0f, 10, 0.0f);
            monsterLavaWayPointPath.Enable();

            Pax4WayPointPath monsterIceWayPointPath = new Pax4WayPointPath(true);
            monsterIceWayPointPath.GenerateCircleWayPoints(-(float)Math.PI/6.0f, new Vector3(-4.0f, 10.0f, 0.0f), 0.5f, 3.0f, 10, (float)Math.PI);
            monsterIceWayPointPath.Enable();

            PopulateWithActors(_physicsPartList, EPopulateWithActorsType._HETEROGENEOUS);

            //***ammo***
            Pax4WayPointPath wayPointPath = null;
            Pax4WayPointPathList wayPointPaths = null;

            Pax4ModifierWayPointPath wayPointPathModifier = new Pax4ModifierWayPointPathRotationZ("", null);
            wayPointPathModifier.Enable();
            ((Pax4ModifierWayPointPathRotationZ)wayPointPathModifier).Ini(0.0f, MathHelper.TwoPi, 20.0f);            
            wayPointPathModifier.SetContinuous();

            wayPointPath = new Pax4WayPointPath();
            wayPointPath.GenerateCircleWayPoints(0.785398f, new Vector3(4.0f, -3.0f, 0.0f), 3.0f, 4.5f, 8, 0.0f, false);
            wayPointPaths = wayPointPath.ToWayPointPaths(3);
            wayPointPaths.Enable();
            PopulateWithActors(_physicsPartList, EPopulateWithActorsType._HOMOGENEOUS, true, wayPointPaths, 3.0f);
            wayPointPathModifier.AddPath(wayPointPaths);

            wayPointPath = new Pax4WayPointPath();
            wayPointPath.GenerateCircleWayPoints(2.35619f, new Vector3(4.0f, 3.0f, 0.0f), 3.0f, 4.5f, 8, 0.0f);
            wayPointPaths = wayPointPath.ToWayPointPaths(3);
            wayPointPaths.Enable();
            PopulateWithActors(_physicsPartList, EPopulateWithActorsType._HOMOGENEOUS, false, wayPointPaths, 3.0f);
            wayPointPathModifier.AddPath(wayPointPaths);

            wayPointPath = new Pax4WayPointPath();
            wayPointPath.GenerateCircleWayPoints(2.35619f, new Vector3(-4.0f, -3.0f, 0.0f), 3.0f, 4.5f, 8, 0.0f);
            wayPointPaths = wayPointPath.ToWayPointPaths(3);
            wayPointPaths.Enable();
            PopulateWithActors(_physicsPartList, EPopulateWithActorsType._HOMOGENEOUS, false, wayPointPaths, 3.0f);
            wayPointPathModifier.AddPath(wayPointPaths);

            wayPointPath = new Pax4WayPointPath();
            wayPointPath.GenerateCircleWayPoints(3.92699f, new Vector3(-4.0f, 3.0f, 0.0f), 3.0f, 4.5f, 8, 0.0f, false);
            wayPointPaths = wayPointPath.ToWayPointPaths(3);
            wayPointPaths.Enable();
            PopulateWithActors(_physicsPartList, EPopulateWithActorsType._HOMOGENEOUS, true, wayPointPaths, 3.0f);
            wayPointPathModifier.AddPath(wayPointPaths);

            wayPointPathModifier.MergePath();// without merging a cool effect am happn

            wayPointPathModifier.Trigger();            
        }
        private void UpdateM19(GameTime gameTime)
        {
        }
            
        private void IniM20()
        {
        }
        private void UpdateM20(GameTime gameTime)
        {
        }

        private void IniM21()
        {
        }
        private void UpdateM21(GameTime gameTime)
        {
        }

        private void IniM22()
        {
        }
        private void UpdateM22(GameTime gameTime)
        {
        }

        private void IniM23()
        {
        }
        private void UpdateM23(GameTime gameTime)
        {
        }

        private void IniM24()
        {
        }
        private void UpdateM24(GameTime gameTime)
        {
        }

        private void IniM25()
        {
        }
        private void UpdateM25(GameTime gameTime)
        {
        }

        private void IniM26()
        {
        }
        private void UpdateM26(GameTime gameTime)
        {
        }

        private void IniM27()
        {
        }
        private void UpdateM27(GameTime gameTime)
        {
        }

        private void IniM28()
        {
        }
        private void UpdateM28(GameTime gameTime)
        {
        }

        private void IniM29()
        {
        }
        private void UpdateM29(GameTime gameTime)
        {
        }

        private void IniM30()
        {
        }
        private void UpdateM30(GameTime gameTime)
        {
        }

        private void IniM31()
        {
        }
        private void UpdateM31(GameTime gameTime)
        {
        }

        private void IniM32()
        {
        }
        private void UpdateM32(GameTime gameTime)
        {
        }
    }
}