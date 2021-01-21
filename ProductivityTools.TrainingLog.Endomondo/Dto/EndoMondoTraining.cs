using System;
using System.Collections.Generic;
using System.Text;

namespace ProductivityTools.TrainingLog.Endomondo.Dto
{
    public class EndoMondoTraining
    {
        public string name { get; set; }
        public string sport { get; set; }
        public string source { get; set; }
        public string created_date { get; set; }
        public string start_time { get; set; }
        public string end_time { get; set; }
        public float duration_s { get; set; }
        public float distance_km { get; set; }
        public float calories_kcal { get; set; }
        public float speed_avg_kmh { get; set; }

        public List<string> Pictures { get; set; }
        public bool GPX { get; set; }
    }
}
