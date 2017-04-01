using System;

namespace DotnetCoreServer.Models
{
    public class User
    {

        public long UserID { get; set; }
        public string FacebookID { get; set; }
        public string FacebookName { get; set; }
        public string FacebookPhotoURL { get; set; }
        public string FacebookAccessToken { get; set; }
        public int Point { get; set;}
        public string AccessToken { get; set; }
        public DateTime CreatedAt { get; set; }
        public int Diamond;
        public int Health;
        public int Defense;
        public int Damage;
        public int Speed;
        public int HealthLevel;
        public int DefenseLevel;
        public int DamageLevel;
        public int SpeedLevel;
        public int Level;
        public int Experience;
        
/*
diamond, health, defense, damage,
                        speed, health_level, defense_level, 
                        damage_level, speed_level,
                        level, experience, deleted
 */

    }
}