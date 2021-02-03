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

    public static class MathUtility
    {
        public enum VecType { x, y };

        /// <summary>
        /// x,y중 한쪽만 우선한 벡터 방향 반환.
        /// 이동 벡터를 x,y 둘 다 가지는게 적합하지 않은 상황에 사용. 
        /// 우선하지 않은 벡터 방향은 0을 반환함.
        /// </summary>
        /// <param name="relativeVec">상대적 좌표</param>
        /// <param name="PriorityVecType">우선할 좌표</param>
        /// <returns></returns>
        public static Vector2 GetDirOnlyOneSide(Vector2 relativeVec, VecType PriorityVecType)
        {
            var dir = GetDir(relativeVec);

            if (dir.x != 0 && dir.y != 0)
            {
                if (PriorityVecType == VecType.x)
                {
                    dir.y = 0;
                }
                else
                {
                    dir.x = 0;
                }
            }

            return dir;
        }

        /// <summary>
        /// 벡터 방향 반환.
        /// </summary>
        /// <param name="relativeVec">상대적 좌표</param>
        /// <returns></returns>
        public static Vector2 GetDir(Vector2 relativeVec)
        {
            var dir = new Vector2(
                relativeVec.x > 0 ? 1 : -1,
                relativeVec.y > 0 ? 1 : -1
                );

            return dir;
        }
    }
}
