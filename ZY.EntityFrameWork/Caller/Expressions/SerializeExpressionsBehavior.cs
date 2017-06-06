namespace TecX.Expressions
{
    using System;
    using System.Collections.ObjectModel;
    using System.Linq.Expressions;
    using System.Runtime.Serialization;
    using System.ServiceModel;
    using System.ServiceModel.Channels;
    using System.ServiceModel.Description;
    using System.ServiceModel.Dispatcher;

    using Serialize.Linq.Nodes;

    public class SerializeExpressionsBehavior : Attribute, IServiceBehavior, IContractBehavior, IWsdlExportExtension
    {
        private readonly ExpressionDataContractSurrogate surrogate;

        public SerializeExpressionsBehavior()
        {
            this.surrogate = new ExpressionDataContractSurrogate();
        }

        void IServiceBehavior.ApplyDispatchBehavior(ServiceDescription serviceDescription, ServiceHostBase host)
        {
            foreach (ServiceEndpoint endpoint in serviceDescription.Endpoints)
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

            this.HideEnumerationClassesFromMetadata(host);
        }

        void IWsdlExportExtension.ExportContract(WsdlExporter exporter, WsdlContractConversionContext context)
        {
            this.ReplaceEnumerationClassParametersWithEnums(context);
        }

        #region Not Implemented

        void IServiceBehavior.Validate(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase)
        {
            /* intentionally not implemented */
        }

        void IServiceBehavior.AddBindingParameters(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase, Collection<ServiceEndpoint> endpoints, BindingParameterCollection bindingParameters)
        {
            /* intentionally not implemented */
        }

        void IContractBehavior.Validate(ContractDescription contractDescription, ServiceEndpoint endpoint)
        {
            /* ContractBehavior is only needed to hook up IWsdlExportExtension with WCF infrastructure */
        }

        void IContractBehavior.ApplyDispatchBehavior(ContractDescription contractDescription, ServiceEndpoint endpoint, DispatchRuntime dispatchRuntime)
        {
            /* ContractBehavior is only needed to hook up IWsdlExportExtension with WCF infrastructure */
        }

        void IContractBehavior.ApplyClientBehavior(ContractDescription contractDescription, ServiceEndpoint endpoint, ClientRuntime clientRuntime)
        {
            /* ContractBehavior is only needed to hook up IWsdlExportExtension with WCF infrastructure */
        }

        void IContractBehavior.AddBindingParameters(ContractDescription contractDescription, ServiceEndpoint endpoint, BindingParameterCollection bindingParameters)
        {
            /* ContractBehavior is only needed to hook up IWsdlExportExtension with WCF infrastructure */
        }

        void IWsdlExportExtension.ExportEndpoint(WsdlExporter exporter, WsdlEndpointConversionContext context)
        {
            /* intentionally not implemented */
        }

        #endregion Not Implemented

        private void ReplaceEnumerationClassParametersWithEnums(WsdlContractConversionContext context)
        {
            foreach (var operation in context.Contract.Operations)
            {
                foreach (var message in operation.Messages)
                {
                    foreach (var part in message.Body.Parts)
                    {
                        if (part.Type.IsAssignableFrom(typeof(Expression)))
                        {
                            part.Type = typeof(ExpressionNode);
                        }

                        //Type enumType;
                        //if (this.surrogate.Generator.TryGetEnumByType(part.Type, out enumType))
                        //{
                        //    part.Type = enumType;
                        //}
                    }
                }
            }
        }

        private void HideEnumerationClassesFromMetadata(ServiceHostBase host)
        {
            ServiceMetadataBehavior smb = host.Description.Behaviors.Find<ServiceMetadataBehavior>();

            if (smb == null)
            {
                return;
            }

            WsdlExporter exporter = smb.MetadataExporter as WsdlExporter;

            if (exporter == null)
            {
                return;
            }

            object dataContractExporter;
            XsdDataContractExporter xsdInventoryExporter;
            if (!exporter.State.TryGetValue(typeof(XsdDataContractExporter), out dataContractExporter))
            {
                xsdInventoryExporter = new XsdDataContractExporter(exporter.GeneratedXmlSchemas);
            }
            else
            {
                xsdInventoryExporter = (XsdDataContractExporter)dataContractExporter;
            }

            exporter.State.Add(typeof(XsdDataContractExporter), xsdInventoryExporter);

            if (xsdInventoryExporter.Options == null)
            {
                xsdInventoryExporter.Options = new ExportOptions();
            }

            xsdInventoryExporter.Options.DataContractSurrogate = this.surrogate;
        }
    }
}