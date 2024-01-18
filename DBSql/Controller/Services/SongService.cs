using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace ProjecteV2.ApiSql.Services{
    public class SongService{

        //
        public async Task<List<Song>> GetSong(string nom, DataContext _context)
        {
            var song = await _context.Songs.Where(a => a.NomSong.Contains(nom)).ToListAsync();

            if (song == null)
            {
                return null;
            }
            
            return song;
        }
        public async Task<List<Song>>GetGenere(string Genere, DataContext _context)
        {
            var song = await _context.Songs.Where(a => a.Genere.Contains(Genere)).ToListAsync();

            if (song == null)
            {
                return null;
            }

            return song;
        }
        public async Task<Song>PostSong(string id, Song song, DataContext _context){
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
        public async Task<Song>PutSong(string id, Song song, DataContext _context){
            _context.Entry(song).State = EntityState.Modified;

            if (id != song.UID)
            {
                return null;
            }

            _context.Entry(song).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SongExists(id, _context))
                {
                    return null;
                }
                else
                {
                    throw;
                }
            }

            return null;
        }
                
        private bool SongExists(string id, DataContext _context)
        {
            return _context.Songs.Any(e => e.UID == id);
        }
    }
}