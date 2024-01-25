using DBSql.Controller;
using Microsoft.EntityFrameworkCore;

namespace ProjecteV2.ApiSql.Services{
    public class ParticipaService{
        public DataContext _context { get; set; }
        public ParticipaService(DataContext context){
            _context = context;
        }
        public async Task <List<Participa>> GetParticipa(string UID){
            var song = await _context.Participa.Include(a => a.SongObj).Where(a => a.SongObj.UID == UID).ToListAsync();

            if (song == null)
            {
                return null;
            }
            
            return song;
        }
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
                if (ParticipaExists(participa.UID))
                {
                    return null;
                }
                if(!artistaController.ArtistaExists(participa.NomArtista))
                {
                    return null;
                }if(!grupController.GrupExists(participa.NomGrup))
                {
                    return null;
                }if(!instrumentController.InstrumentExists(participa.NomInstrument)){
                    return null;
                }
                else
                {
                    throw;
                }
            }
            return participa;      
        }
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
        private bool ParticipaExists(string id)
        {
            return _context.Participa.Any(e => e.UID == id);
        }
    }
}