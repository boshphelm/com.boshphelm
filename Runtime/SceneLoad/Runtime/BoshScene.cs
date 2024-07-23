using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.SceneManagement;

namespace Boshphelm.SceneLoad
{
    public class BoshScene
    {
        private Action _onSceneLoadStarted;
        private Action _onSceneLoadFailed;
        private Action<Scene> _onSceneLoaded;
        private Action _onSceneRemoved;
        private AssetReference _sceneRef;
        public bool loaded;

        public Scene latestLoadedScene;

        public BoshScene(AssetReference sceneRef, Action<Scene> onSceneLoaded = null, Action onSceneLoadFailed = null, Action onSceneLoadStarted = null)
        {
            _onSceneLoadStarted = onSceneLoadStarted;
            _onSceneLoadFailed = onSceneLoadFailed;
            _onSceneLoaded = onSceneLoaded;
            _sceneRef = sceneRef;
        }

        public BoshScene(AssetReference sceneRef, Action onSceneRemoved = null)
        {
            _sceneRef = sceneRef;
            _onSceneRemoved = onSceneRemoved;
        }

        public async Task Load()
        {
            if (_sceneRef == null) return;
            if (!await HasSceneLoadedAlready())
            {
                await LoadSceneByReference();
            }
        }

        private async Task LoadSceneByReference()
        {
            var sceneLoadOpHandle = _sceneRef.LoadSceneAsync(LoadSceneMode.Additive, true, 0);
            _onSceneLoadStarted?.Invoke();
            try
            {
                await sceneLoadOpHandle.Task;
                Scene scene = sceneLoadOpHandle.Result.Scene;
                latestLoadedScene = scene;
                _onSceneLoaded?.Invoke(latestLoadedScene);

                //Addressables.Release(sceneLoadOpHandle);
            }
            catch (Exception ex)
            {
                Debug.LogError($"Error loading scene {_sceneRef.Asset}: {ex}");
            }
        }

        private async Task<bool> HasSceneLoadedAlready()
        {
            var loc = Addressables.LoadResourceLocationsAsync(_sceneRef);
            await loc.Task;

            var result = loc.Result;

            if (result.Count == 0) return false;

            Scene targetScene = SceneManager.GetSceneByPath(result[0].InternalId);
            loaded = targetScene.isLoaded;

            Addressables.Release(loc);
            return loaded;
        }

        public async Task Remove()
        {
            if (_sceneRef != null && _sceneRef.OperationHandle.IsValid())
            {
                var asyncOperationHandle = _sceneRef.UnLoadScene();
                await asyncOperationHandle.Task;

                _onSceneRemoved?.Invoke();
                //Addressables.Release(asyncOperationHandle);
            }
        }
    }
}