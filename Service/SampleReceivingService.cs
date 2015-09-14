using Model;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Service
{

    public class SampleReceivingService : EntityService<SampleReceive>, ISampleReceivingService
    {
        IUnitOfWork _unitOfWork;
        ISampleReceivingRepository _SampleReceivingRepository;

        public SampleReceivingService(IUnitOfWork unitOfWork, ISampleReceivingRepository SampleReceivingRepository)
            : base(unitOfWork, SampleReceivingRepository)
        {
            _unitOfWork = unitOfWork;
            _SampleReceivingRepository = SampleReceivingRepository;
        }

        public SampleReceive GetById(long SampleReceiveID)
        {
            return _SampleReceivingRepository.GetById(SampleReceiveID);
        }
    }
}
