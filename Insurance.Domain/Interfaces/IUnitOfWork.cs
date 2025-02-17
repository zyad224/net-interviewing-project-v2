﻿using System.Threading.Tasks;
namespace Insurance.Domain.Interfaces
{
    public interface IUnitOfWork
    {
        public ISurchargeRepository SurchargeRepo { get; }
        public Task<int> Commit();
    }
}
