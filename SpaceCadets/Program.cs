﻿using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft;
using System.IO;
using System.Linq;

    class SpaceJson
    {
        public string taskName = "";
        public SpaceCadets[] data = new SpaceCadets[]{};
    }
    class SpaceCadets
    {
        public string name = "";
        public string group = "";
        public string discipline = "";
        public double mark = 0;
    }
    class SpaceCadetsMark
    {
        static IEnumerable<Newtonsoft.Json.Linq.JObject> GetStudentsWithHighestGPA(SpaceJson json)
        {
            var max =  json.data.GroupBy(c => c.name).Max(c => c.Average(x=> x.mark));
            var studentsWithHighestGPA = json.data
            .GroupBy(c => c.name)
            .Where(c=> c.Average(x => x.mark) == max)
            .Select(c=> new JObject(new JProperty("Cadet", c.Key), new JProperty("GPA", Convert.ToInt32(c.Average(x=> x.mark)))));

            return studentsWithHighestGPA;
        }

        static IEnumerable<Newtonsoft.Json.Linq.JObject> CalculateGPAByDiscipline(SpaceJson json)
        {
            var GPAByDiscipline = json.data
            .GroupBy(c => c.discipline)
            .Select(d => new JObject(new JProperty(d.Key, d.Average(c => c.mark))));

            return GPAByDiscipline;
        }

        static IEnumerable<Newtonsoft.Json.Linq.JObject> GetBestGroupsByDiscipline (SpaceJson json)
        {
            var bestGroupsByDiscipline = json.data
            .GroupBy(c => new { c.discipline, c.group })
            .Select(d => new { Discipline = d.Key.discipline,Group = d.Key.group, GPA = d.Average(c => c.mark)})
            .GroupBy(d => d.Discipline)
            .Select(s => new JObject(new JProperty("Discipline", s.Key), 
                    new JProperty("Group", s.OrderByDescending(c => c.GPA).FirstOrDefault().Group),
                    new JProperty("GPA", s.Max(c => c.GPA))));

            return bestGroupsByDiscipline;
        }
        static void Main(string[] args)
        {
            string inputPath = args[0];
            string outputPath = args[1];

            var json = JsonConvert.DeserializeObject<SpaceJson>(File.ReadAllText(inputPath));

            if(json.taskName =="GetStudentsWithHighestGPA")
            {
                IEnumerable<Newtonsoft.Json.Linq.JObject> ans = GetStudentsWithHighestGPA(json);
                var result = new JObject(new JProperty("Response", ans));
                File.WriteAllText(outputPath, JsonConvert.SerializeObject(result, Formatting.Indented));
            }
            else if(json.taskName == "CalculateGPAByDiscipline")
            {
                IEnumerable<Newtonsoft.Json.Linq.JObject> ans = CalculateGPAByDiscipline(json);
                var result = new JObject(new JProperty("Response", ans));
                File.WriteAllText(outputPath, JsonConvert.SerializeObject(result, Formatting.Indented));
            }
            else if(json.taskName == "GetBestGroupsByDiscipline")
            {
                IEnumerable<Newtonsoft.Json.Linq.JObject> ans = GetBestGroupsByDiscipline(json);
                var result = new JObject(new JProperty("Response", ans));
                File.WriteAllText(outputPath, JsonConvert.SerializeObject(result, Formatting.Indented));
            }
        }
    }