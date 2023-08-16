using System;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using FreeERP.Model;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;

namespace FreeERP.CustomModelBinder
{
	public class TestBinderProvider : IModelBinderProvider
	{
		public TestBinderProvider()
		{
		}

        public IModelBinder? GetBinder(ModelBinderProviderContext context)
        {
            if (context.Metadata.ModelType == typeof(TestModel))
            {
                return new BinderTypeModelBinder(typeof(TestBinderProvider));
            }
            return null;
        }
    }
}

