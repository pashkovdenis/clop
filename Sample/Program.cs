using ClopClustering.Default;
using Sample.Override;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Sample
{
    internal static class Program
    { 
  
        static async Task Main(string[] args)
        {
            Console.WriteLine(" Clustering Example ");

            var movieList = await MovieSubjectFactory
                                .ParseMoviesAsync(Path.Combine(AppContext.BaseDirectory, "movies.txt"));
             
            var factory = new ClusterFactory<string>(movieList, new SubjectComparer()); 




            Console.ReadLine();
        }



    }
}
