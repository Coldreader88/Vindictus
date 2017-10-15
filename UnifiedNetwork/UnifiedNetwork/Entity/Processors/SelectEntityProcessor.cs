using System;
using System.Collections.Generic;
using UnifiedNetwork.Cooperation;
using UnifiedNetwork.Entity.Operations;
using UnifiedNetwork.LocationService;
using UnifiedNetwork.LocationService.Operations;
using UnifiedNetwork.OperationService;
using Utility;

namespace UnifiedNetwork.Entity.Processors
{
    internal class SelectEntityProcessor : OperationProcessor<SelectEntity>
    {
        public SelectEntityProcessor(Service service, SelectEntity op) : base(op)
        {
            this.service = service;
        }

        public override IEnumerable<object> Run()
        {
            if (base.Operation.Category != this.service.Category)
            {
                base.Finished = true;
                Log<SelectEntityProcessor>.Logger.ErrorFormat("[{0} != {1}], category not matched", base.Operation.Category, this.service.Category);
                yield return SelectEntity.ResultCode.NotExists;
            }
            else
            {
                int serviceID = this.service.AcquireEntity(base.Operation.ID, base.Operation.Category, -1);
                if (serviceID < 0)
                {
                    base.Finished = true;
                    Log<SelectEntityProcessor>.Logger.ErrorFormat("{0} < 0, service not found ID : {1}, Category : {2}", serviceID, base.Operation.ID, base.Operation.Category);
                    yield return SelectEntity.ResultCode.NotExists;
                }
                else
                {
                    int num = 0;
                    while (serviceID != this.service.ID)
                    {
                        bool categoryMatch = false;
                        ICollection<int> collection = this.service.LookUp.FindIndex(base.Operation.Category);
                        if (!collection.Contains(serviceID))
                        {
                            Query queryop = new Query(base.Operation.Category);
                            queryop.OnComplete += delegate (Operation x)
                            {
                                foreach (ServiceInfo current in queryop.ServiceList)
                                {
                                    if (current.ID == serviceID)
                                    {
                                        categoryMatch = true;
                                    }
                                }
                            };
                            yield return new UnifiedNetwork.OperationService.OperationSync
                            {
                                ServiceCategory = typeof(UnifiedNetwork.LocationService.LookUp).FullName,
                                Operation = queryop,
                                Service = this.service
                            };
                        }
                        else
                        {
                            categoryMatch = true;
                        }
                        if (categoryMatch)
                        {
                            base.Finished = true;
                            yield return SelectEntity.ResultCode.Redirect;
                            yield return serviceID;
                            yield break;
                        }
                        num++;
                        if (num > 3)
                        {
                            base.Finished = true;
                            yield return SelectEntity.ResultCode.Error;
                            yield break;
                        }
                        serviceID = this.service.AcquireEntity(base.Operation.ID, base.Operation.Category, serviceID);
                    }
                    base.Finished = true;
                    yield return SelectEntity.ResultCode.Ok;
                }
            }
            yield break;
        }

        private Service service;
    }
}
