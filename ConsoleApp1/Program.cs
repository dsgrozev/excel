using System;
using System.Collections.Generic;
using Microsoft.Office.Interop.Excel;

namespace ConsoleApp1
{
    class Program
    {
        internal static List<Team> teams = new List<Team>();
        internal static List<Player> players = new List<Player>();

        static void Main(string[] args)
        {
            Application xlApp = new Application();
            Workbook xlWorkBook = xlApp.Workbooks.Open(@"C:\Users\dgrozev\Desktop\FantasyWeekFinal2017.xlsx");
            _Worksheet wsTeams = xlWorkBook.Sheets["Teams"];
            //_Worksheet wsPlayers = xlWorkBook.Sheets["Players_Raw"];
            //_Worksheet wsSchedule = xlWorkBook.Sheets["Schedule"];

            Range rangeTeams = wsTeams.UsedRange;
            //Range rangePlayers = wsPlayers.UsedRange;
            //Range rangeSchedule = wsSchedule.UsedRange;

            for (int i = 2; i <= rangeTeams.Rows.Count; i++)
            {
                Team t = new Team(rangeTeams.Cells[i, 1].Value2, rangeTeams.Cells[i, 2].Value2);
                int j = 3;
                foreach (Metric metric in Enum.GetValues(typeof(Metric)))
                {
                    t.Metrics.Add(metric, (float)rangeTeams.Cells[i, j++].Value2);
                }
                teams.Add(t);
            }

            for (int i = 2; i <= rangeTeams.Rows.Count; i++)
            {
                Team t = FindTeam(rangeTeams.Cells[i, 2].Value2);
                for (int j = 3; j < 19; j++)
                {
                    Team opp = FindTeam(rangeTeams.Cells[i, j].Value2);
                    t.Schedule.Add(opp);
                }
            }

            for (int i = 2; i <= rangePlayers.Rows.Count; i++)
            {
                Player p = Player.CreatePlayer(rangePlayers.Cells[i, 1].Value2);

                int missGames = 0;
                if (rangePlayers.Cells[i, 9].Value2 != null)
                {
                    missGames = (int)rangePlayers.Cells[i, 9].Value2;
                }

                float points = 0;
                for (; missGames < p.Team.Schedule.Count; missGames++)  
                {
                    float weekPoints = 0;
                    if (p.Team.Schedule[missGames] != null)
                    {
                        weekPoints = p.Team.Schedule[missGames].Metrics[p.Pos] * p.Rating;
                    }
                    points += weekPoints;
                    wsPlayers.Cells[i, 11 + missGames] = weekPoints.ToString();
                }
                p.ExpectedPoints = points;
                wsPlayers.Cells[i, 10] = points.ToString();
                players.Add(p);
            }

            xlWorkBook.SaveAs(@"C:\Users\dgrozev\Desktop\ff.xlsx");
            xlApp.Quit();
        }

        internal static Team FindTeam(string shortName) => teams.Find(x => x.ShortName == shortName);
    }
}
