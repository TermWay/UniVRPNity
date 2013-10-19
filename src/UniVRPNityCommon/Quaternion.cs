using System;
using System.Runtime.Serialization;

namespace UniVRPNityUtils
{
    [Serializable]
    public class Quaternion
    {
        float x;
        float y;
        float z;
        float w;

        public Quaternion()
        {
            this.x = 0;
            this.y = 0;
            this.z = 0;
            this.w = 0;
        }

        public Quaternion(float x, float y, float z, float w)
        {
            this.x = x;
            this.y = y;
            this.z = z;
            this.w = w;
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

        public float W
        {
            get
            {
                return this.w;
            }
        }

        public override string ToString()
        {
            return x + ", " + y + ", " + z + ", " + w;
        }
    }
}
