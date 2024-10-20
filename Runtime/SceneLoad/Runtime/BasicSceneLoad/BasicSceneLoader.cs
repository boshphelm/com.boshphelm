using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Boshphelm.SceneLoad
{
    public class BasicSceneLoader : MonoBehaviour
    {
        [SerializeField] private int _sceneIndex;

        public Scene LoadedScene { get; private set; }
        private Coroutine _sceneLoadRoutine;

        public System.Action OnSceneLoadCompleted = () => { };

        public void LoadScene()
        {
            if (_sceneLoadRoutine != null) StopCoroutine(_sceneLoadRoutine);
            _sceneLoadRoutine = StartCoroutine(LoadSceneAsync());
        }

        public IEnumerator LoadSceneAsync()
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
            SceneManager.sceneLoaded += OnSceneLoaded;
            var asyncOperation = SceneManager.LoadSceneAsync(_sceneIndex, LoadSceneMode.Additive);

            if (!asyncOperation.isDone)
            {
                yield return null;
            }

        }

        private void OnSceneLoaded(Scene scene, LoadSceneMode loadSceneMode)
        {
            LoadedScene = scene;
            OnSceneLoadCompleted.Invoke();

            SceneManager.sceneLoaded -= OnSceneLoaded;
        }

        public IEnumerator UnloadScene()
        {
            yield return SceneManager.UnloadSceneAsync(_sceneIndex);
        }
    }
}
