using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Extra.vMixApi
{
    public class vMixClient : IvMixClient
    {
        private readonly string _apiAddress = null;
        private readonly string _vmixStorage;

        public vMixClient(string vMixStoragePath, string ipAddress = "127.0.0.1", int port = 8088)
        {
            if (!Directory.Exists(vMixStoragePath))
                throw new DirectoryNotFoundException(vMixStoragePath);

            _vmixStorage = vMixStoragePath;
            _apiAddress = new UriBuilder("http", ipAddress, port, "api").Uri.ToString();
        }

        public async Task<vMixStatus> GetStatusAsync()
        {
            var vMixResponse = await InternalExecuteRequest(string.Empty, null);
            vMixStatus vMixStatus = null;
            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(vMixStatus), new XmlRootAttribute("vmix"));
                vMixStatus = (vMixStatus)serializer.Deserialize(new StringReader(vMixResponse));
            }
            catch (Exception err)
            {
                throw new vMixApiException(err.Message, err);
            }

            return vMixStatus;
        }

        public async Task<bool> GetRecordingStatusAsync()
        {
            var vMixStatus = await this.GetStatusAsync();
            return vMixStatus.Recording.IsActive.ToLower() == "true";
        }

        public async Task<string> StartRecordingAsync()
        {
            var isRecording = await this.GetRecordingStatusAsync();

            if (isRecording)
            {
                throw new vMixApiException("vMix recording is already in Started state");
            }

            var query = new NameValueCollection
            {
                { "Function", Functions.REC_START }
            };

            var files = Directory.GetFiles(_vmixStorage);
            var vMixResponse = await InternalExecuteRequest(string.Empty, query);

            string file = null;
            bool proceed = false;
            int retries = 0;
            while(!proceed && retries < 10)
            {
                retries++;
                file = Directory.GetFiles(_vmixStorage).Except(files).FirstOrDefault();
                if (string.IsNullOrWhiteSpace(file))
                    await Task.Delay(100);
                else
                    proceed = true;
            }
            System.Diagnostics.Debug.WriteLine(retries * 100);
            return file;
        }

        public async Task<string> StopRecordingAsync()
        {
            var isRecording = await this.GetRecordingStatusAsync();

            if (isRecording == false)
            {
                throw new vMixApiException("vMix recording is already in Stopped state");
            }

            var query = new NameValueCollection
            {
                { "Function", Functions.REC_STOP }
            };

            var vMixResponse = await InternalExecuteRequest(string.Empty, query);

            var fi = Directory.GetFiles(_vmixStorage).Select(file => new FileInfo(file)).OrderByDescending(file => file.LastWriteTime).FirstOrDefault();
            return fi?.FullName;
        }

        public async Task<string> GetSnapshotAsync(string destinationFileName, ImageFormat format = ImageFormat.Jpg)
        {
            var dt = Functions.SNAPSHOT + DateTime.Now.ToString("yyyyMMddHHmmss");

            var query = new NameValueCollection
            {
                { "Function", Functions.SNAPSHOT },
                { "Value", $"{dt}.{format.ToString()}" }
            };

            var files = Directory.GetFiles(_vmixStorage);
            var vMixResponse = await InternalExecuteRequest(string.Empty, query);

            string file = null;
            bool proceed = false;
            int retries = 0;
            while (!proceed && retries < 10)
            {
                retries++;
                file = Directory.GetFiles(_vmixStorage).Except(files).FirstOrDefault();
                if (string.IsNullOrWhiteSpace(file))
                    await Task.Delay(100);
                else
                    proceed = true;
            }

            if (!string.IsNullOrWhiteSpace(file) && Path.GetFileNameWithoutExtension(file) == dt)
            {
                var directory = new FileInfo(file).Directory.FullName;
                var destinationFileFullName = Path.Combine(directory, $"{Path.GetFileNameWithoutExtension(destinationFileName)}.{format.ToString().ToLower()}");
                File.Move(file, destinationFileFullName);

                file = destinationFileFullName;
            }

            System.Diagnostics.Debug.WriteLine(retries * 100);
            return file;
        }

        public async Task<string> GetSnapshotInputAsync(int inputNumber, string destinationFileName, ImageFormat format = ImageFormat.Jpg)
        {
            var dt = Functions.SNAPSHOT_INPUT + DateTime.Now.ToString("yyyyMMddHHmmss");

            var query = new NameValueCollection
            {
                { "Function", Functions.SNAPSHOT_INPUT },
                { "Value", $"{dt}.{format.ToString()}" },
                { "Input",  inputNumber.ToString()}
            };

            var files = Directory.GetFiles(_vmixStorage);
            var vMixResponse = await InternalExecuteRequest(string.Empty, query);

            string file = null;
            bool proceed = false;
            int retries = 0;
            while (!proceed && retries < 10)
            {
                retries++;
                file = Directory.GetFiles(_vmixStorage).Except(files).FirstOrDefault();
                if (string.IsNullOrWhiteSpace(file))
                    await Task.Delay(100);
                else
                    proceed = true;
            }

            if (!string.IsNullOrWhiteSpace(file) && Path.GetFileNameWithoutExtension(file) == dt)
            {
                var directory = new FileInfo(file).Directory.FullName;
                var destinationFileFullName = Path.Combine(directory, $"{Path.GetFileNameWithoutExtension(destinationFileName)}.{format.ToString().ToLower()}");
                File.Move(file, destinationFileFullName);

                file = destinationFileFullName;
            }

            System.Diagnostics.Debug.WriteLine(retries * 100);
            return file;
        }

        private async Task<string> InternalExecuteRequest(string resource, NameValueCollection queryString)
        {
            string response = null;
            try
            {
                var client = new vMixWebClient() { BaseAddress = _apiAddress, QueryString = queryString };
                response = await client.DownloadStringTaskAsync(resource);
            }
            catch (WebException wexc)
            {
                var error = new vMixApiException(wexc.Message, wexc);
                if (wexc.Response is HttpWebResponse webResponse)
                {
                    error.StatusCode = webResponse.StatusCode;
                    error.StatusDescription = webResponse.StatusDescription;
                }
                throw error;
            }
            catch (Exception exc)
            {
                throw new vMixApiException(exc.Message, exc);
            }

            if (string.IsNullOrWhiteSpace(response))
                throw new vMixApiException("Invalid response received");

            return response;
        }
    }
}
