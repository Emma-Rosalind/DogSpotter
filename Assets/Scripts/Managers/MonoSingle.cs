using UnityEngine;

public class MonoSingle <T> : MonoBehaviour where T : MonoBehaviour
{
    // Check to see if we're about to be destroyed.
            private static bool m_ShuttingDown = false;
            private static T m_Instance;
    
            // true to set DoNotDestroyOnLoad for the given object
            public static bool Persistent {
                get => true;
            }
    
    
            // if null then SceneSingleton unusable
            public static string[] SceneDependencies
            {
                get => null;
            }
    
            /// <summary>
            /// Access singleton instance through this propriety.
            /// </summary>
            public static T Instance {
                get
                {
                    if (m_ShuttingDown)
                    {
                        return null;
                    }
                    
                    return m_Instance;
                }
            }
            
            public static void CreateInstance(GameObject obj) 
            { m_Instance = obj.AddComponent<T>(); }

            public virtual void Awake()
            {
                if (m_Instance == null)
                {
                    m_Instance = (T)FindFirstObjectByType(typeof(T));
                    DontDestroyOnLoad(m_Instance);
                }
                m_Instance = GetComponent<T>();
            } 
    
    
            public virtual void OnApplicationQuit()
            { m_ShuttingDown = true; }
    
    
            public virtual void OnDestroy()
            { m_ShuttingDown = true; }
}
