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
        public bool ArtistaExists(string id)
        {
            return _context.Artistes.Any(e => e.NomArtista == id);
        }
    }
}