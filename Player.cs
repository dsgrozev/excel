using System;
using System.Collections.Generic;

namespace ConsoleApp1
{
    internal class Player
    {
        //internal string Name { get; private set; }
        //internal Position Pos { get; private set; }
        //internal Team Team { get; private set; }
        //internal float ExpectedPoints { get; set; }
        //internal Dictionary<Metric, float> Metrics { get; } = new Dictionary<Metric, float>();

        //internal Player(string name, Position pos, Team team)
        //{
        //    Name = name ?? throw new ArgumentNullException(nameof(name));
        //    Pos = pos;
        //    Team = team ?? throw new ArgumentNullException(nameof(team));
        //}

        //internal static Player CreatePlayer(string bigName)
        //{
        //    string name = "";
        //    string shortTeam = "";
        //    Position pos = Position.DEF;
        //    Parse(bigName, ref name, ref shortTeam, ref pos);
        //    return new Player(name, pos, Program.FindTeam(shortTeam));
        //}

        //private static void Parse(string bigName, ref string name, ref string shortTeam, ref Position pos)
        //{
        //    var tokens = bigName.Split(' ');
        //    int count = tokens.Length;
        //    pos = (Position)Enum.Parse(typeof(Position), tokens[count - 1]);
        //    shortTeam = tokens[count - 3];
        //    name = bigName.Substring(0, bigName.IndexOf(shortTeam)).Trim();
        //}
    }
}
