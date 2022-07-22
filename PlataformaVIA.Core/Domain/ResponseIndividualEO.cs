namespace PlataformaVIA.Core.Domain
{
    public class ResponseIndividualEO<T>
    {
        public int IdUsuario { get; set; }
        public int IdBusqueda { get; set; }
        public T Entidad { get; set; }
        public Message Mensaje { get; set; }

        public ResponseIndividualEO()
        {
            Mensaje = new Message();
            Mensaje.Error = false;
            Mensaje.ErrorMessage = string.Empty;
        }
    }
}
