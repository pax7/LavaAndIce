using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using Pax4.ProjectMercury;
using Pax.Core;
using System.IO;
using System.Runtime.Serialization;
using Pax4.Jitter.LinearMath;

namespace Pax4.Core
{
    [DataContract]
    [KnownType(typeof(Pax4Camera))]
    public class Pax4Camera : PaxState
    {
        //rendering
        public enum EState
        {
            _ZERO,
            _PERSPECTIVE,
            _ORTHOGRAPHIC,
            _COUNT
        };

        [IgnoreDataMember]
        public static Pax4Camera _current = null;

        [IgnoreDataMember]
        private EState  _projectionState     = EState._PERSPECTIVE;

        [IgnoreDataMember]        
        public Matrix   _matProjection       = Matrix.Identity;
        
        [IgnoreDataMember]
        public Matrix   _matView             = Matrix.Identity;

        [DataMember]
        private Vector3 _upVector           = Vector3.Up;// new Vector3(0.0f, 1.0f, 0.0f);        

        [IgnoreDataMember]
        private Vector3 _upVectorReference  = Vector3.Up;// new Vector3(0.0f, 1.0f, 0.0f);        

        [IgnoreDataMember]
        private float   _upVectorRotation   = 0.0f;

        [IgnoreDataMember]
        private Vector3 _leftVector         = Vector3.Left;

        [IgnoreDataMember]
        private Vector3 _targetReference    = -Vector3.UnitZ;//new Vector3(0.0f, 0.0f, 1.0f);        

        [DataMember]
        public  Vector3 _target             = Vector3.Zero;

        [DataMember]
        public  bool    _hasTarget          = false;

        [DataMember]
        public Vector3 _position    = Vector3.Zero;

        [DataMember]
        public Vector3 _rotation    = Vector3.Zero;

        [DataMember]
        public Vector3 _surround    = Vector3.Zero;

        [DataMember]
        public Vector3 _heading     = Vector3.Zero;

        [DataMember]
        public Vector3 _headingUnit = Vector3.Zero;

        [IgnoreDataMember]
        private Matrix _matRotation     = Matrix.Identity;

        [IgnoreDataMember]
        public bool _updateView = false;

        [DataMember]
        public float _aspectRatio = 1.0f;

        [DataMember]
        public float _isInViewFactor = 1.0f;

        [DataMember]
        public float _fieldOfView = MathHelper.PiOver4;//45

        [DataMember]
        public float _nearPlane = 1.0f;

        [DataMember]
        public float _farPlane = 200.0f;
        //public float _farPlane = 1000.0f;

        [DataMember]
        public static CullMode _cullMode = CullMode.CullCounterClockwiseFace;

        [DataMember]
        public static FillMode _fillMode = FillMode.Solid;

        [IgnoreDataMember]
        public static BlendState _blendState = BlendState.AlphaBlend; //BlendState.Additive; for a cool power up effect
        
        [DataMember]
        public static DepthStencilState _depthStencilState = DepthStencilState.Default;

        [DataMember]
        public bool _immovable = false;

        [DataMember]
        public Vector3 _scale = Vector3.One;

        [DataMember]
        public Vector2 _scale2 = Vector2.One;

        [DataMember]
        public static Vector3 _scale0 = new Vector3(512.0f, 910.0f, 0.0f);

        [DataMember]        
        public static Vector3 _scale00 = new Vector3(512.0f, 910.0f, 0.0f);

        [IgnoreDataMember]
        public Matrix _matScale = Matrix.Identity;

        public BoundingFrustum _boundingFrustum = null;

        [DataMember]
        public Vector4 _moveSensitivity = new Vector4(0.5f,
                                                      0.5f,
                                                      0.5f,
                                                      0.1f);
        [DataMember]
        public Vector4 _lookSensitivity = new Vector4(0.0005f,
                                                      0.0005f,
                                                      0.0005f,
                                                      0.0005f);
        [DataMember]
        public Vector4 _surroundSensitivity = new Vector4(0.0020f,
                                                          0.0020f,
                                                          0.0020f,
                                                          0.0020f);
        [IgnoreDataMember]
        public static RasterizerState _rasterizerState;

        [IgnoreDataMember]
        private Dictionary<String, BasicEffect> _effect = null;

        //private Dictionary<String, PaxXnaLight> _light = null;
        //private bool _enableLighting = false;

        //public static BasicEffect _currentEffect = null;
        //private EffectTechnique _currentEffectTechnique = null;

        [IgnoreDataMember]
        public static Matrix _matPickRayWorld = Matrix.CreateTranslation(0, 0, 0);

        [DataMember]
        public static int _backBufferWidth = 0;

        [DataMember]        
        public static int _backBufferHeight = 0;

        //public Pax4ModifierCamera _cameraPositionModifier = null;
        //public Pax4ModifierCamera _cameraTargetModifier = null;
        //public Pax4ModifierCamera _cameraShakeModifier = null;

        public Pax4Camera(String p_name, PaxState p_parent0)
            : base(p_name, p_parent0)
        {
            _current = this;

            _backBufferWidth = Pax4Game._graphicsDeviceManager.PreferredBackBufferWidth;
            _backBufferHeight = Pax4Game._graphicsDeviceManager.PreferredBackBufferHeight;

            //if (_backBufferWidth > _backBufferHeight)
            //{
                _aspectRatio = (float)_backBufferWidth / (float)_backBufferHeight;
                _isInViewFactor = 2.0f * (float)Math.Tan(_aspectRatio / 2.0f);

                 
            //    _scale.Z = _aspectRatio / 1.77778f;

            //    _scale.X = _scale0.X / (float)_backBufferWidth;
            //    _scale.Y = _scale0.Y / (float)_backBufferHeight;
            //}
            //else
            //{
            //    _aspectRatio = (float)_backBufferHeight / (float)_backBufferWidth;
            //    _scale.Z = 1 / _aspectRatio / 1.77778f;

            //    _scale.X = _scale0.X / (float)_backBufferHeight;
            //    _scale.Y = _scale0.Y / (float)_backBufferWidth;
            //}

            //_scale2.X = _scale.X;
            //_scale2.Y = _scale.Y;

            //_matScale = Matrix.CreateScale(_scale2.X, _scale2.X, 1.0f);

            //_cameraShakeModifier = new Pax4CameraTargetModifier();
            //_cameraShakeModifier.Ini(0.016f);

            //_cameraPositionModifier = new Pax4CameraPositionModifier();
            //_cameraTargetModifier = new Pax4CameraPositionModifier();

            Pax4Game._graphicsDeviceManager.ApplyChanges();
        }

        public void SetOrientation(DisplayOrientation p_displayOrientation = DisplayOrientation.Portrait)
        {
#if !WINDOWS
            //if    (p_displayOrientation == DisplayOrientation.LandscapeLeft
            //    || p_displayOrientation == DisplayOrientation.LandscapeRight)
            //{
            //    //invert width and height
            //    float scaleY = Pax4Camera._scale0.X;
            //    Pax4Camera._scale0.X = Pax4Camera._scale0.Y;
            //    Pax4Camera._scale0.Y = scaleY;
            //}
            //else if (   p_displayOrientation == DisplayOrientation.Portrait
            //         && _backBufferWidth > Pax4Camera._backBufferHeight)
            //{
            //    _backBufferWidth = Pax4Camera._backBufferHeight;
            //    _backBufferHeight = Pax4Game._graphicsDeviceManager.PreferredBackBufferWidth; //GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
            //}
            //else if (   p_displayOrientation == DisplayOrientation.LandscapeLeft
            //         || p_displayOrientation == DisplayOrientation.LandscapeRight
            //         && _backBufferWidth < _backBufferHeight)
            //{
            //    _backBufferHeight = Pax4Camera._backBufferWidth;
            //    _backBufferWidth = Pax4Game._graphicsDeviceManager.PreferredBackBufferHeight; //GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
            //}
            
            Pax4Game._graphicsDeviceManager.SupportedOrientations = p_displayOrientation;// DisplayOrientation.LandscapeLeft | DisplayOrientation.LandscapeRight; 

            Pax4Game._graphicsDeviceManager.ApplyChanges();

            _backBufferWidth = Pax4Game._graphicsDeviceManager.PreferredBackBufferWidth;
            _backBufferHeight = Pax4Game._graphicsDeviceManager.PreferredBackBufferHeight;

            _aspectRatio = (float)_backBufferWidth / (float)_backBufferHeight;
            _isInViewFactor = 2.0f * (float)Math.Tan(_aspectRatio / 2.0f);
#endif
        }    

        public void Draw(GameTime gameTime)
        {
            //_graphicsDevice.Clear(Color.CornflowerBlue);
            //Pax4Game._graphicsDevice.Clear(ClearOptions.Target | ClearOptions.DepthBuffer, Color.Black, 1.0f, 0);

            //if (_renderText)
            //    RenderText();
        }

        public virtual void Update(GameTime gameTime)
        {
            if (_immovable)
                return;

            #region look and pinch and unPinch
            ////this is for testing and debugging only
            //if (Pax4Touch._current._currentTouchState._clean)
            //{
            //    if (Pax4Touch._current._currentTouchState._oneTouch
            //        && Pax4Touch._current._previousTouchState._oneTouch)
            //    {
            //        _rotation.Y -= Pax4Touch._current._currentTouchState._dxdy.X * _lookSensitivity.Y;
            //        _rotation.X -= Pax4Touch._current._currentTouchState._dxdy.Y * _lookSensitivity.X;

            //        _updateView = true;
            //    }
            //    else if (Pax4Touch._current._previousTouchState._twoTouch)
            //    {
            //        if (Pax4Touch._current._currentTouchState._pinch)
            //        {
            //            MoveBackward();
            //            _updateView = true;
            //        }
            //        else if (Pax4Touch._current._currentTouchState._unPinch)
            //        {
            //            MoveForward();
            //            _updateView = true;
            //        }
            //    }
            //}
            #endregion

            //if (_cameraShakeModifier != null)
            //    _cameraShakeModifier.Update(gameTime);

            UpdateView();

            //if (_cameraShakeModifier != null)
            //{
            //    if (_cameraShakeModifier._done)
            //        _hasTarget = _cameraShakeModifier._hasTarget0;
            //}
        }

        //public float xx = -0.3f;
        //    _target.X = xx;
        //    xx += 0.001f;
        //    if (xx >= 0.3f)
        //        xx = -0.3f;

        public virtual void UpdateView()
        {
            if (!_updateView)
                return;

            if (!_hasTarget)
            {
                _matRotation = Matrix.CreateFromYawPitchRoll(_rotation.Y, _rotation.X, 0.0f);

                Vector3 targetTransformedReference = Vector3.Transform(_targetReference, _matRotation);

                _target = _position + targetTransformedReference;

                _heading = targetTransformedReference;

                _headingUnit = _heading;
                _headingUnit.Normalize();

                _leftVector = Vector3.Cross(targetTransformedReference, _upVector);                
            }
            else
            {
                _headingUnit = _target - _position;
                _headingUnit.Normalize();
            }

            _matView = Matrix.CreateLookAt(_position, _target, _upVector);

            _boundingFrustum = new BoundingFrustum(_matView * _matProjection);
            
            //if (_currentEffect != null)
            //    _currentEffect.View = _matView;

            _updateView = false;
        }

        public Dictionary<String, BasicEffect> GetEffect()
        {
            if (_effect == null)
                _effect = new Dictionary<String, BasicEffect>();

            return _effect;
        }

        public void CreateBasicEffect()
        {
            //_currentEffect = new BasicEffect(Pax4Game._graphicsDeviceManager.GraphicsDevice);
            
            //_currentEffect.EnableDefaultLighting();
            //_currentEffect.PreferPerPixelLighting = true;

            //GetEffect();
            //_effect.Add("Effect", _currentEffect);

            //_currentEffect.AmbientLightColor = new Vector3(0.1f, 0.1f, 0.1f);
            //_currentEffect.DiffuseColor = new Vector3(1.0f, 1.0f, 1.0f);
            //_currentEffect.SpecularColor = new Vector3(0.25f, 0.25f, 0.25f);
            //_currentEffect.SpecularPower = 5.0f;
            //_currentEffect.Alpha = 1.0f;

            //_currentEffect.LightingEnabled = true;
            //if (_currentEffect.LightingEnabled)
            //{
            //    _currentEffect.DirectionalLight0.Enabled = true; // enable each light individually
            //    if (_currentEffect.DirectionalLight0.Enabled)
            //    {
            //        // x direction
            //        _currentEffect.DirectionalLight0.DiffuseColor = new Vector3(1, 0, 0); // range is 0 to 1
            //        _currentEffect.DirectionalLight0.Direction = Vector3.Normalize(new Vector3(-1, 0, 0));
            //        // points from the light to the origin of the scene
            //        _currentEffect.DirectionalLight0.SpecularColor = Vector3.One;
            //    }

            //    _currentEffect.DirectionalLight1.Enabled = true;
            //    if (_currentEffect.DirectionalLight1.Enabled)
            //    {
            //        // y direction
            //        _currentEffect.DirectionalLight1.DiffuseColor = new Vector3(0, 0.75f, 0);
            //        _currentEffect.DirectionalLight1.Direction = Vector3.Normalize(new Vector3(0, -1, 0));
            //        _currentEffect.DirectionalLight1.SpecularColor = Vector3.One;
            //    }

            //    _currentEffect.DirectionalLight2.Enabled = true;
            //    if (_currentEffect.DirectionalLight2.Enabled)
            //    {
            //        // z direction
            //        _currentEffect.DirectionalLight2.DiffuseColor = new Vector3(0, 0, 0.5f);
            //        _currentEffect.DirectionalLight2.Direction = Vector3.Normalize(new Vector3(0, 0, -1));
            //        _currentEffect.DirectionalLight2.SpecularColor = Vector3.One;
            //    }
            //}
        }

        public override void Ini()
        {
            SetProjection();

            SetCull(_cullMode);//CullMode.None
            SetFillMode(_fillMode);
            SetBlendState(_blendState);
            
            _updateView = true;
            UpdateView();
        }

        public virtual void SetImmovable(bool p_immovable = true)
        {
            _immovable = p_immovable;
        }

        public void SetProjection(Pax4Camera.EState p_projectionMode = EState._PERSPECTIVE)
        {
            switch (p_projectionMode)
            {
                case EState._PERSPECTIVE:
                    _matProjection = Matrix.CreatePerspectiveFieldOfView(_fieldOfView, _aspectRatio, _nearPlane, _farPlane);
                    break;

                case EState._ORTHOGRAPHIC:
                    _matProjection = Matrix.CreateOrthographic(Pax4Game._graphicsDeviceManager.PreferredBackBufferWidth, Pax4Game._graphicsDeviceManager.PreferredBackBufferHeight, _nearPlane, _farPlane);
                    break;
            }

            SetProjectionMatrix(_matProjection);
        }

        public Matrix GetProjectionMatrix()
        {
            return _matProjection;
        }
        public void SetProjectionMatrix(Matrix p_projection)
        {
            _matProjection = p_projection;
            
            //if (_currentEffect != null)            
            //    _currentEffect.Projection = _matProjection;         
        }

        public Pax4Camera.EState GetProjectionState()
        {
            return _projectionState;
        }

        public Vector3 GetTarget()
        {
            return _target;
        }
        public void SetTarget(JVector p_target)
        {
            _target.X = p_target.X;
            _target.Y = p_target.Y;
            _target.Z = p_target.Z;

            _hasTarget = true;
            _updateView = true;
        }
        public void SetTarget(float p_x, float p_y, float p_z)
        {
            _target.X = p_x;
            _target.Y = p_y;
            _target.Z = p_z;
            _hasTarget = true;
            _updateView = true;
        }

        public float GetAspectRatio()
        {
            return _aspectRatio;
        }
        public void SetAspectRatio(float p_aspectRatio)
        {
            _aspectRatio = p_aspectRatio;
            _isInViewFactor = 2.0f * (float)Math.Tan(_aspectRatio / 2.0f);
        }

        public float GetFieldOfView()
        {
            return _fieldOfView;
        }
        public void SetFieldOfView(float p_fieldOfView)
        {
            _fieldOfView = p_fieldOfView;
        }

        public float GetNearPlane()
        {
            return _nearPlane;
        }
        public void SetNearPlane(float p_nearPlane)
        {
            _nearPlane = p_nearPlane;
        }

        public float GetFarPlane()
        {
            return _farPlane;
        }
        public void SetFarPlane(float p_farPlane)
        {
            _farPlane = p_farPlane;
        }

        public void SetNearFarPlane(float p_nearPlane, float p_farPlane)
        {
            SetNearPlane(p_nearPlane);
            SetFarPlane(p_farPlane);
        }        

        public Vector4 GetLookSensitivity()
        {
            return _lookSensitivity;
        }
        public void SetLookSensitivity(Vector4 p_sensitivity)
        {
            _lookSensitivity = p_sensitivity;
        }
        public void SetLookSensitivity(float p_sx = 0.01f, float p_sy = 0.01f, float p_sz = 0.01f, float p_sw = 0.01f)
        {
            _lookSensitivity.X = p_sx;
            _lookSensitivity.Y = p_sy;
            _lookSensitivity.Z = p_sz;
            _lookSensitivity.W = p_sw;
        }

        public Vector4 GetMoveSensitivity()
        {
            return _moveSensitivity;
        }
        public void SetMoveSensitivity(Vector4 p_sensitivity)
        {
            _moveSensitivity = p_sensitivity;
        }
        public void SetMoveSensitivity(float p_sx = 0.01f, float p_sy = 0.01f, float p_sz = 0.01f, float p_sw = 0.01f)
        {
            _moveSensitivity.X = p_sx;
            _moveSensitivity.Y = p_sy;
            _moveSensitivity.Z = p_sz;
            _moveSensitivity.W = p_sw;
        }

        private void NewRasterizerState()
        {
            _rasterizerState = new RasterizerState();//!* { ScissorTestEnable = true };
            _rasterizerState.CullMode = _cullMode;
            _rasterizerState.FillMode = _fillMode;
           
        }

        public void SetCull(CullMode p_cullMode = CullMode.CullCounterClockwiseFace)
        {
            _cullMode = p_cullMode;
            NewRasterizerState();            
        }

        public void SetFillMode(FillMode p_fillMode = FillMode.Solid)
        {
            _fillMode = p_fillMode;
            NewRasterizerState();          
        }

        public void SetBlendState(BlendState p_blendState)
        {
            _blendState = p_blendState;
            Pax4Game._graphicsDeviceManager.GraphicsDevice.BlendState = _blendState;

            //Pax4Game._graphicsDevice.BlendState.AlphaSourceBlend = Blend.SourceAlpha;
            //Pax4Game._graphicsDevice.BlendState.AlphaDestinationBlend = Blend.InverseSourceAlpha;
            //Pax4Game._graphicsDevice.RenderState.SourceBlend = Blend.SourceAlpha;
            //Pax4Game._graphicsDevice.RenderState.DestinationBlend = Blend.InverseSourceAlpha;
        }        

        public void ResetLook()
        {
        }

        public void SetRotation(Vector3 p_rotation)
        {
            _rotation = p_rotation;            
        }

        public Vector3 GetPosition()
        {
            return _position;
        }
        public void SetPosition(Vector3 p_position)
        {
            _position = p_position;
            _updateView = true;
        }
        public void SetPosition(float p_x, float p_y, float p_z)
        {
            _position.X = p_x;
            _position.Y = p_y;
            _position.Z = p_z;
        }

        //http://www.lighthouse3d.com/tutorials/view-frustum-culling/radar-approach-testing-points-ii/
        private bool IsInView(Vector3 p_position, float p_radius = 0.0f)//this is not working too well
        {
            bool result = false;

            int resultCount = 0;

            Vector3 v = p_position - _position;

            Vector3 pc;

            pc.Z = Vector3.Dot(v, _headingUnit);
            if (pc.Z > _farPlane
                || pc.Z < _nearPlane)
                resultCount--;
            else
                resultCount++;

            float h = pc.Z * _isInViewFactor;
            float w = h * _aspectRatio;

            h /= 2.0f;
            w /= 3.5f;

            pc.Y = Vector3.Dot(v, _upVector);
            if (pc.Y < -h 
                || pc.Y > h)
                resultCount--;
            else
                resultCount++;

            pc.X = Vector3.Dot(v, -_leftVector);
            if (pc.X < -w
                || pc.X > w)
                resultCount--;
            else
                resultCount++;

            if (resultCount == 3)
                result = true;
            else
                result = false;

            return result;
        }

        #region movement

        public void MoveForward()
        {
            Vector3 offSet = (_headingUnit * _moveSensitivity.Z);
            _position += offSet;            
        }

        public void MoveBackward()
        {
            Vector3 offSet = (_headingUnit * _moveSensitivity.Z);
            _position -= offSet;            
        }

        public void MoveUp()
        {
            Vector3 offSet = (_upVector * _moveSensitivity.Y);
            _position += offSet;
            _target += offSet;
        }

        public void MoveDown()
        {
            Vector3 offSet = (_upVector * _moveSensitivity.Y);
            _position -= offSet;
            _target -= offSet;
        }

        public void MoveLeft()
        {
            _position -= (_leftVector * _moveSensitivity.X);            
        }

        public void MoveRight()
        {
            _position += (_leftVector * _moveSensitivity.X);            
        }

        public void LookUp()
        {
            if (_position.Z >= 0)
                _rotation.X += _lookSensitivity.X;
            else
                _rotation.X -= _lookSensitivity.X;
        }

        public void LookDown()
        {
            if (_position.Z >= 0)
                _rotation.X += _lookSensitivity.X;
            else
                _rotation.X -= _lookSensitivity.X;
        }

        public void LookLeft()
        {
            _rotation.Y += _lookSensitivity.Y;            
        }

        public void LookRight()
        {
            _rotation.Y -= _lookSensitivity.Y;             
        }

        public void LeanRight()
        {
            _upVectorRotation -= _moveSensitivity.W;
            _upVector = Vector3.Transform(_upVectorReference, Matrix.CreateRotationZ(_upVectorRotation));
        }

        public void LeanLeft()
        {
            _upVectorRotation += _moveSensitivity.W;
            _upVector = Vector3.Transform(_upVectorReference, Matrix.CreateRotationZ(_upVectorRotation));
        }

        public void SurroundPitchUp()
        {
            Matrix _matSurroundPitchUp = Matrix.CreateFromYawPitchRoll(0.0f, -_surroundSensitivity.X, 0.0f);
            
            if (_position.Z >= 0)
                _position = Vector3.Transform(_position, -_matSurroundPitchUp);
            else
                _position = Vector3.Transform(_position, _matSurroundPitchUp);
        }

        public void SurroundPitchDown()
        {
            Matrix _matSurroundPitchDown = Matrix.CreateFromYawPitchRoll(0.0f, _surroundSensitivity.X, 0.0f);

            if (_position.Z >= 0)
                _position = Vector3.Transform(_position, -_matSurroundPitchDown);
            else
                _position = Vector3.Transform(_position, _matSurroundPitchDown);
        }

        public void SurroundYawLeft()
        {
            Matrix _matSurroundYawLeft = Matrix.CreateFromYawPitchRoll(-_surroundSensitivity.Y, 0.0f, 0.0f);
            
            _position = Vector3.Transform(_position, _matSurroundYawLeft);
        }

        public void SurroundYawRight()
        {
            Matrix _matSurroundYawRight = Matrix.CreateFromYawPitchRoll(_surroundSensitivity.Y, 0.0f, 0.0f);        

            _position = Vector3.Transform(_position, _matSurroundYawRight);
        }

        #endregion //movement
        
        ////text und fonts
        //private bool _renderText = false;
        //private SpriteFont _fontArial = null;

        //public void SetRenderText(bool p_renderText = true)
        //{
        //    _renderText = p_renderText;
        //    if (_renderText)
        //    {
        //        _fontArial = ((Pax4Game)Pax4Game._current).Content.Load<SpriteFont>("Pax/SpriteFont/SpriteFontArial");
        //    }
        //    else
        //    {
        //        _fontArial = null;
        //        _renderText = false;
        //    }
        //}
        //public bool GetRenderText()
        //{
        //    return _renderText;
        //}        
        //public void RenderText()
        //{
        //    if (_fontArial == null)
        //        return;
        //    int rowStep = 15;
        //    int row = -rowStep;

        //    Pax4Game._spriteBatch.DrawString(_fontArial, "_mouse   : " + Pax4Touch._current._currentTouchState._x + ", " + Pax4Touch._current._currentTouchState._y, new Vector2(0, row += rowStep), Color.White);
        //    Pax4Game._spriteBatch.DrawString(_fontArial, "_touch   : " + Pax4Touch._current._currentTouchState._oneTouch + ", " + Pax4Touch._current._currentTouchState._twoTouch, new Vector2(0, row += rowStep), Color.White);

        //    Pax4Game._spriteBatch.DrawString(_fontArial, "_camPos  : " + _position.ToString(), new Vector2(0, row += rowStep), Color.White);

        //    //if (Pax4World._current._currentActor != null)
        //    //    Pax4Game._spriteBatch.DrawString(_fontArial, "_selected: " + Pax4World._current._currentActor._body.ID + ", " + Pax4World._current._currentActor._body.Position.ToString(), new Vector2(0, row += rowStep * 2), Color.White);            
        //}        

        //public void Shake(float p_offsetMax = 1.0f)
        //{
        //    if (!((Pax4CameraTargetModifier)_cameraShakeModifier)._done)
        //        return;

        //    Vector3 target = RandomUtil.NextUnitVector3() * p_offsetMax;

        //    _cameraShakeModifier.SetRoundTrip();
        //    ((Pax4CameraTargetModifier)_cameraShakeModifier).IniTarget1(target);
        //    ((Pax4CameraTargetModifier)_cameraShakeModifier).Trigger();
        //}

        #region serialize

        public override MemoryStream Serialize(bool p_volatile = false)
        {
            return Serialize(this.GetType(), p_volatile);
        }

        #endregion
    }
}