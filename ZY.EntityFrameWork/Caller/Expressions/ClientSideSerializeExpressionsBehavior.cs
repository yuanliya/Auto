namespace TecX.Expressions
{
    using System.Runtime.Serialization;
    using System.ServiceModel.Channels;
    using System.ServiceModel.Description;
    using System.ServiceModel.Dispatcher;

    public class ClientSideSerializeExpressionsBehavior : IEndpointBehavior
    {
        private IDataContractSurrogate surrogate;

        public ClientSideSerializeExpressionsBehavior()
        {
            this.surrogate = new ExpressionDataContractSurrogate();
        }

        public void ApplyClientBehavior(ServiceEndpoint endpoint, ClientRuntime clientRuntime)
        {
            foreach (OperationDescription operation in endpoint.Contract.Operations)
            {
                DataContractSerializerOperationBehavior behavior = operation.Behaviors.Find<DataContractSerializerOperationBehavior>();

                if (behavior == null)
                {
                    behavior = new DataContractSerializerOperationBehavior(operation) { DataContractSurrogate = this.surrogate };

                    operation.Behaviors.Add(behavior);
                }
                else
                {
                    behavior.DataContractSurrogate = this.surrogate;
                }
            }
        }

        #region Not Implemented

        public void Validate(ServiceEndpoint endpoint)
        {
        }

        public void AddBindingParameters(ServiceEndpoint endpoint, BindingParameterCollection bindingParameters)
        {
        }

        public void ApplyDispatchBehavior(ServiceEndpoint endpoint, EndpointDispatcher endpointDispatcher)
        {
        }

        #endregion Not Implemented
    }
}