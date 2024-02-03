using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.JsonPatch.Internal;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ProjecteV2.ApiSql.Services{
    /// <summary>
    /// Servei del grup
    /// </summary>
    public class GrupService{
        public DataContext _context { get; set; }
        public GrupService(DataContext context){
            _context = context;
        }
        /// <summary>
        /// Modifica el grup
        /// </summary>
        /// <param name="NomGrup"></param>
        /// <param name="NomArtista"></param>
        /// <returns></returns>
        public async Task<string> PutGrup(string NomGrup, string NomArtista)
        {
            
            var grup = await _context.Grups.Include(a => a.artistes).FirstOrDefaultAsync(a => a.NomGrup == NomGrup);
            if (grup == null)
            {
                return "artista no trobat";
            }
            var artista = await _context.Artistes.FirstOrDefaultAsync(a => a.NomArtista == NomArtista);
            if (artista == null)
            {
                return "grup no trobat";
            }
            grup.artistes.Add(artista);
            await _context.SaveChangesAsync();
            return "Okay";
        }
        /// <summary>
        /// Busca un grup per el seu nom
        /// </summary>
        /// <param name="NomGrup"></param>
        /// <returns></returns>
        public async Task<Grup> GetGrup(string NomGrup)
        {
            var grup = await _context.Grups.Include(a => a.artistes).Where(a => a.NomGrup == NomGrup).FirstOrDefaultAsync();
            
            return grup;
        }
        /// <summary>
        /// Crea un grup
        /// </summary>
        /// <param name="grup"></param>
        /// <returns></returns>
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
        /// <summary>
        /// Modifica un grup
        /// </summary>
        /// <param name="grup"></param>
        /// <returns></returns>
        public async Task<Grup> ModificarTotGrup(Grup grup){
            _context.Entry(grup).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return grup;
        }
        /// <summary>
        /// Verifica si un grup existeix
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool GrupExists(string id)
        {
            return _context.Grups.Any(e => e.NomGrup == id);
        }
    }
}