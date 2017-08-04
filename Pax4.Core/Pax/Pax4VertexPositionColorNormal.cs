using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Pax4.Core
{
    public struct Pax4VertexPositionColorNormal : IVertexType
    {
        public Vector3 _position;        
        public Color _color;
        public Vector3 _normal;

        public Pax4VertexPositionColorNormal(Vector3 p_position, Vector3 p_normal, Color p_color)
        {
            this._position = p_position;
            this._normal = p_normal;
            this._color = p_color;
        }

        public Pax4VertexPositionColorNormal(Pax4VertexPositionColorNormal p_vertex)
        {
            this._position = p_vertex._position;
            this._normal = p_vertex._normal;
            this._color = p_vertex._color;
        }

        public readonly static VertexDeclaration VertexDeclaration = new VertexDeclaration
        (
            new VertexElement(0, VertexElementFormat.Vector3, VertexElementUsage.Position, 0),
            new VertexElement(sizeof(float) * 3, VertexElementFormat.Color, VertexElementUsage.Color, 0),
            new VertexElement(sizeof(float) * 3 + 4, VertexElementFormat.Vector3, VertexElementUsage.Normal, 0)
        );

        VertexDeclaration IVertexType.VertexDeclaration
        {
            get { return VertexDeclaration; }
        }
    }
}
