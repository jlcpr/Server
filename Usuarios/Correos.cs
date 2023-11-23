using System.Net.Mail;

public class Correos{

    public string Destinatario {get;set;} = "";
    public string Asunto {get;set;} = "";
    public string Mensaje {get;set;} = "";

    public void Enviar(){
        MailMessage correo = new MailMessage();
        correo.From = new MailAddress("pruebajoseromi123@gmail.com", null, System.Text.Encoding.UTF8);//Correo de salida
        correo.To.Add(Destinatario); //Correo destino?
        correo.Subject = this.Asunto; //Asunto
        correo.Body = this.Mensaje; //Mensaje del correo
        correo.IsBodyHtml = true;
        correo.Priority = MailPriority.Normal;
        SmtpClient smtp = new SmtpClient();
        smtp.UseDefaultCredentials = false;
        smtp.Host = "smtp.gmail.com"; //Host del servidor de correo
        smtp.Port = 25; //Puerto de salida
        smtp.Credentials = new System.Net.NetworkCredential("pruebajoseromi123@gmail.com", "bpjqgaijibohwoed");//Cuenta de correo
        smtp.EnableSsl = true;//True si el servidor de correo permite ssl
        smtp.Send(correo);  
    }
}