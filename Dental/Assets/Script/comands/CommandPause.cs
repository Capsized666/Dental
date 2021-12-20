using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Consoleum
{
    public class CommandPause : ConsoleCommand
    {
        public override string Name         { get; protected set; }
        public override string Command      { get; protected set; }
        public override string Description  { get; protected set; }
        public override string Help         { get; protected set; }

        public CommandPause()
        {
            Name = "Pause";
            Command = "pause";
            Description = "Stops Anterpriner work";
            Help = "no arguments";

            AddCommandToConsole();
        }

        public override void RunCommand(string[] args)
        {
            Anterpriner.Instance.WS = false;
        }
            



        public static CommandPause CreateCommand()
        {
            return new CommandPause();
        }
    }
}