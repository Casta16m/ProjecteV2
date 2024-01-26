using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace ProjecteV2.ApiSql.Services{
    /// <summary>
    /// Servei de la canço
    /// </summary>
    public class SongService{
        public DataContext _context { get; set; }
        public SongService(DataContext context){
            _context = context;
        }
        /// <summary>
        /// Busca totes les cançons
        /// </summary>
        /// <param name="UID"></param>
        /// <returns></returns>
        public async Task <List<Song>> GetSongEspecifica(string UID){
            var song = await _context.Songs.Include(a => a.album).Include(a => a.extensio).Where(a => a.UID == UID).ToListAsync();

            if (song == null)
            {
                return null;
            }
            
            return song;
        }
        /// <summary>
        /// Busca una canço per el seu nom
        /// </summary>
        /// <param name="nom"></param>
        /// <returns></returns>
        public async Task<List<Song>> GetSong(string nom)
        {
            var song = await _context.Songs.Include(a=> a.album).Include(a=> a.extensio).Where(a => a.NomSong.Contains(nom)).ToListAsync();

            if (song == null)
            {
                return null;
            }
            
            return song;
        }
        /// <summary>
        /// Busca una canço per la seva ID_MAC
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Song> GetSongWithList(string id)
        {
            var song = await _context.Songs.FirstOrDefaultAsync(a => a.UID == id);

            if (song == null)
            {
                return null;
            }
            
            return song;
        }
        /// <summary>
        /// Busca una canço per el seu genere
        /// </summary>
        /// <param name="Genere"></param>
        /// <returns></returns>
        public async Task<List<Song>>GetGenere(string Genere)
        {
            var song = await _context.Songs.Where(a => a.Genere.Contains(Genere)).ToListAsync();

            if (song == null)
            {
                return null;
            }

            return song;
        }
        /// <summary>
        /// Crea una canço
        /// </summary>
        /// <param name="song"></param>
        /// <param name="NomExtensio"></param>
        /// <returns></returns>
        public async Task<Song>PostSong(Song song, string NomExtensio){
            
            Extensio extensio = new Extensio();
            var extensio2 = await _context.Extensio.FindAsync(NomExtensio);
            extensio.NomExtensio = NomExtensio;
            if(extensio2 == null){
                _context.Extensio.Add(extensio);
                extensio.songs.Add(song);
                song.extensio.Add(extensio);
            }
            else{
                extensio2.songs.Add(song);
                song.extensio.Add(extensio2);
            }
            try
            {
                song.data = DateTime.Now;
                _context.Songs.Add(song);
            }
            catch (DbUpdateException)
            {
                if (SongExists(song.UID))
                {
                    await _context.SaveChangesAsync();
                    return null;
                }
                else if (!ExtensioExists(extensio2.NomExtensio))
                {
                    _context.Songs.Add(song);

                    await _context.SaveChangesAsync();
                    return song;
                }
                else
                {
                    throw;
                }
            }
            await _context.SaveChangesAsync();

            return song;

        }
        /// <summary>
        /// Modifica una canço
        /// </summary>
        /// <param name="song"></param>
        /// <returns></returns>
        public async Task<string> PutSong(Song song){
            _context.Entry(song).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SongExists(song.UID))
                {
                    return "No existeix";
                }
                else
                {
                    throw;
                }
            }

            return "Modificat";
        }  
        /// <summary>
        /// Elimina una canço
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<string> DeleteSong(string id)
        {
            var song = await _context.Songs.FindAsync(id);
            if (song == null)
            {
                return "No existeix";
            }

            _context.Songs.Remove(song);
            await _context.SaveChangesAsync();

            return "Eliminat";
        }  

        /// <summary>
        /// Verifica si la song existeix
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        private bool SongExists(string id)
        {
            return _context.Songs.Any(e => e.UID == id);
        }
        /// <summary>
        /// Verifica si la extensio existeix
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        private bool ExtensioExists(string id)
        {
            return _context.Extensio.Any(e => e.NomExtensio == id);
        }
        /// <summary>
        /// Verifica si la extensio existeix dins una llista de extensions
        /// </summary>
        /// <param name="extensions"></param>
        /// <param name="extensionName"></param>
        /// <returns></returns>
        public bool DoesExtensionExist(List<Extensio> extensions, string extensionName)
        {
            return extensions.Any(e => e.NomExtensio == extensionName);
        }
    }
}