/*using Microsoft.EntityFrameworkCore;

namespace ProjecteV2.ApiSql.Services{
    public class ExtensioService{
        public DataContext _context { get; set; }
        public ExtensioService(DataContext context){
            _context = context;
        }
    public async Task<Extensio> PostExtensio(Extensio extensio){
            _context.Extensio.Add(extensio);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (ExtensioExists(extensio.NomExtensio))
                {
                    return null;
                }
                else
                {
                    throw;
                }
            }

            return extensio;

        }
        public async Task<Extensio> PutExtensio(Extensio extensio){
            _context.Entry(extensio).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ExtensioExists(extensio.NomExtensio))
                {
                    return null;
                }
                else
                {
                    throw;
                }
            }

            return extensio;
        }
        public async Task<Extensio> DeleteExtensio(string id){
            var extensio = await _context.Extensio.FindAsync(id);
            if (extensio == null)
            {
                return null;
            }

            _context.Extensio.Remove(extensio);
            await _context.SaveChangesAsync();

            return extensio;
        }
        private bool ExtensioExists(string id)
        {
            return _context.Extensio.Any(e => e.NomExtensio == id);
    }
    }
}*/