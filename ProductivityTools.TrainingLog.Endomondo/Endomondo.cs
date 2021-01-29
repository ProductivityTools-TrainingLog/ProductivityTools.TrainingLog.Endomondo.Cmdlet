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
            var files = Directory.GetFiles(System.IO.Path.Combine(Path, "Workouts"), "*.json");
            foreach (var file in files)
            {
                List<string> pictures = new List<string>();
                bool points = false;
                using (StreamReader r = new StreamReader(file))
                {
                    string json = r.ReadToEnd();

                    if (json.Contains("points"))
                    {
                        int picturesplace = json.IndexOf("points");
                        json = json.Substring(0, picturesplace - 2) + "]";
                        points = true;
                    }
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

                    json = json.Replace("{", "");
                    json = json.Replace("}", "");
                    json = json.Replace("[", "{");
                    json = json.Replace("]", "}");

                    var item = JsonConvert.DeserializeObject<EndoMondoTraining>(json);
                    item.PicturesLinks = pictures;
                    trainings.Add(item);

                    LoadPictures(item);
                    if(points)
                    {
                        Console.WriteLine("Fdsa");
                        byte[] bytes = File.ReadAllBytes(file.Replace("json","gpx"));
                        item.Gpx = bytes;
                    }

                    yield return item;
                    Console.WriteLine($"{item.name}");
                }
            }
            // return trainings;
        }

        private void LoadPictures(EndoMondoTraining training)
        {
            training.Pictures = new List<byte[]>();
            foreach(var picture in training.PicturesLinks)
            {
                string path = System.IO.Path.Combine(this.Path, picture);
                byte[] bytes = File.ReadAllBytes(path);
                training.Pictures.Add(bytes);
            }
        }
    }
}
