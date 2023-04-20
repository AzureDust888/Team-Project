using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Team_Project
{
    public static class Dir
    {
        public static string GetPathX()
        {
            DirectoryInfo? dir = new DirectoryInfo(Directory.GetCurrentDirectory());
            dir = dir.Parent?.Parent?.Parent;
            return dir.FullName;
        }
    }
}
