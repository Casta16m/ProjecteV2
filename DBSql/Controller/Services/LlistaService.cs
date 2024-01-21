using Microsoft.EntityFrameworkCore;

namespace ProjecteV2.ApiSql{
    public class LlistaService{
        public DataContext _context { get; set; }
        public LlistaService(DataContext context){
            _context = context;
        }
        
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
            if (LlistaExists(llista.Nom))
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
        public async Task <Llista> PutLlista(string NomLlista, string UID, string ID_MAC)
        {
         var llista = await _context.Llista.Include(a => a.songs).FirstOrDefaultAsync(a => a.Nom == NomLlista);
        
        if (llista == null)
        {
            return null;
        }

        var ID_MAC1 = await _context.Llista.FirstOrDefaultAsync(a => a.ID_MAC == ID_MAC);
        if (ID_MAC1 == null)
        {
            return null;
        }

        var song = await _context.Songs.FirstOrDefaultAsync(a => a.UID == UID);
        if (song == null)
        {
            return null;
        }



        if(llista.ID_MAC == ID_MAC1.ID_MAC)
        {
            llista.songs.Add(song);
            _context.Entry(llista).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return llista;

        }
        else
        {
            return null;
        }
        }

        private bool LlistaExists(string id)
        {
            return _context.Llista.Any(e => e.Nom == id);
        }
          
    }
}