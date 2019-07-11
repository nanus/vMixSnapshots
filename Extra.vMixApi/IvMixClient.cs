using System.Threading.Tasks;

namespace Extra.vMixApi
{
    public interface IvMixClient
    {
        Task<vMixStatus> GetStatusAsync();
        Task<string> StartRecordingAsync();
        Task<string> StopRecordingAsync();
        Task<bool> GetRecordingStatusAsync();
        Task<string> GetSnapshotAsync(string destinationFileName, ImageFormat format);
        Task<string> GetSnapshotInputAsync(int inputNumber, string destinationFileName, ImageFormat format);
    }
}