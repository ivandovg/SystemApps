using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SystemApps9_1Dll
{
    [Serializable]
    public class User
    {
        private static int count = 0;
        public int Id { get; set; }
        public string Name { get; set; }

        public User()
        {
            Id = ++count;
        }
        public override string ToString()
        {
            return $"{Id}) {Name}";
        }
    }
}
