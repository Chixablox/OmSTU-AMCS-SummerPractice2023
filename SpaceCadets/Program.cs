﻿using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft;
using System.IO;
using System.Linq;

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
            var max =  json.data.GroupBy(c => c.name).Max(c => c.Average(x=> x.mark));
            var studentsWithHighestGPA = json.data
            .GroupBy(c => c.name)
            .Where(c=> c.Average(x => x.mark) == max)
            .Select(c=> new {Cadet = c.Key, GPA = c.Average(x=> x.mark)})
            .ToList<dynamic>();

            return studentsWithHighestGPA;
        }

        static List<dynamic> CalculateGPAByDiscipline(SpaceJson json)
        {
            var GPAByDiscipline = json.data
            .GroupBy(c => c.discipline)
            .Select(d => new JObject(new JProperty(d.Key, d.Average(c => c.mark))))
            .ToList<dynamic>();

            return GPAByDiscipline;
        }

        static List<dynamic> GetBestGroupsByDiscipline (SpaceJson json)
        {
            var bestGroupsByDiscipline = json.data
            .GroupBy(c => new { c.discipline, c.group })
            .Select(d => new { Discipline = d.Key.discipline,Group = d.Key.group, GPA = d.Average(c => c.mark)})
            .GroupBy(d => d.Discipline)
            .Select(s => new JObject(new JProperty("Discipline", s.Key), 
                    new JProperty("Group", s.OrderByDescending(c => c.GPA).FirstOrDefault().Group),
                    new JProperty("GPA", s.Max(c => c.GPA))))
            .ToList<dynamic>();

            return bestGroupsByDiscipline;
        }
        static void Main(string[] args)
        {

            /*string inputPath = "input1.json";
            string outputPath = "output1.json";*/

            string inputPath = args[0];
            string outputPath = args[1];

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