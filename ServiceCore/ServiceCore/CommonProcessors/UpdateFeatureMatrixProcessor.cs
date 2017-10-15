using System;
using System.Collections.Generic;
using ServiceCore.CommonOperations;
using UnifiedNetwork.Cooperation;
using UnifiedNetwork.OperationService;

namespace ServiceCore.CommonProcessors
{
	public class UpdateFeatureMatrixProcessor : OperationProcessor<UpdateFeatureMatrix>
	{
		public UpdateFeatureMatrixProcessor(Service service, UpdateFeatureMatrix op) : base(op)
		{
			this.Service = service;
		}

		public override IEnumerable<object> Run()
		{
			FeatureMatrix.OverrideFeature(base.Operation.Features);
			FeatureMatrix.OnUpdated(this.Service, base.Operation);
			base.Finished = true;
			yield break;
		}

		private Service Service;
	}
}
