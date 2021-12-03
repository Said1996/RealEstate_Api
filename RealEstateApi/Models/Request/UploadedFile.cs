namespace RealEstateApi.Models.Request
{
    public class UploadedFile
    {
        public string FileName { get; set; }
        public byte[] FileContent { get; set; }
    }
}
