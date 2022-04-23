using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestPath
{
    public QuestEvent start;
    public QuestEvent end;

    public QuestPath(QuestEvent from, QuestEvent to)
    {
        start = from;
        end = to;
    }
}
