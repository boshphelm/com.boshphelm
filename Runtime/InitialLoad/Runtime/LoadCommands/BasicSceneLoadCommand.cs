using Boshphelm.SceneLoad;
using UnityEngine;

namespace Boshphelm.InitialLoad
{
    public class BasicSceneLoaderCommand : LoadCommand
    {
        [SerializeField] private BasicSceneLoader _sceneLoader;

        private float _percentageComplete;
        public override float PercentageComplete => _percentageComplete;

        public override void StartCommand()
        {
            _percentageComplete = 0f;
            _sceneLoader.OnSceneLoadCompleted += OnSceneLoadComplete;
            _sceneLoader.LoadScene();
        }
        private void OnSceneLoadComplete()
        {
            _percentageComplete = 1f;
            CompleteCommand();
        }
        public override void ResetCommand()
        {

        }
    }
}
