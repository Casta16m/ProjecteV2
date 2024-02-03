using DBSql.Controller;
using Microsoft.EntityFrameworkCore;

namespace ProjecteV2.ApiSql.Services{
    /// <summary>
    /// Servei de la participa
    /// </summary>
    public class ParticipaService{
        public DataContext _context { get; set; }
        public ParticipaService(DataContext context){
            _context = context;
        }
        /// <summary>
        /// Busca tot el que conté la taula participa
        /// </summary>
        /// <param name="UID"></param>
        /// <returns></returns>
        public async Task <List<Participa>> GetParticipa(string UID){
            var song = await _context.Participa.Include(a => a.SongObj).Where(a => a.SongObj.UID == UID).ToListAsync();

            if (song == null)
            {
                return null;
            }
            
            return song;
        }
        /// <summary>
        /// Busca una participacio per la seva ID_MAC
        /// </summary>
        /// <param name="participa"></param>
        /// <returns></returns>
         public async Task <Participa> PutParticipaGeneral(Participa participa){
            _context.Entry(participa).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ParticipaExists(participa.UID))
                {
                    return null;
                }
                else
                {
                    throw;
                }
            }
            return participa;
        } 
        /// <summary>
        /// Busca una participacio per la seva ID_MAC
        /// </summary>
        /// <param name="participa"></param>
        /// <returns></returns>
        public async Task <Participa> PostParticipa(Participa participa){
            ArtistaService artistaController = new ArtistaService(_context);
            GrupService grupController = new GrupService(_context);
            InstrumentService instrumentController = new InstrumentService(_context);


            _context.Participa.Add(participa);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (!ParticipaExists(participa.UID) || 
                    !artistaController.ArtistaExists(participa.NomArtista) || 
                    !grupController.GrupExists(participa.NomGrup) || 
                    !instrumentController.InstrumentExists(participa.NomInstrument))
                {
                    return null;
                }
                else
                {
                    throw;
                }
            }
            return participa;      
        }
        /// <summary>
        /// Elimina un participa
        /// </summary>
        /// <param name="UID"></param>
        /// <returns></returns>
        public async Task <string> DeleteParticipa(string UID)
        {
            var participa = await _context.Participa.FindAsync(UID);
            if (participa == null)
            {
                return "no existeix la participacio";
            }

            _context.Participa.Remove(participa);
            await _context.SaveChangesAsync();

            return "okay";
        }
        /// <summary>
        /// Verifica si el Participa existeix
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        private bool ParticipaExists(string id)
        {
            return _context.Participa.Any(e => e.UID == id);
        }
    }
}