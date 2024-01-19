using Microsoft.EntityFrameworkCore;

namespace ProjecteV2.ApiSql{
    public class LlistaService{
        public async Task <List<Llista>> GetLlista(string Nom, DataContext _context){
            var song = await _context.Llista.Include(a => a.songs).Where(a => a.Nom.Contains(Nom)).ToListAsync();

            if (song == null)
            {
                return null;
            }
            
            return song;
        } 
        public async Task <List<Llista>> GetLlistaMac(string ID_MAC, DataContext _context){
            var song = await _context.Llista.Where(a => a.ID_MAC.Contains(ID_MAC)).ToListAsync();

            if (song == null)
            {
                return null;
            }
            
            return song;
        }
        public async Task <Llista> PostLlista(Llista llista, DataContext _context){


            _context.Llista.Add(llista);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
            if (LlistaExists(llista.Nom, _context))
                {
                    return null;
                }
                else
                {
                    throw;
                }

            }
            return llista;      
        }

        private bool LlistaExists(string id, DataContext _context)
        {
            return _context.Llista.Any(e => e.Nom == id);
        }
          
    }
}