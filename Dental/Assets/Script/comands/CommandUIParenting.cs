 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Consoleum
{
    public class CommandUIParenting : ConsoleCommand
    {
        public override string Name { get; protected set; }
        public override string Command { get; protected set; }
        public override string Description { get; protected set; }
        public override string Help { get; protected set; }

        public CommandUIParenting()
        {
            Name = "UI Perenting";
            Command = "setUIP";
            Description = "Set ui parenting relations for elemens";
            Help = "setUIP <ParentName> for <ChildName>";

            AddCommandToConsole();
        }

        public override void RunCommand(string[] args)
        {
                if (ChekObject(args[0]) & args[1] == "for" & ChekObject(args[2])) {

                    foreach (GameObject parent in MCUI.Instance.UIObject)
                    {
                        if (parent.name == args[0])
                        {
                            foreach (GameObject child in MCUI.Instance.UIObject)
                            {
                                if (child.name == args[2])
                                {
                                    child.transform.SetParent(parent.transform);
                                    child.GetComponent<RectTransform>().sizeDelta = Vector2.zero;
                                    child.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
                                    return;
                                }
                            }
                        }
                    }
                }
            else {
                DeveloperConsole.Instance.AddMessageToConsole("Wrong Input");

            }


        }
        private bool ChekObject(string obj) {
            foreach (var item in MCUI.Instance.UIObject)
            {
                if (item.name==obj)
                {
                    return true;
                }
            }
            return false;
        }


            



        public static CommandUIParenting CreateCommand()
        {
            return new CommandUIParenting();
        }
    }
}