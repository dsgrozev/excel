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
            Workbook xlWorkBook = xlApp.Workbooks.Open(@"C:\Users\Dimitar\OneDrive\Documents\FantasyWeek1_14_2019.xlsx");
            _Worksheet wsTeams = xlWorkBook.Sheets["Teams"];
            Range rangeTeams = wsTeams.UsedRange;

            for (int i = 2; i <= rangeTeams.Rows.Count; i++)
            {
                Team t = new Team(rangeTeams.Cells[i, 1].Value2, rangeTeams.Cells[i, 2].Value2);
                teams.Add(t);
            }

            _Worksheet data = xlWorkBook.Sheets["Offense Data"];
            Range range = data.UsedRange;

            for (int i = 2; i <= range.Rows.Count; i++)
            {
                Team offTeam = FindTeam(rangeTeams.Cells[i, 3].Value2);
                Team defTeam = FindTeam(rangeTeams.Cells[i, 4].Value2);
                for (int j = 7; j <= range.Columns.Count; j++)
                {
                    if (!Enum.TryParse<Metric>(rangeTeams.Cells[1, j].Value2, out Metric metric))
                    {
                        continue;
                    }
                    int weekNumber = rangeTeams.Cells[i, 5].Value2;
                    float value = rangeTeams.Cells[i, j].Value2;
                    if (value != 0)
                    {
                        offTeam.AddOffensiveMetric(weekNumber, metric, value);
                        defTeam.AddDefensiveMetric(weekNumber, metric, value);
                    }
                }
            }

            //_Worksheet wsPlayers = xlWorkBook.Sheets["Players_Raw"];
            //_Worksheet wsSchedule = xlWorkBook.Sheets["Schedule"];


            //Range rangePlayers = wsPlayers.UsedRange;
            //Range rangeSchedule = wsSchedule.UsedRange;



            //for (int i = 2; i <= rangeTeams.Rows.Count; i++)
            //{
            //    Team t = FindTeam(rangeTeams.Cells[i, 2].Value2);
            //    for (int j = 3; j < 19; j++)
            //    {
            //        Team opp = FindTeam(rangeTeams.Cells[i, j].Value2);
            //        t.Schedule.Add(opp);
            //    }
            //}

            //for (int i = 2; i <= rangePlayers.Rows.Count; i++)
            //{
            //    Player p = Player.CreatePlayer(rangePlayers.Cells[i, 1].Value2);

            //    int missGames = 0;
            //    if (rangePlayers.Cells[i, 9].Value2 != null)
            //    {
            //        missGames = (int)rangePlayers.Cells[i, 9].Value2;
            //    }

            //    float points = 0;
            //    for (; missGames < p.Team.Schedule.Count; missGames++)  
            //    {
            //        float weekPoints = 0;
            //        if (p.Team.Schedule[missGames] != null)
            //        {
            //            weekPoints = p.Team.Schedule[missGames].Metrics[p.Pos] * p.Rating;
            //        }
            //        points += weekPoints;
            //        wsPlayers.Cells[i, 11 + missGames] = weekPoints.ToString();
            //    }
            //    p.ExpectedPoints = points;
            //    wsPlayers.Cells[i, 10] = points.ToString();
            //    players.Add(p);
            //}

            //xlWorkBook.SaveAs(@"C:\Users\dgrozev\Desktop\ff.xlsx");
            //xlApp.Quit();
        }

        internal static Team FindTeam(string Name) => teams.Find(x => x.Name == Name);
    }
}
