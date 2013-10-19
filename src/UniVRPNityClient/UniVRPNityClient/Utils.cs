namespace UniVRPNity
{
    public static class Utils
    {
        public static UnityEngine.Vector3 Convert(UniVRPNityUtils.Vector3 vec)
        {
           return new UnityEngine.Vector3(vec.X, vec.Y, vec.Z);
        }

        public static UnityEngine.Quaternion Convert(UniVRPNityUtils.Quaternion quat)
        {
            return new UnityEngine.Quaternion(quat.X, quat.Y, quat.Z, quat.W);
        }

        public static UnityEngine.Vector4 Convert(UnityEngine.Quaternion quat)
        {
            return new UnityEngine.Vector4(quat.x, quat.y, quat.z, quat.w);
        }
    }
}
