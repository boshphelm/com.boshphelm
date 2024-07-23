using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.SceneManagement;

namespace Boshphelm.SceneLoad
{
    public abstract class BoshSceneLoader : MonoBehaviour
    {
        [SerializeField] private AssetReference _sceneRef;
        [HideInInspector] public Scene LatestLoadedScene;

        public abstract void RemoveCurrentScene();

        public async Task LoadScene()
        {
            await LoadScene(_sceneRef);
        }

        public async Task LoadScene(AssetReference sceneRef)
        {
            BoshScene sceneToLoad = new BoshScene(sceneRef, OnSceneLoaded, OnSceneLoadFailed, OnSceneLoadStarted);
            await sceneToLoad.Load();
            LatestLoadedScene = sceneToLoad.latestLoadedScene;
        }

        public async Task RemoveScene()
        {
            await RemoveScene(_sceneRef);
        }

        public async Task RemoveScene(AssetReference sceneRef)
        {
            BoshScene sceneToUnload = new BoshScene(sceneRef, OnSceneRemoved);

            await sceneToUnload.Remove();
        }

        protected abstract void OnSceneLoadStarted();
        protected abstract void OnSceneLoaded(Scene scene);
        protected abstract void OnSceneLoadFailed();
        protected abstract void OnSceneRemoved();
    }
}