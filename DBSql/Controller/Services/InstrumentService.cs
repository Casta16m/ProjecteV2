using Microsoft.EntityFrameworkCore;

namespace ProjecteV2.ApiSql.Services{
    public class InstrumentService{
        public DataContext _context { get; set; }
        public InstrumentService(DataContext context){
            _context = context;
        }
        public async Task<List<Instrument>> GetInstrument(string nom)
        {
            var instrument = await _context.Instrument.Where(a => a.Nom.Contains(nom)).ToListAsync();

            if (instrument == null)
            {
                return null;
            }

            return instrument;
        }
        public async Task<string> PostInstrument(Instrument instrument)
        {
            _context.Instrument.Add(instrument);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (InstrumentExists(instrument.Nom))
                {
                    return null;
                }
                else
                {
                    throw;
                }
            }

            return "Okay";
        }
        public async Task <Instrument> PutInstrument(Instrument instrument){
            _context.Entry(instrument).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!InstrumentExists(instrument.Nom))
                {
                    return null;
                }
                else
                {
                    throw;
                }
            }
            return instrument;
        }
        public bool InstrumentExists(string id)
        {
            return _context.Instrument.Any(e => e.Nom == id);
        }
    }
}