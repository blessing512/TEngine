
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using Luban;

namespace ET
{
    public partial struct vector2
    {
        public vector2(ByteBuf _buf)
        {
            X = _buf.ReadFloat();
            Y = _buf.ReadFloat();

            PostInit();
        }

        public static vector2 Deserializevector2(ByteBuf _buf)
        {
            return new vector2(_buf);
        }

        public readonly float X;

        public readonly float Y;

        public override string ToString()
        {
            return "{ "
            + "x:" + X + ","
            + "y:" + Y + ","
            + "}";
        }

        partial void PostInit();
    }
}
