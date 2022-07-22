using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using Microsoft.Win32;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PlataformaVIA.Core.Domain;
using System.Runtime.InteropServices;
using System.Configuration;

namespace PlataformaVIA.MailAdministrator
{
    public partial class Service1 : ServiceBase
    {
        private int eventID = 1;
        private RegistryKey localKey;
        private string UrlAPI;
        private string UrlAPIPlataforma;
        Timer timer = new Timer();


        public Service1()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Metodo que genera el Token como encabezado de la solicitud para el llamado a la API_NUBE
        /// </summary>
        /// <param name="Client"></param>
        /// <returns></returns>
        protected HttpClient DefaultHeaderLocal()
        {
            UrlAPI = localKey.OpenSubKey(@"SOFTWARE\IGT\PlataformaVIA").GetValue("UrlAPILocal").ToString(); ;
            HttpClient Cliente = new HttpClient()
            {
                BaseAddress = new Uri(UrlAPI)
            };

            Cliente.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            return Cliente;
        }


        /// <summary>
        /// Metodo que genera el Token como encabezado de la solicitud para el llamado a la API_NUBE
        /// </summary>
        /// <param name="Client"></param>
        /// <returns></returns>
        protected HttpClient DefaultHeaderNube()
        {
            Dictionary<string, string> tokenDetails = null;

            if (Environment.Is64BitOperatingSystem)
                localKey = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry64);
            else
                localKey = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry32);

            string username = localKey.OpenSubKey(@"SOFTWARE\IGT\PlataformaVIA").GetValue("AutenticationUserNameAPI").ToString(); ;
            string password = localKey.OpenSubKey(@"SOFTWARE\IGT\PlataformaVIA").GetValue("AutenticationPassAPI").ToString(); ;
            string grant_type = localKey.OpenSubKey(@"SOFTWARE\IGT\PlataformaVIA").GetValue("AutenticationGrant_Type").ToString(); ;
            UrlAPIPlataforma = localKey.OpenSubKey(@"SOFTWARE\IGT\PlataformaVIA").GetValue("UrlAPIPlataforma").ToString(); ;

            HttpClient Cliente = new HttpClient()
            {
                BaseAddress = new Uri(UrlAPIPlataforma)
            };

            try
            {
                var login = new Dictionary<string, string>{
               {"grant_type", grant_type},
               {"username", username},
               {"password", password},
            };

                var response = Cliente.PostAsync("token", new FormUrlEncodedContent(login)).Result;

                if (response.IsSuccessStatusCode)
                {
                    tokenDetails = JsonConvert.DeserializeObject<Dictionary<string, string>>(response.Content.ReadAsStringAsync().Result);
                    if (tokenDetails != null && tokenDetails.Any())
                    {
                        var authValue = new AuthenticationHeaderValue("Bearer", tokenDetails.FirstOrDefault().Value);
                        Cliente.DefaultRequestHeaders.Authorization = authValue;
                    }
                }

                Cliente.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            }
            catch (Exception ex)
            {
                EventLog.WriteEntry("AdministracionCorreo DHN: " + ex.Message + ex.InnerException, EventLogEntryType.Error, eventID++);
            }
            return Cliente;
        }

        public void AdministracionCorreo(object source, ElapsedEventArgs e)
        //public void AdministracionCorreo()
        {
            try
            {
                MailUtil Util = new MailUtil();
                ResponseEO<AdministracionCorreo> responseDr = null;

                var response = DefaultHeaderNube().GetAsync("api/AdministracionCorreo/ConsultarCorreos").Result;

                if (response.IsSuccessStatusCode)
                {
                    responseDr = JsonConvert.DeserializeObject<ResponseEO<AdministracionCorreo>>(response.Content.ReadAsStringAsync().Result);

                    if (responseDr.Entidades.Count() > 0)
                    {
                        foreach (var obj in responseDr.Entidades)
                        {
                            Util.EnvioCorreo(obj);

                            IEnumerable<AdministracionCorreo> objEmail = new AdministracionCorreo[] { new AdministracionCorreo {
                                 ID_CORREO = obj.ID_CORREO,
                                 TITULO = obj.TITULO,
                                 ASUNTO = obj.ASUNTO,
                                 MENSAJE = obj.MENSAJE,
                                 DESTINATARIOS = obj.DESTINATARIOS,
                                 COPIA_DESTINATARIOS = obj.COPIA_DESTINATARIOS,
                                 PATH_ADJUNTO = obj.PATH_ADJUNTO,
                                 ID_ESTADO = obj.ID_ESTADO,
                                 RESPUESTA = obj.RESPUESTA
                            } };

                            ResponseEO<AdministracionCorreo> ResponseEO = new ResponseEO<AdministracionCorreo>()
                            {
                                Entidades = objEmail
                            };

                            var myContent = JsonConvert.SerializeObject(ResponseEO);
                            var stringContent = new StringContent(myContent, UnicodeEncoding.UTF8, "application/json");

                            var result = DefaultHeaderNube().PostAsync("api/AdministracionCorreo/ActualizarEstadoCorreos", stringContent).Result;
                        }
                    }
                }

                var responseLocal = DefaultHeaderLocal().GetAsync("api/AdministracionCorreoLocal").Result;

                if (responseLocal.IsSuccessStatusCode)
                {
                    var ltresponse = JsonConvert.DeserializeObject<List<AdministracionCorreo>>(responseLocal.Content.ReadAsStringAsync().Result);

                    var ltPendiente = ltresponse.Where(Email => Email.ID_ESTADO == 2).ToList();

                    if (ltPendiente.Count() > 0)
                    {
                        foreach (var obj in responseDr.Entidades)
                        {
                            Util.EnvioCorreo(obj);

                            var myContentLocal = JsonConvert.SerializeObject(obj);
                            var stringContentLocal = new StringContent(myContentLocal, UnicodeEncoding.UTF8, "application/json");

                            var resultLocal = DefaultHeaderLocal().PutAsync("api/AdministracionCorreoLocal/" + obj.ID_CORREO.ToString(), stringContentLocal).Result;
                        }

                    }
                }
            }

            catch (Exception ex)
            {
                EventLog.WriteEntry("AdministracionCorreo" + ex.Message, EventLogEntryType.Error, eventID++);
            }
        }

        public void OnDebug()
        {
            try
            {
                //AdministracionCorreo();
            }
            catch (Exception ex)
            {
                EventLog.WriteEntry("CorreoPlataformaVia", "error" + ex.Message, EventLogEntryType.Error, eventID++);
            }
        }

        protected override void OnStart(string[] args)
        {
            try
            {
                if (Environment.Is64BitOperatingSystem)
                    localKey = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry64);
                else
                    localKey = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry32);

                string Time = localKey.OpenSubKey(@"SOFTWARE\IGT\PlataformaVIA").GetValue("timerInterval").ToString(); ;

                timer.Elapsed += new ElapsedEventHandler(AdministracionCorreo);
                timer.Interval = double.Parse(Time); // 1 Minutos
                timer.Enabled = true;
                timer.Start();
            }
            catch (Exception ex)
            {
                EventLog.WriteEntry("CorreoPlataformaVia OnStart: ", ex.Message);
            }
        }

        protected override void OnStop()
        {
            EventLog.WriteEntry("In OnStopat " + DateTime.Now);
        }


    }
}
