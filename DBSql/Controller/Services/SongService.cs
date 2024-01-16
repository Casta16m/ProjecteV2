namespace ProjecteV2.ApiSql.Services{
    public class SongService{

        //
        public async Task <Song> GetSong(string nom, DataContext _context)
        {
            var Nomsong = await _context.Songs.FindAsync(nom);
            return Nomsong;
        
        }
    }
}