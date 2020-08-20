using ClopClustering.Default;
using Sample.Override;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Sample
{
    internal static class Program
    {
        static async Task Main()
        {
            var movieList = await MovieSubjectFactory
                               .ParseMoviesAsync(Path.Combine(AppContext.BaseDirectory, "movies.txt"));
                Console.Clear();  
                var factory = new ClusterFactory<string>(movieList, new SubjectComparer(), 10);
                factory.MakeClusters();

                foreach (var cluster in factory)
                {
                    Console.WriteLine(cluster.Description);
                    Console.WriteLine("--------------------------------"); 
                    foreach (var item in cluster.Subjects)
                    {
                        Console.WriteLine(item);
                    } 

                    Console.WriteLine("");
                    Console.WriteLine("");
                } 
                Console.ReadLine();
           
        }
    }
}
