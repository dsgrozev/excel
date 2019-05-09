using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleApp1
{
    internal class Team
    {
        internal string Name { get; private set; }
        internal string ShortName { get; private set; }
        internal List<Team> Schedule { get; } = new List<Team>();
        internal Dictionary<Metric, float> metricsSummaryOffensive = new Dictionary<Metric, float>();
        internal Dictionary<Metric, float> metricsSummaryDefensive = new Dictionary<Metric, float>();
        internal Dictionary<int, Dictionary<Metric, float>> offensiveRecord = new Dictionary<int, Dictionary<Metric, float>>();
        internal Dictionary<int, Dictionary<Metric, float>> defensiveRecord = new Dictionary<int, Dictionary<Metric, float>>();

        public Team(string name, string shortName)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            ShortName = shortName ?? throw new ArgumentNullException(nameof(shortName));
        }

        public void AddOffensiveMetric(int weekNumber, Metric metric, float amount)
        {
            AddMetric(weekNumber, metric, offensiveRecord, amount);
        }

        public void AddDefensiveMetric(int weekNumber, Metric metric, float amount)
        {
            AddMetric(weekNumber, metric, defensiveRecord, amount);
        }

        private void AddMetric(int weekNumber, Metric metric, Dictionary<int, Dictionary<Metric, float>> record, float amount)
        {
            if (!record.ContainsKey(weekNumber))
            {
                record.Add(weekNumber, new Dictionary<Metric, float>());
            }

            if (!record[weekNumber].ContainsKey(metric))
            {
                record[weekNumber].Add(metric, 0);
            }

            record[weekNumber][metric] += amount;
        }

        internal void SummarizeMetrics()
        {
            Summarize(offensiveRecord, metricsSummaryOffensive);
            Summarize(defensiveRecord, metricsSummaryDefensive);
        }

        private void Summarize(Dictionary<int, Dictionary<Metric, float>> records, Dictionary<Metric, float> summary)
        {
            int maxWeek = records.Keys.Max();
            int divider = 0;
            if (maxWeek > 4)
            {
                divider = maxWeek + 8;
            }
            else if (maxWeek > 2)
            {
                divider = maxWeek + 2;
            }
            else
            {
                divider = maxWeek;
            }

            for (int i = 1; i <= maxWeek; i++)
            {
                foreach (Metric metric in Enum.GetValues(typeof(Metric)))
                {
                    var value = records[i][metric] / divider;

                    summary[metric] += value;
                    if (maxWeek > 2 && i > maxWeek - 2)
                    {
                        summary[metric] += value;
                    }
                    if (maxWeek > 4 && i > maxWeek - 4)
                    {
                        summary[metric] += value;
                    }
                    if (maxWeek > 4 && i > maxWeek - 2)
                    {
                        summary[metric] += value;
                    }
                }
            }
        }
    }
}
