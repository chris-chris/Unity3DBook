using System;

namespace unity3d.Models
{
    public class RankData
    {
        public Int64 Rank { get; set; }
        public Int64 UserID { get; set; }
        public string FacebookID { get; set; }
        public string FacebookName { get; set; }
        public string FacebookPhotoURL { get; set; }
        public int Point { get; set; }
    }
}