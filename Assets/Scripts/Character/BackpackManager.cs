using System.Collections;
using UnityEngine;

namespace CP.Character
{
    public class BackpackManager : MonoBehaviour
    {
        public const int MaxCapacity = 3;

        public string[] matters = new string[MaxCapacity];
        public int matterCount = 0;

        private bool IsFull()
        {
            return matterCount == MaxCapacity;
        }

        public bool AddMatter(string matter)
        {
            if (IsFull())
            {
                return false;
            }
            matters[matterCount++] = matter;
            return true;
        }

        public bool HasMatter(string target)
        {
            for(int i = 0; i < matterCount; i++)
            {
                if (matters[i] == target)
                {
                    return true;
                }
            }
            return false;
        }
    }
}