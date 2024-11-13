using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VVSProjekat
{
    public class Category
    {
        public string Name { get; set; }
        public List<string> Tags { get; set; } = new List<string>();

        public Category(string name)
        {
            Name = name;
        }

        public void AddTag(string tag)
        {
            Tags.Add(tag);
        }
    }

}

