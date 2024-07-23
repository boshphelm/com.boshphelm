using System;
using UnityEngine;

namespace Boshphelm.Commands
{
    public abstract class CommandExecuteHandler : MonoBehaviour
    {
        [SerializeField] private Command[] _commands;

        private int _commandIndex;
        protected int CommandCount => _commands.Length;

        public float ExecutionProgress
        {
            get
            {
                if (!IsThereAnyAvailableCommandOnCurrentIndex()) return 1f;

                float completionRateForPerCommand = 1f / CommandCount;
                Command currentCommand = _commands[_commandIndex];

                float currentCompletedCommandPercentage = (float)_commandIndex / CommandCount;
                float currentCommandPercentageCompletionRate = currentCommand.Percentage * completionRateForPerCommand;

                return currentCompletedCommandPercentage + currentCommandPercentageCompletionRate;
            }
        }

        public System.Action onAllCommandsExecuted = () => { };


        public void ExecuteCommands()
        {
            _commandIndex = -1;
            ResetAllCommand(); 
            ExecuteNextCommand();
        }

        private void ResetAllCommand()
        {
            for(int i = 0; i < _commands.Length-1; i++) 
                _commands[i].ResetCommand();
        }

        private void ExecuteNextCommand()
        {
            _commandIndex++;
            //Debug.Log("EXECUTING NEXT COMMAND : " + _commandIndex);
            if (IsThereAnyAvailableCommandOnCurrentIndex())
            {
                ExecuteCommand();
            }
            else
            {
                OnAllCommandsExecuted();
            }
        }

        private void OnAllCommandsExecuted()
        {
            //Debug.Log("ALL COMMANDS ARE EXECUTED.");
            onAllCommandsExecuted.Invoke();
        }

        private void ExecuteCommand()
        {
            Command currentCommand = _commands[_commandIndex];
            currentCommand.onCommandComplete += OnCommandCompleted;
            currentCommand.StartCommand();
        }

        private void OnCommandCompleted(Command command)
        {
            //Debug.Log("COMMAND COMPLTED : " + command);
            bool checkedAndClosedConnectionWithCommand = CheckAndCloseConnectionWithCompletedCommand(command);
            if (!checkedAndClosedConnectionWithCommand) return;

            ExecuteNextCommand();
        }

        private bool CheckAndCloseConnectionWithCompletedCommand(Command command)
        {
            bool isCompletedAndCurrentCommandEqual = CheckCompletedAndCurrentCommandEquality(command);
            if (!isCompletedAndCurrentCommandEqual) return false;

            CloseConnectionWithCommand(command);
            return true;
        }

        private bool CheckCompletedAndCurrentCommandEquality(Command command)
        {
            Command currentCommand = _commands[_commandIndex];
            if (currentCommand != command)
            {
                Debug.LogError("CURRENT COMMAND : " + currentCommand.gameObject + " NOT EQUAL TO COMPLETED COMMAND : " + command.gameObject);
                return false;
            }

            return true;
        }

        private void CloseConnectionWithCommand(Command command)
        {
            command.onCommandComplete -= OnCommandCompleted;
        }


        private bool IsThereAnyAvailableCommandOnCurrentIndex() => _commandIndex < CommandCount;
    }
}