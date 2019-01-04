using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using AutoMapper;
using Entity;
using Entity.Model;
using Infrastructure;
using Repository.Interface;
using Service.Interface;

namespace Service.Service
{
    public class SignalRService : ISignalRService
    {
        private readonly ISignalRRepository _signalRRepository;

        public SignalRService(ISignalRRepository signalRRepository)
        {
            _signalRRepository = signalRRepository;
        }

        public string GetConnectionId(string id)
        {
            var results = _signalRRepository.Get(x=>x.UserId==id);
            if (results == null) return string.Empty;

            return results.ConnectionId;
        }
    }
}
