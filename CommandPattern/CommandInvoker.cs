﻿using MVCDesignPatternsApp.IAppRepository;

namespace MVCDesignPatternsApp.CommandPattern
{
    public class CommandInvoker
    {
        private readonly List<ICommand> _commands = new();

        public void AddCommand(ICommand command) => _commands.Add(command);

        public void ExecuteCommands()
        {
            foreach (var command in _commands)
            {
                command.Execute();
            }
            _commands.Clear();
        }
    }
}
