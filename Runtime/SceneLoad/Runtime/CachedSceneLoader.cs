using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Boshphelm.SceneLoad
{
    public abstract class CachedSceneLoader : BoshSceneLoader
    {
        private Scene _currentScene;
        protected GameObject sceneRootGO;

        protected override void OnSceneLoaded(Scene scene)
        {
            _currentScene = scene;

            var rootGOs = _currentScene.GetRootGameObjects();
            if (rootGOs != null && rootGOs.Length > 0)
            {
                sceneRootGO = rootGOs[0];
            }
        }

        public virtual async Task OpenScene()
        {
            if (sceneRootGO == null)
            {
                await LoadScene();
            }
            else
            {
                sceneRootGO.SetActive(true);
            }
        }

        public virtual void CloseScene()
        {
            if (sceneRootGO != null) sceneRootGO.SetActive(false);
        }

        public override void RemoveCurrentScene()
        {
            SceneManager.UnloadSceneAsync(_currentScene);
        }

        protected override void OnSceneLoadFailed()
        {
        }

        protected override void OnSceneLoadStarted()
        {
        }

        protected override void OnSceneRemoved()
        {
        }
    }
}