﻿using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft;
using System.IO;
using System.Linq;
using System.CommandLine;

    class SpaceJson
    {
        public string taskName;
        public SpaceCadets[] data;
    }
    class SpaceCadets
    {
        public string name;
        public string group;
        public string discipline;
        public double mark;
    }
    class SpaceCadetsMark
    {
        static List<dynamic> GetStudentsWithHighestGPA(SpaceJson json)
        {
            double highestGPA = json.data.Max(cadet=> (cadet.mark+cadet.mark)/2);
            var studentsWithHighestGPA = json.data
            .Where(cadet=>((cadet.mark+cadet.mark)/2==highestGPA))
            .Select(cadet=> new{Name = cadet.name, GPA = cadet.mark })
            .Distinct()
            .ToList<dynamic>();

            return studentsWithHighestGPA;
        }

        static List<dynamic> CalculateGPAByDiscipline(SpaceJson json)
        {
            var GPAByDiscipline = json.data
            .GroupBy(cadet => cadet.discipline)
            .Select(dis => new JObject(new JProperty(dis.Key, dis.Average(cadet => cadet.mark))))
            .ToList<dynamic>();

            return GPAByDiscipline;
        }

        static List<dynamic> GetBestGroupsByDiscipline (SpaceJson json)
        {
            var bestGroupsByDiscipline = json.data
            .GroupBy(s => new { s.discipline, s.group })
            .Select(g => new
            {
                Discipline = g.Key.discipline,
                Group = g.Key.group,
                GPA = g.Average(s => s.mark)
            })
            .GroupBy(g => g.Discipline)
            .Select(g => new JObject(
                new JProperty("Discipline", g.Key),
                new JProperty("Group", g.OrderByDescending(gg => gg.GPA).FirstOrDefault()?.Group),
                new JProperty("GPA", g.Max(gg => gg.GPA))))
            .ToList<dynamic>();

            return bestGroupsByDiscipline;
        }
        static void Main(string[] args)
        {

            string inputPath = Convert.ToString(args[0]);
            string outputPath = Convert.ToString(args[1]);

            var json = JsonConvert.DeserializeObject<SpaceJson>(File.ReadAllText(inputPath));

            if(json.taskName =="GetStudentsWithHighestGPA")
            {
                List<dynamic> ans = GetStudentsWithHighestGPA(json);
                var result = new { Response = ans };
                File.WriteAllText(outputPath, JsonConvert.SerializeObject(result, Formatting.Indented));
            }
            else if(json.taskName == "CalculateGPAByDiscipline")
            {
                List<dynamic> ans = CalculateGPAByDiscipline(json);
                var result = new { Response = ans };
                File.WriteAllText(outputPath, JsonConvert.SerializeObject(result, Formatting.Indented));
            }
            else if(json.taskName == "GetBestGroupsByDiscipline")
            {
                List<dynamic> ans = GetBestGroupsByDiscipline(json);
                var result = new { Response = ans };
                File.WriteAllText(outputPath, JsonConvert.SerializeObject(result, Formatting.Indented));
            }
        }
    }