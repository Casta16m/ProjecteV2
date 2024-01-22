using Microsoft.EntityFrameworkCore;

namespace ProjecteV2.ApiSql.Services{
    public class AlbumService{
        public DataContext _context { get; set; }
        public AlbumService(DataContext context){
            _context = context;
        }
        public async Task <List<Album>> GetAlbum(string Nom){
            var song = await _context.Album.Include(a => a.SongObj).Where(a => a.NomAlbum.Contains(Nom)).ToListAsync();

            if (song == null)
            {
                return null;
            }
            
            return song;
        }

    }
}