namespace VUDK.Extensions
{
    using UnityEngine;

    public static class QuaternionExtensions
    {
        public static Vector3 SignedEulerAngles(this Quaternion rotation)
        {
            Vector3 eulerAngles = rotation.eulerAngles;
            eulerAngles.x = rotation.x < 0f ? eulerAngles.x - 360 : eulerAngles.x;
            eulerAngles.y = rotation.y < 0f ? eulerAngles.y - 360 : eulerAngles.y;
            eulerAngles.z = rotation.z < 0f ? eulerAngles.z - 360 : eulerAngles.z;
            return eulerAngles;
        }
    }
}
