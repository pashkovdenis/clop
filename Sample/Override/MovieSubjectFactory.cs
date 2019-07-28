using ClopClustering.Default.Attributes;
using ClopClustering.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Sample.Override
{
    internal static  class MovieSubjectFactory
    { 
        public static async  Task<IReadOnlyCollection<Subject<string>>> ParseMoviesAsync (string file)
        {
            var lines = await File.ReadAllLinesAsync(file);  
            var subjects = new List<Subject<string>>(lines.Count());  

            foreach (var line in lines )
            {
                string[] entry = Regex.Split(line, "#");
                var attributes = entry.Select(x => new SubjectAttribute(x)).ToList();
                attributes.Add(new SubjectAttribute( SplitKey( entry[6]), isKey: true));
                var subject = new Subject<string>(entry[1], attributes) ;  
                subjects.Add(subject); 
            }
             
            return subjects.AsReadOnly();
        }

        private static string SplitKey (string key)
        {
            var str = key.Split(new string[] { ".", ",", " ", "-", ";", ":" }, StringSplitOptions.RemoveEmptyEntries);
            return str[0];
        }
         
    }
}
