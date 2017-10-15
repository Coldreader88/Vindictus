using System;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;

namespace Nexon.Enterprise.ServiceFacade.Channels.ChunkingChannel
{
	[AttributeUsage(AttributeTargets.Method)]
	public sealed class ChunkingBehavior : Attribute, IOperationBehavior
	{
		public ChunkingBehavior(ChunkingAppliesTo appliesTo)
		{
			this.appliesTo = appliesTo;
		}

		public ChunkingAppliesTo AppliesTo
		{
			get
			{
				return this.appliesTo;
			}
		}

		public void AddBindingParameters(OperationDescription description, BindingParameterCollection parameters)
		{
			ChunkingBindingParameter chunkingBindingParameter = parameters.Find<ChunkingBindingParameter>();
			if (chunkingBindingParameter == null)
			{
				chunkingBindingParameter = new ChunkingBindingParameter();
				parameters.Add(chunkingBindingParameter);
			}
			if ((this.appliesTo & ChunkingAppliesTo.InMessage) == ChunkingAppliesTo.InMessage)
			{
				chunkingBindingParameter.AddAction(description.Messages[0].Action);
			}
			if (!description.IsOneWay && (this.appliesTo & ChunkingAppliesTo.OutMessage) == ChunkingAppliesTo.OutMessage)
			{
				chunkingBindingParameter.AddAction(description.Messages[1].Action);
			}
		}

		public void ApplyClientBehavior(OperationDescription description, ClientOperation proxy)
		{
		}

		public void ApplyDispatchBehavior(OperationDescription description, DispatchOperation dispatch)
		{
		}

		public void Validate(OperationDescription description)
		{
		}

		private ChunkingAppliesTo appliesTo;
	}
}
