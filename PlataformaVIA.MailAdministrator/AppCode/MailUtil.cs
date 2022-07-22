using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32;
using PlataformaVIA.Core.Domain;
using PlataformaVIA.MailAdministrator.AppCode;

namespace PlataformaVIA.MailAdministrator
{
    public class MailUtil : Service1
    {
        private SmtpClient SmtpServer;
        private string MailerDaemonMail = "";

        private SmtpClient InicializarSMTP()
        {
            RegistryKey localKey;

            if (Environment.Is64BitOperatingSystem)
                localKey = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry64);
            else
                localKey = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry32);

            var SMTPServer = localKey.OpenSubKey(@"SOFTWARE\IGT\PlataformaVIA").GetValue("SMTPServer").ToString();
            var MailerDaemonPassword = localKey.OpenSubKey(@"SOFTWARE\IGT\PlataformaVIA").GetValue("MailerDaemonPassword").ToString();

            MailerDaemonMail = localKey.OpenSubKey(@"SOFTWARE\IGT\PlataformaVIA").GetValue("MailerDaemonMail").ToString();

            SmtpClient SmtpServer = new SmtpClient()
            {
                Host = SMTPServer,
                Port = 25,
                EnableSsl = false,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(MailerDaemonMail, MailerDaemonPassword),
                Timeout = 9000000
            };

            return SmtpServer;


        }

        public void EnvioCorreo(AdministracionCorreo Obj)
        {
            try
            {
                SmtpServer = InicializarSMTP();
                var Alias = ConfigurationManager.AppSettings["Alias"];

                MailMessage mail = new MailMessage()
                {
                    From = new MailAddress(MailerDaemonMail, Alias.ToString()),
                    Subject = Obj.ASUNTO,
                    IsBodyHtml = true,
                    BodyEncoding = Encoding.UTF8,
                };

                try
                {
                    if (!string.IsNullOrEmpty(Obj.PATH_ADJUNTO)) ArchivosAdjuntos(Obj.PATH_ADJUNTO, mail);
                    mail.To.Add(Obj.DESTINATARIOS);

                    if (!string.IsNullOrEmpty(Obj.COPIA_DESTINATARIOS)) mail.CC.Add(Obj.COPIA_DESTINATARIOS);

                    CrearPlantilla(mail, Obj.TITULO, Obj.MENSAJE, string.IsNullOrEmpty(Obj.PATH_ADJUNTO));

                    SmtpServer.Send(mail);

                    Obj.ID_ESTADO = (long)EnumEstado.EstadoMensaje.Enviado;
                }
                catch (Exception ex)
                {
                    EventLog.WriteEntry("Error Envio Mail id: " + Obj.ID_CORREO + ".     " + ex.Message, EventLogEntryType.Error);
                    Obj.ID_ESTADO = (long)EnumEstado.EstadoMensaje.Error_Envio;
                    Obj.RESPUESTA = ex.Message;
                }
            }
            catch (Exception ex)
            {
                EventLog.WriteEntry("Error : " + ex.Message, EventLogEntryType.Error);
            }
        }

        private void ArchivosAdjuntos(string Adjuntos, MailMessage mail)
        {
            List<string> ltsAdjuntos = Adjuntos.Split(';').ToList();

            foreach (var item in ltsAdjuntos)
            {
                System.Net.Mail.Attachment attachment;

                FileInfo File = new FileInfo(item.ToString());

                if (File.Exists)
                {
                    attachment = new System.Net.Mail.Attachment(item.ToString());
                    mail.Attachments.Add(attachment);
                }
            }
        }

        private void CrearPlantilla(MailMessage objMail, string Titulo, string Mensaje, bool adjunto)
        {
            StringBuilder mailBody = new System.Text.StringBuilder();
            LinkedResource logo = new LinkedResource("../../AppCode/img/logo.png", MediaTypeNames.Image.Jpeg);
            LinkedResource IGT = new LinkedResource("../../AppCode/img/IGT.png", MediaTypeNames.Image.Jpeg);
            LinkedResource infoB = new LinkedResource("../../AppCode/img/Info.jpg", MediaTypeNames.Image.Jpeg);
            LinkedResource Adjunto = new LinkedResource("../../AppCode/img/article1.png", MediaTypeNames.Image.Jpeg);
            ContentType mimeType = new System.Net.Mime.ContentType("text/html; charset=UTF-8");

            IGT.ContentId = "IGTLogo";
            infoB.ContentId = "Informacion";
            logo.ContentId = "companylogo";
            Adjunto.ContentId = "Adjunto";

            mailBody.Append("<style type='text/css'>");
            mailBody.Append("body {margin: 0; padding: 0; min-width: 100%!important;}");
            mailBody.Append("img {height: auto;}");
            mailBody.Append(".content {width: 100%; max-width: 600px;}");
            mailBody.Append(".header {padding: 40px 30px 20px 30px;}");
            mailBody.Append(".innerpadding {padding: 30px 30px 30px 30px;}");
            mailBody.Append(".borderbottom {border-bottom: 1px solid #f2eeed;}");
            mailBody.Append(".subhead {font-size: 20px; color: #ffffff; font-family: Calibri; letter-spacing: 1px;}");
            mailBody.Append(".h1, .h2, .bodycopy {color: #000000; font-family: sans-serif;}");
            mailBody.Append(".h1 {font-size: 33px; line-height: 38px; font-weight: bold;}");
            mailBody.Append(".h2 {padding: 0 0 15px 0; font-size: 24px; line-height: 28px; font-weight: bold;}");
            mailBody.Append(".h3 {color: #ffffff; padding: 0 0 15px 0; font-size: 25px; line-height: 28px; font-weight: bold;}");
            mailBody.Append(".bodycopy {font-size: 16px; line-height: 22px;}");
            mailBody.Append(".button {text-align: center; font-size: 18px; font-family: sans-serif; font-weight: bold; padding: 0 30px 0 30px;}");
            mailBody.Append(".button a {color: #ffffff; text-decoration: none;}");
            mailBody.Append(".footer {padding: 20px 30px 15px 30px;}");
            mailBody.Append(".footercopy {font-family: sans-serif; font-size: 11px; color: #ffffff; align: center; }");
            mailBody.Append(".footercopy a {color: #ffffff; text-decoration: underline;}");
            mailBody.Append("</style>");
            mailBody.Append("<table width='800px' bgcolor='#f6f8f1' border='0' cellpadding='0' cellspacing='0'>");
            mailBody.Append("<tr>");
            mailBody.Append("  <td>");
            mailBody.Append("    <table bgcolor='#ffffff' class='content' style='width: 100%; max-width: 600px;' align='center' cellpadding='0' cellspacing='0' border='0'>");
            mailBody.Append("      <tr>");
            mailBody.Append("        <td bgcolor='#004593' class='header' style='padding: 40px 30px 20px 30px;'>");
            mailBody.Append("          <table class='col425' align='left' border='0' cellpadding='0' cellspacing='0' style='width:100%; padding: 30px 0 0 0; max-width:425px;'>");
            mailBody.Append("            <tr>");
            mailBody.Append("              <td height='70'>");
            mailBody.Append("                <table width='100%' border='0' cellspacing='0' cellpadding='0'>");
            mailBody.Append("                  <tr>");
            mailBody.Append("                    <td class='subhead' style='padding: 0 0 0 3px; font-size: 20px; color: #ffffff; font-family: Calibri; letter-spacing: 1px;'>");
            mailBody.Append("                      Notificación");
            mailBody.Append("                    </td>");
            mailBody.Append("                  </tr>");
            mailBody.Append("                  <tr>");
            mailBody.Append("                    <td class='h3' style='padding: 5px 0 0 0; color: #ffffff; padding: 0 0 15px 0; font-size: 25px; line-height: 28px; font-weight: bold;'>");
            mailBody.Append("                      " + Titulo + "");
            mailBody.Append("                    </td>");
            mailBody.Append("                  </tr>");
            mailBody.Append("                </table>");
            mailBody.Append("              </td>");
            mailBody.Append("            </tr>");
            mailBody.Append("          </table>");
            mailBody.Append("          <table width='70' align='left' border='0' cellpadding='0' cellspacing='0'>");
            mailBody.Append("            <tr>");
            mailBody.Append("              <td height='70' style='padding: 8px 0 20px 0; '>");
            mailBody.Append("                <img class='fix' src='cid:companylogo' width='98' height'100' border='0' alt='' />");
            mailBody.Append("              </td>");
            mailBody.Append("            </tr>");
            mailBody.Append("          </table>");
            mailBody.Append("        </td>");
            mailBody.Append("      </tr>");
            mailBody.Append("      <tr>");
            mailBody.Append("        <td class='innerpadding borderbottom' style='padding: 30px 30px 30px 30px; border-bottom: 1px solid #f2eeed;'>");
            mailBody.Append("          <table width='100%' border='0' cellspacing='0' cellpadding='0'>");
            mailBody.Append("            <tr>");
            mailBody.Append("              <td class='h2' style='padding: 0 0 15px 0; font-size: 24px; line-height: 28px; font-weight: bold;'>");

            mailBody.Append("              </td>");
            mailBody.Append("            </tr>");
            mailBody.Append("            <tr>");
            mailBody.Append("              <td class='bodycopy' style='color: #000000; font-family: sans-serif; font-size: 16px; line-height: 22px;'>");
            mailBody.Append("			  <a>");
            mailBody.Append("                " + Mensaje + "");
            mailBody.Append("              </td>");
            mailBody.Append("            </tr>");
            mailBody.Append("          </table>");
            mailBody.Append("        </td>");
            mailBody.Append("      </tr>");

            if (!adjunto)
            {
                #region Adjunto
                mailBody.Append("      <tr>");
                mailBody.Append("        <td class='innerpadding borderbottom' style='padding: 30px 30px 30px 30px; border-bottom: 1px solid #f2eeed;'>");
                mailBody.Append("          <table width='115' align='left' border='0' cellpadding='0' cellspacing='0'>");
                mailBody.Append("            <tr>");
                mailBody.Append("              <td height='115' style='padding: 0 0 20px 0; '>");
                mailBody.Append("                <img class='fix' src='cid:Adjunto' width='85' height='85' border='0' alt='' />");
                mailBody.Append("              </td>");
                mailBody.Append("            </tr>");
                mailBody.Append("          </table>");
                mailBody.Append("          <!--[if (gte mso 9)|(IE)]>");
                mailBody.Append("            <table width='380' align='left' cellpadding='0' cellspacing='0' border='0'>");
                mailBody.Append("              <tr>");
                mailBody.Append("                <td>");
                mailBody.Append("          <![endif]-->");
                mailBody.Append("          <table class='col380' align='left' border='0' cellpadding='0' cellspacing='0' style='width: 100%; max-width: 380px; '>");
                mailBody.Append("            <tr>");
                mailBody.Append("              <td>");
                mailBody.Append("                <table width='100%' border='0' cellspacing='0' cellpadding='0'>");
                mailBody.Append("                  <tr>");
                mailBody.Append("                    <td class='h2' style='padding-top: 500px; padding: 0 0 15px 0; font-size: 20px; line-height: 28px; font-weight: bold;'>");
                mailBody.Append("                  </br>");
                mailBody.Append("                      Es importante que sepas que este correo cuenta con archivos adjuntos.");
                mailBody.Append("                    </td>");
                mailBody.Append("                  </tr>");

                mailBody.Append("                </table>");
                mailBody.Append("              </td>");
                mailBody.Append("            </tr>");
                mailBody.Append("          </table>");
                mailBody.Append("          <!--[if (gte mso 9)|(IE)]>");
                mailBody.Append("                </td>");
                mailBody.Append("              </tr>");
                mailBody.Append("          </table>");
                mailBody.Append("          <![endif]-->");
                mailBody.Append("        </td>");
                mailBody.Append("      </tr>");
                #endregion
            }
            mailBody.Append("      <tr>");
            mailBody.Append("        <td>");
            mailBody.Append("          <table border='0' cellspacing='0' cellpadding='0'>");
            mailBody.Append("            <tr>");
            mailBody.Append("              <td>");
            mailBody.Append("                <img src='cid:Informacion'  width='100%' height='35%' alt=''/>");
            mailBody.Append("              </td>");
            mailBody.Append("            </tr>");
            mailBody.Append("          </table>");
            mailBody.Append("        </td>");
            mailBody.Append("      </tr>");

            mailBody.Append("      <tr>");
            mailBody.Append("        <td class='footer' bgcolor='#004593' style='padding: 20px 30px 15px 30px;'>");
            mailBody.Append("          <table width='100%' border='0' cellspacing='0' cellpadding='0'>");
            mailBody.Append("            <tr>");
            mailBody.Append("              <td align='center' class='footercopy' style='font-family: sans-serif; font-size: 11px; color: #ffffff; align: center;'>");
            mailBody.Append("                &reg; 2019 IGT. Todos los derechos reservados.<br/>");

            mailBody.Append("                <span class='hide'>ESTE CORREO ELECTRÓNICO FUE GENERADO AUTOMÁTICAMENTE - NO RESPONDA A ESTE CORREO ELECTRÓNICO </ span > ");
            mailBody.Append("              </td>");
            mailBody.Append("              <td width='20%'>");
            mailBody.Append("                <img src='cid:IGTLogo' alt='' width='100%' align='right' />");
            mailBody.Append("              </td>");
            mailBody.Append("            </tr>");
            mailBody.Append("          </table>");
            mailBody.Append("        </td>");
            mailBody.Append("      </tr>");
            mailBody.Append("    </table>");
            mailBody.Append("    <!--[if (gte mso 9)|(IE)]>");
            mailBody.Append("          </td>");
            mailBody.Append("        </tr>");
            mailBody.Append("    </table>");
            mailBody.Append("    <![endif]-->");
            mailBody.Append("    </td>");
            mailBody.Append("  </tr>");
            mailBody.Append("</table>");

            var body = mailBody.ToString();

            AlternateView plainView = AlternateView.CreateAlternateViewFromString(body.ToString(), null, MediaTypeNames.Text.Plain);
            //plainView.TransferEncoding = TransferEncoding.QuotedPrintable;

            AlternateView alternate = AlternateView.CreateAlternateViewFromString(body.ToString(), Encoding.UTF8, MediaTypeNames.Text.Html);
            alternate.TransferEncoding = TransferEncoding.QuotedPrintable;

            alternate.LinkedResources.Add(logo);
            alternate.LinkedResources.Add(Adjunto);
            alternate.LinkedResources.Add(IGT);
            alternate.LinkedResources.Add(infoB);

            objMail.AlternateViews.Add(plainView);
            objMail.AlternateViews.Add(alternate);
        }
    }
}
