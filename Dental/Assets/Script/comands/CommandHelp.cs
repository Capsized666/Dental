using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Consoleum
{
    public class CommandHelp : ConsoleCommand
    {
        public override string Name { get; protected set; }
        public override string Command { get; protected set; }
        public override string Description { get; protected set; }
        public override string Help { get; protected set; }

        public CommandHelp()
        {
            Name = "Help";
            Command = "help";
            Description = "Info about comands";
            Help = "no arguments";

            AddCommandToConsole();
        }

        public override void RunCommand(string[] args)
        {
            foreach (var item in DeveloperConsole.Commands)
            {
                var n = item.Value.Command;
                var d = item.Value.Description;
                DeveloperConsole.Instance.AddMessageToConsole($"{n}<{d}");
            }
        }


        public static CommandHelp CreateCommand()
        {
            return new CommandHelp();
        }
    }
}




