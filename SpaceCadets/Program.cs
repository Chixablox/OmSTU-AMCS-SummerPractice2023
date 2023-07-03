﻿using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft;
using System.IO;
using System.Linq;
using System.CommandLine;

namespace SpaceCadets
{
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
        static void Main(string[] args)
        {

            string inputPath = args[0];
            string outputPath = args[1];

            var json = JsonConvert.DeserializeObject<SpaceJson>(File.ReadAllText(inputPath));

            if(json.taskName=="GetStudentsWithHighestGPA")
            {
                double highestGPA = 5;
                var studentsWithHighestGPA = json.data
                .Where(cadet=>(cadet.mark+cadet.mark/2==highestGPA))
                .Select(cadet=>new {Name = cadet.name, GPA = cadet.mark })
                .ToList<dynamic>();

                File.WriteAllText(outputPath, JsonConvert.SerializeObject(studentsWithHighestGPA));
            }
        }
    }
}