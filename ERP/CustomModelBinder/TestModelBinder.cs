using System;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using FreeERP.Model;

namespace FreeERP.CustomModelBinder
{
	public class TestModelBinder : IModelBinder
	{
		public TestModelBinder()
		{
		}

        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            TestModel testModel = new TestModel();
            if (bindingContext.ValueProvider?.GetValue("FirstName").Length > 1)
            {
                _ = bindingContext.ValueProvider.GetValue("FirstName").FirstValue;
            }
            bindingContext.Result = ModelBindingResult.Success(testModel);
            return Task.CompletedTask;
        }
    }
}

