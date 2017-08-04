using System;
using System.IO;
using System.Json;
using System.Runtime.Serialization;
using Microsoft.Xna.Framework;
using Pax.Core;
using Pax4.Core;

namespace Pax4.Core
{
    [DataContract]
    [KnownType(typeof(Pax4Object))]
    public class Pax4Object : PaxState
    {
        [DataMember]
        public bool _isDisabled = true;
        
        [DataMember]
        public bool _isInvisible = false;
        
        public Pax4Object(String p_name, PaxState p_parent0)
           : base(p_name, p_parent0)
        {
            
        }

        public virtual void Update(GameTime gameTime)
        {
            if (_dxRequested)
                Dx();
        }

        public virtual void Draw(GameTime gameTime)
        {
        }

        //move to Pax4State
        [Intent(typeof(Pax4Object), "Enable")]
        public virtual void Enable()
        {
            _isDisabled = false;
        }

        [Intent(typeof(Pax4Object), "Disable")]
        public virtual void Disable()
        {
            _isDisabled = true;
        }

        [Intent(typeof(Pax4Object), "SetIsInvisible", typeof(bool), "p_isInvisible")]
        public virtual void SetIsInvisible(bool p_isInvisible = false)
        {
            _isInvisible = p_isInvisible;
        }

        public override void Dx()
        {
            if (Pax4World._current != null)
                Pax4World._current.RemoveObject(this);

            base.Dx();
        }

        #region serialize

        public override MemoryStream Serialize(bool p_volatile = false)
        {
            return Serialize(this.GetType(), p_volatile);
        }

        #endregion
    }
}