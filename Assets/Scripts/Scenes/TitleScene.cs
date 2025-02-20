using UnityEngine;
using UnityEngine.SceneManagement;

namespace Scenes
{
    public class TitleScene : MonoBehaviour
    {
        public void ClickStart()
        {
            SceneManager.LoadScene(1);
        }

    }
}
