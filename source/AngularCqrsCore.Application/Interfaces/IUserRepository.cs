﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace Application.Interfaces
{
    public interface IUserRepository
    {
        Task<User> GetByLoginAsync(string login);

        Task<List<User>> GetAllAsync();
    }
}
