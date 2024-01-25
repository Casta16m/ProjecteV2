using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace ProjecteV2.ApiSql.Services{
    public class AlbumService{
        public DataContext _context { get; set; }
        public AlbumService(DataContext context){
            _context = context;
        }

        public async Task <List<Album>> GetAlbum(string Nom, DateTime data){
            var song = await _context.Album.Include(a => a.SongObj).Where(a => a.NomAlbum.Contains(Nom)).Where(a=> a.data == data).ToListAsync();

            if (song == null)
            {
                return null;
            }
            
            return song;
        }
        public async Task <Album> PostAlbum(Album album){
                      _context.Album.Add(album);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (AlbumExists(album.NomAlbum))
                {
                    return null;
                }
                else
                {
                    throw;
                }
            }

            return album;
        }
        public async Task <Album> PutAlbum(Album album){
            _context.Entry(album).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AlbumExists(album.NomAlbum))
                {
                    return null;
                }
                else
                {
                    throw;
                }
            }

            return album;
        }
        public async Task<string> AfegirSongAlbum(string NomAlbum, DateTime data,string UID){
            Album album1 = new Album();
            var album = await _context.Album.Include(a => a.SongObj).Where(a => a.NomAlbum.Contains(NomAlbum)).Where(a=> a.data == data).FirstOrDefaultAsync();
            var song = await _context.Songs.FindAsync(UID);
            if(album == null){
                return "No existeix l'album";
            }
            if(song == null){
                return "No existeix la canço";
            }
            album1.UIDSong = UID;
            album1.NomAlbum = NomAlbum;
            album1.data = data;
            _context.Album.Add(album1);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AlbumExists(album.NomAlbum))
                {
                    return "No existeix l'album";
                }
                else
                {
                    throw;
                }
            }

            return "album afegit";
        }
        public async Task <Album> DeleteAlbum(string id){
            var album = await _context.Album.FindAsync(id);
            if (album == null)
            {
                return null;
            }

            _context.Album.Remove(album);
            await _context.SaveChangesAsync();

            return album;
        }
        private bool AlbumExists(string id)
        {
            return _context.Album.Any(e => e.NomAlbum == id);
        }
    }
}