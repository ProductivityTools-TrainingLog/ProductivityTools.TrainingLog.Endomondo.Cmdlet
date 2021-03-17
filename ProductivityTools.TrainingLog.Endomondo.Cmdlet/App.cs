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
        private string Account { get; set; }
        public string TrainingLogApiAddress { get; }

        public App(string path, string account, string trainingLogApiAddress)
        {
            this.Path = path;
            this.Account = account;
            this.TrainingLogApiAddress = trainingLogApiAddress;
            this.Endomondo = new Endomondo(this.Path);
        }

        public void Import()
        {
            foreach (var endomondoTraining in this.Endomondo.GetEndomondoTrainings())
            {
                Training training = MapTraining(endomondoTraining);
                PostTraining(training);
            }
        }

        private Training MapTraining(EndoMondoTraining endomondoTraining)
        {
            Training training = new Training();
            training.Application = "Endomondo";
            training.Account = this.Account;

            training.Sport = GetSport(endomondoTraining.sport);

            training.Name = endomondoTraining.name;
            training.Source = endomondoTraining.source;
            if (training.Source==null)
            {
                if (endomondoTraining.Gpx!=null)
                {
                    training.Source = "Imported";
                }
                else
                {
                    training.Source = "INPUT_MANUAL";
                }
            }
            training.Duration = (int)endomondoTraining.duration_s;
            training.Start = DateTime.Parse(endomondoTraining.start_time);
            training.End = DateTime.Parse(endomondoTraining.end_time);
            training.Distance = endomondoTraining.distance_km;
            training.Calories = endomondoTraining.calories_kcal;
            training.AverageSpeed = endomondoTraining.speed_avg_kmh;
            training.Pictures = endomondoTraining.Pictures;
            training.Gpx = endomondoTraining.Gpx;
            return training;
        }

        private TrainingType GetSport(string sport)
        {
            Dictionary<string, TrainingType> mapper = new Dictionary<string, TrainingType>();
            mapper.Add("ROWING_INDOOR", TrainingType.Rowing);
            mapper.Add("BADMINTON", TrainingType.Badminton);
            mapper.Add("ROLLER_SKIING", TrainingType.RollerSkating);
            mapper.Add("CYCLING_SPORT", TrainingType.Cycling);
            mapper.Add("STAIR_CLIMBING", TrainingType.StairClimbing);
            mapper.Add("MOUNTAIN_BIKING", TrainingType.MountainBiking);
            mapper.Add("ICE_SKATING", TrainingType.IceSkating);
            mapper.Add("YOGA", TrainingType.Yoga);
            mapper.Add("KAYAKING", TrainingType.Kayaking);
            mapper.Add("OTHER", TrainingType.NotKnown);
            mapper.Add("WALKING", TrainingType.Walking);
            mapper.Add("FITNESS_WALKING", TrainingType.NordicWalking);
            mapper.Add("ROLLER_SKATING", TrainingType.RollerSkating);
            mapper.Add("DANCING", TrainingType.Dancing);
            mapper.Add("GYMNASTICS", TrainingType.Fitness);
            mapper.Add("ORIENTEERING", TrainingType.Orienteering);
            mapper.Add("SQUASH", TrainingType.Squash);
            mapper.Add("HIKING", TrainingType.Hiking);
            mapper.Add("TABLE_TENNIS", TrainingType.TableTennis);
            mapper.Add("SKIING_CROSS_COUNTRY", TrainingType.CrossCountrySkiing);
            mapper.Add("MARTIAL_ARTS", TrainingType.MuayThai);
            mapper.Add("SWIMMING", TrainingType.Swimming);
            mapper.Add("SOCCER", TrainingType.Soccer);
            mapper.Add("ROWING", TrainingType.Rowing);
            mapper.Add("SKIING_DOWNHILL", TrainingType.SkiingDownhill);
            mapper.Add("CROSS_TRAINING", TrainingType.CrossTraining);
            mapper.Add("RIDING", TrainingType.Riding);
            mapper.Add("RUNNING", TrainingType.Running);
            mapper.Add("CLIMBING", TrainingType.Climbing);
            mapper.Add("CYCLING_TRANSPORTATION", TrainingType.Cycling);
            mapper.Add("WEIGHT_TRAINING", TrainingType.WeightTraining);
            mapper.Add("SKATEBOARDING", TrainingType.Skateboarding);
            mapper.Add("TENNIS", TrainingType.Tennis);
            mapper.Add("AEROBICS", TrainingType.Fitness);
            mapper.Add("ROPE_JUMPING", TrainingType.RopeJumping);
            mapper.Add("RUNNING_TRAIL", TrainingType.TrailRunning);
            mapper.Add("TREADMILL_RUNNING", TrainingType.TradmillRunning);
            mapper.Add("PILATES", TrainingType.Pilates);
            mapper.Add("STRETCHING", TrainingType.Stretching);
            mapper.Add("SURFING", TrainingType.Surfing);

            var r = mapper[sport];
            return r;
        }

        private void PostTraining(Training training)
        {
            HttpPostClient client = new HttpPostClient(true);
            client.SetBaseUrl(this.TrainingLogApiAddress);

            var result2 = client.PostAsync<object>("Training", "Add", training).Result;
            Console.WriteLine(result2);
        }
    }
}
