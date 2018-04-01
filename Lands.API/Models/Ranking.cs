namespace Lands.API.Models
{
    using Domain;

    public class Ranking
    {
        public int RankingId
        {
            get;
            set;
        }

        public User User
        {
            get;
            set;
        }

        public Board Board
        {
            get;
            set;
        }

        public int Points
        {
            get;
            set;
        }
    }
}