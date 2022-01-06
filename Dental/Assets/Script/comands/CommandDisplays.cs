using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Consoleum
{
    public class CommandDisplay : ConsoleCommand
    {
        public override string Name { get; protected set; }
        public override string Command { get; protected set; }
        public override string Description { get; protected set; }
        public override string Help { get; protected set; }

        public CommandDisplay()
        {
            Name = "Display";
            Command = "disp";
            Description = "Return count of current displaces," +
                "also create list of displase to intertact";
            Help = "no arguments";

            AddCommandToConsole();
        }

        public override void RunCommand(string[] args)
        {
            var c = Display.displays;
            var cons = DeveloperConsole.Instance;
            Anterpriner.Instance.Displays = c;
            //cons.ParseInput($"{c.Length}:Displays| ");
            //cons.AddMessageToConsole($"{args.Length}");

            if (args.Length == 0)
            {
                cons.AddMessageToConsole($"{c.Length}:Displays| ");
                foreach (var item in c)
                {
                    var s = $"S {item.systemWidth}x{item.systemHeight}|";
                    s += $"\nR {item.renderingWidth}X{item.renderingWidth}|L {item.active}";
                    DeveloperConsole.Instance.AddMessageToConsole($"{s}");
                }
            }
            if (args.Length == 2)
            {
                int dispn =0 ;
                if (Int32.TryParse(args[0],out dispn)& 
                    (args[1]=="t"| args[1] == "f")) {
                    bool act = args[1] == "t" ? true : false;
                    Anterpriner.Instance.Displays[dispn].Activate();
                    
                }
                //cons.AddMessageToConsole($"{c.Length}:Displays| ");
                /*
                foreach (var item in c)
                {
                    var s = $"S {item.systemWidth}x{item.systemHeight}|";
                    s += $"\nR {item.renderingWidth}X{item.renderingWidth}|L {item.active}";
                    DeveloperConsole.Instance.AddMessageToConsole($"{s}");
                }
                 */ 
            }

        }


        public static CommandDisplay CreateCommand()
        {
            return new CommandDisplay();
        }
    }
}




