using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Awards
{
    [Serializable]
    public class Award
    {
        public int id;
        public List<int> Win;
        public List<FlayersWin> FlayersWins;
        public int ValueWin;
        public string TextBingo;
        public bool IsBingo;

        public Award()
        {
            Win = new List<int>();
        }
    }

    [Serializable]
    public class Body
    {
        public List<Award> awards;

        public Body()
        {
            awards = new List<Award>();
        }
    }

    [Serializable]
    public class Root
    {
        public string message;
        public Body body;

        public Root()
        {
            body = new Body();
        }
    }

    [Serializable]
    public class FlayersWin
    {
        public int NumberFlayers;
        public bool isWin = false;
    }
}
