using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace ProjecteV2.ApiSql.Services{
    public class GrupService{
        public DataContext _context { get; set; }
        public GrupService(DataContext context){
            _context = context;
        }
        public async Task<string> PutGrup(string NomGrup, string NomArtista)
        {
            
            var grup = await _context.Grups.Include(a => a.artistes).FirstOrDefaultAsync(a => a.NomGrup == NomGrup);
            if (grup == null)
            {
                return null;
            }
            var artista = await _context.Artistes.FirstOrDefaultAsync(a => a.NomArtista == NomArtista);
            if (artista == null)
            {
                return null;
            }
            grup.artistes.Add(artista);
            //artista.Grups.Add(grup);
            await _context.SaveChangesAsync();
            return "Okay";
        }
        public async Task<Grup> PostGrup(Grup grup)
        {
            _context.Grups.Add(grup);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (GrupExists(grup.NomGrup))
                {
                    return null;
                }
                else
                {
                    throw;
                }
            }

            return grup;
        }
        public bool GrupExists(string id)
        {
            return _context.Grups.Any(e => e.NomGrup == id);
        }
    }
}