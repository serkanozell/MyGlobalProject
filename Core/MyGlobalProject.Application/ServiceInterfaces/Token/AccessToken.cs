﻿namespace MyGlobalProject.Application.ServiceInterfaces.Token
{
    public class AccessToken
    {
        public string Token { get; set; }
        public DateTime Expiration { get; set; }
    }
}
