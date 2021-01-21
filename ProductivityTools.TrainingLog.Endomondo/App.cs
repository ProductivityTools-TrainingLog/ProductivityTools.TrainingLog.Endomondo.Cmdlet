using ProductivityTools.SimpleHttpPostClient;
using ProductivityTools.TrainingLog.Contract;
using ProductivityTools.TrainingLog.Endomondo.Dto;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ProductivityTools.TrainingLog.Endomondo
{
    public class App
    {

        Endomondo Endomondo;

        public string Path { get; set; }

        public App(string path)
        {
            this.Path = path;
            this.Endomondo = new Endomondo(this.Path);
        }

        public void Import()
        {
            //List<EndoMondoTraining> endomondoTrainings = GetEndomondoTrainings();
            foreach (var endomondoTraining in this.Endomondo.GetEndomondoTrainings())
            {
                Training training = MapTraining(endomondoTraining);
                PostTraining(training);
            }
        }

        private Training MapTraining(EndoMondoTraining endomondoTraining)
        {
            Training training = new Training();
            training.Sport = endomondoTraining.sport;

            //training.Description = endomondoTraining.name;
            training.Duration = TimeSpan.FromSeconds(endomondoTraining.duration_s);
            training.Start = DateTime.Parse(endomondoTraining.start_time);
            training.End = DateTime.Parse(endomondoTraining.end_time);
            training.Distance = endomondoTraining.distance_km * 1000;
            training.Calories = endomondoTraining.calories_kcal;
            training.AverageSpeed = endomondoTraining.speed_avg_kmh;
            //string s = @"c:\Users\pwujczyk\Desktop\Pamela.jpg";
            //byte[] bytes = File.ReadAllBytes(s);

            return training;
        }


        private void PostTraining(Training training)
        {
            HttpPostClient client = new HttpPostClient(true);
            client.SetBaseUrl("https://localhost:5001");

            var result2 = client.PostAsync<object>("Training", "Add", training).Result;
        }

    }
}
