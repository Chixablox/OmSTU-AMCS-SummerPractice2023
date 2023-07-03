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
            .GroupBy(cadet => new { cadet.discipline, cadet.group })
            .Select(cadet => new
            {
                Discipline = cadet.Key.discipline,
                Group = cadet.Key.group,
                GPA = cadet.Average(c1 => c1.mark)
            })
            .GroupBy(cadet => cadet.Discipline)
            .Select(cadet => new JObject(
                new JProperty("Discipline", cadet.Key),
                new JProperty("Group", cadet.OrderByDescending(c1 => c1.GPA).FirstOrDefault()?.Group),
                new JProperty("GPA", cadet.Max(c1 => c1.GPA))))
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