// <copyright file="WebApiModelBinderProvider.cs" company="SHHH Innovations">
//   Copyright (c) SHHH Innovations. All rights reserved.
// </copyright>

namespace SHHH.Infrastructure.Web.Http
{
    using System;
    using System.Collections.Generic;
    using System.Web.Http;
    using System.Web.Http.ModelBinding;

    /// <summary>
    /// Web API Model Binder Provider
    /// </summary>
    public class WebApiModelBinderProvider : ModelBinderProvider
    {
        /// <summary>
        /// Initializes static members of the <see cref="WebApiModelBinderProvider" /> class.
        /// </summary>
        static WebApiModelBinderProvider()
        {
            Binders = new Dictionary<Type, IModelBinder>();
        }

        /// <summary>
        /// Gets the binders.
        /// </summary>
        /// <value>
        /// The binders.
        /// </value>
        public static Dictionary<Type, IModelBinder> Binders { get; private set; }

        /// <summary>
        /// Finds a binder for the given type.
        /// </summary>
        /// <param name="configuration">A configuration object.</param>
        /// <param name="modelType">The type of the model to bind against.</param>
        /// <returns>
        /// A binder, which can attempt to bind this type. Or null if the binder knows statically that it will never be able to bind the type.
        /// </returns>
        public override IModelBinder GetBinder(HttpConfiguration configuration, Type modelType)
        {
            if (!Binders.ContainsKey(modelType))
            {
                return null;
            }

            return Binders[modelType];
        }
    }
}
