using Microsoft.EntityFrameworkCore;

namespace ProjecteV2.ApiSql{
    /// <summary>
    /// Servei de la llista
    /// </summary>
    public class LlistaService{
        public DataContext _context { get; set; }
        public LlistaService(DataContext context){
            _context = context;
        }
        /// <summary>
        /// Busca totes les llistes
        /// </summary>
        /// <param name="Nom"></param>
        /// <returns></returns>
        public async Task <List<Llista>> GetLlista(string Nom){
            var song = await _context.Llista.Include(a => a.songs).Where(a => a.Nom.Contains(Nom)).ToListAsync();

            if (song == null)
            {
                return null;
            }
            
            return song;
        } 
        /// <summary>
        /// Busca una llista per la seva ID_MAC
        /// </summary>
        /// <param name="ID_MAC"></param>
        /// <returns></returns>
        public async Task <List<Llista>> GetLlistaMac(string ID_MAC){
            var song = await _context.Llista.Where(a => a.ID_MAC.Contains(ID_MAC)).ToListAsync();

            if (song == null)
            {
                return null;
            }
            
            return song;
        }
        /// <summary>
        /// Busca una llista per la seva ID_MAC
        /// </summary>
        /// <param name="llista"></param>
        /// <returns></returns>
        public async Task <Llista> PostLlista(Llista llista){


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
        /// <summary>
        /// Busca una llista per la seva ID_MAC
        /// </summary>
        /// <param name="NomLlista"></param>
        /// <param name="UID"></param>
        /// <param name="ID_MAC"></param>
        /// <returns></returns>
        public async Task <string> PutLlista(string NomLlista, string UID, string ID_MAC)
        {
         var llista = await _context.Llista.Include(a => a.songs).FirstOrDefaultAsync(a => a.Nom == NomLlista);
        
        if (llista == null)
        {
            return "no existeix la llista";
        }

        var ID_MAC1 = await _context.Llista.FirstOrDefaultAsync(a => a.ID_MAC == ID_MAC);
        if (ID_MAC1 == null)
        {
            return "no existeix la ID_MAC";
        }

        var song = await _context.Songs.FirstOrDefaultAsync(a => a.UID == UID);
        if (song == null)
        {
            return "no existeix la canço";
        }
        if(llista.ID_MAC == ID_MAC1.ID_MAC)
        {
            llista.songs.Add(song);
            _context.Entry(llista).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return "okay";
        }
        else
        {
            return null;
        }
        }
        /// <summary>
        /// Modifica una llista general
        /// </summary>
        /// <param name="llista"></param>
        /// <returns></returns>
        public async Task <Llista> PutLlistaGeneral(Llista llista){
            _context.Entry(llista).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LlistaExists(llista.Nom))
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
        /// <summary>
        /// Elimina una canço de la llista
        /// </summary>
        /// <param name="NomLlista"></param>
        /// <param name="MAC"></param>
        /// <param name="UID"></param>
        /// <returns></returns>
        public async Task <string> DeleteLlistaSong(string NomLlista, string MAC, string UID)
        {
            var llista = await _context.Llista.Include(a => a.songs).Where(a => a.Nom.Contains(NomLlista)).Where(a=> a.ID_MAC.Contains(MAC)).FirstOrDefaultAsync();
            if (llista == null)
            {
                return "no existeix la llista";
            }
            var song = await _context.Songs.FirstOrDefaultAsync(a => a.UID == UID);
            if (song == null)
            {
                return "no existeix la canço";
            }
            if(llista.ID_MAC == MAC)
            {
                llista.songs.Remove(song);
                _context.Entry(llista).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return "okay";
            }
            else
            {
                return null;
            }

        }
        /// <summary>
        /// Verificar si la llista existeix
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        private bool LlistaExists(string id)
        {
            return _context.Llista.Any(e => e.Nom == id);
        }
          
    }
}