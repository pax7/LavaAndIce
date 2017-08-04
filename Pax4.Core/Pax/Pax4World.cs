using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Pax.Core;

using System.Linq;
using Pax4.Jitter;
using Pax4.Jitter.Collision;
using Pax4.Jitter.LinearMath;
using Pax4.Jitter.Dynamics;
using Pax4.Jitter.Dynamics.Constraints;

namespace Pax4.Core
{
    public class Pax4World : PaxState
    {
        public static Pax4World _current = null;

        public HashSet<Pax4Object> _update = new HashSet<Pax4Object>();
        public HashSet<Pax4Object> _draw = new HashSet<Pax4Object>();

        //public Dictionary<int, Pax4ObjectPhysicsPart> _selectedPhysicsPart = new Dictionary<int, Pax4ObjectPhysicsPart>();
        
        public Pax4ObjectPhysicsPart _selectedPhysicsPart = null;

        public World _physicsSystem = null;

        public List<Pax4WayPointPath> _wayPointPath = new List<Pax4WayPointPath>();
        public List<Pax4ModifierWayPointPath> _wayPointPathModifier = new List<Pax4ModifierWayPointPath>();
        
        public delegate void UpdateDelegate(GameTime gameTime);
        public UpdateDelegate _updateDelegate = null;
        
        public bool _updateSelectedObject = true;

        public Pax4World(String p_name, PaxState p_parent0)
            : base(p_name, p_parent0)
        {   
            IniPhysicsSystem();
        }

        public virtual void Enable()
        {
            _current = this;
        }

        public virtual void Update(GameTime gameTime)
        {
            foreach (Pax4Object part in _update)
                part.Update(gameTime);                

            if (_wayPointPathModifier != null)
            {
                for (int i = 0; i < _wayPointPathModifier.Count; i++)
                    _wayPointPathModifier[i].Update(gameTime);
            }

            if(_updateSelectedObject)
                UpdateSelectedObject();

            if(_updateDelegate != null)
                _updateDelegate(gameTime);

            UpdatePhysicsSystem(gameTime);
        }

        public virtual void Draw(GameTime gameTime)
        {
            Pax4Game._graphicsDeviceManager.GraphicsDevice.RasterizerState = Pax4Camera._rasterizerState;
            Pax4Game._graphicsDeviceManager.GraphicsDevice.DepthStencilState = DepthStencilState.Default;
            Pax4Game._graphicsDeviceManager.GraphicsDevice.BlendState = Pax4Camera._blendState;

#if WINDOWS
            Pax4Game._graphicsDeviceManager.GraphicsDevice.SamplerStates[0] = SamplerState.AnisotropicWrap;
#else
            Pax4Game._graphicsDeviceManager.GraphicsDevice.SamplerStates[0] = SamplerState.LinearWrap;
#endif

            foreach (Pax4ObjectSceneryPart part in _draw)
            {
                if (part._isOutsideView)
                    continue;
                else
                    part.Draw(gameTime);
            }
        }

        public virtual void AddUpdateObject(Pax4Object p_object)
        {
            if (_update != null)
                _update.Add(p_object);
        }

        public virtual void RemoveUpdateObject(Pax4Object p_object)
        {
            if (_update != null)
                _update.Remove(p_object);
        }

        public virtual void AddDrawObject(Pax4Object p_object)
        {
            if (_draw != null)
                _draw.Add(p_object);
        }

        public virtual void RemoveDrawObject(Pax4Object p_object)
        {
            if(_draw != null)
                _draw.Remove(p_object);
        }

        public virtual void RemoveObject(Pax4Object p_object)
        {
            _update.Remove(p_object);
            _draw.Remove(p_object);
        }

        public virtual void AddConstraint(Constraint p_constraint)
        {
            if (_physicsSystem == null)
                return;

            _physicsSystem.AddConstraint(p_constraint);
        }

        public virtual void RemoveConstraint(Constraint p_constraint)
        {
            if (_physicsSystem == null)
                return;

            _physicsSystem.RemoveConstraint(p_constraint);
        }

        public override void Dx()
        {
            DxPhysicsSystem();

            DxUpdate();

            DxDraw();

            Pax4Model._current.DxChild();

            if (this == _current)
                _current = null;

            base.Dx();
        }

        public virtual void DxUpdate()
        {
            if(_update != null)
                _update.Clear();
            _update = null;
        }

        public virtual void DxDraw()
        {
            if (_draw != null)
                _draw.Clear();
            _draw = null;
        }

        //******************************************
        //physics***********************************
        //******************************************
        public virtual void IniPhysicsSystem()
        {
             DxPhysicsSystem();

             CollisionSystem collisionSystem = new CollisionSystemPersistentSAP();
             //collisionSystem.EnableSpeculativeContacts = true;//!*

             _physicsSystem = new World(collisionSystem);

             _physicsSystem.AllowDeactivation = true;
        }

        public virtual void UpdatePhysicsSystem(GameTime gameTime)
        {
            if (_physicsSystem == null)
                return;

            float step = (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (step > 1.0f / 100.0f) step = 1.0f / 100.0f;//60.0f
            _physicsSystem.Step(step, true);//!* see how this behaves with false on le phonz
        }

        public virtual void DxPhysicsSystem()
        {
            if (_physicsSystem == null)
                return;

            _physicsSystem.Clear();

            _physicsSystem = null;
        }

        public int GetNextAvailableWayPointPathIndex()
        {
            int result = 0;

            for (int i = 0; i < _wayPointPath.Count; i++)
            {
                if (_wayPointPath[i]._locked)
                {
                    result++;
                    continue;
                }

                if (_wayPointPath[i]._wayPoint.Length == 1 && _wayPointPath[i]._residentCount == 0)
                    return i;
                else
                {
                    if (_wayPointPath[i]._residentCount < _wayPointPath[result]._residentCount)
                        result = i;
                }
            }

            return result;
        }

        public Pax4WayPointPath GetNextAvailableWayPointPath()
        {
            return _wayPointPath[GetNextAvailableWayPointPathIndex()];
        }

        public void AddWayPointPath(Pax4WayPointPath p_wayPointPath = null)
        {
            if (p_wayPointPath == null)
                return;

            _wayPointPath.Add(p_wayPointPath);
        }

        public void RemoveWayPointPath(Pax4WayPointPath p_wayPointPath = null)
        {
            if (p_wayPointPath == null)
                return;

            _wayPointPath.Remove(p_wayPointPath);
        }

        public void AddWayPointPathModifier(Pax4ModifierWayPointPath p_wayPointPathModifier = null)
        {
            if (p_wayPointPathModifier == null)
                return;

            _wayPointPathModifier.Add(p_wayPointPathModifier);
        }

        public void RemoveWayPointPathModifier(Pax4ModifierWayPointPath p_wayPointPathModifier = null)
        {
            if (p_wayPointPathModifier == null)
                return;

            _wayPointPathModifier.Remove(p_wayPointPathModifier);
        }

        public void ResetWayPointPath()
        {
            if (_wayPointPath != null)
                _wayPointPath.Clear();
            else
                _wayPointPath = new List<Pax4WayPointPath>();

            if (_wayPointPathModifier != null)
                _wayPointPathModifier.Clear();
            else
                _wayPointPathModifier = new List<Pax4ModifierWayPointPath>();
        }

        public void UpdateSelectedObject()
        {
            //if (Pax4Touch._current._currentTouchState._oneTap)
            if (   Pax4Touch._current._currentTouchState._clean
                && Pax4Touch._current._currentTouchState._oneTap)
            {
                JVector rayOrigin;
                JVector rayDirection;

                Pax4Tools.RayTo(Pax4Touch._current._currentTouchState._xy.X,
                                Pax4Touch._current._currentTouchState._xy.Y,
                                out rayOrigin,
                                out rayDirection);

                RigidBody body;
                JVector hitNormal;
                JVector hitPoint;
                float hitDistance = 0.0f;
                float fraction;

                rayDirection *= Pax4Camera._current._farPlane;

                bool result = _physicsSystem.CollisionSystem.Raycast(rayOrigin,
                                                                     rayDirection, //at this point it's a full length ray ;) 
                                                                     RaycastCallback, 
                                                                     out body, out hitNormal, out fraction);
                
                _selectedPhysicsPart = null;

                if (result)
                {
                    hitPoint = rayOrigin + fraction * rayDirection;

                    hitDistance = (hitPoint - rayOrigin).Length();

                    _selectedPhysicsPart = (Pax4ObjectPhysicsPart)body._paxState;
                }
            }            
        }

        public static bool RaycastCallback(RigidBody p_body, JVector p_normal, float p_fraction)
        {
            Pax4Object obj = (Pax4Object)p_body._paxState;
            
            if (obj._isDisabled)
                return false;
            else
                return true;
        }

        public void GrabSelectedObject()
        {
            #region drag and drop physical objects with the mouse
            //if (mouseState.LeftButton == ButtonState.Pressed &&
            //    mousePreviousState.LeftButton == ButtonState.Released ||
            //    padState.IsButtonDown(Buttons.RightThumbstickDown) &&
            //    gamePadPreviousState.IsButtonUp(Buttons.RightThumbstickUp))
            //{
            //    JVector ray = Conversion.ToJitterVector(RayTo(mouseState.X, mouseState.Y));
            //    JVector camp = Conversion.ToJitterVector(Camera.Position);

            //    ray = JVector.Normalize(ray) * 100;

            //    float fraction;

            //    bool result = World.CollisionSystem.Raycast(camp, ray, RaycastCallback, out grabBody, out hitNormal, out fraction);

            //    if (result)
            //    {
            //        hitPoint = camp + fraction * ray;

            //        if (grabConstraint != null) World.RemoveConstraint(grabConstraint);

            //        JVector lanchor = hitPoint - grabBody.Position;
            //        lanchor = JVector.Transform(lanchor, JMatrix.Transpose(grabBody.Orientation));

            //        grabConstraint = new SingleBodyConstraints.PointOnPoint(grabBody, lanchor);
            //        grabConstraint.Softness = 0.01f;
            //        grabConstraint.BiasFactor = 0.1f;

            //        World.AddConstraint(grabConstraint);
            //        hitDistance = (Conversion.ToXNAVector(hitPoint) - Camera.Position).Length();
            //        scrollWheel = mouseState.ScrollWheelValue;
            //        grabConstraint.Anchor = hitPoint;
            //    }
            //}

            //if (mouseState.LeftButton == ButtonState.Pressed || padState.IsButtonDown(Buttons.RightThumbstickDown))
            //{
            //    hitDistance += (mouseState.ScrollWheelValue - scrollWheel) * 0.01f;
            //    scrollWheel = mouseState.ScrollWheelValue;

            //    if (grabBody != null)
            //    {
            //        Vector3 ray = RayTo(mouseState.X, mouseState.Y); ray.Normalize();
            //        grabConstraint.Anchor = Conversion.ToJitterVector(Camera.Position + ray * hitDistance);
            //        grabBody.IsActive = true;
            //        if (!grabBody.IsStatic)
            //        {
            //            grabBody.LinearVelocity *= 0.98f;
            //            grabBody.AngularVelocity *= 0.98f;
            //        }
            //    }
            //}
            //else
            //{
            //    if (grabConstraint != null) World.RemoveConstraint(grabConstraint);
            //    grabBody = null;
            //    grabConstraint = null;
            //}
            #endregion
        }
    }
}