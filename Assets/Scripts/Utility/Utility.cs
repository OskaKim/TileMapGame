using UnityEngine;

namespace Utility
{
    public static class RandomUtility
    {
        // 랜덤한 bool 값 반환
        public static bool Bool()
        {
            return Random.Range(0, 2) == 0;
        }
    }
}
