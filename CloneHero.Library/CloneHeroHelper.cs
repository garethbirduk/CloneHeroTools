using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloneHeroLibrary
{
    public class CloneHeroHelper
    {
        public CloneHeroHelper(IConfiguration configuration)
        {
            Configuration = configuration;

            RootPath = Configuration.GetSection("CloneHero")["Directory"];
            Paths = Directory.GetFiles(RootPath, "*.ini", SearchOption.AllDirectories)
                .ToList();
            Paths.RemoveAll(x => x.Contains("desktop.ini"));
            GetGenres();
        }

        public async Task RunAsync()
        {
            RemoveGenres();
        }

        public List<string> BadGenres()
        {
            return new List<string>()
            {
                "Doom Metal",
                "Avant-Garde Black Metal",
                "Avant-Garde Metal",
                "Black Metal",
                "Blackened Death Metal",
                "Blackened Ska",
                "Blackgaze",
                "Broken Beat",
                "Brutal Death Metal",
                "Brutal Deathcore",
                "Death Metal",
                "Deathcore",
                "Deathgrind",
                "Doom Death",
                "Doom Metal",
                "Extreme Metal",
                "Fingerstyle",
                "Finnish Dragon Power Metal",
                "Funk Metal",
                "Mathcore/grind/whatevercore",
                "Melodic Death Metal",
                "Melodic Deathcore",
                "Nu Metal",
                "Nu-Metal",
                "Progressive Death Metal",
                "Progressive Deathcore",
                "R&B",
                "R&B/Pop",
                "Rap",
                "Rap Metal",
                "Rap Rock",
                "Shred",
                "Ska",
                "Ska Punk",
                "Slash Core",
                "SID Metal",
                "Space Rock",
                "Speed Metal",
                "Speedcore",
                "Symphonic Black Metal",
                "Symphonic Death Metal",
                "Symphonic Metal",
                "Symphonic Power Metal",
                "Technical Brutal Death Metal",
                "Technical Death Metal",
                "Technical Deathcore",
                "Zoidberg",
                "fuck this",
                "Industrial",
                "Industrial Hardcore",
                "Industrial Metal",
                "Mathcore",
                "Math core",
                "Metal",
                "Metalcore",
                "Melodic Metalcore"
            };
        }
        public List<string> Genres = new List<string>();
        public string RootPath { get; }
        public List<string> Paths { get; private set; }
        public IConfiguration Configuration { get; }

        public void RemoveGenres()
        {
            var list = new List<string>();
            var paths = Paths.Where(x => BadGenres().Contains(GetGenre(x)));
            foreach (var path in paths)
                RemovePath(path);
        }

        private void RemovePath(string path)
        {
            var movePath = Path.Combine(RootPath, "..", "removed");
            var source = Path.GetDirectoryName(path);
            var local = source.Replace(RootPath, "");
            var destination = movePath + local;
            if (!Directory.Exists(destination))
                Directory.CreateDirectory(destination);
            foreach (var file in Directory.GetFiles(source))
                File.Move(file, Path.Combine(destination, Path.GetFileName(file)));   
        }

        internal void GetGenres()
        {
            Genres.Clear();
            foreach (var path in Paths)
            {
                Genres.Add(GetGenre(path));
            }
            Genres = Genres.Distinct().OrderBy(x => x).ToList();
            foreach (var genre in Genres)
                Console.WriteLine(genre);
        }

        private string GetGenre(string path)
        {
            var lines = File.ReadAllLines(path).ToList();
            var genre = lines.Where(x => x.Replace(" ", "").ToLowerInvariant().Contains($"genre=")).Single();
            genre = genre.Replace("genre = ", "").Replace("genre=", "");
            return genre;
        }
    }
}
