using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Pax4.Core
{
    public class Pax4WayPointPathList
    {
        public List<Pax4WayPointPath> _wayPointPath = new List<Pax4WayPointPath>();

        public Pax4WayPointPathList()
        {
        }

        public void Enable()
        {
            for (int i = 0; i < _wayPointPath.Count; i++)
                _wayPointPath[i].Enable();
        }

        public void Disable()
        {
            for (int i = 0; i < _wayPointPath.Count; i++)
                _wayPointPath[i].Disable();
        }

        public void Add(Pax4WayPointPath p_wayPointPathList = null)
        {
            if (p_wayPointPathList == null)
                return;

            _wayPointPath.Add(p_wayPointPathList);
        }

        public void FromWayPointPath(Pax4WayPointPath p_wayPointPath = null, bool p_useWayPointPath0 = false, int p_modValue = 1)
        {
            if (p_wayPointPath == null)
                return;

            Vector3[] wayPoint = null;

            if (p_useWayPointPath0)
                wayPoint = p_wayPointPath._wayPoint0;
            else
                wayPoint = p_wayPointPath._wayPoint;

            if (wayPoint.Length <= 0)
                return;

            Pax4WayPointPath wayPointPath = null;

            for (int i = 0; i < wayPoint.Length; i++)
            {
                if (i % p_modValue != 0)
                    continue;

                wayPointPath = new Pax4WayPointPath();
                wayPointPath.GenerateWayPoint(wayPoint[i]);
                //wayPointPath.SetTransform(p_wayPointPath._matScale, Matrix.Identity, p_wayPointPath._matTranslation);
                wayPointPath.SetTransform(p_wayPointPath);
                _wayPointPath.Add(wayPointPath);
            }
        }
    }

    public class Pax4WayPointPath
    {
        public enum EPax4WayPointPathType
        {
            _NULL,
            _CIRCLE,
            _CIRCLE_Z,
            _SPIRAL,
            _SPIRAL_Z,
            _CURVE37,
            _CURVE37_Z,
            _COUNT
        };        

        public Vector3[] _wayPoint0 = null;        
        public Vector3[] _wayPoint = null;
        //Pax4VertexPositionColorNormal[] _vertex = null;

        public Matrix _matTranslation = Matrix.Identity;
        public Matrix _matRotation = Matrix.Identity;
        public Matrix _matScale = Matrix.Identity;

        public Matrix _matTransform = Matrix.Identity;

        public int _residentCount = 0;

        public bool _locked = false; //don't automatilly add objects to this path

        public bool _mutex = true;

        public Pax4WayPointPath(bool p_locked = false)
        {
            _locked = p_locked;
        }

        public void Enable()
        {
            if (Pax4World._current != null)
                Pax4World._current.AddWayPointPath(this);
        }

        public void Disable()
        {
            if (Pax4World._current != null)
                Pax4World._current.RemoveWayPointPath(this);
        }

        public int GetRandomWayPointIndex()
        {
            Random rand = new Random();
            return rand.Next(0, _wayPoint0.Length - 1);
        }

        public void SetTransform(Pax4WayPointPath p_wayPointPath = null)
        {
            if (p_wayPointPath == null)
                return;

            _matScale = p_wayPointPath._matScale;
            _matRotation = p_wayPointPath._matRotation;
            _matTranslation = p_wayPointPath._matTranslation;
        }

        public void SetTransformIdentity()
        {
            _matScale = Matrix.Identity;
            _matRotation = Matrix.Identity;
            _matTranslation = Matrix.Identity;
        } 

        public void SetTransform(Matrix p_matScale, Matrix p_matRotation, Matrix p_matTranslation)
        {
            _matScale = p_matScale;
            _matRotation = p_matRotation;
            _matTranslation = p_matTranslation;
        }       

        public void SetPosition(Vector3 p_position)
        {
            _matTranslation = Matrix.CreateTranslation(p_position);
            _matTransform = _matScale * _matRotation * _matTranslation;

            Vector3.Transform(_wayPoint0, ref _matTransform, _wayPoint);
        }

        public void SetRotationZ(float p_rotationZ)
        {
            _matRotation = Matrix.CreateRotationZ(p_rotationZ);
            _matTransform = _matScale * _matRotation * _matTranslation;
            
            Vector3.Transform(_wayPoint0, ref _matTransform, _wayPoint);
        }

        public void SetRotation(Vector3 p_yawPitchRoll)
        {
            _matRotation = Matrix.CreateFromYawPitchRoll(p_yawPitchRoll.Y, p_yawPitchRoll.X, p_yawPitchRoll.Z);
            _matTransform = _matScale * _matRotation * _matTranslation;
            
            Vector3.Transform(_wayPoint0, ref _matTransform, _wayPoint);
        }

        public void SetRotation(float p_yaw, float p_pitch, float p_roll)
        {
            _matRotation = Matrix.CreateFromYawPitchRoll(p_yaw, p_pitch, p_roll);
            _matTransform = _matScale * _matRotation * _matTranslation;
            
            Vector3.Transform(_wayPoint0, ref _matTransform, _wayPoint);
        }

        public void SetScale(float p_scale)
        {
            _matScale = Matrix.CreateScale(p_scale);
            _matTransform = _matScale * _matRotation * _matTranslation;
            
            Vector3.Transform(_wayPoint0, ref _matTransform, _wayPoint);
        }

        public void AddWayPoint(Vector3 p_position)
        {
            Vector3[] wayPoint = null;
            Vector3[] wayPoint0 = null;

            if (_wayPoint0 == null)
            {
                _wayPoint0 = new Vector3[1];
                _wayPoint = new Vector3[1];
            }
            else
            {
                wayPoint0 = new Vector3[_wayPoint0.Length + 1];
                wayPoint = new Vector3[_wayPoint.Length + 1];

                for (int i = 0; i < wayPoint0.Length - 1; i++)
                {
                    wayPoint0[i] = _wayPoint0[i];
                    wayPoint[i] = _wayPoint[i];
                }

                _wayPoint0 = wayPoint0;
                _wayPoint = wayPoint;
            }

            _wayPoint0[_wayPoint0.Length - 1] = p_position;
            _wayPoint[_wayPoint.Length - 1] = p_position;
        }

        public void AddWayPoint(Vector3[] p_position)
        {
            Vector3[] wayPoint = null;
            Vector3[] wayPoint0 = null;

            if (_wayPoint0 == null)
            {
                _wayPoint0 = new Vector3[p_position.Length];
                _wayPoint = new Vector3[p_position.Length];
            }
            else
            {
                wayPoint0 = new Vector3[_wayPoint0.Length + p_position.Length];
                wayPoint = new Vector3[_wayPoint.Length + p_position.Length];

                int i = 0;
                for (; i < _wayPoint0.Length; i++)
                {
                    wayPoint0[i] = _wayPoint0[i];
                    wayPoint[i] = _wayPoint[i];
                }

                for (int j = 0; i < wayPoint0.Length; i++, j++)
                {
                    wayPoint0[i] = p_position[j];
                    wayPoint[i] = p_position[j];
                }
                
                _wayPoint0 = wayPoint0;
                _wayPoint = wayPoint;
            }
        }

        public void GenerateWayPoint(Vector3 p_position)
        {
            _wayPoint0 = new Vector3[1];
            _wayPoint = new Vector3[1];
            _wayPoint0[0] = p_position;
            _wayPoint[0] = p_position;
        }

        public void GenerateLineWayPoints(Vector3 p_position0, Vector3 p_position1, int p_steps, bool p_forward = true)
        {
            Vector3 direction = p_position1 - p_position0;

            _wayPoint0 = new Vector3[p_steps];
            _wayPoint = new Vector3[p_steps];

            Vector3 step;
            Vector3 position;

            if (p_forward)
            {
                position = p_position0;
                step = direction / p_steps;
            }
            else
            {
                position = p_position1;
                step = -direction / p_steps;
            }            

            for (int i = 0; i < p_steps; i++, position += step)
                _wayPoint0[i] = position;
        }

        public void GenerateLineWayPoints(float p_rotationZ, Vector3 p_center, float p_length, int p_steps, bool p_forward = true)
        {
            if (p_steps <= 0)
                return;

            _wayPoint0 = new Vector3[p_steps];
            _wayPoint = new Vector3[p_steps];

            double step = p_length / (p_steps - 1);

            double x = 0.0;

            if (p_forward)
            {
                x = -p_length / 2.0f;
            }
            else
            {
                x = p_length / 2.0f;
                step *= -1.0f;
            }

            for (int i = 0; i < p_steps; i++, x += step)
            {
                _wayPoint0[i].X = (float)x;
                _wayPoint0[i].Y = 0.0f;
                _wayPoint0[i].Z = 0.0f;
            }

            SetRotationZ(p_rotationZ);
            SetPosition(p_center);
        }

        public void GenerateTriangleWayPoints(float p_rotationZ, Vector3 p_center, float p_sideLength, int p_steps)
        {
            if (p_steps <= 0)
                return;

            _wayPoint0 = new Vector3[(p_steps * p_steps) / 2 + p_steps / 2];// + p_sideLength];
            _wayPoint = new Vector3[_wayPoint0.Length];

            double xStep = p_sideLength / (p_steps - 1);
            double x0 = -p_sideLength / 2.0;
            double x1 = -x0;

            double yStep = (float)Math.Sqrt(p_sideLength * p_sideLength - 0.25f * p_sideLength * p_sideLength);
            double y0 = -yStep / 2.0; //height/2.0f            
            double y1 = -y0;   
            yStep /= (p_steps - 1);

            double y = y0;
            for (int i = 0; y <= y1; y += yStep)
            {
                for (double x = x0; x <= x1; i++, x += xStep)
                {
                    _wayPoint0[i].X = (float)x;
                    _wayPoint0[i].Y = (float)y;
                    _wayPoint0[i].Z = 0.0f;
                }

                x0 = x0 + xStep / 2.0;
                x1 = x1 - xStep / 2.0;
            }

            SetRotationZ(p_rotationZ);
            SetPosition(p_center);
        }

        public void GenerateRectangleWayPoints(float p_rotationZ, Vector3 p_center, float p_width, float p_height, int p_xCount, int p_yCount)
        {
            if (p_xCount <= 0 || p_yCount <= 0)
                return;

            _wayPoint0 = new Vector3[p_xCount * p_yCount];
            _wayPoint = new Vector3[_wayPoint0.Length];

            double xStep = p_width / (p_xCount - 1);
            double x0 = -p_width / 2.0;
            double x1 = -x0;

            double yStep = -p_height / (p_yCount - 1);
            double y0 = p_height / 2.0;
            double y1 = -y0;

            double y = y0;
            for (int i = 0; y >= y1; y += yStep)
            {
                for (double x = x0; x <= x1; i++, x += xStep)
                {
                    _wayPoint0[i].X = (float)x;
                    _wayPoint0[i].Y = (float)y;
                    _wayPoint0[i].Z = 0.0f;
                }
            }

            SetRotationZ(p_rotationZ);
            SetPosition(p_center);
        }

        public void GenerateCircleWayPoints(float p_rotationZ, Vector3 p_center, float p_radiusX, float p_radiusY, int p_residentCount, float p_theta0 = 0.0f, bool p_forward = true)
        {
            if (p_residentCount <= 0)
                return;

            int steps = p_residentCount * 3;

            _wayPoint0 = new Vector3[steps];
            _wayPoint = new Vector3[steps];

            double theta = p_theta0;
            double step = 2.0f * (float)Math.PI / steps;
            if (!p_forward)
            {
                theta = 2.0f * (float)Math.PI;
                step *= -1.0f;
            }
            for (int i = 0; i < steps; i++, theta += step)
            {
                _wayPoint0[i].X = p_radiusX * (float)Math.Cos(theta);
                _wayPoint0[i].Y = p_radiusY * (float)Math.Sin(theta);
                _wayPoint0[i].Z = 0.0f;
            }

            SetRotationZ(p_rotationZ);
            SetPosition(p_center);
        }

        public void GenerateSpiralWayPoints(float p_rotationZ, Vector3 p_center, float p_radiusX, float p_radiusY, int p_loopCount, bool p_curve37, int p_steps, float p_theta0 = 0.0f, bool p_forward = true)
        {
            if (p_steps <= 0)
                return;

            _wayPoint0 = new Vector3[p_steps];
            _wayPoint = new Vector3[p_steps];

            double theta = p_theta0;
            double step = 2.0f * (float)Math.PI / p_steps;
            if (!p_forward)
            {
                theta = 2.0f * (float)Math.PI;
                step *= -1.0f;
            }

            for (int i = 0; i < p_steps; i++, theta += step)
            {
                if (p_curve37)
                {
                    _wayPoint0[i].X = p_radiusX * (float)Math.Abs(theta / (2.0f * Math.PI)) * (float)Math.Cos(p_loopCount * theta);
                    _wayPoint0[i].Y = p_radiusY * (float)Math.Abs(theta / (2.0f * Math.PI)) * (float)Math.Sin(2.0f * p_loopCount * theta);                    
                }
                else
                {
                    _wayPoint0[i].X = p_radiusX * (float)Math.Abs(theta / (2.0f * Math.PI)) * (float)Math.Cos(p_loopCount * theta);
                    _wayPoint0[i].Y = p_radiusY * (float)Math.Abs(theta / (2.0f * Math.PI)) * (float)Math.Sin(p_loopCount * theta);
                }
            }

            SetRotationZ(p_rotationZ);
            SetPosition(p_center);
        }

        public void GenerateCurve37WayPoints(float p_rotationZ, Vector3 p_center, float p_radiusX, float p_radiusY, float p_loopCount, int p_steps, float p_theta0 = 0.0f, bool p_forward = true)
        {
            if (p_steps <= 0)
                return;

            _wayPoint0 = new Vector3[p_steps];
            _wayPoint = new Vector3[p_steps];

            double theta = p_theta0;
            double step = 2.0f * (float)Math.PI / p_steps;
            if (!p_forward)
            {
                theta = 2.0f * (float)Math.PI;
                step *= -1.0f;
            }

            p_loopCount = p_loopCount + 1;

            for (int i = 0; i < p_steps; i++, theta += step)
            {                
                _wayPoint0[i].X = p_radiusX * (float)Math.Cos(theta);
                _wayPoint0[i].Y = p_radiusY * (float)Math.Sin(p_loopCount * theta);
            }

            SetRotationZ(p_rotationZ);
            SetPosition(p_center);
        }

        public Pax4WayPointPathList ToWayPointPaths(int p_modValue = 1, bool p_useWayPointPath0 = false)
        {
            Pax4WayPointPathList result = new Pax4WayPointPathList();

            result.FromWayPointPath(this, p_useWayPointPath0, p_modValue);

            return result;
        }

        public static void ChainUp(List<Pax4ObjectPhysicsPart> p_physicsPartList, bool p_endPositionFixed = false)
        {
            if (p_physicsPartList == null || p_physicsPartList.Count <= 0)
                return;

            Pax4ObjectPhysicsPart chainLink0 = null;
            Pax4ObjectPhysicsPart chainLink1 = null;

            chainLink0 = p_physicsPartList[0];
            for (int i = 1; i < p_physicsPartList.Count; i++)
            {
                chainLink1 = p_physicsPartList[i];

                Vector3 hingePositionParent = (chainLink1.GetPosition() - chainLink0.GetPosition()) / 2.0f; ;

                chainLink0.CreateHingeJoint(chainLink1, true, hingePositionParent, Pax4ObjectPhysicsPart._defaultHingeAxis);
                chainLink0 = chainLink1;
            }

            if (p_endPositionFixed)
                p_physicsPartList[p_physicsPartList.Count - 1].EnableConstraint();
        }
    }
}