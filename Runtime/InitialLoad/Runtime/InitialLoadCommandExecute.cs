using System.Collections;
using System.Threading.Tasks;
using Boshphelm.Commands;
using Boshphelm.SceneLoad;
using Boshphelm.Utility;
using UnityEngine;

namespace Boshphelm.InitialLoad
{
    public class InitialLoadCommandExecute : MonoBehaviour
    {
        [SerializeField] private SimpleSceneLoader _initialLoadSceneLoader;
        [SerializeField] private LoadCommand[] _commands;
/* 
        [SerializeField] private FadeInOut _fadeInOut; */
        //[SerializeField] private LevelLoadController _levelLoadController;

        private int _commandIndex;
        private int CommandCount => _commands.Length;
        private float PercentageForPerCommand => CommandCount != 0 ? 1f / CommandCount : 1f;

        public float PercentageComplete
        {
            get
            {
                float percentageForPerCommand = PercentageForPerCommand;
                float finishedCommandPercentage = _commandIndex * percentageForPerCommand;
                float currentCommandPercentage = _currentLoadCommand.PercentageComplete * percentageForPerCommand;

                return finishedCommandPercentage + currentCommandPercentage;
            }
        }

        private const float _initialLoadFadeOutTime = .5f;

        private Coroutine _finishingRoutine;

        private LoadCommand _currentLoadCommand;

        private bool _commandsAreExecuting;

        private async void Start()
        {
            await StartInitialLoadingPhase();
        }

        private async Task StartInitialLoadingPhase()
        {
            //_fadeInOut.FadeOutDirectly();

            await _initialLoadSceneLoader.LoadScene();

            /*_loader = _initialLoaderRequested.RaiseEvent();
            _loader.SetPercentage(0f);*/

            _commandsAreExecuting = true;

            _commandIndex = 0;
            if (CommandCount <= 0)
            {
                FinishInitialLoadingPhase();
            }
            else
            {
                ExecuteCommand();
            }
        }

        private void ExecuteCommand()
        {
            _currentLoadCommand = _commands[_commandIndex];
            if (_currentLoadCommand == null)
            {
                OnCommandCompleted(_currentLoadCommand);
            }
            else
            {
                _currentLoadCommand.onCommandComplete += OnCommandCompleted;
                _currentLoadCommand.StartCommand();
            }
        }


        public void OnCommandCompleted(Command command)
        {
            _currentLoadCommand.onCommandComplete -= OnCommandCompleted;
            if (command != _currentLoadCommand) Debug.LogError("Completed command is not the current one.");

            _commandIndex++;
            if (_commandIndex >= CommandCount)
            {
                print("OnCommandCompleted");
                FinishInitialLoadingPhase();
            }
            else
            {
                ExecuteCommand();
            }
        }

        private void FinishInitialLoadingPhase()
        {
            //_onTriggerFirstGameInitiation.RaiseEvent();
            _commandsAreExecuting = false;

            //_onInitialLoadCommandExecuterFinished.RaiseEvent();

            if (_finishingRoutine != null) StopCoroutine(_finishingRoutine);
            _finishingRoutine = StartCoroutine(WaitAndFinishInitialLoad());
        }

        private IEnumerator WaitAndFinishInitialLoad()
        {
            //_loader.SetPercentage(1f);

            WaitForSeconds waiting = new WaitForSeconds(_initialLoadFadeOutTime);
            yield return waiting;

            yield return _initialLoadSceneLoader.UnloadScene();

            //_levelLoadController.PlayInitialLevel(); 
            //_fadeInOut.FadeIn(_initialLoadFadeOutTime);
        }
    }
}