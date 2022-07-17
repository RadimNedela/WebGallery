namespace WebGalery.Domain.SessionHandling
{
    public class SessionProvider : ISessionProvider
    {
        public Session Session { get; set; }

        public SessionProvider(Session session)
        {
            Session = session;
        }
    }
}
