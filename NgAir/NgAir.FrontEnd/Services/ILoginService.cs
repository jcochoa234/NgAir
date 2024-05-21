﻿using System.Threading.Tasks;

namespace NgAir.FrontEnd.Services
{
    public interface ILoginService
    {
        Task LoginAsync(string token);

        Task LogoutAsync();
    }
}