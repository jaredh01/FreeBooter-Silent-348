using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameConfig
{
    public static int NumberOfPlayers = 4;

    public static int[] StagesFor2P = {1, 2};
    public static int[] StagesFor3P = {1, 2};
    public static int[] StagesFor4P = {1, 2};

    public static int[] DetermineStageList()
    {
        switch ( NumberOfPlayers )
        {
            case 2:
                return StagesFor2P;
            case 3:
                return StagesFor3P;
            case 4:
                return StagesFor4P;
            default:
                return StagesFor4P;
        }
    }
}
