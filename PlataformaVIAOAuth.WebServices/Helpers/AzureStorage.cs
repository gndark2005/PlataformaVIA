
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using PlataformaVIA.Core.Domain.AdministradorDocumentos;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace PlataformaVIAOAuth.WebServices.Helpers
{
    public class AzureStorage
    {
        private static AzureStorage _instance;
        public static AzureStorage Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new AzureStorage();
                }
                return _instance;
            }
        }

        public async System.Threading.Tasks.Task<string> InsertIntoStorageBase64(string Data, string filename)
        {
            try
            {
                if (!string.IsNullOrEmpty(Data))
                {
                    byte[] _dataFile = Convert.FromBase64String(Data);
                    string contentType = string.Empty;
                    string ext = string.Empty;

                    var type = Data.Substring(0, 5);

                    switch (type.ToUpper())
                    {
                        case "IVBOR":
                            contentType = "image/png";
                            ext = ".png";
                            break;
                        case "/9J/4":
                            contentType = "image/jpg";
                            ext = ".jpg";
                            break;
                        case "JVBER":
                            contentType = "application/pdf";
                            ext = ".pdf";
                            break;
                        case "U1PKC":
                            contentType = "application/txt";
                            ext = ".txt";
                            break;
                        case "E1XYD":
                            contentType = "application/rtf";
                            ext = ".rtf";
                            break;
                        case "UMFYI":
                            contentType = "application/rar";
                            ext = ".rar";
                            break;
                        case "AAABA":
                            contentType = "image/x-icon";
                            ext = ".ico";
                            break;
                    }

                    var _containerName = ConfigurationManager.AppSettings.Get("ContainerAzure").ToString();
                    var _carpetaAzure = ConfigurationManager.AppSettings.Get("CarpetaAzure").ToString();

                    CloudStorageAccount cloudStorageAccount = CloudStorageAccount.Parse(ConfigurationManager.AppSettings.Get("StorageConnectionStringAzure").ToString());

                    CloudBlobClient _blobClient = cloudStorageAccount.CreateCloudBlobClient();
                    CloudBlobContainer _cloudBlobContainer = _blobClient.GetContainerReference(_containerName);

                    //create a container if it is not already exists

                    if (await _cloudBlobContainer.CreateIfNotExistsAsync())
                    {
                        await _cloudBlobContainer.SetPermissionsAsync(new BlobContainerPermissions { PublicAccess = BlobContainerPublicAccessType.Blob });
                    }

                    //get Blob reference

                    CloudBlockBlob cloudBlockBlob = _cloudBlobContainer.GetBlockBlobReference(_carpetaAzure +"/" +filename + ext); cloudBlockBlob.Properties.ContentType = contentType;

                    await cloudBlockBlob.UploadFromByteArrayAsync(_dataFile, 0, _dataFile.Length);

                    return cloudBlockBlob.Name;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return string.Empty;
        }

        public FileConstructor GetFileFromStorage(string ubicacion)
        {
            try
            {
                var _containerName = ConfigurationManager.AppSettings.Get("ContainerAzure").ToString();
                CloudStorageAccount cloudStorageAccount = CloudStorageAccount.Parse(ConfigurationManager.AppSettings.Get("StorageConnectionStringAzure").ToString());
                CloudBlobClient _blobClient = cloudStorageAccount.CreateCloudBlobClient();
                CloudBlobContainer _cloudBlobContainer = _blobClient.GetContainerReference(_containerName);
                CloudBlockBlob _blockBlob = _cloudBlobContainer.GetBlockBlobReference(ubicacion);

                _blockBlob.FetchAttributes();
                long fileByteLength = _blockBlob.Properties.Length;
                Byte[] myByteArray = new Byte[fileByteLength];
                _blockBlob.DownloadToByteArray(myByteArray, 0);

                FileConstructor file = new FileConstructor { ByteArray = myByteArray, TipoArchivo = _blockBlob.Properties.ContentType.ToString(), NombreArchivo = _blockBlob.Name };

                return file;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool DeleteFileStorage(string ubicacion)
        {
            try
            {
                var _containerName = ConfigurationManager.AppSettings.Get("ContainerAzure").ToString();
                CloudStorageAccount cloudStorageAccount = CloudStorageAccount.Parse(ConfigurationManager.AppSettings.Get("StorageConnectionStringAzure").ToString());
                CloudBlobClient _blobClient = cloudStorageAccount.CreateCloudBlobClient();
                CloudBlobContainer _cloudBlobContainer = _blobClient.GetContainerReference(_containerName);
                CloudBlockBlob _blockBlob = _cloudBlobContainer.GetBlockBlobReference(ubicacion);

                _blockBlob.DeleteAsync();

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool VerifyBlobExists(string ubicacion)
        {
            try
            {
                System.Web.HttpContext currentContext = System.Web.HttpContext.Current;

                var _containerName = ConfigurationManager.AppSettings.Get("ContainerAzure").ToString();
                CloudStorageAccount cloudStorageAccount = CloudStorageAccount.Parse(ConfigurationManager.AppSettings.Get("StorageConnectionStringAzure").ToString());
                CloudBlobClient _blobClient = cloudStorageAccount.CreateCloudBlobClient();
                CloudBlobContainer _cloudBlobContainer = _blobClient.GetContainerReference(_containerName);
                CloudBlockBlob _blockBlob = _cloudBlobContainer.GetBlockBlobReference(ubicacion);

                if (_blockBlob.Exists())
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                return false;

            }
        }

        public void DownloadFileFromStorage(string ubicacion)
        {

            try
            {
                System.Web.HttpContext currentContext = System.Web.HttpContext.Current;

                var _containerName = ConfigurationManager.AppSettings.Get("ContainerAzure").ToString();
                CloudStorageAccount cloudStorageAccount = CloudStorageAccount.Parse(ConfigurationManager.AppSettings.Get("StorageConnectionStringAzure").ToString());
                CloudBlobClient _blobClient = cloudStorageAccount.CreateCloudBlobClient();
                CloudBlobContainer _cloudBlobContainer = _blobClient.GetContainerReference(_containerName);
                CloudBlockBlob _blockBlob = _cloudBlobContainer.GetBlockBlobReference(ubicacion);

                _blockBlob.FetchAttributes();
                long fileByteLength = _blockBlob.Properties.Length;
                Byte[] myByteArray = new Byte[fileByteLength];
                _blockBlob.DownloadToByteArray(myByteArray, 0);

                MemoryStream memStream = new MemoryStream();

                _blockBlob.DownloadToStream(memStream);
                currentContext.Response.ContentType = _blockBlob.Properties.ContentType.ToString();


                string fileName = Regex.Replace(_blockBlob.Name.Split('/').Last(), @"[^\w\.@-]", "");

                currentContext.Response.AddHeader("Content-Disposition", "Attachment; filename=" + fileName);
                currentContext.Response.AddHeader("Content-Length", _blockBlob.Properties.Length.ToString());
                currentContext.Response.BinaryWrite(memStream.ToArray());
                currentContext.Response.Flush();
                currentContext.Response.Close();

            }
            catch (Exception ex)
            {
                throw ex;

            }
        }
    }
}