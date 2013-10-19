using System;
using System.Runtime.Serialization;

namespace UniVRPNityUtils
{
    [Serializable]
    public class Vector3
    {
        float x;
        float y;
        float z;

        public Vector3()
        {
            this.x = 0;
            this.y = 0;
            this.z = 0;
        }

        public Vector3(float x, float y, float z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }

        public float X
        {
            get
            {
                return this.x;
            }
        }

        public float Y
        {
            get
            {
                return this.y;
            }
        }

        public float Z
        {
            get
            {
                return this.z;
            }
        }

        public override string ToString()
        {
            return x + ", " + y + ", " + z;
        }
    }
}
