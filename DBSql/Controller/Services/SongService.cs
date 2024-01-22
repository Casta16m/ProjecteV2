using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace ProjecteV2.ApiSql.Services{
    public class SongService{
        public DataContext _context { get; set; }
        public SongService(DataContext context){
            _context = context;
        }
        
        public async Task<List<Song>> GetSong(string nom)
        {
            var song = await _context.Songs.Include(a=> a.album).Where(a => a.NomSong.Contains(nom)).ToListAsync();

            if (song == null)
            {
                return null;
            }
            
            return song;
        }

public async Task<Song> GetSongWithList(string id)
        {
            var song = await _context.Songs.FirstOrDefaultAsync(a => a.UID == id);

            if (song == null)
            {
                return null;
            }
            
            return song;
        }
        
        public async Task<List<Song>>GetGenere(string Genere)
        {
            var song = await _context.Songs.Where(a => a.Genere.Contains(Genere)).ToListAsync();

            if (song == null)
            {
                return null;
            }

            return song;
        }
        public async Task<Song>PostSong(string id, Song song){
            song.data = DateTime.Now;
            _context.Songs.Add(song);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (SongExists(song.NomSong, _context))
                {
                    return null;
                }
                else
                {
                    throw;
                }
            }

            return song;

        }
        public async Task<string> PutSong(Song song){
            _context.Entry(song).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SongExists(song.UID, _context))
                {
                    return null;
                }
                else
                {
                    throw;
                }
            }

            return "Okay";
        }

                
        private bool SongExists(string id, DataContext _context)
        {
            return _context.Songs.Any(e => e.UID == id);
        }
    }
}