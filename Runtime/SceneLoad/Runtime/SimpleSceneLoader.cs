using System.Collections;
using UnityEngine.SceneManagement;

namespace Boshphelm.SceneLoad
{
    public class SimpleSceneLoader : BoshSceneLoader
    {
        private Scene _currentScene;

        public override void RemoveCurrentScene()
        {
            StartCoroutine(UnloadScene());
        }

        public IEnumerator UnloadScene()
        {
            yield return SceneManager.UnloadSceneAsync(_currentScene);
        }

        protected override void OnSceneLoadStarted()
        {
        }

        protected override void OnSceneLoaded(Scene scene)
        {
            _currentScene = scene;
        }

        protected override void OnSceneLoadFailed()
        {
        }

        protected override void OnSceneRemoved()
        {
        }
    }
}