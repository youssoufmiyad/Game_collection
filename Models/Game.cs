using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game_collection.Models
{
    public class Game
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Genre {  get; set; }
        public string Description { get; set; }
        public string Plateform { get; set; }
        public DateTime ReleaseDate { get; set; }
        public DateTime AcquisitionDate { get; set; }
        public double Price { get; set; }
        public double PriceResell { get; set; }
        public string Cover { get; set; }
        public string Collection { get; set; }

        public Game(string name, string genre, string description, string plateform, DateTime releaseDate, DateTime acquisitionDate, double price, double priceResell, string? cover)
        {
            Name = name;
            Genre = genre;
            Description = description;
            Plateform = plateform;
            ReleaseDate = releaseDate;
            AcquisitionDate = acquisitionDate;
            Price = price;
            PriceResell = priceResell;
            Cover = cover;
        }

        public Game(int id, string name, string genre, string description, string plateform, DateTime releaseDate, DateTime acquisitionDate, double price, double priceResell, string? cover)
        {
            Id = id;
            Name = name;
            Genre = genre;
            Description = description;
            Plateform = plateform;
            ReleaseDate = releaseDate;
            AcquisitionDate = acquisitionDate;
            Price = price;
            PriceResell = priceResell;
            Cover = cover;
        }

        public Game(int id, string name, string genre, string description, string plateform, DateTime releaseDate, DateTime acquisitionDate, double price, double priceResell, string? cover, string? collection)
        {
            Id = id;
            Name = name;
            Genre = genre;
            Description = description;
            Plateform = plateform;
            ReleaseDate = releaseDate;
            AcquisitionDate = acquisitionDate;
            Price = price;
            PriceResell = priceResell;
            Cover = cover;
            Collection = collection;
        }
    }
}
