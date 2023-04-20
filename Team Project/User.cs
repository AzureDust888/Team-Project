using Nest;
using System;
using System.Collections.Generic;
using System.Data.Linq.Mapping;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Team_Project
{
    [Table(Name = "Users")]
    public class User
    {
        [Column(IsPrimaryKey = true, IsDbGenerated = true, Name = "Id")]
        public int Id { get; set; }
        [Column]
        public string? UserName { get; set; }
        [Column]
        public string? Password { get; set; }
        [Column]
        public byte[]? Avatar { get; set; }

        [Column]
        public string? CharacterName { get; set; }
        [Column]
        public string? WeaponName { get; set; }
        [Column]
        public double CurrentHp { get; set; }
        [Column]
        public double CurrentMp { get; set; }
        [Column]
        public double MaxHp { get; set; }
        [Column]
        public double MaxMp { get; set; }
        [Column]
        public int Lvl { get; set; }
        [Column]
        public double CurrentExp { get; set; }

        [Column]
        public double BackBorderLeft { get; set; }
        [Column]
        public double BackBorderTop { get; set; }
        [Column]
        public double BTBorderLeft { get; set; }
        [Column]
        public double BTBorderTop { get; set; }

        public User() { }

        public User(string? userName, string? password)
        {
            UserName = userName;
            Password = password;
            Avatar = File.ReadAllBytes(Dir.GetPathX() + "\\Resources\\Avatar.png");
            CharacterName = userName;
            WeaponName = "Base_Sword";
            CurrentHp = 100;
            CurrentMp = 100;
            MaxHp = 200;
            MaxMp = 200;
            Lvl = 1;
            CurrentExp = 0;
            BackBorderLeft = 2510;
            BackBorderTop = 2265;
            BTBorderLeft = 0;
            BTBorderTop = 0;
        }

        public User(string? userName, string? password, byte[]? avatar, string? weaponName, double currentHp, double currentMp, double maxHp, double maxMp, int lvl, double currentExp)
        {
            UserName = userName;
            Password = password;
            Avatar = avatar;
            CharacterName = userName;
            WeaponName = weaponName;
            CurrentHp = currentHp;
            CurrentMp = currentMp;
            MaxHp = maxHp;
            MaxMp = maxMp;
            Lvl = lvl;
            CurrentExp = currentExp;
        }
    }
}
