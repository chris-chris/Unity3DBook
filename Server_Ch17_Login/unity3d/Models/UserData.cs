
using System;
namespace unity3d.Models
{
    public class UserData
    {
        public Int64 UserID { get; set; }
        public string FacebookID { get; set; }
        public string FacebookName { get; set; }
        public string FacebookPhotoURL { get; set; }
        public int Point { get; set; }
        public int Diamond { get; set; }

        public int Experience { get; set; }
        public int ExpForNextLevel { get; set; }
        public int ExpAfterLastLevel { get; set; }
        public int Level { get; set; }

        public int Health { get; set; }
        public int Damage { get; set; }
        public int Speed { get; set; }
        public int Defense { get; set; }

        public int HealthLevel { get; set; }
        public int DamageLevel { get; set; }
        public int SpeedLevel { get; set; }
        public int DefenseLevel { get; set; }

        public string AccessToken { get; set; }
    }
}
