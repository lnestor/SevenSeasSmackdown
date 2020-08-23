using UnityEngine;

namespace Shared
{
    public static class SharedResources
    {
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        public static void MarkShared()
        {
            GameObject shared = GameObject.Instantiate(Resources.Load("Shared")) as GameObject;
            GameObject.DontDestroyOnLoad(shared);
        }
    }
}
