using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerProgram.Models
{
    public class Album
    {
        public string? NomAlbum { get; set; }
        public string? data { get; set; }
        public string? UIDSong { get; set; }
        public string? SongObj { get; set; }
    }

    public class Grups
    {
        public string? NomGrup { get; set; }
        public object? artistes { get; set; }
        public object? participa { get; set; }
    }

    public class Artist
    {
        public string? NomArtista { get; set; }
        public int AnyNaixement { get; set; }
        public object? grups { get; set; }
        public object? participa { get; set; }
    }

    public class Instrument
    {
        public string? Nom { get; set; }
        public string? Model { get; set; }
        public object? participa { get; set; }

    }

    public class Songs
    {
        public string UID { get; set; }
        public DateTime? data { get; set; }
        public string? NomSong { get; set; }
        public object? SongObj { get; set; }
        public string? SongOriginal { get; set; }
        public string? Genere { get; set; }
        public object? album { get; set; }
        public object? llista { get; set; }
        public object? participa { get; set; }
        public object? extensio { get; set; }
        public object? songs { get; set; }
    }

    public class Llista
    {
        public string Nom { get; set; }
        public string ID_MAC { get; set; }
        public List<object> songs { get; set; }
    }
    public class Grup
    {
        public string NomGrup { get; set; }

    }
    public class Historial
    {
        public string _ID { get; set; }
        public string mac { get; set; }
        public DateTime data { get; set; }

        public string OID { get; set; }
    }
}
