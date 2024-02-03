using Microsoft.AspNetCore.Http;

namespace ProjecteV2.ApiMongoDB{
    /// <summary>
    /// Classe per a la col·lecció de songs
    /// </summary>
    public class SongUploadModel
    {
        public string Uid { get; set; }
        public IFormFile Audio { get; set; }
    }
}
