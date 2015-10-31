<%@ Application Language="C#" %>

<script runat="server">

    void Application_Start(object sender, EventArgs e) 
    {
        Application["Usuarios"] = "0";
        // Code that runs on application startup

    }
    
    void Application_End(object sender, EventArgs e) 
    {
        //  Code that runs on application shutdown

    }
        
    void Application_Error(object sender, EventArgs e) 
    { 
        // Code that runs when an unhandled error occurs

    }

    void Session_Start(object sender, EventArgs e) 
    {
        // Code that runs when a new session is started
        Application.Lock();
        int cant = Int32.Parse(Application["Usuarios"].ToString());
        cant++;
        Application["Usuarios"] = cant.ToString();
        Application.UnLock();
    }

    void Session_End(object sender, EventArgs e) 
    {
        Application.Lock();
        int cant = Int32.Parse(Application["Usuarios"].ToString());
        cant--;
        Application["Usuarios"] = cant.ToString();
        Application.UnLock();
    }

    protected void Application_AuthenticateRequest(object sender, EventArgs e)
    {
        if (HttpContext.Current.User != null)
        {
            var id = HttpContext.Current.User.Identity;
            HttpContext.Current.User = new System.Security.Principal.GenericPrincipal(id, FOCA_Negocio.GestorSesiones.obtenerRoles(id.Name));
        }
    }
</script>