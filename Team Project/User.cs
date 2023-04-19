using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Team_Project
{
    public class User
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public byte[] Avatar { get; set; }

        public string CharacterName { get; set; }
        public double CurrentHp { get; set; }
        public double CurrentMp { get; set; }
        public double MaxHp { get; set; }
        public double MaxMp { get; set; }
        public int Lvl { get; set; }
        public double CurrentExp { get; set; }

    }
}
