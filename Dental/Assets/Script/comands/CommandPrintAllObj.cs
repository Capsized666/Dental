using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Consoleum
{
    public class CommandPrintAllObj : ConsoleCommand
    {
        public override string Name { get; protected set; }
        public override string Command { get; protected set; }
        public override string Description { get; protected set; }
        public override string Help { get; protected set; }

        public CommandPrintAllObj()
        {
            Name = "Print All Object";
            Command = "pao";
            Description = "Write all obgect in comendant";
            Help = "no arguments";

            AddCommandToConsole();
        }

        public override void RunCommand(string[] args)
        {
         
            if (args.Length == 0) { 
                var a = Comendant.Instance.getOOSNames();//getAllObject();

                DeveloperConsole.Instance.AddMessageToConsole("<Game Object>");
                for (int i = 0; i < a.Length; i++)
                {
                    DeveloperConsole.Instance.AddMessageToConsole(a[i]);
                    //Debug.Log();
                }
                DeveloperConsole.Instance.AddMessageToConsole("<UI Object>");
                a = MCUI.Instance.getOOSNames();

                for (int i = 0; i < a.Length; i++)
                {
                    DeveloperConsole.Instance.AddMessageToConsole(a[i]);
                    //Debug.Log();
                }
            }

            if (args.Length==1 && args[0]=="go") {

                var a = Comendant.Instance.getAllObject();//getAllObject();
                for (int i = 0; i < a.Length; i++)
                {
                    DeveloperConsole.Instance.AddMessageToConsole(a[i]);
                    //Debug.Log();
                }

            }

            if (args.Length == 1 && args[0] == "ui")
            {

                var a = MCUI.Instance.getAllObject();//getAllObject();
                for (int i = 0; i < a.Length; i++)
                {
                    DeveloperConsole.Instance.AddMessageToConsole(a[i]);
                    //Debug.Log();
                }

            }
        }





        public static CommandPrintAllObj CreateCommand()
        {
            return new CommandPrintAllObj();
        }
    }
}
