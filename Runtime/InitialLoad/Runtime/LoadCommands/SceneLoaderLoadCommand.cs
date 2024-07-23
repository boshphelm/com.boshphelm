using Boshphelm.SceneLoad;
using UnityEngine;

namespace Boshphelm.InitialLoad
{
    public class SceneLoaderLoadCommand : LoadCommand
    {
        [SerializeField] private bool _worksOnDevelopment;
        [SerializeField] private BoshSceneLoader _sceneLoader;

        public override float PercentageComplete => _percentageComplete;

        private float _percentageComplete;

        public override void StartCommand()
        {
            _percentageComplete = 0f;
            // TODO: Works On Development ??

            LoadSceneAsync();
        }

        public override void ResetCommand()
        {
        }

        private async void LoadSceneAsync()
        {
            _percentageComplete = 0f;

            await _sceneLoader.LoadScene();

            _percentageComplete = 1f;

            CompleteCommand();
        }
    }
}