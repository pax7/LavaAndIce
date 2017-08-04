using System;
using Microsoft.Xna.Framework;
using Pax4.Jitter.LinearMath;

namespace Pax4.Core
{
    public class Pax4Tools
    {
        public readonly static Matrix _matPickRayWorld = Matrix.CreateTranslation(0, 0, 0);

        public static String NumberCommaFormat(int p_number)
        {
            return String.Format("{0:n0}", p_number);
        }

        public static String NumberCommaFormat(float p_number)
        {
            return String.Format("{0:n}", p_number);            
        }

        public static String FloatSecondsToMinutesSeconds(float p_value)
        {
            String result = "";

            int minutes = 0;
            int seconds = 0;

            minutes = (int)(p_value / 60.0f);

            seconds = (int)(p_value - (minutes * 60));

            if (minutes < 10)
                result += " ";

            result += minutes.ToString() + ":";

            if (seconds < 10.0f)
                result += "0";

            result += seconds.ToString();

            return result;
        }        

        public static Vector3 WorldToScreen(Vector3 p_world)
        {
            Vector4 effectPosition4 = Vector4.Transform(p_world, Pax4Camera._current._matView * Pax4Camera._current._matProjection);

            p_world.X = (int)((effectPosition4.X / effectPosition4.W + 1.0f) / 2.0f * Pax4Camera._backBufferWidth);
            p_world.Y = (int)((-effectPosition4.Y / effectPosition4.W + 1.0f) / 2.0f * Pax4Camera._backBufferHeight);
            p_world.Z = 0.0f;

            return p_world;
        }

        public static float GetVector3Angle(Vector3 p_v0, Vector3 p_v1)
        {
            float result = 0.0f;

            p_v0.Normalize();
            p_v1.Normalize();

            if (p_v0 == p_v1)
                return 0.0f;
            else
                result = (float)Math.Acos(Vector3.Dot(p_v0, p_v1));

            if (Math.Abs(result) < 0.0001 || float.IsNaN(result))
                return 0.0f;

            result *= ((p_v1.Y * p_v0.X - p_v0.Y * p_v1.X) >= 0.0f ? 1.0f : -1.0f);

            return result;
        }

        public static void GetVector3YawPitch(Vector3 p_v, out float p_yaw, out float p_pitch)
        {//reference is Vector3.Forward
            p_yaw = 0.0f;
            p_pitch = 0.0f;

            if (p_v == Vector3.Zero)
            {
                return;
            }
            else
            {
                p_v.Normalize();
                if (Math.Abs(p_v.X) <= 0.0001f && Math.Abs(p_v.Z) <= 0.0001f)
                    p_yaw = 0.0f;
                else
                    p_yaw = -(float)Math.Atan2(p_v.X, -p_v.Z);

                p_pitch = (float)Math.Atan2(p_v.Y, Math.Sqrt(p_v.X * p_v.X + p_v.Z * p_v.Z));

                if (Math.Abs(p_yaw) < 0.0001f)
                    p_yaw = 0.0f;

                if (Math.Abs(p_pitch) < 0.0001f)
                    p_pitch = 0.0f;
            }
        }

        public static void GetMatrixYawPitchRoll(Matrix p_m, out float p_yaw, out float p_pitch, out float p_roll)
        {
            p_yaw = -(float)Math.Atan2(p_m.M13, p_m.M23);

            p_pitch = (float)Math.Acos(p_m.M33);

            p_roll = (float)Math.Atan2(p_m.M31, p_m.M31);

            if (Math.Abs(p_yaw) < 0.0001f)
                p_yaw = 0.0f;

            if (Math.Abs(p_pitch) < 0.0001f)
                p_pitch = 0.0f;

            if (Math.Abs(p_roll) < 0.0001f)
                p_roll = 0.0f;
        }

        public static Vector3 YawPitchToVector3(float p_yaw, float p_pitch)
        {
            Vector3 result = Vector3.Zero;

            float sinPitch = (float)Math.Sin(p_pitch);
            float cosPitch = (float)Math.Cos(p_pitch);
            float sinYaw = (float)Math.Sin(p_yaw);
            float cosYaw = (float)Math.Cos(p_yaw);

            result.X = -cosPitch * sinYaw;
            result.Y = sinPitch;
            result.Z = -cosPitch * cosYaw;

            return result;
        }

        public static void GetQuaternionYawPitchRoll(ref Quaternion p_quaternion, out Vector3 p_pitchYawRoll)
        {
            float w = p_quaternion.W;
            float x = p_quaternion.X;
            float y = p_quaternion.Y;
            float z = p_quaternion.Z;

            float yaw = (float)Math.Atan2(2.0f * y * w - 2.0f * x * z, 1 - 2.0f * y * y - 2.0f * z * z);
            float pitch = (float)Math.Atan2(2.0f * x * w - 2.0f * y * z, 1 - 2.0f * x * x - 2.0f * z * z);
            float roll = (float)Math.Asin(2.0f * x * y + 2.0f * z * w);

            p_pitchYawRoll.Y = yaw;
            p_pitchYawRoll.X = pitch;
            p_pitchYawRoll.Z = roll;

            //  matrix = [[1 - 2*(y*y + z*z),     2*(x*y - w*z),     2*(x*z + w*y), 0],
            //[    2*(x*y + w*z), 1 - 2*(x*x + z*z),     2*(y*z - w*x), 0],
            //[    2*(x*z - w*y),     2*(y*z + w*x), 1 - 2*(x*x + y*y), 0],
            //[                0,                 0,                 0, 1]]
        }

        public static float FooAggregate(float p_x, float p_y)
        {
            if ((p_x > 0 && p_y < 0)
                || (p_x < 0 && p_y > 0))
            {
                p_x += (-p_y);
            }
            return p_x;
        }

        /// <summary>
        /// To Bound Int to Lowest Value and Highest Value
        /// </summary>
        /// <param name="p_position"></param>
        /// <param name="max"></param>
        /// <param name="min"></param>
        /// <returns></returns>
        public static int BoundOutput(int p_position, int max, int min = 0)
        {
            if (p_position > max)
            {
                p_position = max;
            }
            if (p_position < min)
            {
                p_position = min;
            }
            return p_position;
        }

        public static bool BoundOutput(ref float p_position, float max, float min = 0f)
        {
            bool set = false;
            if (p_position > max)
            {
                set = true;
                p_position = max;
            }
            if (p_position < min)
            {
                set = true;
                p_position = min;
            }
            return set;
        }

        public static void RayTo(float p_x, float p_y, out Vector3 p_rayOrigin, out Vector3 p_rayDirection)
        {
            Vector3 nearSource = new Vector3(p_x, p_y, 0);
            Vector3 farSource = new Vector3(p_x, p_y, 1);

            Vector3 nearPoint = Pax4Game._graphicsDeviceManager.GraphicsDevice.Viewport.Unproject(nearSource,
                                                                                                  Pax4Camera._current._matProjection,
                                                                                                  Pax4Camera._current._matView,
                                                                                                  _matPickRayWorld);

            Vector3 farPoint = Pax4Game._graphicsDeviceManager.GraphicsDevice.Viewport.Unproject(farSource,
                                                                                                 Pax4Camera._current._matProjection,
                                                                                                 Pax4Camera._current._matView,
                                                                                                 _matPickRayWorld);

            p_rayDirection = farPoint - nearPoint;

            p_rayDirection.Normalize();

            p_rayOrigin = nearPoint;
        }

        public static void RayTo(float p_x, float p_y, out JVector p_rayOrigin, out JVector p_rayDirection, bool p_normalizeRay = false)
        {
            Vector3 nearSource = new Vector3(p_x, p_y, 0);
            Vector3 farSource = new Vector3(p_x, p_y, 1);

            Vector3 nearPoint = Pax4Game._graphicsDeviceManager.GraphicsDevice.Viewport.Unproject(nearSource,
                                                                                                  Pax4Camera._current._matProjection,
                                                                                                  Pax4Camera._current._matView,
                                                                                                  _matPickRayWorld);

            Vector3 farPoint = Pax4Game._graphicsDeviceManager.GraphicsDevice.Viewport.Unproject(farSource,
                                                                                                 Pax4Camera._current._matProjection,
                                                                                                 Pax4Camera._current._matView,
                                                                                                 _matPickRayWorld);

            p_rayDirection = ToJVector(farPoint - nearPoint);

            if(p_normalizeRay)
                p_rayDirection.Normalize();

            p_rayOrigin = ToJVector(nearPoint);
        }        

        public static JVector ToJVector(Vector3 vector)
        {
            return new JVector(vector.X, vector.Y, vector.Z);
        }

        public static Matrix ToXnaMatrix(JMatrix p_matrix)
        {
            return new Matrix(p_matrix.M11,
                              p_matrix.M12,
                              p_matrix.M13,
                              0.0f,
                              p_matrix.M21,
                              p_matrix.M22,
                              p_matrix.M23,
                              0.0f,
                              p_matrix.M31,
                              p_matrix.M32,
                              p_matrix.M33,
                              0.0f, 0.0f, 0.0f, 0.0f, 1.0f);
        }

        public static Matrix ToXnaMatrix(JMatrix p_matrix, JVector p_position)
        {
            return new Matrix(p_matrix.M11,
                              p_matrix.M12,
                              p_matrix.M13,
                              0.0f,
                              p_matrix.M21,
                              p_matrix.M22,
                              p_matrix.M23,
                              0.0f,
                              p_matrix.M31,
                              p_matrix.M32,
                              p_matrix.M33,
                              0.0f, 
                              p_position.X,
                              p_position.Y,
                              p_position.Z, 
                              1.0f);
        }

        public static JMatrix ToJitterMatrix(Matrix p_matrix)
        {
            JMatrix result;
            
            result.M11 = p_matrix.M11;
            result.M12 = p_matrix.M12;
            result.M13 = p_matrix.M13;
            result.M21 = p_matrix.M21;
            result.M22 = p_matrix.M22;
            result.M23 = p_matrix.M23;
            result.M31 = p_matrix.M31;
            result.M32 = p_matrix.M32;
            result.M33 = p_matrix.M33;

            return result;
        }

        public static Vector3 ToXnaVector(JVector p_vector)
        {
            return new Vector3(p_vector.X, p_vector.Y, p_vector.Z);
        }
    }
}