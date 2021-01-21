using Newtonsoft.Json;
using ProductivityTools.TrainingLog.Endomondo.Dto;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace ProductivityTools.TrainingLog.Endomondo
{
    class Endomondo
    {
        public string Path { get; set; }

        public Endomondo(string path)
        {
            this.Path = path;
        }

        public IEnumerable<EndoMondoTraining> GetEndomondoTrainings()
        {
            List<EndoMondoTraining> trainings = new List<EndoMondoTraining>();
            var files = Directory.GetFiles(this.Path, "*.json");
            foreach (var file in files)
            {
                List<string> pictures = new List<string>();
                bool points = false;
                using (StreamReader r = new StreamReader(file))
                {
                    string json = r.ReadToEnd();

                    if (json.Contains("pictures"))
                    {
                        int picturesplace = json.IndexOf("pictures");

                        var picturesJson = "{" + json.Substring(picturesplace);
                        string pattern = @"\w*(resources/\w*/\w*/\w*/\w*/\w*.\w*)\w*";
                        Regex rg = new Regex(pattern);
                        var xxxxx = rg.Matches(picturesJson);
                        pictures = xxxxx.Cast<Match>().Select(x => x.Value).ToList();

                        json = json.Substring(0, picturesplace - 2) + "]";
                    }


                    if (json.Contains("tags"))
                    {
                        int picturesplace = json.IndexOf("tags");
                        json = json.Substring(0, picturesplace - 2) + "]";

                    }

                    if (json.Contains("points"))
                    {
                        int picturesplace = json.IndexOf("points");
                        json = json.Substring(0, picturesplace - 2) + "]";
                        points = true;
                    }

                    json = json.Replace("{", "");
                    json = json.Replace("}", "");
                    json = json.Replace("[", "{");
                    json = json.Replace("]", "}");

                    var item = JsonConvert.DeserializeObject<EndoMondoTraining>(json);
                    item.Pictures = pictures;
                    item.GPX = points;
                    trainings.Add(item);
                    yield return item;
                    Console.WriteLine($"{item.name}");
                }
            }
            // return trainings;
        }
    }
}
