using Microsoft.EntityFrameworkCore;

namespace ProjecteV2.ApiSql.Services{
    /// <summary>
    /// Servei de l'artista
    /// </summary>
    public class ArtistaService{
        public DataContext _context { get; set; }
        public ArtistaService(DataContext context){
            _context = context;
        }
        /// <summary>
        /// Busca tots els artistes que tinguin el nom que li passem
        /// </summary>
        /// <param name="nom"></param>
        /// <returns></returns>
        public async Task<List<Artista>> GetArtista(string nom)
        {
            var artista = await _context.Artistes.Where(a => a.NomArtista.Contains(nom)).ToListAsync();

            if (artista == null)
            {
                return null;
            }
            
            return artista;
        }
        /// <summary>
        /// Creem un artista
        /// </summary>
        /// <param name="artista"></param>
        /// <returns></returns>
        public async Task<string> PostArtista(Artista artista)
        {
            _context.Artistes.Add(artista);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (ArtistaExists(artista.NomArtista))
                {
                    return null;
                }
                else
                {
                    return null;
                }
            }

            return "Okay";
        }
        /// <summary>
        /// Modifiquem l'artista que li passem
        /// </summary>
        /// <param name="artista"></param>
        /// <returns></returns>
        public async Task<Artista> PutArtista(Artista artista){


            _context.Entry(artista).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ArtistaExists(artista.NomArtista))
                {
                    return null;
                }
                else
                {
                    throw;
                }
            }

            return artista;
        }
        /// <summary>
        /// Eliminem l'artista que li passem
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Artista> DeleteArtista(string id){
            var artista = await _context.Artistes.FindAsync(id);
            if (artista == null)
            {
                return null;
            }

            _context.Artistes.Remove(artista);
            await _context.SaveChangesAsync();

            return artista;
        }
        /// <summary>
        /// Verifiquem si l'artista existeix
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool ArtistaExists(string id)
        {
            return _context.Artistes.Any(e => e.NomArtista == id);
        }
    }
}