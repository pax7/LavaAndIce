using System.Collections.Generic;

using Microsoft.Xna.Framework;

using System;
using Microsoft.Xna.Framework.Graphics;
using System.IO;
using System.Runtime.Serialization;
using Pax4.Jitter.Dynamics;
using Pax4.Jitter.Dynamics.Joints;
using Pax4.Jitter.Dynamics.Constraints;
using Pax4.Jitter.LinearMath;
using Pax4.Jitter.Collision;

namespace Pax4.Core
{
    [DataContract]
    [KnownType(typeof(Pax4ObjectPhysicsPart))]
    public class Pax4ObjectPhysicsPart : Pax4ObjectSceneryPart
    {
        public const float _defaultRestitution = 0.5f;//0.8f
        public const float _defaultStaticFriction = 0.5f;//0.8f
        public const float _defaultKineticFriction = 0.25f;//0.7f

        public static JVector _defaultHingePositionParent = JVector.Zero;
        public static JVector _defaultHingeAxis = JVector.Backward;
        
        public RigidBody _body = null;

        private HashSet<Constraint> _constraint = null;
        
        public Pax4ObjectPhysicsPart _hingeJointParent = null;

        public Dictionary<Pax4ObjectPhysicsPart, HingeJoint> _hingeJoint = null;

        public Matrix _matScale = Matrix.Identity;

        public bool _isSelectable = false;

        public bool _addBodyForce = false;
        public bool _addWorldForce = false;

        public Pax4ObjectPhysicsPart(String p_name, Pax4Object p_parent0)
            : base(p_name, p_parent0)
        {
        }
        
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (!_body.IsStaticOrInactive)
            {
                _matWorld = _matScale * (_body.Orientation * JMatrix.CreateTranslation(_body.Position));
                
                SetPosition(_body.Position, false);
            }
        }

        public override void Draw(GameTime gameTime)
        {
            if (_isOutsideView)
                return;

            //base.Draw(gameTime);
        }

        public float GetMass()
        {
            if (_body == null)
                return -1.0f;

            return _body.Mass;
        }

        public virtual void CreateBody()
        {
            //Shape b1 = new BoxShape(new JVector(3, 1, 1));
            //Shape b2 = new BoxShape(new JVector(1, 1, 3));
            //Shape b3 = new CylinderShape(3.0f, 0.5f);

            //CompoundShape.TransformedShape t1 = new CompoundShape.TransformedShape(b1, JMatrix.Identity, JVector.Zero);
            //CompoundShape.TransformedShape t2 = new CompoundShape.TransformedShape(b2, JMatrix.Identity, JVector.Zero);
            //CompoundShape.TransformedShape t3 = new CompoundShape.TransformedShape(b3, JMatrix.Identity, new JVector(0, 0, 0));

            //CompoundShape ms = new CompoundShape(new CompoundShape.TransformedShape[3] { t1, t2, t3 });

            //body = new RigidBody(ms);

            //_body = new RigidBody();
            //_body._paxState = this;

            _body.Material.Restitution = _defaultRestitution;
            _body.Material.StaticFriction = _defaultStaticFriction;
            _body.Material.KineticFriction = _defaultKineticFriction;

            ////Pax4World._current._physicsSystem.CollisionSystem.PassedBroadphase += new PassedBroadphaseHandler(PassedBroadphaseHandler);
            ////Pax4World._current._physicsSystem.CollisionSystem.CollisionDetected += new CollisionDetectedHandler(CollisionDetectedHandler);
        }

        public static bool PassedBroadphaseHandler(IBroadphaseEntity entity1, IBroadphaseEntity entity2)
        {
            return true;
        }

        public static void CollisionDetectedHandler(RigidBody body1, RigidBody body2, JVector point1, JVector point2, JVector normal, float penetration)
        {
            return;
        }

        public static bool RaycastCallback(RigidBody body, JVector normal, float fraction)
        {
            return true;
        }

        public override void Dx()
        {
            if (_hingeJointParent != null)
                _hingeJointParent.DisableHingeJoint(this);

            Disable();

            base.Dx();
        }

        public override void Enable()
        {
            if (!_isDisabled)
                return;

            base.Enable();

            _matWorld = _matScale * Pax4Tools.ToXnaMatrix(_body.Orientation, _body.Position);

            EnableConstraint();
            EnableHingeJoint();
        }

        public override void Disable()
        {
            if (_isDisabled)
                return;

            base.Disable();           

            DisableConstraint();
            DisableHingeJoint();
        }        
        
        public virtual void AddConstraint(Constraint p_constraint)
        {
            if (_constraint == null)
                _constraint = new HashSet<Constraint>();

            if (!_constraint.Contains(p_constraint))
                _constraint.Add(p_constraint);
        }

        public virtual void EnableConstraint()
        {
            if (_constraint == null)
                return;

            foreach (Constraint constraint in _constraint)
                Pax4World._current.AddConstraint(constraint);
        }

        public virtual void DisableConstraint()
        {
            if (_constraint == null)
                return;

            foreach (Constraint constraint in _constraint)
                Pax4World._current.RemoveConstraint(constraint);
        }

        public override void SetScale(JVector p_scale, bool p_transform = false)
        {
            base.SetScale(p_scale, p_transform);

            _matScale = Matrix.CreateScale(p_scale.X, p_scale.Y, p_scale.Z);
        }

        public override void SetRotation(JVector p_rotation, bool p_transform = false)
        {
            base.SetRotation(p_rotation, p_transform);
        }

        public override JVector GetPosition()
        {   
            return _body.Position;
        }

        public virtual void MoveTo(JVector p_rotation, JVector p_position)
        {
            if (_body == null)
                return;

            _body.Orientation = JMatrix.CreateFromYawPitchRoll(p_rotation.Y, p_rotation.X, p_rotation.Z);
            _body.Position = p_position;
            
            SetRotation(p_rotation, false);

            _boundingSphere.Center.X = p_position.X;
            _boundingSphere.Center.Y = p_position.Y;
            _boundingSphere.Center.Z = p_position.Z;
        }

        public override Matrix GetWorld()
        {   
            return Pax4Tools.ToXnaMatrix(_body.Orientation, _body.Position);
        }

        public virtual void SetIsSelectable(bool p_isSelectable = true)
        {
            _isSelectable = p_isSelectable;
        }

        public void SetIsStatic(bool p_isStatic = true)
        {
            _body.IsStatic = p_isStatic;
        }

        public void CreateHingeJoint(Pax4ObjectPhysicsPart p_child,             
                                     bool p_disableChildController,
                                     JVector p_hingePositionParent,
                                     JVector p_hingeAxis)
        {
            if (_body == null || p_child == null)
                return;

            if (_hingeJoint == null)
                _hingeJoint = new Dictionary<Pax4ObjectPhysicsPart, HingeJoint>();

            if(p_disableChildController)
                p_child.DisableConstraint();

            HingeJoint hingeJoint = new HingeJoint(Pax4World._current._physicsSystem,
                                                   this._body,
                                                   p_child._body,
                                                   p_hingePositionParent,
                                                   p_hingeAxis);
            
            p_child._hingeJointParent = this;
            _hingeJoint.Add(p_child, hingeJoint);
        }

        public void EnableHingeJoint()
        {
            if (_hingeJoint == null)
                return;

            foreach(KeyValuePair<Pax4ObjectPhysicsPart, HingeJoint> kvp in _hingeJoint)
                kvp.Value.Activate();
        }

        public void DisableHingeJoint(Pax4ObjectPhysicsPart p_child = null)
        {
            if (_hingeJoint == null)
                return;

            if (p_child != null)
            {
                if (_hingeJoint.ContainsKey(p_child))
                {
                    _hingeJoint[p_child].Deactivate();
                    _hingeJoint.Remove(p_child);
                }
            }
            else
            {
                foreach (KeyValuePair<Pax4ObjectPhysicsPart, HingeJoint> kvp in _hingeJoint)
                    kvp.Value.Deactivate();
            }
        }

        #region serialize

        public override MemoryStream Serialize(bool p_volatile = false)
        {
            return Serialize(this.GetType(), p_volatile);
        }

        #endregion
    }
}