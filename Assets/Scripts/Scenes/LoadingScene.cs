using UnityEngine;
using UnityEngine.SceneManagement;

namespace Scenes
{
    public class LoadingScene : MonoBehaviour
    {
        // Update is called once per frame
        void Update()
        {
            if (LoadingManager.Instance.IsReady())
            {
                SceneManager.LoadScene("Game");
            }
        }
    }
}

