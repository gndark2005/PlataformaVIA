using Microsoft.Azure.Storage;
using Microsoft.Azure.Storage.Blob;
using PlataformaVIA.Core.Domain.AdministradorDocumentos;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace PlataformaVIA.Presentacion.Helpers
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

        public async System.Threading.Tasks.Task<bool> InsertIntoStorage(HttpPostedFileBase file, string ubicacion, string filename)
        {
            bool value = false;


            try
            {
                
               if (file.ContentLength > 0)
               {
                   var _containerName = ConfigurationManager.AppSettings.Get("ContainerAzure").ToString();
                   string _FileName = Path.GetFileName(file.FileName);

                   /////////////////////////
                   ///
                   CloudStorageAccount cloudStorageAccount = CloudStorageAccount.Parse(ConfigurationManager.AppSettings.Get("StorageConnectionStringAzure").ToString());

                   CloudBlobClient _blobClient = cloudStorageAccount.CreateCloudBlobClient();
                   CloudBlobContainer _cloudBlobContainer = _blobClient.GetContainerReference(_containerName);

                   //create a container if it is not already exists

                   if (await _cloudBlobContainer.CreateIfNotExistsAsync())
                   {

                       await _cloudBlobContainer.SetPermissionsAsync(new BlobContainerPermissions { PublicAccess = BlobContainerPublicAccessType.Blob });

                   }
                  
                   //get Blob reference

                   CloudBlockBlob cloudBlockBlob = _cloudBlobContainer.GetBlockBlobReference(ubicacion + filename); cloudBlockBlob.Properties.ContentType = file.ContentType;

                   await cloudBlockBlob.UploadFromStreamAsync(file.InputStream);

                    value = true;

                   return value;
               }                             
           }
           catch(Exception ex)
           {             
                return false;
           }
            return value;
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

        public FileConstructor GetFileFromStoragePublic(string ubicacion)
        {
            try
            {
                var _containerName = ConfigurationManager.AppSettings.Get("ContainerAzureSFG").ToString();
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

        public async System.Threading.Tasks.Task<bool> VerifyBlobExists(string ubicacion)
        {
            try
            {
                ubicacion = Regex.Replace(ubicacion, @"[^\u0000-\u007F]+", string.Empty);

                System.Web.HttpContext currentContext = System.Web.HttpContext.Current;

                var _containerName = ConfigurationManager.AppSettings.Get("ContainerAzure").ToString();
                CloudStorageAccount cloudStorageAccount = CloudStorageAccount.Parse(ConfigurationManager.AppSettings.Get("StorageConnectionStringAzure").ToString());
                CloudBlobClient _blobClient = cloudStorageAccount.CreateCloudBlobClient();
                CloudBlobContainer _cloudBlobContainer = _blobClient.GetContainerReference(_containerName);
                CloudBlockBlob _blockBlob = _cloudBlobContainer.GetBlockBlobReference(ubicacion);


                if (await _blockBlob.ExistsAsync())
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

            ubicacion = Regex.Replace(ubicacion, @"[^\u0000-\u007F]+", string.Empty);

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
            catch(Exception ex)
            {
                throw ex;
              
            }           
        }
    }
}