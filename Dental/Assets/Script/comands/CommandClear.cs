using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Consoleum
{
    public class CommandClear : ConsoleCommand
    {
        public override string Name { get; protected set; }
        public override string Command { get; protected set; }
        public override string Description { get; protected set; }
        public override string Help { get; protected set; }

        public CommandClear()
        {
            Name        = "Clear";
            Command     = "clear";
            Description = "Clear consol log";
            Help        = "no arguments";

            AddCommandToConsole();
        }

        public override void RunCommand(string[] args)
        {
            DeveloperConsole.Instance.ClearConsole();
        }

        public static CommandClear CreateCommand()
        {
            return new CommandClear();
        }
    }
}
