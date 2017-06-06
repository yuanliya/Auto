namespace TecX.Expressions
{
    using System;
    using System.CodeDom;
    using System.Collections.ObjectModel;
    using System.Linq.Expressions;
    using System.Reflection;
    using System.Runtime.Serialization;

    using Serialize.Linq.Extensions;
    using Serialize.Linq.Nodes;

    public class ExpressionDataContractSurrogate : IDataContractSurrogate
    {
        public Type GetDataContractType(Type type)
        {
            if (typeof(Expression).IsAssignableFrom(type))
            {
                return typeof(ExpressionNode);
            }

            return type;
        }

        public object GetObjectToSerialize(object obj, Type targetType)
        {
            Expression expression = obj as Expression;
            if (expression != null)
            {
                return expression.ToExpressionNode();
            }

            return obj;
        }

        public object GetDeserializedObject(object obj, Type targetType)
        {
            if (typeof(Expression).IsAssignableFrom(targetType))
            {
                return ((ExpressionNode)obj).ToExpression();
            }

            return obj;
        }

        #region Not Implemented

        public object GetCustomDataToExport(MemberInfo memberInfo, Type dataContractType)
        {
            return null;
        }

        public object GetCustomDataToExport(Type clrType, Type dataContractType)
        {
            return null;
        }

        public void GetKnownCustomDataTypes(Collection<Type> customDataTypes)
        {
        }

        public Type GetReferencedTypeOnImport(string typeName, string typeNamespace, object customData)
        {
            return null;
        }

        public CodeTypeDeclaration ProcessImportedType(CodeTypeDeclaration typeDeclaration, CodeCompileUnit compileUnit)
        {
            return typeDeclaration;
        }

        #endregion Not Implemented
    }
}
