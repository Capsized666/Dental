using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Consoleum
{
    public class CommandPlayOn : ConsoleCommand
    {
        public override string Name { get; protected set; }
        public override string Command { get; protected set; }
        public override string Description { get; protected set; }
        public override string Help { get; protected set; }

        public CommandPlayOn()
        {
            Name = "Play";
            Command = "play";
            Description = "Play next stepsin script";
            Help = "no arguments";

            AddCommandToConsole();
        }

        public override void RunCommand(string[] args)
        {
            Anterpriner.Instance.WS = true;
        }


        public static CommandPlayOn CreateCommand()
        {
            return new CommandPlayOn();
        }
    }
}