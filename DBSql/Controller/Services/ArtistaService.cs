using Microsoft.EntityFrameworkCore;

namespace ProjecteV2.ApiSql.Services{
    public class ArtistaService{
        public DataContext _context { get; set; }
        public ArtistaService(DataContext context){
            _context = context;
        }

        public async Task<List<Artista>> GetArtista(string nom)
        {
            var artista = await _context.Artistes.Where(a => a.NomArtista.Contains(nom)).ToListAsync();

            if (artista == null)
            {
                return null;
            }
            
            return artista;
        }
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
        public bool ArtistaExists(string id)
        {
            return _context.Artistes.Any(e => e.NomArtista == id);
        }
    }
}